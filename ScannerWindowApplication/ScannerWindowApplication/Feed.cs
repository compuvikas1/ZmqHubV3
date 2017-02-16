using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerWindowApplication
{
    public class Feed
    {
        public string tokenno, feedtime, ltp, ltq, bidsize, bidprice, askprice, asksize;

        public Feed(string tokenno, string feedtime, string ltp, string ltq, string bidsize, string bidprice, string askprice, string asksize)
        {
            this.tokenno = tokenno;            
            this.feedtime = feedtime;
            this.ltp = ltp;
            this.ltq = ltq;
            this.bidsize = bidsize;
            this.bidprice = bidprice;
            this.askprice = askprice;
            this.asksize = asksize;
        }
    }
}
