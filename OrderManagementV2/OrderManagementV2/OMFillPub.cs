using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementV2
{
    class OMFillPub
    {
        private string zmqPubHost;
        private string zmqPubPort;

        public OMFillPub() {
            //fetch Ipaddress and portno from App.config
            zmqPubHost = ConfigurationManager.AppSettings.Get("OMFillPubHost");
            zmqPubPort = ConfigurationManager.AppSettings.Get("OMFillPubPort");
            if (zmqPubHost == null)
            {
                zmqPubHost = "localhost";
                zmqPubPort = "5572";
            }
        }
        
        //publish the orderinfo to fillpublisher
        public int zmqUpdate(OrderFillStruct of, string machineID, string userID)
        {
            string zmqConnString = ">tcp://" + zmqPubHost + ":" + zmqPubPort;
            using (var requestSocket = new RequestSocket(zmqConnString))
            {
                of.display();
                string data = machineID+":"+ userID+":"+of.OrderNo+":"+of.FillID+":"+of.Price+":"+of.Quantity+":"+of.FilledQuantity;
                Console.WriteLine("Sending : {0}, {1} , {2}",zmqPubHost, zmqPubPort, data);
                requestSocket.SendFrame(data);
                var message = requestSocket.ReceiveFrameString();
                Console.WriteLine("requestSocket : Received '{0}'", message);
            }
            return 0;
        }

        //publish the orderinfo to fillpublisher
        public int zmqUpdates(string data)
        {
            string zmqConnString = ">tcp://" + zmqPubHost + ":" + zmqPubPort;
            using (var requestSocket = new RequestSocket(zmqConnString))
            {
                Console.WriteLine("Sending : {0}, {1} , {2}", zmqPubHost, zmqPubPort, data);
                requestSocket.SendFrame(data);
                var message = requestSocket.ReceiveFrameString();
                Console.WriteLine("requestSocket : Received '{0}'", message);
            }
            return 0;
        }
    }
}
