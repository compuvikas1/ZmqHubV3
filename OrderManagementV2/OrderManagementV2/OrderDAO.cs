using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

//TODO: check if conn is not valid, again open teh connection.
namespace OrderManagementV2
{
    class OrderDAO
    {
        public string orderSeq = "getNextOrder";
        public string orderIDSeq = "getNextOrderID";
        public string FillSeq = "getNextFillID";
        public string eodSeq = "getNextEODID";
        private SqlConnection conn;

        public OrderDAO()
        {
            string DataSource = ConfigurationManager.AppSettings.Get("OMDBServer");
            string Database = ConfigurationManager.AppSettings.Get("OMDatabase");
            string user = ConfigurationManager.AppSettings.Get("OMDBUser");
            string pass = ConfigurationManager.AppSettings.Get("OMDBPass");

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = DataSource;
            builder.InitialCatalog = Database;
            builder.UserID = user;
            builder.Password = pass;
            conn = new SqlConnection(builder.ToString());
            conn.Open();
        }

        private void refreshConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
        }

        private int getNextSeq(string seqName)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = seqName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("value", ""));

                    var returnParameter = cmd.Parameters.Add("@value", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    var tnp = returnParameter.Value;
                    return Convert.ToInt32(tnp);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Some exception : " + ex.Message);
            }
            return -1;
        }

        private int getNextSeq()
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "getNextVal";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("value", ""));

                    var returnParameter = cmd.Parameters.Add("@value", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    var tnp = returnParameter.Value;
                    return Convert.ToInt32(tnp);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Some exception : " + ex.Message);
            }
            return -1;
        }

        private int getLastVersion(int orderNo, ref int ver, ref int id, ref int fixAcceptedID, ref string fixRefID)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select id, fixAcceptedID, LinkedOrderId, fixOrderID, version from orders where version = (select max(version) from orders where orderNo = " + orderNo + ") and orderNo = " + orderNo + ";";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["version"];
                            if (tmp != DBNull.Value) { ver = Convert.ToInt32(tmp); } else { ver = 0; }
                            tmp = reader["id"];
                            if (tmp != DBNull.Value) { id = Convert.ToInt32(tmp); } else { id = 0; }
                            tmp = reader["fixAcceptedID"];
                            if (tmp != DBNull.Value) { fixAcceptedID = (int)Convert.ToInt32((Int64)tmp); } else { fixAcceptedID = 0; }
                            tmp = reader["fixOrderID"];
                            if (tmp != DBNull.Value) { fixRefID = ((string)tmp); } else { fixRefID = "0"; }
                        }
                    }
                }
                Console.WriteLine("OrderNo : " + orderNo + " ver : " + ver + " id : " + id);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getLatestVersion) : " + ex.Message);
            }
            return -1;
        }

        private int getLastVersionFromOrderID(int orderID)
        {
            int ver = -1;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select max(version) version from orders where OrderNo = (select OrderNo from Orders where ID = " + orderID + ");";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["version"];
                            if (tmp != DBNull.Value) { ver = Convert.ToInt32(tmp); } else { ver = 0; }
                        }
                    }
                }
                Console.WriteLine("OrderID : " + orderID + " ver : " + ver);
                return ver;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getLastVersionFromOrderID) : " + ex.Message);
            }
            return -1;
        }


        public int getOrderNoFromOrderID(int orderID)
        {
            int orderNo = -1;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select OrderNo from orders where ID = " + orderID;

                    cmd.CommandText = query;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["OrderNo"];
                            if (tmp != DBNull.Value) { orderNo = Convert.ToInt32(tmp); } else { orderNo = -1; }
                        }
                    }
                }
                return orderNo;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getLatestVersion) : " + ex.Message);
            }
            return -1;
        }
        private int fillStatus(int orderNo)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query1 = "select quantity qty from orders where version = (select max(version) from orders where orderNo = " + orderNo + ") and orderNo = " + orderNo + ";";
                    string query2 = "select sum(FilledQuantity) qty from fills where orderNo = " + orderNo + " group by orderNo;";
                    cmd.CommandText = query1;
                    int OrdQty = 0;
                    int FillQty = 0;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["qty"];
                            if (tmp != DBNull.Value) { OrdQty = Convert.ToInt32(tmp); } else { OrdQty = 0; }
                        }
                    }
                    cmd.CommandText = query2;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["qty"];
                            if (tmp != DBNull.Value) { FillQty = Convert.ToInt32(tmp); } else { FillQty = 0; }
                        }
                    }
                    if (OrdQty == FillQty && OrdQty > 0) // Order is competed;
                    {
                        return 0; // 100 means comleted
                    }
                    return OrdQty - FillQty;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(fillstatus) : " + ex.Message);
            }
            return -1;
        }

        private string sanitiseField(char[] field)
        {
            string data = new string(field);
            if (data.Contains("#"))
            {
                return new string(data.Substring(0, data.IndexOf('#')).ToCharArray());
            }
            return data;
        }

        public int insertOrderWithOrdeNO(int orderNo)
        {
            try
            {
                int id = 0;
                int fixAcceptedID = -1;
                string fixOrderID = "-1";
                int linkedOrderID = -1;
                string OrderStatus = "";
                string symbol = "";
                float price = 0;
                float quantity = 0;
                int strike = 0;
                char direction = 'N';
                int version = 0;
                string expiry = "";
                string callput = "";
                string exch = "";
                string machineID = "";
                string userID = "";
                int timeinforce = -1;
                int ordertype = -1;
                int tokenID = -1;

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select ID,OrderStatus,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,symbol,price,strike,quantity,direction,version,expiry,callput,exch,machineID,userID,timeinforce,ordertype from orders where version = (select max(version) from orders where orderNo = " + orderNo + ") and orderNo = " + orderNo + ";";
                    cmd.CommandText = query;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["id"]; if (tmp != DBNull.Value) { id = Convert.ToInt32(tmp); } else { id = 0; }
                            tmp = reader["OrderStatus"]; if (tmp != DBNull.Value) { OrderStatus = Convert.ToString(tmp); } else { OrderStatus = ""; }
                            tmp = reader["symbol"]; if (tmp != DBNull.Value) { symbol = Convert.ToString(tmp); } else { symbol = ""; }
                            tmp = reader["price"]; if (tmp != DBNull.Value) { price = (float)Convert.ToDouble(tmp); } else { price = 0; }
                            tmp = reader["strike"]; if (tmp != DBNull.Value) { strike = Convert.ToInt32(tmp); } else { strike = 0; }
                            tmp = reader["fixAcceptedID"]; if (tmp != DBNull.Value) { fixAcceptedID = (int)Convert.ToInt32((Int64)tmp); } else { fixAcceptedID = 0; }
                            tmp = reader["fixOrderID"]; if (tmp != DBNull.Value) { fixOrderID = ((string)tmp); } else { fixOrderID = "0"; }
                            tmp = reader["LinkedOrderID"]; if (tmp != DBNull.Value) { linkedOrderID = Convert.ToInt32(tmp); } else { linkedOrderID = 0; }
                            tmp = reader["quantity"]; if (tmp != DBNull.Value) { quantity = (float)Convert.ToDouble(tmp); } else { quantity = 0; }
                            tmp = reader["direction"]; if (tmp != DBNull.Value) { direction = Convert.ToChar(tmp); } else { direction = 'N'; }
                            tmp = reader["version"]; if (tmp != DBNull.Value) { version = Convert.ToInt32(tmp); } else { version = 0; }
                            tmp = reader["expiry"]; if (tmp != DBNull.Value) { expiry = Convert.ToString(tmp); } else { expiry = ""; }
                            tmp = reader["callput"]; if (tmp != DBNull.Value) { callput = Convert.ToString(tmp); } else { callput = ""; }
                            tmp = reader["exch"]; if (tmp != DBNull.Value) { exch = Convert.ToString(tmp); } else { exch = ""; }
                            tmp = reader["machineID"]; if (tmp != DBNull.Value) { machineID = Convert.ToString(tmp); } else { machineID = ""; }
                            tmp = reader["userID"]; if (tmp != DBNull.Value) { userID = Convert.ToString(tmp); } else { userID = ""; }
                            tmp = reader["timeinforce"]; if (tmp != DBNull.Value) { timeinforce = Convert.ToInt32(tmp); } else { timeinforce = 0; }
                            tmp = reader["ordertype"]; if (tmp != DBNull.Value) { ordertype = Convert.ToInt32(tmp); } else { ordertype = 0; }
                            tmp = reader["tokenID"]; if (tmp != DBNull.Value) { tokenID = Convert.ToInt32(tmp); } else { tokenID = 0; }
                        }
                    }
                }

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int orderID = getNextSeq(orderIDSeq);
                    Console.WriteLine("Next Seq : {0}", orderID);
                    string query = "INSERT INTO ORDERS (ID, OrderNo, OrderStatus,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,symbol,price, strike, quantity,direction,version,callput, exch, machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                        + orderID + "," + orderNo + "," + "'COMPLETED'" + "," + linkedOrderID + "," + fixAcceptedID + ",'" + fixOrderID + "'," + tokenID + ",'" + symbol + "'" + "," + price + "," + strike + "," + quantity
                        + ",'" + direction + "'," + (version + 1) + ",'" + callput + "','" + exch + "','" + machineID + "','" + userID + "'," + timeinforce + "," + ordertype + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    Console.WriteLine("insertOrderWithOrdeNo query : {0}", query);
                    cmd.ExecuteNonQuery();
                    return orderID;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception (insertOrderWithOrdeNO) : " + ex.Message);
            }
            return -1;
        }

        public int insertOrder(ref OrderStruct os)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int orderID = getNextSeq(orderIDSeq);
                    int orderNo = getNextSeq(orderSeq);
                    Console.WriteLine("Next Seq : {0}", orderNo);
                    string symbol = sanitiseField(os.symbol);
                    string query = "INSERT INTO ORDERS (ID, OrderNo, OrderStatus,tokenID,symbol,price, strike, quantity,direction,version,expiry,callput, exch, machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                        + orderID + "," + orderNo + "," + "'NEW'" + "," + os.tokenID +",'" + symbol + "'" + "," + os.price + "," + os.strike + "," + os.quantity + ",'"
                        + os.direction + "',1,'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','" + new string(os.machineID)
                        + "','" + new string(os.userID) + "'," + os.timeInForce + "," + os.orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    os.ID = orderID;
                    os.OrderNo = orderNo;
                    return orderNo;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception (Insert) : " + ex.Message);
            }
            return -1;
        }

        public int amendOrder(ref OrderStruct os)//Update the previouss and add new
        {
            try
            {
                int orderID = getNextSeq(orderIDSeq);
                int orderNo = os.OrderNo;
                int ver = -1;
                OrderStruct localOs = new OrderStruct();
                getOrderFromDB(os.OrderNo,ref localOs);
                //getLastVersion(orderNo, ref ver, ref id, ref fixAcceptedID, ref fixOrderID);
                //Console.WriteLine("DBG: " + orderNo + " : " + ver + " : " + id);

                if (localOs.version == 0) { ver = 1; } else { ver = localOs.version + 1; }
                Console.WriteLine("DBG: " + localOs.OrderNo + " : " + ver + " : " + orderID);
                /*
                 Assumption: IN case we are receiveing -1 or 0 for fixAcceptedID, we are not allowing the modification. we will go for cancel
                 and rebook -- need to check.
                 */
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string symbol = sanitiseField(os.symbol);
                    string query = "INSERT INTO ORDERS (ID,OrderNo,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,OrderStatus,symbol,price, strike, quantity,direction,version,expiry,callput,exch,machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                        + orderID + "," + orderNo + "," + localOs.ID + "," + localOs.fixAcceptedID + ",'" + localOs.fixOrderID +"',"+ localOs.tokenID + "," + "'AMEND'" + ",'" + symbol + "'," + os.price + "," + os.strike + "," + os.quantity + ",'" + os.direction + "',"
                        + ver + ",'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','" + new string(os.machineID) + "','"
                        + new string(os.userID) + "'," + os.timeInForce + "," + os.orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    os.ID = localOs.fixAcceptedID;
                }
                return orderID;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(Amend) : " + ex.Message);
            }
            return -1;
        }

        public int cancelOrder(ref OrderStruct os)//Update the previouss and add new
        {
            try
            {
                int orderID = getNextSeq(orderIDSeq);
                int orderNo = os.OrderNo;
                int ver = -1;
                int id = -1;
                int fixAcceptedID = -1;
                string fixOrderID = "-1";
                getLastVersion(orderNo, ref ver, ref id, ref fixAcceptedID, ref fixOrderID);
                Console.WriteLine("DBG: " + orderNo + " : " + ver + " : " + id);
                if (ver == 0) { ver = 1; } else { ver++; }
                Console.WriteLine("DBG: " + orderNo + " : " + ver + " : " + id);
                /*
                 Assumption: IN case we are receiveing -1 or 0 for fixAcceptedID, we are not allowing the modification. we will go for cancel
                 and rebook -- need to check.
                 */
                /*if (fixAcceptedID == 0) {
                    Console.WriteLine("DBG: We are getting 0 from DB for fixAcceptedID. Hence we should do retry\n");
                    return -1;
                } */
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string symbol = sanitiseField(os.symbol);
                    string query = "INSERT INTO ORDERS (ID,OrderNo,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,OrderStatus,symbol,price, strike, quantity,direction,version,expiry,callput,exch,machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                        + orderID + "," + orderNo + "," + id + "," + fixAcceptedID + ",'" + fixOrderID + "'," + os.tokenID + ",'CANCEL'" + ",'" + symbol + "'," + os.price + "," + os.strike + "," + os.quantity + ",'" + os.direction + "',"
                        + ver + ",'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','" + new string(os.machineID) + "','"
                        + new string(os.userID) + "'," + os.timeInForce + "," + os.orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    os.ID = fixAcceptedID;
                }
                return orderID;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(Cancel) : " + ex.Message);
            }
            return -1;
        }

        /* Used only for testing from OM Interface*/
        public int addOrderFills(OrderFillStruct ofs)
        {
            int orderNo = -1;
            int ordStatus = -1;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextSeq = getNextSeq(FillSeq);
                    orderNo = getOrderNoFromOrderID(ofs.OrderNo);
                    string query = "INSERT INTO FILLS (ID,OrderNo,FillID,Quantity,Price, FilledQuantity, insertTS) VALUES ("
                    + nextSeq + "," + orderNo + "," + ofs.FillID + "," + ofs.Quantity + "," + ofs.Price + "," + ofs.FilledQuantity + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                if ((ordStatus = fillStatus(orderNo)) == 0) // Order completed. Make Insert.
                {
                    insertOrderWithOrdeNO(orderNo);
                }
                return ordStatus;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(addOrderFills) : " + ex.Message);
            }
            return -1;
        }

        public int addOrderFillsFromFIX(int orderID, int orderNo, string FixRef, float qty, float price, float filledQty, string status)
        {
            int ordStatus = -1;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextSeq = getNextSeq(FillSeq);
                    string query = "INSERT INTO FILLS (ID,OrderNo,FillID,Quantity,Price, FilledQuantity, insertTS) VALUES ("
                    + nextSeq + "," + orderNo + "," + FixRef + "," + qty + "," + price + "," + filledQty + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query :" + query);
                    cmd.ExecuteNonQuery();
                }
                if ((ordStatus = fillStatus(orderNo)) == 0) // Order completed. Make Insert.
                {
                    insertOrderWithOrdeNO(orderNo);
                }
                return ordStatus;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(addOrderFills) : " + ex.Message);
            }
            return -1;
        }

        public int addFIXResponse(Int32 orderID, string ExchangeOrderId, char status, string status_msg, string other_msg)
        {
            int ordStatus = 0;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextSeq = getNextSeq(FillSeq);
                    string query = "INSERT INTO fixResponse (ID,OrderNo,FixRef,status,status_msg, other_msg, insertTS) VALUES ("
                    + nextSeq + "," + orderID + "," + ExchangeOrderId + ",'" + status + "','" + status_msg + "','" + other_msg + "',SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query :" + query);
                    cmd.ExecuteNonQuery();
                }
                return ordStatus;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(addFIXResponse) : " + ex.Message);
            }
            return -1;
        }

        public int setOrdersWithReplacedFixResponse(int orderID, string status, string fixRef, ref OrderStruct os)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextOrderSeqID = getNextSeq(orderIDSeq);
                    int lastOrderID = getOrderFromDBwithID(orderID, ref os);
                    int ver = getLastVersionFromOrderID(orderID);
                    string query = "INSERT INTO ORDERS (ID,OrderNo,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,OrderStatus,symbol,price, strike, quantity,direction,version,expiry, callput, exch, machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                    + nextOrderSeqID + "," + os.OrderNo + "," + lastOrderID + "," + orderID + ",'" + fixRef + "'," + os.tokenID + ",'FIX-" + status + "','" + new string(os.symbol) + "'," + os.price + "," + os.strike + "," + os.quantity + ",'"
                    + os.direction + "'," + (ver + 1) + ",'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','"
                    + new string(os.machineID) + "','" + new string(os.userID) + "'," + os.timeInForce + "," + os.orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    getOrderFromDB(os.OrderNo, ref os);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(setOrdersWithReplacedFixResponse) : " + ex.Message);
            }
            return -1;
        }


        public int setOrdersWithFixResponse(int orderID, string status, string fixRef, ref OrderStruct os)
        {
            int fixAcceptedID = -1;
            try
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextOrderSeqID = getNextSeq(orderIDSeq);
                    int lastOrderID = getLastVersionFromOrderID(orderID, ref os);
                    if (status == "PENDING_NEW")
                    {
                        fixAcceptedID = orderID;
                    }
                    else
                    {
                        fixAcceptedID = os.fixAcceptedID;
                    }
                    string query = "INSERT INTO ORDERS (ID,OrderNo,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,OrderStatus,symbol,price, strike, quantity,direction,version,expiry, callput, exch, machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                    + nextOrderSeqID + "," + os.OrderNo + "," + lastOrderID + "," + fixAcceptedID + ",'" + fixRef + "'," + os.tokenID + ",'FIX-" + status + "','" + new string(os.symbol) + "'," + os.price + "," + os.strike + "," + os.quantity + ",'"
                    + os.direction + "'," + (os.version + 1) + ",'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','"
                    + new string(os.machineID) + "','" + new string(os.userID) + "'," + os.timeInForce + "," + os.orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    getOrderFromDB(os.OrderNo, ref os);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getOrderStatusFromDB) : " + ex.Message);
            }
            return -1;
        }

        public int setOrdersWithFixResponseRestated(int orderID, string status, string fixRef, float price, char orderType, ref OrderStruct os)
        {
            int fixAcceptedID = -1;
            try
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextOrderSeqID = getNextSeq(orderIDSeq);
                    int lastOrderID = getLastVersionFromOrderID(orderID, ref os);
                    if (status == "PENDING_NEW")
                    {
                        fixAcceptedID = orderID;
                    }
                    else
                    {
                        fixAcceptedID = os.fixAcceptedID;
                    }
                    string query = "INSERT INTO ORDERS (ID,OrderNo,LinkedOrderID,fixAcceptedID,fixOrderID,tokenID,OrderStatus,symbol,price, strike, quantity,direction,version,expiry, callput, exch, machineID,userID,timeinforce,ordertype,insertTS) VALUES ("
                    + nextOrderSeqID + "," + os.OrderNo + "," + lastOrderID + "," + fixAcceptedID + ",'" + fixRef + "'," + os.tokenID + ",'FIX-" + status + "','" + new string(os.symbol) + "'," + price + "," + os.strike + "," + os.quantity + ",'"
                    + os.direction + "'," + (os.version + 1) + ",'" + new string(os.expiry) + "','" + new string(os.callput) + "','" + new string(os.exch) + "','"
                    + new string(os.machineID) + "','" + new string(os.userID) + "'," + os.timeInForce + "," + orderType + ",SYSDATETIME());";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    getOrderFromDB(os.OrderNo, ref os);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getOrderStatusFromDB) : " + ex.Message);
            }
            return -1;
        }

        private void getOrderStatusFromDB(int OrderID, ref char[] OrderStatus)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select OrderStatus from orders where version = (select max(version) from orders where orderNo = " + OrderID + ") and orderNo = " + OrderID + "; ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["OrderStatus"];
                            if (tmp != DBNull.Value)
                            {
                                OrderStatus = ((string)tmp).ToCharArray();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getOrderStatusFromDB) : " + ex.Message);
            }
        }

        public int getOrderFromDB(int OrderNo, ref OrderStruct os)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ID,OrderNo,OrderStatus,fixAcceptedID, fixOrderID, tokenID, orderType, timeInForce, symbol,price, quantity,direction,version,machineID,userID,insertTS FROM ORDERS where version = (select max(version) from orders where orderNo = " + OrderNo + ") and orderNo = " + OrderNo + ";";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["ID"];
                            if (tmp != DBNull.Value) { os.ID = Convert.ToInt32((Int64)tmp); }
                            tmp = reader["OrderNo"];
                            if (tmp != DBNull.Value) { os.OrderNo = Convert.ToInt32((Int64)tmp); }
                            tmp = reader["OrderStatus"];
                            if (tmp != DBNull.Value) { os.OrderStatus = ((string)tmp).ToCharArray(); }
                            tmp = reader["symbol"];
                            if (tmp != DBNull.Value) { os.symbol = ((string)tmp).ToCharArray(); }
                            tmp = reader["price"];
                            if (tmp != DBNull.Value) { os.price = (float)((Decimal)tmp); }
                            tmp = reader["quantity"];
                            if (tmp != DBNull.Value) { os.quantity = (float)((Decimal)tmp); }
                            tmp = reader["direction"];
                            if (tmp != DBNull.Value) { os.direction = ((string)tmp).ToCharArray()[0]; }
                            tmp = reader["version"];
                            if (tmp != DBNull.Value) { os.version = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["machineID"];
                            if (tmp != DBNull.Value) { os.machineID = ((string)tmp).ToCharArray(); }
                            tmp = reader["userID"];
                            if (tmp != DBNull.Value) { os.userID = ((string)tmp).ToCharArray(); }
                            tmp = reader["fixAcceptedID"];
                            if (tmp != DBNull.Value) { os.fixAcceptedID = (int)Convert.ToInt32((Int64)tmp); }
                            tmp = reader["fixOrderID"];
                            if (tmp != DBNull.Value) { os.fixOrderID = ((string)tmp).ToCharArray(); }
                            tmp = reader["orderType"];
                            if (tmp != DBNull.Value) { os.orderType = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["timeInForce"];
                            if (tmp != DBNull.Value) { os.timeInForce = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["tokenID"];
                            if (tmp != DBNull.Value) { os.tokenID = (int)Convert.ToInt32((Int32)tmp); }
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception(getOrderStatusFromDB) : " + e.Message + "\n" + e.StackTrace);
                return -1;
            }
        }

        public int getOrderFromDBwithID(int OrderID, ref OrderStruct os)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ID,OrderNo,OrderStatus,fixAcceptedID, fixOrderID, tokenID, symbol,price, quantity,direction,version,machineID,userID,insertTS,orderType,timeInForce FROM ORDERS where ID = " + OrderID + ";";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["ID"];
                            if (tmp != DBNull.Value) { os.ID = Convert.ToInt32((Int64)tmp); }
                            tmp = reader["OrderNo"];
                            if (tmp != DBNull.Value) { os.OrderNo = Convert.ToInt32((Int64)tmp); }
                            tmp = reader["OrderStatus"];
                            if (tmp != DBNull.Value) { os.OrderStatus = ((string)tmp).ToCharArray(); }
                            tmp = reader["symbol"];
                            if (tmp != DBNull.Value) { os.symbol = ((string)tmp).ToCharArray(); }
                            tmp = reader["price"];
                            if (tmp != DBNull.Value) { os.price = (float)((Decimal)tmp); }
                            tmp = reader["quantity"];
                            if (tmp != DBNull.Value) { os.quantity = (float)((Decimal)tmp); }
                            tmp = reader["direction"];
                            if (tmp != DBNull.Value) { os.direction = ((string)tmp).ToCharArray()[0]; }
                            tmp = reader["version"];
                            if (tmp != DBNull.Value) { os.version = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["machineID"];
                            if (tmp != DBNull.Value) { os.machineID = ((string)tmp).ToCharArray(); }
                            tmp = reader["userID"];
                            if (tmp != DBNull.Value) { os.userID = ((string)tmp).ToCharArray(); }
                            tmp = reader["fixAcceptedID"];
                            if (tmp != DBNull.Value) { os.fixAcceptedID = (int)Convert.ToInt32((Int64)tmp); }
                            tmp = reader["fixOrderID"];
                            if (tmp != DBNull.Value) { os.fixOrderID = ((string)tmp).ToCharArray(); }
                            tmp = reader["orderType"];
                            if (tmp != DBNull.Value) { os.orderType = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["timeInForce"];
                            if (tmp != DBNull.Value) { os.timeInForce = (int)Convert.ToInt32((Int32)tmp); }
                            tmp = reader["tokenID"];
                            if (tmp != DBNull.Value) { os.tokenID = (int)Convert.ToInt32((Int32)tmp); }
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception(getOrderStatusFromDB) : " + e.Message + "\n" + e.StackTrace);
                return -1;
            }
        }

        private int getLastVersionFromOrderID(int orderID, ref OrderStruct os)
        {
            int orderNo = -1;
            int oldOrderID = -1;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select ID, OrderNo from ORDERS WHERE ID = " + orderID + ";";
                    cmd.CommandText = query;
                   // Console.WriteLine("query : {0}", query);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["OrderNo"];
                            if (tmp != DBNull.Value) { orderNo = Convert.ToInt32(tmp); } else { orderNo = -1; }
                            tmp = reader["ID"];
                            if (tmp != DBNull.Value) { oldOrderID = Convert.ToInt32(tmp); } else { oldOrderID = -1; }
                        }
                    }
                    getOrderFromDB(orderNo, ref os);
                }
                return oldOrderID;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getLastVersionFromOrderID) : " + ex.Message);
            }
            return -1;
        }

        public int getMachineAndUserFromDB(int OrderNo, ref string machineID, ref string userID)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT machineID,userID FROM ORDERS where version = (select max(version) from orders where orderNo = " + OrderNo + ") and orderNo = " + OrderNo + ";";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["machineID"];
                            if (tmp != DBNull.Value) { machineID = ((string)tmp); }
                            tmp = reader["userID"];
                            if (tmp != DBNull.Value) { userID = ((string)tmp); }
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception(getOrderStatusFromDB) : " + e.Message + "\n" + e.StackTrace);
                return -1;
            }
        }

        //EOD Position handling methods
        // get the token, user, direction, qty
        //Formula --> val = ['b']->val - ['s']->val;

        private float getYdayEodPosition(int tokenID, string userID)
        {
            try
            {
                float ydayEod = 0;
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT value from eodPositions where eod_Date = GETDATE()-1 and mode = 'auto' and tokenID = " + tokenID + " and userID = '" + userID + "';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["value"];
                            if (tmp != DBNull.Value) { ydayEod = (float)((Decimal)tmp); }
                        }
                    }
                }
                return ydayEod;
            } catch(Exception ex)
            {
                Console.Write("Exception(getYdayEodPosition) : " + ex.Message);
            }
            return -1;
        }

        private float calculateEodPosition(int tokenID, string userID)
        {
            try
            {
                float buyFillQty = 0;
                float saleFillQty = 0;
                float position = 0;
                float ydayEod = 0;
                int dateOffset = 0;
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select p.direction direction, sum(pp.quantity) filledQty, pp.fillDate fillDate " +
                        "from(select o.OrderNo, max(version) ver, o.direction, o.tokenID, o.userID from orders o "+
                        "Group By o.OrderNo, o.direction, o.tokenID, o.userID) p INNER JOIN(select orderNo, DATEADD(dd, "+
                        "DATEDIFF(dd, 0, insertTS), 0) fillDate, sum(FilledQuantity) quantity from fills group by OrderNo, "+
                        "DATEADD(dd, DATEDIFF(dd, 0, insertTS), 0)) pp ON pp.orderNo = p.OrderNo WHERE p.tokenID = "+ tokenID +" and "+
                        "p.userID = '"+ userID +"' and fillDate = DATEADD(dd, DATEDIFF(dd, 0, GETDATE() - "+ dateOffset +
                        "), 0) Group by p.direction, pp.fillDate;";
                    cmd.CommandText = query;
                    //Console.WriteLine("query : {0}", query);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tmp = reader["direction"];
                            if (tmp != DBNull.Value)
                            {
                                var val = reader["filledQty"];
                                if (tmp.Equals("B"))
                                {
                                    buyFillQty = (float)((Decimal)val);
                                }
                                else
                                {
                                    saleFillQty = (float)((Decimal)val);
                                }
                            }
                        }
                    }                     
                }
                ydayEod = getYdayEodPosition(tokenID,userID);
                position = ydayEod + buyFillQty - saleFillQty;
                Console.WriteLine("ydayeod : " + ydayEod + " buyFillQty : " + buyFillQty +" saleFillQty : " + saleFillQty +" position :" + position);
                return position;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(setPosition) : " + ex.Message);
            }
            return -1;
        }

        public int setPosition(int tokenID, float value, string userID, string mode)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    int nextEODSeqID = getNextSeq(eodSeq);                    
                    string query = "INSERT INTO eodPositions (ID,eod_date,tokenID, userID, value, mode,insertTS) VALUES ("
                    + nextEODSeqID + ",SYSDATETIME()," + tokenID + ",'" + userID + "'," + value + ",'"+ mode +"',SYSDATETIME());";
                    cmd.CommandText = query;
                    Console.WriteLine("query : {0}", query);
                    cmd.ExecuteNonQuery();
                    return nextEODSeqID;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception(setPosition) : " + ex.Message);
            }
            return -1;
        }

        public int setEodPosition(int tokenID, string userID) {
            float value = 0;
            int eodID = -1;
            value = calculateEodPosition(tokenID, userID);
            Console.WriteLine("setEodPosition value : " + value);
            if (value != 0)
            {
                eodID = setPosition(tokenID, value, userID, "auto");
            }
            return eodID;
        }

        public int takeSnapshotPosition(int tokenID, string userID)
        {
            float value = 0;
            int eodID = -1;
            value = calculateEodPosition(tokenID, userID);
            eodID = setPosition(tokenID, value, userID, "manual");
            return eodID;
        }

        public float getCurrentPosition(int tokenID, string userID)
        {
            return(calculateEodPosition(tokenID, userID));
        }

        private int getTokenAndUserIDYtd(ref Dictionary<string,int> eodList)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select distinct p.tokenID tokID, p.userID  usrID from ( select distinct o.OrderNo, o.tokenID, o.userID from orders o) p"+
                        " INNER JOIN (select distinct orderNo, DATEADD(dd, DATEDIFF(dd, 0, insertTS), 0) fillDate from fills ) pp ON pp.orderNo = p.OrderNo"+
                        " WHERE fillDate = DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0);";
                    Console.WriteLine(cmd.CommandText);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tokID = reader["tokID"];
                            var usrID = reader["usrID"];
                            eodList.Add(tokID+":"+usrID,1);
                        }
                    }
                    cmd.CommandText = "select tokenID, userID from eodPositions where DATEADD(dd, DATEDIFF(dd,0,eod_date), 0) = DATEADD(dd, DATEDIFF(dd,0,GETDATE()), 0);";
                    Console.WriteLine(cmd.CommandText);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tokID = reader["tokenID"];
                            var usrID = reader["userID"];
                            eodList.Add(tokID + ":" + usrID, 2);
                        }
                    }
                }
                return eodList.Count;
            }
            catch (Exception ex)
            {
                Console.Write("Exception(getTokenAndUserIDYtd) : " + ex.Message);
            }
            return -1;
        }

        public int runEodPositions()
        {
            //get all the token from today
            var eodList = new Dictionary<string, int>();
            int totalToken = getTokenAndUserIDYtd(ref eodList);
            Console.WriteLine("Total token to be selected for EOD : "+totalToken);
            foreach(string item in eodList.Keys)
            {
                string [] items = item.Split(':');
                int val = setEodPosition(Convert.ToInt32(items[0]),items[1]);
                Console.WriteLine("For Token : " + items[0]+ " userID : "+items[1] + " Eod Save ID : "+val);
            }
            return 0;
        }
    }
}

