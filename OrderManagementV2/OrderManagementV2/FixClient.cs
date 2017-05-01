using System;
using QuickFix;
using QuickFix.Fields;
using System.Data;
using System.IO;

namespace OrderManagementV2
{
    
    class FixClient : QuickFix.IApplication
    {
        string logFileName = null;

        public void logMe(string data)
        {
            Console.WriteLine(data);
            File.AppendAllText(logFileName, data);
        }

        public static SessionID MySess;

        DataTable dtStocks = new DataTable();

        public FixClient()
        {
            if(logFileName == null)
            {
                string dateNow = DateTime.Now.ToString("dd_MM_yyyy");
                logFileName = "OMLog_" + dateNow + ".txt";
            }
            //this.frm1 = form1;
        }

        public string getRejectType(CxlRejReason e)
        {
            switch (e.getValue())
            {
                case 0:
                    return "REJECTED: TOO_LATE_TO_CANCEL";                    
                case 1:
                    return "REJECTED: UNKNOWN_ORDER";
                case 2:
                    return "REJECTED: BROKER_EXCHANGE_OPTION";
                case 3:
                    return "REJECTED: ORDER_ALREADY_IN_PENDING_CANCEL_OR_PENDING_REPLACE_STATUS";
                case 4:
                    return "REJECTED: UNABLE_TO_PROCESS_ORDER_MASS_CANCEL_REQUEST";
                case 5:
                    return "REJECTED: ORIGORDMODTIME_DID_NOT_MATCH_LAST_TRANSACTTIME_OF_ORDER";
                case 6:
                    return "REJECTED: DUPLICATE_CLORDID_RECEIVED";
                case 7:
                    return "REJECTED: PRICE_EXCEEDS_CURRENT_PRICE";
                case 8:
                    return "REJECTED: PRICE_EXCEEDS_CURRENT_PRICE_BAND";
                case 18:
                    return "REJECTED: INVALID_PRICE_INCREMENT";
                case 99:
                    return "REJECTED: OTHER";
            }
            return null;
        }

        public string getExecType(ExecType e)
        {
            switch (e.getValue())
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

        public void FromApp(QuickFix.Message msg, SessionID sessionID)
        {
            logMe("\r\n \r\n" + "FromApp :" + msg.ToString());

            try
            {
                if (msg is QuickFix.FIX42.OrderCancelReject)
                {
                    Console.WriteLine("Order Cancel Reject event raised");
                    QuickFix.FIX42.OrderCancelReject er = (QuickFix.FIX42.OrderCancelReject)msg;

                    if (er.IsSetClOrdID())
                        Console.WriteLine("ClOrdID : " + er.ClOrdID);
                    if (er.IsSetOrigClOrdID())
                        Console.WriteLine("OrigClOrdID : " + er.OrigClOrdID);
                    if (er.IsSetOrdStatus())
                        Console.WriteLine("OrderStatus : " + er.OrdStatus);
                    if (er.IsSetCxlRejResponseTo())
                        Console.WriteLine("CxlRejResponseTo : " + er.CxlRejResponseTo);
                    if (er.IsSetCxlRejReason())
                        Console.WriteLine("CxlRejReason : " + er.CxlRejReason);
                    if (er.IsSetOrderID())
                        Console.WriteLine("OrderID : " + er.OrderID);
                    
                    if (er.CxlRejResponseTo.getValue() == CxlRejResponseTo.ORDER_CANCEL_REQUEST)
                    {
                        // Order Cancel Rejected reason
                        // 1= UNKNOWN_ORDER, 2 = Broker Option, 3 = Order already in Pending Cancel or Pending Replace status

                        string orderDetails = "CANCEL_REJECTED";
                        Int32 origOrderID = -1;
                        if (er.IsSetOrigClOrdID())
                        {
                            origOrderID = Convert.ToInt32(er.OrigClOrdID.getValue());
                        }
                        Orders o = new Orders();
                        if (er.IsSetEncodedText())
                            orderDetails += " : " + er.EncodedText.getValue();
                        o.FIXResponseCanModRejectHandler(Convert.ToInt32(er.ClOrdID.getValue()), er.OrderID.getValue(), origOrderID, er.CxlRejResponseTo.getValue(), "CANCEL_" + getRejectType(er.CxlRejReason), orderDetails);
                    }
                    else if (er.CxlRejResponseTo.getValue() == CxlRejResponseTo.ORDER_CANCEL_REPLACE_REQUEST)
                    {
                        // Order modification Rejected reason
                        // 1 = Broker Option
                        // 3 = Order already in Pending Cancel or Pending Replace status

                        string orderDetails = "MODIFY_REJECTED";
                        Int32 origOrderID = -1;
                        if (er.IsSetOrigClOrdID())
                        {
                            origOrderID = Convert.ToInt32(er.OrigClOrdID.getValue());
                        }
                        Orders o = new Orders();
                        if (er.IsSetEncodedText())
                            orderDetails += " : " + er.EncodedText.getValue();
                        o.FIXResponseCanModRejectHandler(Convert.ToInt32(er.ClOrdID.getValue()), er.OrderID.getValue(), origOrderID, er.CxlRejResponseTo.getValue(), "MODIFY_" + getRejectType(er.CxlRejReason), orderDetails);
                    }

                }
                else if (msg is QuickFix.FIX42.ExecutionReport)
                {
                    string ordTypeField = msg.GetField(40);
                    if (ordTypeField == "1")
                    {
                        IField field = new DecimalField(44, 0);
                        msg.SetField(field);
                    }

                    QuickFix.FIX42.ExecutionReport er = (QuickFix.FIX42.ExecutionReport)msg;
                    string orderDetails = Convert.ToString(er.ExecType.getValue()) + "|" + er.OrderQty.getValue() + "|" + er.Price.getValue() + "|" + er.LastShares.getValue() + "|" + er.LastPx.getValue() + "|" + er.CumQty.getValue() + "|" + er.AvgPx.getValue() + "|" + er.OrdType.getValue() + "|";
                    if (er.IsSetText())
                        orderDetails = orderDetails + er.Text + "|";
                    else
                        orderDetails = orderDetails + "NULL|";

                    logMe("\r\n \r\n" + "Got execution Report - ExecType = " + orderDetails);
                    Console.WriteLine("\r\n \r\n" + "Got execution Report - ExecType = " + orderDetails);
                    Orders o = new Orders();
                    if (er.ExecType.getValue() == ExecType.FILL || er.ExecType.getValue() == ExecType.PARTIAL_FILL)
                    {
                        o.FIXResponseHandlerForFill(Convert.ToInt32(er.ClOrdID.getValue()), er.OrderID.getValue(), (float)Convert.ToDouble(er.OrderQty.getValue()), (float)Convert.ToDouble(er.LastPx.getValue()), (float)Convert.ToDouble(er.LastShares.getValue()), getExecType(er.ExecType), er.ExecType.getValue());
                    }
                    else
                    {
                        Int32 origOrderID = -1;
                        float price = -1;
                        char orderType = '2';
                        if (er.IsSetOrigClOrdID())
                        {
                            origOrderID = Convert.ToInt32(er.OrigClOrdID.getValue());
                        }
                        if(er.IsSetPrice())
                        {
                            price = (float)Convert.ToDouble(er.Price.getValue());
                        }
                        if(er.IsSetOrdType())
                        {
                            orderType = er.OrdType.getValue();
                        }

                        //orderid is string since value is too large for int variable
                        o.FIXResponseHandler(Convert.ToInt32(er.ClOrdID.getValue()), er.OrderID.getValue(), origOrderID, er.ExecType.getValue(), getExecType(er.ExecType), orderDetails, price, orderType);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception(FixClient FromAPP) " + e.Message);
            }
        }
        public void OnCreate(SessionID sessionID)
        {
            logMe("\r\n \r\n" + "Session created : " + sessionID.ToString());
            MySess = sessionID;

        }
        public void OnLogout(SessionID sessionID)
        {
            logMe("\r\n \r\n" + "Logged out : " + sessionID.ToString());

        }
        public void OnLogon(SessionID sessionID)
        {
            logMe("\r\n \r\n" + "Logged In : " + sessionID.ToString());
        }
        public void FromAdmin(Message msg, SessionID sessionID)
        {
            //logMe("\r\n \r\n" + "FromAdmin :" + msg.ToString() + "SessionID : " + sessionID.ToString());
        }
        public void ToAdmin(Message msg, SessionID sessionID)
        {
            logMe("\r\n" + "ToAdmin :" + msg.ToString() + "SessionID : " + sessionID.ToString());
        }
        public void ToApp(Message msg, SessionID sessionID)
        {
            logMe("\r\n" + "ToApp :" + msg.ToString());

        }
    }
}

