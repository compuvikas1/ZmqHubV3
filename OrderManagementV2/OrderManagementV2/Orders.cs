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
        private bool isOrderAmendPossible(char [] orderStatus)
        {
            return true;
        }
        
        public Orders() { }
                
        public int addAck() {
            return 0;
        }

        public int addReject()
        {
            return 0;
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
            int ordNo = odao.insertOrder(os);

            ClOrdID OrdId = new ClOrdID(Convert.ToString(ordNo));
            string symboldata = sanitiseField(os.symbol);

            //check Futures or Options Order
            string exch = new string(os.exch);
            
            if(exch.Equals("NFO"))
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
                    if(callput.Equals("CE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(1);
                    else if (callput.Equals("PE")) // 0 = PE & 1 = CE
                        orderNOP.PutOrCall = new PutOrCall(0);
                    orderNOP.Account = new Account("D2");

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
            int ordNo = ord.amendOrder(ref os);
            Console.WriteLine("TODO: Send FIX - cancel for os.LinkedOrderID ; Send FIX - New for os.ID");
            //Send FIX - cancel for os.LinkedOrderID
            //Send FIX - New for os.ID
            return ordNo;            
        }
        
        public int cancelOrder(OrderStruct os) {//will add check in case of already filled, no cancel.
            OrderDAO ord = new OrderDAO();
            OrderStruct dbos = new OrderStruct(8,8); 
            if(ord.getOrderFromDB(os.OrderNo,ref dbos) < 0)
            {
                Console.WriteLine("Error : Unable to get order details from DB");
                return -1;
            }
            Console.WriteLine("DBG: truct val");
            dbos.display();
            string ordStatus = new string(dbos.OrderStatus);
            if(ordStatus.Equals("CANCELED") || ordStatus.Equals("COMPLETED"))
            {
                Console.WriteLine("Order is already "+ordStatus);
                return -1;
            }
            Console.WriteLine("Order No ["+dbos.OrderNo+"] : Status ["+ordStatus+"]");
            //return -1;
            if (ord.cancelOrder(dbos) < 0) { return -1; }
            Console.WriteLine("TODO: Send FIX - cancel for os.ID");
            //Send FIX - cancel for os.ID
            return 0;
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
