using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerWindowApplication
{
    public class SecurityMaster
    {
        public string TokenNo, UnderlyingScripNo, Instrument, Symbol, TradeSymbol, MLot, ExpiryDate, StrikePrice, OptType, FullName;
        public double prevClosePrice;

        public SecurityMaster(string TokenNo, string UnderlyingScripNo, string Instrument, string Symbol, string TradeSymbol, 
        string MLot, string ExpiryDate, string StrikePrice, string OptType, string fullname)
        {
            this.TokenNo = TokenNo;
            this.UnderlyingScripNo = UnderlyingScripNo;
            this.Instrument = Instrument;
            this.Symbol = Symbol;
            this.TradeSymbol = TradeSymbol;
            this.MLot = MLot;
            this.ExpiryDate = ExpiryDate;
            this.StrikePrice = StrikePrice;
            this.OptType = OptType;
            this.FullName = fullname;
        }
    }
}
