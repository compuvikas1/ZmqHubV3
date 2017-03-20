using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Globalization;

namespace ScannerWindowApplication
{
    public partial class TradingBoxV2 : Form
    {
        string tokenno = null;
        Boolean flag = true;
        Thread th = null;
        Boolean displayFuture = false;
        int tabsel = 0;
        double tokenClosePrice = 0.0;
        public string orderid = "0";
        char lastaction = 'S';

        public TradingBoxV2(string tokenno)
        {
            InitializeComponent();
            this.tokenno = tokenno;            
        }

        private void TradingBoxV2_Load(object sender, EventArgs e)
        {
            var symbolListEquity = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Instrument == "EQ").Select(x => x.Value.Symbol).Distinct().ToList();
            cmbStocksSymbol.Items.AddRange(symbolListEquity.ToArray());

            var symbolListFutures = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Instrument == "FUTSTK" || x.Value.Instrument == "FUTIDX").Select(x => x.Value.Symbol).Distinct().ToList();
            cmbFutSymbol.Items.AddRange(symbolListFutures.ToArray());            

            var symbolListOptions = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Instrument == "OPTSTK" || x.Value.Instrument == "OPTIDX").Select(x => x.Value.Symbol).Distinct().ToList();
            cmbOptionsSymbol.Items.AddRange(symbolListOptions.ToArray());

            if (ScannerDashboard.dictSecurityMaster.ContainsKey(tokenno))
            {
                SecurityMaster secMaster = ScannerDashboard.dictSecurityMaster[tokenno];

                //Instrument == "EQ" -> Stocks Tab
                //Instrument == "FUTSTK" -> Stocks Tab
                //Instrument == "OPTSTK" -> Stocks Tab
                //Instrument == "FUTIDX" -> Stocks Tab
                //Instrument == "OPTIDX" -> Stocks Tab
                if (secMaster.Instrument == "EQ")
                {
                    tabsel = 0;

                    string Symbol = secMaster.Symbol;

                    cmbStocksSymbol.SelectedItem = Symbol;

                    cmbStocksTif.SelectedIndex = 0;
                    cmbStocksVenue.SelectedIndex = 0;
                    cmbStocksType.SelectedIndex = 0;

                    displayFuture = true;
                    tabControl1.SelectedIndex = 0;
                }
                else if (secMaster.Instrument == "FUTSTK" || secMaster.Instrument == "FUTIDX")
                {
                    tabsel = 1;

                    string Symbol = secMaster.Symbol;
                    loadExpiry(Symbol, secMaster.Instrument);

                    cmbFutSymbol.SelectedItem = Symbol;
                    cmbFutExpiry.SelectedItem = secMaster.ExpiryDate;
                    cmbFuturesTif.SelectedIndex = 0;
                    cmbFuturesVenue.SelectedIndex = 0;
                    cmbFuturesType.SelectedIndex = 0;
                    displayFuture = true;
                    tabControl1.SelectedIndex = 1;
                }
                else if (secMaster.Instrument == "OPTSTK" || secMaster.Instrument == "OPTIDX")
                {
                    tabsel = 2;

                    string Symbol = secMaster.Symbol;
                    loadExpiry(Symbol, secMaster.Instrument);

                    cmbOptionsSymbol.SelectedItem = Symbol;
                    cmbOptionsExpiry.SelectedItem = secMaster.ExpiryDate;
                    cmbOptionsCallPut.SelectedItem = secMaster.OptType;

                    loadStrike(Symbol, secMaster.ExpiryDate, secMaster.OptType);
                    cmbOptionsStrike.SelectedItem = secMaster.StrikePrice;
                    cmbOptionsTif.SelectedIndex = 0;
                    cmbOptionsDest.SelectedIndex = 0;
                    cmbOptionsType.SelectedIndex = 0;

                    displayFuture = true;
                    tabControl1.SelectedIndex = 2;
                }
            }
            th = new Thread(new ThreadStart(ThreadTrading));
            Console.WriteLine("Threads started :");
            th.Start();
        }

        public void ThreadTrading()
        {
            while (flag && ScannerBox.openedMainForm)
            {
                try
                {
                    if (displayFuture)
                    {
                        if(FillSubscriber.dictFillDetails.ContainsKey(orderid))
                        {
                            string strFilldetails = FillSubscriber.dictFillDetails[orderid];
                            //FillID: Price: Qty: FilledQty
                            string []filldetailsarr = strFilldetails.Split(':');
                            ShowStatus1(strFilldetails);
                        }

                        if (Subscriber.dictFeedDetails.ContainsKey(tokenno))
                        {
                            Feed feed = Subscriber.dictFeedDetails[tokenno];
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                if (tabsel == 0)
                                {
                                    double closePrice = tokenClosePrice; // need to fetch the close price of previous day for this tokenno
                                    txtStocksLTP.Text = feed.ltp;
                                    txtStocksLTQ.Text = feed.ltq;
                                    txtStocksBidSize.Text = feed.bidsize;
                                    txtStocksBidPrice.Text = feed.bidprice;
                                    txtStocksAskPrice.Text = feed.askprice;
                                    txtStocksAskSize.Text = feed.asksize;
                                    txtStocksSpread.Text = Convert.ToString(Math.Round((Convert.ToDouble(feed.askprice) - Convert.ToDouble(feed.bidprice)), 2));
                                    txtStocksClose.Text = Convert.ToString(closePrice);

                                    double ltp = Convert.ToDouble(feed.ltp);

                                    double change = closePrice - Convert.ToDouble(feed.ltp);
                                    txtStocksChange.Text = Convert.ToString(change);
                                    
                                    if (closePrice > 0)
                                    {
                                        double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                        txtStocksPercentChange.Text = Convert.ToString(percChange);
                                    }
                                    else
                                    {
                                        txtStocksPercentChange.Text = Convert.ToString(closePrice);
                                    }
                                    dgvStocksPrices.Rows.Clear();

                                    if (Subscriber.dictFeedLevels.ContainsKey(tokenno))
                                    {
                                        string levels = Subscriber.dictFeedLevels[tokenno];
                                        string[] leveldata = levels.Split('|');
                                        string[] lev = new string[4];

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[0]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[2]) / 100.0);
                                        lev[2] = leveldata[1];
                                        lev[3] = leveldata[3];
                                        dgvStocksPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[4]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[6]) / 100.0);
                                        lev[2] = leveldata[5];
                                        lev[3] = leveldata[7];
                                        dgvStocksPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[8]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[10]) / 100.0);
                                        lev[2] = leveldata[9];
                                        lev[3] = leveldata[11];
                                        dgvStocksPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[12]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[14]) / 100.0);
                                        lev[2] = leveldata[13];
                                        lev[3] = leveldata[15];
                                        dgvStocksPrices.Rows.Add(lev);
                                    }

                                    //dgvStocksTrades.Rows.Clear();

                                    if (Subscriber.dictTrades.ContainsKey(tokenno))
                                    {
                                        Dictionary<string, TradeDetails> tradedict = Subscriber.dictTrades[tokenno];
                                        List<TradeDetails> tradeDetailsList = tradedict.Values.OrderByDescending(t => t.LastTradeTime).Take(8).ToList();
                                        var bindingList = new BindingList<TradeDetails>(tradeDetailsList);
                                        var source = new BindingSource(bindingList, null);
                                        dgvStocksTrades.DataSource = source;
                                    }
                                }
                                else if (tabsel == 1)
                                {
                                    double closePrice = tokenClosePrice; // need to fetch the close price of previous day for this tokenno
                                    txtFutLTP.Text = feed.ltp;
                                    txtFutLTQ.Text = feed.ltq;
                                    txtFutBidSz.Text = feed.bidsize;
                                    txtFutBidPz.Text = feed.bidprice;
                                    txtFutAskPz.Text = feed.askprice;
                                    txtFutAskSz.Text = feed.asksize;
                                    txtFutSpread.Text = Convert.ToString(Math.Round((Convert.ToDouble(feed.askprice) - Convert.ToDouble(feed.bidprice)), 2));
                                    txtFutClosePrice.Text = Convert.ToString(closePrice);

                                    double ltp = Convert.ToDouble(feed.ltp);

                                    double change = closePrice - Convert.ToDouble(feed.ltp);
                                    txtFutChange.Text = Convert.ToString(change);

                                    if (closePrice > 0)
                                    {
                                        double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                        txtFutPercentChange.Text = Convert.ToString(percChange);
                                    }
                                    else
                                    {
                                        txtFutPercentChange.Text = Convert.ToString(closePrice);
                                    }

                                    dgvFuturesPrices.Rows.Clear();
                                    if (Subscriber.dictFeedLevels.ContainsKey(tokenno))
                                    {
                                        string levels = Subscriber.dictFeedLevels[tokenno];
                                        string[] leveldata = levels.Split('|');

                                        {
                                            string[] lev = new string[4];

                                            lev[0] = Convert.ToString(Convert.ToInt32(leveldata[0]) / 100.0);
                                            lev[1] = Convert.ToString(Convert.ToInt32(leveldata[2]) / 100.0);
                                            lev[2] = leveldata[1];
                                            lev[3] = leveldata[3];
                                            dgvFuturesPrices.Rows.Add(lev);

                                            lev[0] = Convert.ToString(Convert.ToInt32(leveldata[4]) / 100.0);
                                            lev[1] = Convert.ToString(Convert.ToInt32(leveldata[6]) / 100.0);
                                            lev[2] = leveldata[5];
                                            lev[3] = leveldata[7];
                                            dgvFuturesPrices.Rows.Add(lev);

                                            lev[0] = Convert.ToString(Convert.ToInt32(leveldata[8]) / 100.0);
                                            lev[1] = Convert.ToString(Convert.ToInt32(leveldata[10]) / 100.0);
                                            lev[2] = leveldata[9];
                                            lev[3] = leveldata[11];
                                            dgvFuturesPrices.Rows.Add(lev);

                                            lev[0] = Convert.ToString(Convert.ToInt32(leveldata[12]) / 100.0);
                                            lev[1] = Convert.ToString(Convert.ToInt32(leveldata[14]) / 100.0);
                                            lev[2] = leveldata[13];
                                            lev[3] = leveldata[15];
                                            dgvFuturesPrices.Rows.Add(lev);

                                        }

                                        //List<ScannerDashboard.TimeandSales> SortedList1 = ScannerDashboard.listTrades.OrderByDescending(o => o.ftime).ToList();

                                        //foreach(ScannerDashboard.TimeandSales timeSales in SortedList1)
                                        //{
                                        //    string[] lev = new string[3];

                                        //    lev[0] = timeSales.ftime;
                                        //    lev[1] = Convert.ToString(timeSales.price);
                                        //    lev[2] = timeSales.quantity;

                                        //    dgvFuturesTrade.Rows.Add(lev);
                                        //}

                                        //dgvFuturesTrade.Rows.Clear();

                                        if (Subscriber.dictTrades.ContainsKey(tokenno))
                                        {
                                            Dictionary<string, TradeDetails> tradedict = Subscriber.dictTrades[tokenno];
                                            List<TradeDetails> tradeDetailsList = tradedict.Values.OrderByDescending(t => t.LastTradeTime).Take(8).ToList();
                                            var bindingList = new BindingList<TradeDetails>(tradeDetailsList);
                                            var source = new BindingSource(bindingList, null);
                                            dgvFuturesTrade.DataSource = source;
                                        }
                                    }

                                }
                                else if(tabsel == 2)
                                {
                                    double closePrice = tokenClosePrice; // need to fetch the close price of previous day for this tokenno
                                    txtOptionsLTP.Text = feed.ltp;
                                    txtOptionsLTQ.Text = feed.ltq;
                                    txtOptionsBidSize.Text = feed.bidsize;
                                    txtOptionsBidPrice.Text = feed.bidprice;
                                    txtOptionsAskPrice.Text = feed.askprice;
                                    txtOptionsAskSize.Text = feed.asksize;
                                    txtOptionsSpread.Text = Convert.ToString(Math.Round((Convert.ToDouble(feed.askprice) - Convert.ToDouble(feed.bidprice)), 2));
                                    txtOptionsClosePrice.Text = Convert.ToString(closePrice);

                                    double ltp = Convert.ToDouble(feed.ltp);
                                    double change = closePrice - Convert.ToDouble(ltp);
                                    txtOptionsChange.Text = Convert.ToString(change);

                                    if (closePrice > 0)
                                    {
                                        double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                        txtOptionsPercentChange.Text = Convert.ToString(percChange);
                                    }
                                    else
                                    {
                                        txtOptionsPercentChange.Text = Convert.ToString(closePrice);
                                    }

                                    dgvOptionsPrices.Rows.Clear();

                                    if (Subscriber.dictFeedLevels.ContainsKey(tokenno))
                                    {
                                        string levels = Subscriber.dictFeedLevels[tokenno];
                                        string[] leveldata = levels.Split('|');

                                        string[] lev = new string[4];

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[0]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[2]) / 100.0);
                                        lev[2] = leveldata[1];
                                        lev[3] = leveldata[3];
                                        dgvOptionsPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[4]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[6]) / 100.0);
                                        lev[2] = leveldata[5];
                                        lev[3] = leveldata[7];
                                        dgvOptionsPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[8]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[10]) / 100.0);
                                        lev[2] = leveldata[9];
                                        lev[3] = leveldata[11];
                                        dgvOptionsPrices.Rows.Add(lev);

                                        lev[0] = Convert.ToString(Convert.ToInt32(leveldata[12]) / 100.0);
                                        lev[1] = Convert.ToString(Convert.ToInt32(leveldata[14]) / 100.0);
                                        lev[2] = leveldata[13];
                                        lev[3] = leveldata[15];
                                        dgvOptionsPrices.Rows.Add(lev);                                                                                

                                        if (Subscriber.dictTrades.ContainsKey(tokenno))
                                        {
                                            Dictionary<string, TradeDetails> tradedict = Subscriber.dictTrades[tokenno];
                                            List<TradeDetails> tradeDetailsList = tradedict.Values.OrderByDescending(t => t.LastTradeTime).Take(8).ToList();
                                            var bindingList = new BindingList<TradeDetails>(tradeDetailsList);
                                            var source = new BindingSource(bindingList, null);
                                            dgvOptionsTrade.DataSource = source;
                                        }

                                    }
                                }
                            });                            
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Thread Stopped");
            if (flag == true && ScannerBox.openedMainForm == false)
            {
                this.Close();
            }
        }


        
        string oldMsg = "";
        public void ShowStatus1(string status)
        {
            oldMsg = this.StatusLabelMessage1.Text;
            if (status.Contains('\0'))
            {
                this.StatusLabelMessage1.Text = status.Substring(0, status.IndexOf('\0'));
            }
            else
            {
                this.StatusLabelMessage1.Text = status;
                this.StatusLabelMessage1.Invalidate(); // To force status bar redraw
                this.statusStrip1.Refresh();
            }
        }

        public void RestoreStatus1()
        {
            this.StatusLabelMessage1.Text = oldMsg;
        }

        //public void ShowStatus4(string status)
        //{
        //    oldMsg = this.StatusLabelMessage4.Text;
        //    this.StatusLabelMessage4.Text = status;
        //    this.StatusLabelMessage4.Invalidate(); // To force status bar redraw
        //}

        //public void RestoreStatus4()
        //{
        //    this.StatusLabelMessage4.Text = oldMsg;
        //}

        public void displayTextArea(string data)
        {
            richTextBox2.AppendText(data + "\n");
        }
        
        private void cmbFuturesSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFutSymbol.SelectedItem != null)
            {
                var symbol = cmbFutSymbol.SelectedItem.ToString();
                string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && (x.Value.Instrument == "FUTSTK" || x.Value.Instrument == "FUTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();
                loadExpiry(symbol, instrument);
            }
        }

        public void loadExpiry(String symbol, String instrument)
        {
            if (instrument == "FUTSTK" || instrument == "FUTIDX")
            {
                cmbFutExpiry.Items.Clear();
                var expiryList = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && x.Value.Instrument == instrument).Select(x => x.Value.ExpiryDate).Distinct().ToList();
                cmbFutExpiry.Items.AddRange(expiryList.ToArray());
            }
            else if (instrument == "OPTSTK" || instrument == "OPTIDX")
            {
                cmbOptionsExpiry.Items.Clear();
                var expiryList = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && x.Value.Instrument == instrument).Select(x => x.Value.ExpiryDate).Distinct().ToList();
                cmbOptionsExpiry.Items.AddRange(expiryList.ToArray());
            }         
        }

        public void loadStrike(String symbol, String Expiry, String opttype)
        {
            cmbOptionsStrike.Items.Clear();
            var strikeList = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && x.Value.ExpiryDate == Expiry && x.Value.OptType == opttype).Select(x => x.Value.StrikePrice).Distinct().ToList();
            cmbOptionsStrike.Items.AddRange(strikeList.ToArray());            
        }

        private void btnFuturesBuy_Click(object sender, EventArgs e)
        {
            string symbol = cmbFutSymbol.SelectedItem.ToString();            
            DateTime oExpiryDate = DateTime.ParseExact(cmbFutExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            //string expiry = "20170223";
                        
            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtFuturesQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbFuturesType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbFuturesType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;                        
                    }

                    try
                    {
                        limitPrice = Convert.ToDouble(txtFuturesLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus1("Enter Valid value in Price" + ex.Message);
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {
                        char action = 'B';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, "NA", "NFO", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'B';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, "NA", "NFO", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        
        private void btnFuturesSell_Click(object sender, EventArgs e)
        {
            string symbol = cmbFutSymbol.SelectedItem.ToString();
            DateTime oExpiryDate = DateTime.ParseExact(cmbFutExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            //string expiry = "20170223";

            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtFuturesQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbFuturesType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbFuturesType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;
                    }

                    try
                    {
                        limitPrice = Convert.ToDouble(txtFuturesLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus1("Enter Valid value in Price" + ex.Message);
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {
                        char action = 'S';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, "NA", "NFO", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'S';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, "NA", "NFO", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();

            DateTime oExpiryDate = DateTime.ParseExact(cmbOptionsExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            string strike = cmbOptionsStrike.SelectedItem.ToString();
            
            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtOptionsQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbOptionsType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                        try
                        {
                            limitPrice = Convert.ToDouble(txtOptionsPrice.Text);
                        }
                        catch (Exception ex)
                        {
                            ShowStatus1("Enter Valid value in Price" + ex.Message);
                        }
                    }
                    else if (cmbOptionsType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;
                        try
                        {
                            limitPrice = Convert.ToDouble(txtOptionsPrice.Text);
                        }
                        catch (Exception ex)
                        {
                            ShowStatus1("Enter Valid value in Price" + ex.Message);
                        }
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {
                        char action = 'B';
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'B';
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();

            DateTime oExpiryDate = DateTime.ParseExact(cmbOptionsExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            string strike = cmbOptionsStrike.SelectedItem.ToString();

            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtOptionsQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbOptionsType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbOptionsType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;
                        try
                        {
                            limitPrice = Convert.ToDouble(txtOptionsPrice.Text);
                        }
                        catch (Exception ex)
                        {
                            ShowStatus1("Enter Valid value in Price" + ex.Message);
                        }
                    }

                    if (ordType == 1)
                    {
                        char action = 'S';
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'S';
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void cmbFuturesExpiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Symbol = cmbFutSymbol.SelectedItem.ToString();
            if (cmbFutExpiry.SelectedItem != null)
            {
                string Expiry = cmbFutExpiry.SelectedItem.ToString();
                string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && (x.Value.Instrument == "FUTSTK" || x.Value.Instrument == "FUTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();

                this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.ExpiryDate == Expiry && x.Value.Instrument == instrument).Select(x => x.Key).Distinct().ToList().FirstOrDefault();
                if (ScannerDashboard.dictSecurityCloseMaster.ContainsKey(tokenno))
                    tokenClosePrice = ScannerDashboard.dictSecurityCloseMaster[tokenno];
                else
                    tokenClosePrice = 0;

                tabsel = 1;
            }
        }

        private void cmbOptionsCallPut_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            if (cmbOptionsExpiry.SelectedItem != null)
            {
                string expiry = cmbOptionsExpiry.SelectedItem.ToString();
                if (cmbOptionsCallPut.SelectedItem != null)
                {
                    string callput = cmbOptionsCallPut.SelectedItem.ToString();
                    loadStrike(symbol, expiry, callput);
                }
            }
        }

        private void cmbOptionsStrike_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Symbol = cmbOptionsSymbol.SelectedItem.ToString();
            if (cmbOptionsExpiry.SelectedItem != null)
            {
                string Expiry = cmbOptionsExpiry.SelectedItem.ToString();
                if (cmbOptionsCallPut.SelectedItem != null)
                {
                    string callput = cmbOptionsCallPut.SelectedItem.ToString();

                    if (cmbOptionsStrike.Items.Count > 0)
                    {
                        if (cmbOptionsStrike.SelectedItem != null)
                        {
                            string strike = cmbOptionsStrike.SelectedItem.ToString();
                            string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && (x.Value.Instrument == "OPTSTK" || x.Value.Instrument == "OPTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();

                            this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.ExpiryDate == Expiry && x.Value.Instrument == instrument && x.Value.OptType == callput && x.Value.StrikePrice == strike).Select(x => x.Key).Distinct().ToList().First();
                            if (ScannerDashboard.dictSecurityCloseMaster.ContainsKey(tokenno))
                                tokenClosePrice = ScannerDashboard.dictSecurityCloseMaster[tokenno];
                            else
                                tokenClosePrice = 0;
                            tabsel = 2;
                        }
                    }
                }
            }
        }

        private void cmbOptionsSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOptionsSymbol.SelectedItem != null)
            {
                string symbol = cmbOptionsSymbol.SelectedItem.ToString();
                string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && (x.Value.Instrument == "OPTSTK" || x.Value.Instrument == "OPTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();
                loadExpiry(symbol, instrument);
            }
        }

        private void cmbOptionsExpiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            if (cmbOptionsExpiry.SelectedItem != null)
            {
                string expiry = cmbOptionsExpiry.SelectedItem.ToString();
                if (cmbOptionsCallPut.SelectedItem != null)
                {
                    string callput = cmbOptionsCallPut.SelectedItem.ToString();
                    loadStrike(symbol, expiry, callput);
                }
            }
        }

        private void splitContainer1_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRowView)) || e.Data.GetDataPresent(typeof(string)))
            {
                // Determine which category the item was draged to                
                {
                    displayFuture = false;

                    // Get references to the category and campaign
                    string dropTokenno = "";
                    string symbol = "";
                    string expiry = "";
                    string strike = "";
                    string callput = "";
                    string instrument = "";

                    if (e.Data.GetDataPresent(typeof(DataRowView)))
                    {
                        DataRowView dataRow = (DataRowView)e.Data.GetData(typeof(DataRowView));
                        dropTokenno = (string)dataRow[0];
                        SecurityMaster secMaster = ScannerDashboard.dictSecurityMaster[dropTokenno];
                        symbol = secMaster.Symbol;
                        expiry = secMaster.ExpiryDate;
                        strike = secMaster.StrikePrice;
                        callput = secMaster.OptType;
                        instrument = secMaster.Instrument;
                    }
                    else if(e.Data.GetDataPresent(typeof(string)))
                    {
                        dropTokenno = (string)e.Data.GetData(typeof(string));
                        SecurityMaster secMaster = ScannerDashboard.dictSecurityMaster[dropTokenno];
                        symbol = secMaster.Symbol;
                        expiry = secMaster.ExpiryDate;
                        strike = secMaster.StrikePrice;
                        callput = secMaster.OptType;
                        instrument = secMaster.Instrument;
                    }

                    

                    if (instrument == "EQ")
                    {                        
                        //clear the old data for options tab
                        //cmbOptionsStrike.Items.Clear();
                        //cmbOptionsCallPut.Items.Clear();
                        //cmbOptionsExpiry.Items.Clear();
                        //cmbOptionsSymbol.Items.Clear();

                        cmbOptionsStrike.SelectedIndex = -1;
                        cmbOptionsCallPut.SelectedIndex = -1;
                        cmbOptionsExpiry.SelectedIndex = -1;
                        cmbOptionsSymbol.SelectedIndex = -1;

                        txtOptionsChange.Text = "";
                        txtOptionsPercentChange.Text = "";
                        txtOptionsAskSize.Text = "";
                        txtOptionsAskPrice.Text = "";
                        txtOptionsBidPrice.Text = "";
                        txtOptionsBidSize.Text = "";
                        txtOptionsClosePrice.Text = "";
                        txtOptionsLTP.Text = "";
                        txtOptionsLTQ.Text = "";
                        txtOptionsPrice.Text = "";
                        txtOptionsQty.Text = "";
                        txtOptionsSpread.Text = "";
                        strike = "0";
                        callput = "";

                        //clear the old data for futures tab
                        //cmbFutExpiry.Items.Clear();
                        //cmbFutSymbol.Items.Clear();
                        cmbFutExpiry.SelectedIndex = -1;
                        cmbFutSymbol.SelectedIndex = -1;
                        txtFutAskPz.Text = "";
                        txtFutAskSz.Text = "";
                        txtFutBidPz.Text = "";
                        txtFutBidSz.Text = "";
                        txtFutChange.Text = "";
                        txtFutClosePrice.Text = "";
                        txtFutLTQ.Text = "";
                        txtFutLTP.Text = "";
                        txtFutPercentChange.Text = "";
                        txtFutSpread.Text = "";
                        txtFuturesLimit.Text = "";
                        txtFuturesQty.Text = "";

                        dgvFuturesPrices.Rows.Clear();
                        dgvFuturesTrade.Rows.Clear();

                        dgvOptionsPrices.Rows.Clear();
                        dgvOptionsTrade.Rows.Clear();

                        cmbStocksSymbol.SelectedItem = symbol;

                        tabsel = 0;
                        tabControl1.SelectedIndex = 0;
                    }
                    if (instrument == "FUTSTK" || instrument == "FUTIDX")
                    {                        
                        cmbOptionsStrike.Items.Clear();
                        //cmbOptionsCallPut.Items.Clear();
                        cmbOptionsExpiry.Items.Clear();
                        //cmbOptionsSymbol.Items.Clear();

                        cmbOptionsStrike.SelectedIndex = -1;
                        cmbOptionsCallPut.SelectedIndex = -1;
                        cmbOptionsExpiry.SelectedIndex = -1;
                        cmbOptionsSymbol.SelectedIndex = -1;

                        txtOptionsChange.Text = "";
                        txtOptionsPercentChange.Text = "";
                        txtOptionsAskSize.Text = "";
                        txtOptionsAskPrice.Text = "";
                        txtOptionsBidPrice.Text = "";
                        txtOptionsBidSize.Text = "";
                        txtOptionsClosePrice.Text = "";
                        txtOptionsLTP.Text = "";
                        txtOptionsLTQ.Text = "";
                        txtOptionsPrice.Text = "";
                        txtOptionsQty.Text = "";
                        txtOptionsSpread.Text = "";
                        strike = "0";
                        callput = "";

                        // clear the stocks data
                        cmbStocksSymbol.SelectedIndex = -1;
                        txtStocksAskPrice.Text = "";
                        txtStocksAskSize.Text = "";
                        txtStocksBidPrice.Text = "";
                        txtStocksBidSize.Text = "";
                        txtStocksChange.Text = "";
                        txtStocksClose.Text = "";
                        txtStocksLimit.Text = "";
                        txtStocksLTP.Text = "";
                        txtStocksLTQ.Text = "";
                        txtStocksPercentChange.Text = "";
                        txtStocksQty.Text = "";
                        txtStocksSpread.Text = "";

                        cmbFutSymbol.SelectedItem = symbol;
                        loadExpiry(symbol, instrument);
                        cmbFutExpiry.SelectedItem = expiry;

                        dgvStocksPrices.Rows.Clear();
                        dgvStocksTrades.Rows.Clear();

                        dgvOptionsPrices.Rows.Clear();
                        dgvOptionsTrade.Rows.Clear();
                        
                        tabsel = 1;
                        tabControl1.SelectedIndex = 1;
                    }
                    else if (instrument == "OPTSTK" || instrument == "OPTIDX")
                    {
                        //cmbFutExpiry.Items.Clear();
                        //cmbFutSymbol.Items.Clear();
                        cmbFutExpiry.SelectedIndex = -1;
                        cmbFutSymbol.SelectedIndex = -1;
                        txtFutAskPz.Text = "";
                        txtFutAskSz.Text = "";
                        txtFutBidPz.Text = "";
                        txtFutBidSz.Text = "";
                        txtFutChange.Text = "";
                        txtFutClosePrice.Text = "";
                        txtFutLTQ.Text = "";
                        txtFutLTP.Text = "";
                        txtFutPercentChange.Text = "";
                        txtFutSpread.Text = "";
                        txtFuturesLimit.Text = "";
                        txtFuturesQty.Text = "";

                        cmbStocksSymbol.SelectedIndex = -1;
                        txtStocksAskPrice.Text = "";
                        txtStocksAskSize.Text = "";
                        txtStocksBidPrice.Text = "";
                        txtStocksBidSize.Text = "";
                        txtStocksChange.Text = "";
                        txtStocksClose.Text = "";
                        txtStocksLimit.Text = "";
                        txtStocksLTP.Text = "";
                        txtStocksLTQ.Text = "";
                        txtStocksPercentChange.Text = "";
                        txtStocksQty.Text = "";
                        txtStocksSpread.Text = "";

                        dgvStocksPrices.Rows.Clear();
                        dgvStocksTrades.Rows.Clear();

                        dgvFuturesPrices.Rows.Clear();
                        dgvFuturesTrade.Rows.Clear();

                        cmbOptionsSymbol.SelectedItem = symbol;
                        loadExpiry(symbol, instrument);
                        cmbOptionsExpiry.SelectedItem = expiry;
                        cmbOptionsCallPut.SelectedItem = callput;

                        loadStrike(symbol, expiry, callput);

                        if (cmbOptionsStrike.Items.Count > 0)
                        {
                            cmbOptionsStrike.SelectedItem = strike;                            
                            //this.feedkey = symbol + "," + expiry + "," + strike + "," + callput + "," + exch;
                            tabsel = 2;
                            tabControl1.SelectedIndex = 2;
                        }                        
                    }

                    displayFuture = true;

                }
            }
        }
             

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            tabControl1.DoDragDrop(tokenno, DragDropEffects.Copy);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmbStocksSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStocksSymbol.SelectedItem != null)
            {
                string Symbol = cmbStocksSymbol.SelectedItem.ToString();
                string instrument = "EQ";
                this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument).Select(x => x.Key).Distinct().ToList().FirstOrDefault();
                if (ScannerDashboard.dictSecurityCloseMaster.ContainsKey(tokenno))
                    tokenClosePrice = ScannerDashboard.dictSecurityCloseMaster[tokenno];
                else
                    tokenClosePrice = 0;

                tabsel = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String value = "";
            FillSubscriber.dictFillDetails.TryGetValue(textBox1.Text, out value);
            MessageBox.Show(value);
        }

        private void TradingBoxV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            flag = false;
        }

        private void btnStocksBuy_Click(object sender, EventArgs e)
        {
            string symbol = cmbStocksSymbol.SelectedItem.ToString();
            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtStocksQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbStocksType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbStocksType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;                        
                    }

                    try
                    {
                        limitPrice = Convert.ToDouble(txtStocksLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus1("Enter Valid value in Price" + ex.Message);
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {
                        char action = 'B';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty +" Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {                            
                            OrderClient.insertOrder(symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'B';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void btnStocksSell_Click(object sender, EventArgs e)
        {
            string symbol = cmbStocksSymbol.SelectedItem.ToString();
            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtStocksQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbStocksType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbStocksType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;                        
                    }

                    try
                    {
                        limitPrice = Convert.ToDouble(txtStocksLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus1("Enter Valid value in Price" + ex.Message);
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {
                        char action = 'S';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {
                        char action = 'S';
                        lastaction = action;
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.insertOrder(symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, action, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {                
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void btnStocksCancel_Click(object sender, EventArgs e)
        {
            string symbol = cmbStocksSymbol.SelectedItem.ToString();
            OrderClient.cancelOrder(symbol, orderid);
        }

        private void btnFuturesCancel_Click(object sender, EventArgs e)
        {
            string symbol = cmbFutSymbol.SelectedItem.ToString();
            OrderClient.cancelOrder(symbol, orderid);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string symbol = cmbStocksSymbol.SelectedItem.ToString();
            int Qty = 0;
            double limitPrice = 0;
            try
            {
                Qty = Convert.ToInt32(txtStocksQty.Text);
                if (Qty > 0)
                {
                    //1 for MARKET and  2 for LIMIT
                    int ordType = 0;

                    if (cmbStocksType.SelectedItem.ToString().Equals("MARKET"))
                    {
                        ordType = 1;
                    }
                    else if (cmbStocksType.SelectedItem.ToString().Equals("LIMIT"))
                    {
                        ordType = 2;
                    }

                    try
                    {
                        limitPrice = Convert.ToDouble(txtStocksLimit.Text);
                    }
                    catch (Exception ex)
                    {
                        ShowStatus1("Enter Valid value in Price" + ex.Message);
                    }

                    if (ordType == 1 && limitPrice > 0)
                    {                        
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {                            
                            OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, lastaction, ordType, 0, this);
                        }
                    }
                    else if (ordType == 2 && limitPrice > 0)
                    {                        
                        DialogResult dialogResult = MessageBox.Show("Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, "20170223", "NA", "STK", "0", limitPrice, Qty, lastaction, ordType, 0, this);
                        }
                    }
                    else
                    {
                        ShowStatus1("Enter Valid value in Price");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Enter Valid number in Qty" + ex.Message);
            }
        }

        private void btnFutureModify_Click(object sender, EventArgs e)
        {
            string symbol = cmbFutSymbol.SelectedItem.ToString();
            string futExpiry = cmbFutExpiry.SelectedItem.ToString();
            try
            {
                DateTime dtfutExpiry = DateTime.ParseExact(futExpiry, "yyyy-MM-dd", null);
                futExpiry = dtfutExpiry.ToString("yyyyMMdd");
                int Qty = 0;
                double limitPrice = 0;
                try
                {
                    Qty = Convert.ToInt32(txtFuturesQty.Text);
                    if (Qty > 0)
                    {
                        //1 for MARKET and  2 for LIMIT
                        int ordType = 0;

                        if (cmbFuturesType.SelectedItem.ToString().Equals("MARKET"))
                        {
                            ordType = 1;
                        }
                        else if (cmbFuturesType.SelectedItem.ToString().Equals("LIMIT"))
                        {
                            ordType = 2;
                        }

                        try
                        {
                            limitPrice = Convert.ToDouble(txtFuturesLimit.Text);
                        }
                        catch (Exception ex)
                        {
                            ShowStatus1("Enter Valid value in Price" + ex.Message);
                        }

                        if (ordType == 1 && limitPrice > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Modify Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                //OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, "20170223", "NA", "FUT", "0", limitPrice, Qty, lastaction, ordType, 0, this);
                                OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, futExpiry, "NA", "FUT", "0", limitPrice, Qty, lastaction, ordType, 0, this);
                            }
                        }
                        else if (ordType == 2 && limitPrice > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Modify Buy Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice, "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, futExpiry, "NA", "FUT", "0", limitPrice, Qty, lastaction, ordType, 0, this);
                            }
                        }
                        else
                        {
                            ShowStatus1("Enter Valid value in Price");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowStatus1("Enter Valid number in Qty" + ex.Message);
                }
            }
            catch(Exception ex)
            {
                ShowStatus1("Select Expiry for the Symbol : " + ex.Message);
            }
        }

        private void btnOptionCancel_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            OrderClient.cancelOrder(symbol, orderid);
        }

        private void btnOptionModify_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();

            string optExpiry = cmbOptionsExpiry.SelectedItem.ToString();
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            string strike = cmbOptionsStrike.SelectedItem.ToString();
            try
            {
                DateTime dtOptExpiry = DateTime.ParseExact(optExpiry, "yyyy-MM-dd", null);
                optExpiry = dtOptExpiry.ToString("yyyyMMdd");
                int Qty = 0;
                double limitPrice = 0;
                try
                {
                    Qty = Convert.ToInt32(txtOptionsQty.Text);
                    if (Qty > 0)
                    {
                        //1 for MARKET and  2 for LIMIT
                        int ordType = 0;

                        if (cmbOptionsType.SelectedItem.ToString().Equals("MARKET"))
                        {
                            ordType = 1;
                        }
                        else if (cmbOptionsType.SelectedItem.ToString().Equals("LIMIT"))
                        {
                            ordType = 2;
                        }

                        try
                        {
                            limitPrice = Convert.ToDouble(txtOptionsPrice.Text);
                        }
                        catch (Exception ex)
                        {
                            ShowStatus1("Enter Valid value in Price" + ex.Message);
                        }

                        if (ordType == 1 && limitPrice > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Modify Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice + " expiry = " + optExpiry + " callput : " + callput + " strike: " + strike, "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, optExpiry, callput, "OPT", strike, limitPrice, Qty, lastaction, ordType, 0, this);
                            }
                        }
                        else if (ordType == 2 && limitPrice > 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Modify Order Symbol = " + symbol + " Qty = " + Qty + " Price = " + limitPrice + " expiry = " + optExpiry + " callput : " +callput + " strike: " + strike, "Some Title", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                OrderClient.modifyOrder(Convert.ToInt32(orderid), symbol, optExpiry, callput, "OPT", strike, limitPrice, Qty, lastaction, ordType, 0, this);
                            }
                        }
                        else
                        {
                            ShowStatus1("Enter Valid value in Price");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowStatus1("Enter Valid number in Qty" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                ShowStatus1("Select Expiry for the Symbol : " + ex.Message);
            }
        }
    }
}
