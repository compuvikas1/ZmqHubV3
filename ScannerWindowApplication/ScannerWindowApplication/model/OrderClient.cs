﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ScannerWindowApplication
{
    class OrderClient
    {
        static TradingBoxV3 tradingBox = null;
        public static Guid MachineGuid = ToGuid("TradingBox1");
        public static Guid UserGuid = ToGuid("user1");

        public static void stringToStruct(string clientString, ref OrderStruct ord)
        {
            string symbol = "";
            string[] toks = clientString.Split(':');
            //foreach (string tok in toks) { Console.WriteLine("["+tok+"]"); }
            symbol = toks[1].ToString();
            int pad = 11 - toks[1].Length;
            if (pad > 0)
            {
                for (int i = 0; i < pad; i++)
                {
                    symbol = symbol + "#";
                }
            }

            if (toks[0].Equals("INS") && toks.Length >= 11)
            {
                ord.methodID = 1;
                ord.symbol = symbol.ToCharArray();
                ord.OrderStatus = "NEW00000".ToCharArray();
                ord.price = (float)Convert.ToDouble(toks[2].ToString());
                ord.quantity = (float)Convert.ToDouble(toks[3].ToString());
                ord.strike = (int)Convert.ToDecimal(toks[4].ToString());
                ord.direction = toks[5][0];
                ord.expiry = toks[6].ToCharArray();
                ord.callput = toks[7].ToCharArray();
                ord.exch = toks[8].ToCharArray();
                ord.machineID = toks[9].ToCharArray();
                ord.userID = toks[10].ToCharArray();
                ord.orderType = (int)Convert.ToDecimal(toks[11].ToString());
                ord.timeInForce = (int)Convert.ToDecimal(toks[12].ToString());
                ord.tokenID = (int)Convert.ToDecimal(toks[13].ToString());
            }
            else if (toks[0].Equals("CAN") && toks.Length >= 3)
            {
                ord.methodID = 2;
                ord.symbol = symbol.ToCharArray();
                ord.OrderNo = (int)Convert.ToDecimal(toks[2].ToString());
            }
            else if (toks[0].Equals("AMD") && toks.Length >= 12)
            {
                ord.methodID = 3;
                ord.symbol = symbol.ToCharArray();
                ord.OrderStatus = "AMD00000".ToCharArray();
                ord.OrderNo = (int)Convert.ToDecimal(toks[2].ToString());
                ord.price = (float)Convert.ToDouble(toks[3].ToString());
                ord.quantity = (float)Convert.ToDouble(toks[4].ToString());
                ord.strike = (int)Convert.ToDecimal(toks[5].ToString());
                ord.direction = toks[6][0];
                ord.expiry = toks[7].ToCharArray();
                ord.callput = toks[8].ToCharArray();
                ord.exch = toks[9].ToCharArray();
                ord.machineID = toks[10].ToCharArray();
                ord.userID = toks[11].ToCharArray();
                ord.orderType = (int)Convert.ToDecimal(toks[12].ToString());
                ord.timeInForce = (int)Convert.ToDecimal(toks[13].ToString());
            }
            else if (toks[0].Equals("EOD") && toks.Length >= 1)
            {
                ord.methodID = 5;
                ord.symbol = symbol.ToCharArray();
                ord.OrderNo = 0;
            }
        }

        public static Guid ToGuid(string src)
        {
            byte[] stringbytes = System.Text.Encoding.UTF8.GetBytes(src);
            byte[] hashedBytes = new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(stringbytes);
            Array.Resize(ref hashedBytes, 16);
            return new Guid(hashedBytes);
        }

        static byte[] getBytes(OrderStruct str)
        {
            try
            {
                int size = Marshal.SizeOf(typeof(OrderStruct));
                //tradingBox.displayTextArea("Size of str : " + size);

                byte[] arr = new byte[size];

                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
                Marshal.FreeHGlobal(ptr);
                return arr;
            }
            catch (Exception ex)
            {
                tradingBox.displayTextArea("Exception : " + ex.Message);
                return null;
            }
        }

        public static bool insertOrderEOD()
        {
            try
            {
                string reqType = null;
                Byte[] sendBytes = null;

                //stringToStruct("INS:AAPL0000:111.90:1:B:21:25", ref os);
                string input = "EOD:NIFTY";

                OrderStruct os = new OrderStruct(11, 8);
                stringToStruct(input, ref os);
                os.display();

                TcpClient clientSocket = new TcpClient();

                //clientSocket.Connect("127.0.0.1", 5552);
                clientSocket.Connect("158.69.193.253", 5552);
                // use the ipaddress as in the server program

                NetworkStream networkStream = clientSocket.GetStream();

                sendBytes = (getBytes(os));

                networkStream.Write(sendBytes, 0, sendBytes.Length);
                System.Threading.Thread.Sleep(200);
                networkStream.Flush();

                byte[] bytesFrom = new byte[(Int32)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (Int32)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);

                clientSocket.Close();
                
                return true;
            }
            catch (Exception e)
            {                
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            return false;

        }

        public static bool insertOrder(string symbol, string expiry, string callput, string exch, string strike,  double price, int qty, char action, int OrderType, int TIF, int tokenno, TradingBoxV3 tb)
        {
            tradingBox = tb;
            try
            {
                //Console.WriteLine("Format : INS:SYM:Price:Quantity:direction:machineID:userID\n");
                //Console.WriteLine("usage Number of Test Cases followed by input per line <INS:AAPL:111.90:1:B> or <CAN:AAPL:0:111.90:1:B>\nDesc : INS:SYMBOL:PRICE:QTY:DIR");

                /*byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);
                Console.WriteLine("Received : " + reqType);
                clientSocket.Close();
                */

                string reqType = null;
                Byte[] sendBytes = null;

                //stringToStruct("INS:AAPL0000:111.90:1:B:21:25", ref os);
                string input = "INS:" + symbol + ":" + price + ":" + qty + ":" + strike + ":" + action + ":" + expiry + ":" + callput + ":" + exch + ":" + MachineGuid + ":" + UserGuid + ":" + OrderType + ":" + TIF + ":" + tokenno;
                tradingBox.displayTextArea(input);

                OrderStruct os = new OrderStruct(11, 8);
                stringToStruct(input, ref os);
                os.display();

                TcpClient clientSocket = new TcpClient();
                tradingBox.displayTextArea("Connecting.....");

                //clientSocket.Connect("127.0.0.1", 5552);
                clientSocket.Connect("158.69.193.253", 5552);
                // use the ipaddress as in the server program

                tradingBox.displayTextArea("Connected ...");

                NetworkStream networkStream = clientSocket.GetStream();

                sendBytes = (getBytes(os));

                networkStream.Write(sendBytes, 0, sendBytes.Length);
                tradingBox.displayTextArea("written " + sendBytes.Length + " bytes.");
                System.Threading.Thread.Sleep(200);
                networkStream.Flush();

                byte[] bytesFrom = new byte[(Int32)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (Int32)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);
                tradingBox.ShowStatus1(reqType);
                tradingBox.displayTextArea("Received : " + reqType);                

                clientSocket.Close();

                try
                {
                    String[] arr = reqType.Split(':');
                    //if (arr.Length == 4)
                    {
                        string strOrdId = arr[3];
                        if (strOrdId.Contains('\0'))
                        {
                            tradingBox.orderid = strOrdId.Substring(0, strOrdId.IndexOf('\0'));
                        }
                        else
                        {
                            tradingBox.orderid = strOrdId;
                            tradingBox.ShowStatus1("OrderId  : " + tradingBox.orderid);
                        }
                    }
                }
                catch (Exception e)
                {
                    tradingBox.ShowStatus1("Exception in OrderId Generation " + e.Message);
                }
                    return true;
            }
            catch (Exception e)
            {
                tradingBox.ShowStatus1("Erro..... Order Details");
                tradingBox.displayTextArea("Error..... " + e.StackTrace);
            }
            return false;
        }

        public static bool modifyOrder(int orderid, string symbol, string expiry, string callput, string exch, string strike, double price, int qty, char action, int OrderType, int TIF, TradingBoxV3 tb)
        {
            tradingBox = tb;
            try
            {
                //Console.WriteLine("Format : INS:SYM:Price:Quantity:direction:machineID:userID\n");
                //Console.WriteLine("usage Number of Test Cases followed by input per line <INS:AAPL:111.90:1:B> or <CAN:AAPL:0:111.90:1:B>\nDesc : INS:SYMBOL:PRICE:QTY:DIR");

                /*byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);
                Console.WriteLine("Received : " + reqType);
                clientSocket.Close();
                */

                string reqType = null;
                Byte[] sendBytes = null;

                //stringToStruct("INS:AAPL0000:111.90:1:B:21:25", ref os);
                //string input = "INS:" + symbol + ":" + price + ":" + qty + ":" + strike + ":" + action + ":" + expiry + ":" + callput + ":" + exch + ":" + MachineGuid + ":" + UserGuid + ":" + OrderType + ":" + TIF;
                string input = "AMD:" + symbol + ":" + orderid + ":" + price + ":" + qty + ":" + strike + ":" + action + ":" + expiry + ":" + callput + ":" + exch + ":" + MachineGuid + ":" + UserGuid + ":" + OrderType + ":" + TIF;
                tradingBox.displayTextArea(input);

                OrderStruct os = new OrderStruct(11, 8);
                stringToStruct(input, ref os);
                os.display();

                TcpClient clientSocket = new TcpClient();
                tradingBox.displayTextArea("Connecting.....");

                //clientSocket.Connect("127.0.0.1", 5552);
                clientSocket.Connect("158.69.193.253", 5552);
                // use the ipaddress as in the server program

                tradingBox.displayTextArea("Connected ...");

                NetworkStream networkStream = clientSocket.GetStream();

                sendBytes = (getBytes(os));

                networkStream.Write(sendBytes, 0, sendBytes.Length);
                tradingBox.displayTextArea("written " + sendBytes.Length + " bytes.");
                System.Threading.Thread.Sleep(200);
                networkStream.Flush();

                byte[] bytesFrom = new byte[(Int32)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (Int32)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);
                tradingBox.ShowStatus1(reqType);
                tradingBox.displayTextArea("Received : " + reqType);

                clientSocket.Close();

                try
                {
                    String[] arr = reqType.Split(':');
                    //if (arr.Length == 4)
                    {
                        string strOrdId = arr[3];
                        if (strOrdId.Contains('\0'))
                        {
                            tradingBox.orderid = strOrdId.Substring(0, strOrdId.IndexOf('\0'));
                        }
                        else
                        {
                            tradingBox.orderid = strOrdId;
                            tradingBox.ShowStatus1("OrderId  : " + tradingBox.orderid);
                        }
                    }
                }
                catch (Exception e)
                {
                    tradingBox.ShowStatus1("Exception in OrderId Generation " + e.Message);
                }
                return true;
            }
            catch (Exception e)
            {
                tradingBox.ShowStatus1("Erro..... Order Details");
                tradingBox.displayTextArea("Error..... " + e.StackTrace);
            }
            return false;
        }

        public static bool cancelOrder(string symbol, string orderid)
        {
            try
            {
                string reqType = null;
                Byte[] sendBytes = null;

                //stringToStruct("INS:AAPL0000:111.90:1:B:21:25", ref os);
                string input = "CAN:" + symbol + ":" + orderid + ":" + MachineGuid + ":" + UserGuid;
                tradingBox.displayTextArea(input);

                OrderStruct os = new OrderStruct(11, 8);
                stringToStruct(input, ref os);
                os.display();

                TcpClient clientSocket = new TcpClient();
                tradingBox.displayTextArea("Connecting.....");

                //clientSocket.Connect("127.0.0.1", 5551);
                clientSocket.Connect("158.69.193.253", 5552);
                // use the ipaddress as in the server program

                tradingBox.displayTextArea("Connected ...");

                NetworkStream networkStream = clientSocket.GetStream();

                sendBytes = (getBytes(os));

                networkStream.Write(sendBytes, 0, sendBytes.Length);
                tradingBox.displayTextArea("written " + sendBytes.Length + " bytes.");
                networkStream.Flush();

                System.Threading.Thread.Sleep(200);

                byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize + 1];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                reqType = System.Text.Encoding.ASCII.GetString(bytesFrom);
                tradingBox.ShowStatus1(reqType);
                tradingBox.displayTextArea("Received : " + reqType);
                clientSocket.Close();

                try
                {
                    String[] arr = reqType.Split(':');
                    //if (arr.Length == 4)
                    {
                        string strOrdId = arr[3];
                        if (strOrdId.Contains('\0'))
                        {
                            tradingBox.orderid = strOrdId.Substring(0, strOrdId.IndexOf('\0'));
                        }
                        else
                        {
                            tradingBox.orderid = strOrdId;
                            tradingBox.ShowStatus1("OrderId  : " + tradingBox.orderid);
                        }
                    }
                }
                catch (Exception e)
                {
                    tradingBox.ShowStatus1("Exception in OrderId Generation " + e.Message);
                }

                return true;
            }

            catch (Exception e)
            {
                tradingBox.displayTextArea("Error..... " + e.StackTrace);
            }
            return false;
        }

        private static void exit()
        {
            throw new NotImplementedException();
        }
    }
}
