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
    class TradeDetails
    {
        public string LastTradeTime { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }

        public TradeDetails(string LTT, double prc, string qty)
        {
            LastTradeTime = LTT;
            Price = prc;
            Size = qty;
        }
    }

    class Subscriber
    {
        //public static long feedtimeadd = 3155130000000;
        static DateTime epoch = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static Dictionary<string, Feed> dictFeedDetails = new Dictionary<string, Feed>();
        public static Dictionary<string, string> dictFeedLevels = new Dictionary<string, string>();
        public static Dictionary<string, Dictionary<string, TradeDetails>> dictTrades = new Dictionary<string, Dictionary<string, TradeDetails>>();

        String publisherIP = "";
        String publisherPort = "";
        ScannerDashboard parentSD;
        public Subscriber(ScannerDashboard sd) { parentSD = sd; }
        public void ThreadB()
        {
            try
            {
                //string topic = args[0] == "All" ? "" : args[0];
                //string topic = "";
                //Console.WriteLine("Subscriber started for Topic : {0}", _topic);

                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    publisherIP = ConfigurationManager.AppSettings["publisherIP"];
                    publisherPort = ConfigurationManager.AppSettings["publisherPort"];
                    subSocket.Connect("tcp://"+ publisherIP+":" + publisherPort);
                    //subSocket.SubscribeToAnyTopic();
                    if (parentSD.dictFilters.Count == 0)
                    {
                        subSocket.SubscribeToAnyTopic();
                    }
                    else
                    {
                        foreach(var filters in parentSD.dictFilters)
                        {                            
                            //Console.WriteLine("Subscribing Socket for Symbol : " + filters.Key);
                            subSocket.Subscribe(filters.Key);
                        }
                    }

                    //Console.WriteLine("Subscriber socket connecting...");
                    while (true)
                    {
                        try
                        {
                            if (ScannerBox.openedMainForm == false)
                                break;
                            string messageReceived;

                            System.Threading.Thread.Sleep(5);

                            if (subSocket.TryReceiveFrameString(out messageReceived))
                            {
                                //string messageReceived = subSocket.ReceiveFrameString();
                                string[] origMessageSplit = messageReceived.Split(';');
                                string[] arr = origMessageSplit[0].Split('|');

                                //string tokenno, ltp, ltq, bidprice, bidsize, askprice, asksize, feedtime
                                //token,ltp,ltq,b_amt,b_qty,a_amt,a_qty,lft,ltt,totalVol,open,high,low
                                string tokenno = arr[0];
                                double ltp = Convert.ToDouble(arr[1]) / 100.0;
                                string ltq = arr[2];
                                double bidprice = Convert.ToDouble(arr[3]) / 100.0;
                                string bidsize = arr[4];
                                double askprice = Convert.ToDouble(arr[5]) / 100.0;
                                string asksize = arr[6];                                
                                
                                DateTime dt1 = epoch.AddSeconds(Convert.ToInt64(arr[7]) / 1000);
                                string feedtime = dt1.ToString("HH:mm:ss");

                                string ltt = arr[8];
                                string tradetime = "";
                                if (ltt.Equals("0") == false)
                                {                                    
                                    DateTime dt2 = epoch.AddMilliseconds(Convert.ToInt64(ltt));
                                    tradetime = dt2.ToString("HH:mm:ss.ff");
                                }

                                string volume = arr[9];
                                string open = "0";
                                string high = "0";
                                string low = "0";

                                if (arr.Length >= 12)
                                {
                                    open = arr[10];
                                    high = arr[11];
                                    low = arr[12];
                                }

                                //DateTime dt = DateTime.Now;
                                //string feedtime = dt.ToString("HH:mm:ss");                        

                                Feed feed = new Feed(tokenno, feedtime, Convert.ToString(ltp), ltq, bidsize, Convert.ToString(bidprice), Convert.ToString(askprice), asksize, tradetime, volume, open, high,low);

                                ScannerBox.qfeed.Enqueue(feed);
                                string key = feed.tokenno;                                
                                dictFeedDetails[key] = feed;

                                if(dictTrades.ContainsKey(tokenno) == false)
                                {
                                    dictTrades[tokenno] = new Dictionary<string, TradeDetails>();
                                }

                                Dictionary<string, TradeDetails> dictTradeDetails = dictTrades[tokenno];
                                if(dictTradeDetails.ContainsKey(ltt) == false)
                                {
                                    dictTradeDetails[ltt] = new TradeDetails(tradetime, ltp, ltq);
                                    
                                }

                                if (origMessageSplit.Length > 1)
                                {
                                    dictFeedLevels[key] = origMessageSplit[1];
                                }                                
                            }                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
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
