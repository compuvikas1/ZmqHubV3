using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerWindowApplication
{
    public class Feed
    {
        public string tokenno, feedtime, LastTradeTime;
        public int ltq, bidsize, asksize;
        public Int64 volume;
        public double ltp, bidprice, askprice, open, high, low;

        public Feed(string tokenno, string feedtime, string ltp, string ltq, string bidsize, string bidprice, string askprice,
            string asksize, string LastTradeTime, string volume, string open, string high, string low)
        {
            this.tokenno = tokenno;
            this.feedtime = feedtime;
            this.ltp = Convert.ToDouble(ltp);
            this.ltq = Convert.ToInt32(ltq);
            this.bidsize = Convert.ToInt32(bidsize);
            this.bidprice = Convert.ToDouble(bidprice);
            this.askprice = Convert.ToDouble(askprice);
            this.asksize = Convert.ToInt32(asksize);
            this.LastTradeTime = LastTradeTime;
            this.volume = Convert.ToInt64(volume);
            this.open = Convert.ToDouble(open);
            this.high = Convert.ToDouble(high);
            this.low = Convert.ToDouble(low);

        }

    }
}
