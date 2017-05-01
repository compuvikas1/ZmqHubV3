using QuickFix;
using QuickFix.Fields;
using System;

namespace OrderManagementV2
{
    //Order events status
    enum state
    {
        NEW,
        SCRATCH,
        AMEND,
        CANCEL,
        REJECT,
        WORKING,
        COMPLETED
    }

    //Zmq Action State
    enum action
    {
        SAVE,
        CANCEL,
        PUBLISH
    }

    class Orders
    {
        private bool isOrderAmendPossible(char[] orderStatus)
        {
            return true;
        }

        public Orders() { }

        public int addAck()
        {
            return 0;
        }

        public int addReject()
        {
            return 0;
        }

        public string getExecType(char e)
        {
            switch (e)
            {
                case '0':
                    return "NEW";
                case '1':
                    return "PARTIAL_FILL";
                case '2':
                    return "FILLED";
                case '4':
                    return "CANCELLED";
                case '5':
                    return "REPLACED";
                case '6':
                    return "PENDING_CANCEL";
                case '8':
                    return "REJECTED";
                case 'A':
                    return "PENDING_NEW";
                case 'C':
                    return "EXPIRED";
                case 'D':
                    return "RESTATED";
                case 'E':
                    return "PENDING_REPLACE";
            }
            return null;
        }
        //remove the extra # added in the symbol name
        //we added the # to maintain the length in symbolname while forwarding from zmq
        private string sanitiseField(char[] field)
        {
            string data = new string(field);
            if (data.Contains("#"))
            {
                return new string(data.Substring(0, data.IndexOf('#')).ToCharArray());
            }
            return data;
        }


        public int addOrder(OrderStruct os)
        {
            OrderDAO odao = new OrderDAO();
            Console.WriteLine("Going to call DB");
            int ordNo = odao.insertOrder(ref os);

            // Keep the OrderNO for client and pass the OrderID to FIX.
            ClOrdID OrdId = new ClOrdID(Convert.ToString(os.ID));
            string symboldata = sanitiseField(os.symbol);

            //check Futures or Options Order
            string exch = new string(os.exch);

            if (exch.Equals("STK"))
            {
                if (os.direction == 'B')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.BUY);
                    int Qty = (int)os.quantity;

                    var orderNFO = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side, new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));

                    orderNFO.SetField(new OrderQty(Qty));
                    orderNFO.ExDestination = new ExDestination("NS");
                    orderNFO.SecurityType = new SecurityType(SecurityType.COMMON_STOCK);
                    //string expiry = new string(os.expiry);
                    //orderNFO.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNFO.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNFO.OrdType = ordType;

                    orderNFO.Price = new Price((decimal)os.price);

                    Console.WriteLine("STOCK BUY Order for : " + symbol + "orderType : " + ordType);

                    //Send Future order to fix server
                    Session.SendToTarget(orderNFO, FixClient.MySess);
                }
                else if (os.direction == 'S')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.SELL);
                    int Qty = (int)os.quantity;

                    var orderNFO = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));

                    orderNFO.SetField(new OrderQty(Qty));

                    orderNFO.ExDestination = new ExDestination("NS");
                    orderNFO.SecurityType = new SecurityType(SecurityType.COMMON_STOCK);
                    //string expiry = new string(os.expiry);
                    //orderNFO.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNFO.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNFO.OrdType = ordType;
                    orderNFO.Price = new Price((decimal)os.price);
                    Console.WriteLine("STOCK SELL Order for : " + symbol + "orderType : " + ordType);
                    //Send Future order to fix server
                    Session.SendToTarget(orderNFO, FixClient.MySess);
                }
            }
            else if (exch.Equals("NFO"))
            {
                if (os.direction == 'B')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.BUY);
                    int Qty = (int)os.quantity;

                    var orderNFO = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));
                    orderNFO.Price = new Price((decimal)os.price);
                    orderNFO.SetField(new OrderQty(Qty));

                    orderNFO.ExDestination = new ExDestination("NS");
                    orderNFO.SecurityType = new SecurityType(SecurityType.FUTURE);
                    string expiry = new string(os.expiry);
                    orderNFO.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNFO.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNFO.OrdType = ordType;
                    Console.WriteLine("NFO BUY Order for ExpiryDate " + expiry.Substring(0, 6));

                    //Send Future order to fix server
                    Session.SendToTarget(orderNFO, FixClient.MySess);
                }
                else if (os.direction == 'S')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.SELL);
                    int Qty = (int)os.quantity;

                    var orderNFO = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));
                    orderNFO.Price = new Price((decimal)os.price);
                    orderNFO.SetField(new OrderQty(Qty));

                    orderNFO.ExDestination = new ExDestination("NS");
                    orderNFO.SecurityType = new SecurityType(SecurityType.FUTURE);
                    string expiry = new string(os.expiry);
                    orderNFO.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNFO.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNFO.OrdType = ordType;

                    Console.WriteLine("NFO SELL Order for ExpiryDate " + expiry.Substring(0, 6));
                    //Send Future order to fix server
                    Session.SendToTarget(orderNFO, FixClient.MySess);
                }
            }
            else if (exch.Equals("NOP"))
            {
                if (os.direction == 'B')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.BUY);
                    int Qty = (int)os.quantity;
                    string callput = new string(os.callput);

                    var orderNOP = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));
                    orderNOP.Price = new Price((decimal)os.price);
                    orderNOP.SetField(new OrderQty(Qty));

                    orderNOP.ExDestination = new ExDestination("NS");
                    orderNOP.SecurityType = new SecurityType(SecurityType.OPTION);

                    string expiry = new string(os.expiry);
                    orderNOP.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNOP.StrikePrice = new StrikePrice((decimal)(os.strike));
                    if (callput.Equals("CE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(1);
                    else if (callput.Equals("PE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(0);
                    orderNOP.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNOP.OrdType = ordType;
                    Console.WriteLine("NOP BUY Order for ExpiryDate " + expiry.Substring(0, 6));
                    //Send Option order to fix server
                    Session.SendToTarget(orderNOP, FixClient.MySess);
                }
                else if (os.direction == 'S')
                {
                    Symbol symbol = new Symbol(symboldata);
                    Side side = new Side(Side.SELL);
                    int Qty = (int)os.quantity;
                    string callput = new string(os.callput);

                    var orderNOP = new QuickFix.FIX42.NewOrderSingle(OrdId, new HandlInst('1'), symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()), new OrdType(OrdType.LIMIT));
                    orderNOP.Price = new Price((decimal)os.price);
                    orderNOP.SetField(new OrderQty(Qty));

                    orderNOP.ExDestination = new ExDestination("NS");
                    orderNOP.SecurityType = new SecurityType(SecurityType.OPTION);

                    string expiry = new string(os.expiry);
                    orderNOP.MaturityMonthYear = new MaturityMonthYear(expiry.Substring(0, 6));
                    orderNOP.StrikePrice = new StrikePrice((decimal)(os.strike));
                    if (callput.Equals("CE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(1);
                    else if (callput.Equals("PE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(0);
                    orderNOP.Account = new Account("D2");
                    OrdType ordType = new OrdType(OrdType.LIMIT);
                    if (os.orderType == 1)
                    {
                        ordType = new OrdType(OrdType.MARKET);
                    }
                    orderNOP.OrdType = ordType;

                    Console.WriteLine("NOP SELL Order for ExpiryDate " + expiry.Substring(0, 6));
                    //Send Option order to fix server
                    Session.SendToTarget(orderNOP, FixClient.MySess);
                }
            }
            return ordNo;
        }

        public int amendOrder(ref OrderStruct os)
        {
            if (!isOrderAmendPossible(os.OrderStatus))
            {
                return -1;
            }
            OrderDAO ord = new OrderDAO();
            int newOrdID = ord.amendOrder(ref os); // OrderID is the last FixAccepted version
            Console.WriteLine("TODO: Send FIX - cancel for os.LinkedOrderID ; Send FIX - New for os.ID");
            OrigClOrdID origCLOrdID = new OrigClOrdID(Convert.ToString(os.ID));
            ClOrdID OrdId = new ClOrdID(Convert.ToString(newOrdID));
            string symboldata = sanitiseField(os.symbol);
            Symbol symbol = new Symbol(symboldata);
            Side side = new Side(Side.BUY);
            if (os.direction == 'S')
            {
                side = new Side(Side.SELL);
            }
            OrdType ordType = new OrdType(OrdType.LIMIT);
            if (os.orderType == 1)
            {
                ordType = new OrdType(OrdType.MARKET);
            }
            var orderMod = new QuickFix.FIX42.OrderCancelReplaceRequest(origCLOrdID, OrdId, new HandlInst('1'), symbol, side, new TransactTime(DateTime.Now.ToUniversalTime()), ordType);
            int Qty = (int)os.quantity;
            orderMod.Price = new Price((decimal)os.price);
            orderMod.SetField(new OrderQty(Qty));
            Console.WriteLine("Modifying origOrdId/FixAcceptedID : " + os.ID + " newOrderID : " + newOrdID);
            Console.WriteLine("symboldata : " + symboldata + " side : " + side + " Price : " + os.price + " qty : " + os.quantity);
            Session.SendToTarget(orderMod, FixClient.MySess);
            return os.OrderNo;
        }

        public int cancelOrder(OrderStruct os)
        {//will add check in case of already filled, no cancel.
            OrderDAO ord = new OrderDAO();
            OrderStruct dbos = new OrderStruct(11, 8);

            if (ord.getOrderFromDB(os.OrderNo, ref dbos) < 0)
            {
                Console.WriteLine("Error : Unable to get order details from DB");
                return -1;
            }
            Console.WriteLine("DBG: truct val");
            dbos.display();
            string ordStatus = new string(dbos.OrderStatus);
            //if(ordStatus.Equals("CANCELED") || ordStatus.Equals("COMPLETED"))
            //{
            //    Console.WriteLine("Order is already "+ordStatus);
            //    return -1;
            //}
            Console.WriteLine("Order No [" + dbos.OrderNo + "] : Status [" + ordStatus + "]");
            //return -1;
            int ordNo = ord.cancelOrder(ref dbos);
            if (ordNo < 0) { return -1; }
            Console.WriteLine("TODO: Send FIX - cancel for os.ID");
            //Send FIX - cancel for os.ID

            OrigClOrdID origCLOrdID = new OrigClOrdID(Convert.ToString(dbos.fixAcceptedID));
            ClOrdID OrdId = new ClOrdID(Convert.ToString(ordNo));
            string symboldata = sanitiseField(os.symbol);
            Symbol symbol = new Symbol(symboldata);
            Side side = new Side(Side.BUY);

            if (os.direction == 'S')
            {
                side = new Side(Side.SELL);
            }

            Console.WriteLine("cancelling origOrdId : " + dbos.fixAcceptedID + " newOrdId : " + ordNo);
            Console.WriteLine("symboldata : " + symboldata + " side : " + side);

            var orderCan = new QuickFix.FIX42.OrderCancelRequest(origCLOrdID, OrdId, symbol, side,
                new TransactTime(DateTime.Now.ToUniversalTime()));

            Session.SendToTarget(orderCan, FixClient.MySess);
            return os.OrderNo;
        }

        public int addFills(OrderFillStruct ofs) // stored the fills in db and send it to fillpublisher
        {
            OrderDAO ord = new OrderDAO();
            int ordStatus = ord.addOrderFills(ofs);
            if (ordStatus == -1)
            {
                Console.WriteLine("error in DB");
                return -1;
            }
            else if (ordStatus == 0)
            {
                Console.WriteLine("This Order is completed now [Order ID] = " + ofs.OrderNo);
            }
            else
            {
                Console.WriteLine("Order qty diff : " + ordStatus);
            }
            string machineID = "";
            string userID = "";
            ord.getMachineAndUserFromDB(ofs.OrderNo, ref machineID, ref userID);
            OMFillPub ofp = new OMFillPub();
            if (ofp.zmqUpdate(ofs, machineID, userID) == 0)
            {
                Console.WriteLine("Zmq update success");
            }
            else
            {
                Console.WriteLine("Zmq update fail");
            }
            return 0;
        }

        public int FIXResponseCanModRejectHandler(Int32 OrderID, string ExchangeOrderId, Int32 origOrdID, char status, string status_msg, string other_msg)
        {
            string data = "";
            OrderDAO ord = new OrderDAO();
            OrderStruct os = new OrderStruct();
            int iret = -1;
            Console.WriteLine("FIXResponseCanModRejectHandler : " + status);
            if (status == '1')
            { // rejected so move the order to originalID
                //iret = ord.setOrdersWithReplacedFixResponse(origOrdID, "CANCEL_REJECTED", ExchangeOrderId, ref os);
                iret = ord.setOrdersWithFixResponse(OrderID, "CANCEL_REJECTED", ExchangeOrderId, ref os);
            }
            else if (status == '2')
            { // rejected so move the order to originalID
                //iret = ord.setOrdersWithReplacedFixResponse(origOrdID, "MODIFY_REJECTED", ExchangeOrderId, ref os);
                iret = ord.setOrdersWithFixResponse(OrderID, "MODIFY_REJECTED", ExchangeOrderId, ref os);
            }
            if (iret == -1)
            {
                Console.WriteLine("Error from DB [" + data + "]");
                //return -1;
            }
            else
            {
                Console.WriteLine("Order Updated");
            }
            data = new string(os.machineID) + ":" + new string(os.userID) + ":" + os.OrderNo + ":" + new string(os.OrderStatus) + ":" + ExchangeOrderId + ":" + status + ":" + status_msg + ":" + other_msg;
            OMFillPub ofp = new OMFillPub();
            if (ofp.zmqUpdates(data) == 0)
            {
                Console.WriteLine("Zmq update success");
            }
            else
            {
                Console.WriteLine("Zmq update fail");
            }
            ord.addFIXResponse(OrderID, ExchangeOrderId, status, status_msg, other_msg);
            return 0;
        }

        public int FIXResponseHandler(Int32 OrderID, string ExchangeOrderId, Int32 origOrdID, char status, string status_msg, string other_msg, float price, char orderType)
        {
            string data = "";
            OrderDAO ord = new OrderDAO();
            OrderStruct os = new OrderStruct();
            int iret = -1;
            if (status == 'X') // i am considering the reject of amendment.
            { // rejected so move the order to originalID
                iret = ord.setOrdersWithReplacedFixResponse(origOrdID, getExecType(status), ExchangeOrderId, ref os);
            }
            else if (status == '5')
            { // accepted the amendment i.e. Replaced so add the new ord no.
                iret = ord.setOrdersWithReplacedFixResponse(OrderID, getExecType(status), ExchangeOrderId, ref os);
            } // In case of Order Rejected - call DB for reject status.
            else if (status == 'D')
            {
                iret = ord.setOrdersWithFixResponseRestated(OrderID, getExecType(status), ExchangeOrderId, price, orderType, ref os);

                os.price = price;
                os.orderType = Convert.ToInt32(orderType);
            }
            else
            {
                iret = ord.setOrdersWithFixResponse(OrderID, getExecType(status), ExchangeOrderId, ref os);
            }
            if (iret == -1)
            {
                Console.WriteLine("Error from DB [" + data + "]");
                //return -1;
            }
            else
            {
                Console.WriteLine("Order Updated");
            }
            data = new string(os.machineID) + ":" + new string(os.userID) + ":" + os.OrderNo + ":" + new string(os.OrderStatus) + ":" + ExchangeOrderId + ":" + status + ":" + status_msg + ":" + other_msg;
            OMFillPub ofp = new OMFillPub();
            if (ofp.zmqUpdates(data) == 0)
            {
                Console.WriteLine("Zmq update success");
            }
            else
            {
                Console.WriteLine("Zmq update fail");
            }
            ord.addFIXResponse(OrderID, ExchangeOrderId, status, status_msg, other_msg);
            return 0;
        }

        public int FIXResponseHandlerForFill(int OrderID, string ExchangeOrderId, float qty, float price, float filledQty, string status, char exectype)
        {
            OrderDAO ord = new OrderDAO();
            OrderStruct os = new OrderStruct();
            int orderNo = ord.getOrderNoFromOrderID(OrderID);
            ord.addOrderFillsFromFIX(OrderID, orderNo, ExchangeOrderId, qty, price, filledQty, status);
            ord.getOrderFromDB(orderNo, ref os);
            float pos = ord.getCurrentPosition(os.tokenID, new string(os.userID));            
            string other_msg = Convert.ToString(pos);
            string data = new string(os.machineID) + ":" + new string(os.userID) + ":" + os.OrderNo + ":" + new string(os.OrderStatus) + ":" + ExchangeOrderId + ":" + exectype + ":" + status + ":" + other_msg;
            OMFillPub ofp = new OMFillPub();
            if (ofp.zmqUpdates(data) == 0)
            {
                Console.WriteLine("Zmq update success");
            }
            else
            {
                Console.WriteLine("Zmq update fail");
            }
            return 0;
        }

        public int takeEODPositions()
        {
            OrderDAO ord = new OrderDAO();
            return (ord.runEodPositions());
        }

        private int isPartillyFilled()
        {
            return 0;
        }

        private int isFullyFilled()
        {
            return 0;
        }

        private bool canCancelOrder()
        {
            return false;
        }
    }
}
