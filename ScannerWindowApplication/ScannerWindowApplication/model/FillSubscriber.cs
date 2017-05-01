using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ScannerWindowApplication
{
    class FillSubscriber
    {
        public static Dictionary<string, string> dictFillDetails = new Dictionary<string, string>();
        String FillIP = "";
        String FillPort = "";
        
        public FillSubscriber() { }
        public void ThreadB()
        {
            try
            {                
                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    FillIP = ConfigurationManager.AppSettings["FillIP"];
                    FillPort = ConfigurationManager.AppSettings["FillPort"];
                    subSocket.Connect("tcp://" + FillIP + ":" + FillPort);

                    subSocket.SubscribeToAnyTopic();
                    //subSocket.Subscribe(OrderClient.MachineGuid + ":" + OrderClient.UserGuid);

                    //Console.WriteLine("Subscriber socket connecting...");
                    while (true)
                    {
                        try
                        {
                            if (ScannerBox.openedMainForm == false)
                                break;
                            string messageReceived;
                            if (subSocket.TryReceiveFrameString(out messageReceived))
                            {
                                //string messageReceived = subSocket.ReceiveFrameString();
                                Console.WriteLine("messageReceived " + messageReceived);
                                string[] arr = messageReceived.Split(':');
                                //machineID + ":" + userID+":"+OrderNo+":"+ExchangeOrderId+":"+status+":"+status_msg+":"+other_msg
                                //[3d0e9dfa-5c5d-85a2-1833-e1e1cf52c13e:b5479e69-9bab-1023-068f-f8bafaafb7de:35:1470420475:0:0:0
                                //machineID + ":" + userID + ":" + OrderNo + ":" + OrderStatus + ":" + ExchangeOrderId + ":" + status + ":" + status_msg + ":" + other_msg;
                                dictFillDetails[arr[2]] = arr[3] + ":" + arr[4] + ":" + arr[5] + ":" + arr[6] + ":" +arr[7];
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("fillsubscriber : " + e.Message);
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            finally
            {
                NetMQConfig.Cleanup();
            }
        }
    }
}
