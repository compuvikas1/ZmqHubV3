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
                        if (Subscriber.dictFeedDetails.ContainsKey(tokenno))
                        {
                            Feed feed = Subscriber.dictFeedDetails[tokenno];
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                if (tabsel == 0)
                                {
                                    double closePrice = Convert.ToDouble(feed.askprice); // need to fetch the close price of previous day for this tokenno
                                    txtStocksLTP.Text = feed.ltp;
                                    txtStocksLTQ.Text = feed.ltq;
                                    txtStocksBidSize.Text = feed.bidsize;
                                    txtStocksBidPrice.Text = feed.bidprice;
                                    txtStocksAskPrice.Text = feed.askprice;
                                    txtStocksAskSize.Text = feed.asksize;

                                    double ltp = Convert.ToDouble(feed.ltp);

                                    double change = closePrice - Convert.ToDouble(feed.ltp);
                                    txtStocksChange.Text = Convert.ToString(change);
                                    double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                    txtStocksPercentChange.Text = Convert.ToString(percChange);

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
                                    double closePrice = Convert.ToDouble(feed.askprice); // need to fetch the close price of previous day for this tokenno
                                    txtFutLTP.Text = feed.ltp;
                                    txtFutLTQ.Text = feed.ltq;
                                    txtFutBidSz.Text = feed.bidsize;
                                    txtFutBidPz.Text = feed.bidprice;
                                    txtFutAskPz.Text = feed.askprice;
                                    txtFutAskSz.Text = feed.asksize;

                                    double ltp = Convert.ToDouble(feed.ltp);

                                    double change = closePrice - Convert.ToDouble(feed.ltp);
                                    txtFutChange.Text = Convert.ToString(change);
                                    double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                    txtFutPercentChange.Text = Convert.ToString(percChange);

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
                                    double closePrice = Convert.ToDouble(feed.askprice); // need to fetch the close price of previous day for this tokenno
                                    txtFutLTP.Text = feed.ltp;
                                    txtFutLTQ.Text = feed.ltq;
                                    txtOptionsBidSize.Text = feed.bidsize;
                                    txtOptionsBidPrice.Text = feed.bidprice;
                                    txtOptionsAskPrice.Text = feed.askprice;
                                    txtOptionsAskSize.Text = feed.asksize;

                                    double ltp = Convert.ToDouble(feed.ltp);
                                    double change = closePrice - Convert.ToDouble(ltp);
                                    txtOptionsChange.Text = Convert.ToString(change);
                                    
                                    double percChange = Math.Round(((ltp - closePrice) / closePrice) * 100, 2);
                                    txtOptionsPercentChange.Text = Convert.ToString(percChange);

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

        private void btnStocksBuy_Click(object sender, EventArgs e)
        {
           
        }

        public void displayTextArea(string data)
        {
            richTextBox2.AppendText(data + "\n");
        }


        private void cmbFuturesSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            var symbol = cmbFutSymbol.SelectedItem.ToString();
            string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && (x.Value.Instrument == "FUTSTK" || x.Value.Instrument == "FUTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();
            loadExpiry(symbol, instrument);
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
            
            //DateTime oExpiryDate = DateTime.ParseExact(cmbFuturesExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            //string expiry = oExpiryDate.ToString("yyyyMMdd");
            string expiry = "20170223";
            
            double price = Convert.ToDouble(txtFuturesLimit.Text);

            int qty = Convert.ToInt32(txtFuturesQty.Text);
            char action = 'B';
            
            OrderClient.insertOrder(symbol,expiry, "NA","NFO", "0", price, qty, action, this);
        }

        
        private void btnFuturesSell_Click(object sender, EventArgs e)
        {
            string symbol = cmbFutSymbol.SelectedItem.ToString();

            //DateTime oExpiryDate = DateTime.ParseExact(cmbFuturesExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            //string expiry = oExpiryDate.ToString("yyyyMMdd");
            string expiry = "20170223";

            double price = Convert.ToDouble(txtFuturesLimit.Text);
            int qty = Convert.ToInt32(txtFuturesQty.Text);
            char action = 'S';

            OrderClient.insertOrder(symbol, expiry, "NA", "NFO", "0", price, qty, action, this);
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();

            DateTime oExpiryDate = DateTime.ParseExact(cmbOptionsExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            string strike = cmbOptionsStrike.SelectedItem.ToString();

            double price = Convert.ToDouble(txtOptionsPrice.Text);
            int qty = Convert.ToInt32(txtOptionsQty.Text);
            char action = 'B';

            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, price, qty, action, this);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();

            DateTime oExpiryDate = DateTime.ParseExact(cmbOptionsExpiry.SelectedItem.ToString(), "yyyy-MM-dd", null);
            string expiry = oExpiryDate.ToString("yyyyMMdd");
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            string strike = cmbOptionsStrike.SelectedItem.ToString();

            double price = Convert.ToDouble(txtOptionsPrice.Text);
            int qty = Convert.ToInt32(txtOptionsQty.Text);
            char action = 'S';

            OrderClient.insertOrder(symbol, expiry, callput, "NOP", strike, price, qty, action, this);
        }

        private void cmbFuturesExpiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Symbol = cmbFutSymbol.SelectedItem.ToString();
            string Expiry = cmbFutExpiry.SelectedItem.ToString();
            string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && (x.Value.Instrument == "FUTSTK" || x.Value.Instrument == "FUTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();

            this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.ExpiryDate == Expiry && x.Value.Instrument == instrument).Select(x => x.Key).Distinct().ToList().FirstOrDefault();
            
            tabsel = 1;
        }

        private void cmbOptionsCallPut_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            string expiry = cmbOptionsExpiry.SelectedItem.ToString();            
            string callput = cmbOptionsCallPut.SelectedItem.ToString();
            
            loadStrike(symbol, expiry, callput);
        }

        private void cmbOptionsStrike_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Symbol = cmbOptionsSymbol.SelectedItem.ToString();
            string Expiry = cmbOptionsExpiry.SelectedItem.ToString();
            string callput = cmbOptionsCallPut.SelectedItem.ToString();

            if (cmbOptionsStrike.Items.Count > 0)
            {
                string strike = cmbOptionsStrike.SelectedItem.ToString();
                string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && (x.Value.Instrument == "OPTSTK" || x.Value.Instrument == "OPTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();

                this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.ExpiryDate == Expiry && x.Value.Instrument == instrument && x.Value.OptType == callput && x.Value.StrikePrice == strike).Select(x => x.Key).Distinct().ToList().First();
                tabsel = 2;
            }
        }

        private void cmbOptionsSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            string instrument = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == symbol && (x.Value.Instrument == "OPTSTK" || x.Value.Instrument == "OPTIDX")).Select(x => x.Value.Instrument).Distinct().ToList().First();
            loadExpiry(symbol, instrument);
        }

        private void cmbOptionsExpiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbol = cmbOptionsSymbol.SelectedItem.ToString();
            string expiry = cmbOptionsExpiry.SelectedItem.ToString();
            if (cmbOptionsCallPut.SelectedItem != null)
            {
                string callput = cmbOptionsCallPut.SelectedItem.ToString();
                loadStrike(symbol, expiry, callput);
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
                    // Get references to the category and campaign
                    string symbol = "";
                    string expiry = "";
                    string strike = "";
                    string callput = "";
                    string exch = "";

                    if (e.Data.GetDataPresent(typeof(DataRowView)))
                    {
                        DataRowView dataRow = (DataRowView)e.Data.GetData(typeof(DataRowView));
                        symbol = (string)dataRow[1];
                        expiry = (string)dataRow[2];
                        strike = (string)dataRow[3];
                        callput = (string)dataRow[4];
                        exch = (string)dataRow[5];
                    }
                    else if(e.Data.GetDataPresent(typeof(string)))
                    {
                        string rowString = (string)e.Data.GetData(typeof(string));
                        string[] dataRow = rowString.Split(',');
                        
                        symbol = (string)dataRow[0];
                        expiry = (string)dataRow[1];
                        strike = (string)dataRow[2];
                        callput = (string)dataRow[3];
                        exch = (string)dataRow[4];
                    }

                    if (exch == "NFO")
                    {
                        cmbFutSymbol.SelectedItem = symbol;
                        loadExpiry(symbol, "NFO");
                        cmbFutExpiry.SelectedItem = expiry;

                        cmbOptionsStrike.Items.Clear();
                        cmbOptionsCallPut.Items.Clear();
                        cmbOptionsExpiry.Items.Clear();
                        cmbOptionsSymbol.Items.Clear();

                        cmbOptionsStrike.Text = "";
                        cmbOptionsCallPut.Text = "";
                        cmbOptionsExpiry.Text = "";
                        cmbOptionsSymbol.Text = "";

                        txtOptionsChange.Text = "";
                        txtOptionsPercentChange.Text = "";
                        txtOptionsAskSize.Text = "";
                        txtOptionsAskPrice.Text = "";
                        txtOptionsBidPrice.Text = "";
                        txtOptionsBidSize.Text = "";

                        strike = "0";
                        callput = "";
                        exch = "NFO";
                        //this.feedkey = symbol + "," + expiry + "," + strike + "," + callput + "," + exch;
                        
                        tabsel = 1;
                        tabControl1.SelectedIndex = 1;
                    }
                    else if (exch == "NOP")
                    {
                        cmbFutExpiry.Items.Clear();
                        cmbFutSymbol.Items.Clear();
                        cmbFutExpiry.Text = "";
                        cmbFutSymbol.Text = "";

                        txtFutChange.Text = "";
                        txtFutLTQ.Text = "";
                        txtFutAskPz.Text = "";
                        txtFutBidSz.Text = "";
                        txtFuturesLimit.Text = "";                        
                        txtFutBidPz.Text = "";
                        txtFutPercentChange.Text = "";
                        txtFutLTP.Text = "";

                        //this.feedkey = symbol + "," + expiry + "," + strike + "," + callput + "," + exch;
                        
                        cmbOptionsSymbol.SelectedItem = symbol;
                        cmbOptionsExpiry.SelectedItem = expiry;
                        cmbOptionsCallPut.SelectedItem = callput;

                        if (cmbOptionsStrike.Items.Count > 0)
                        {
                            cmbOptionsStrike.SelectedItem = strike;                            
                            //this.feedkey = symbol + "," + expiry + "," + strike + "," + callput + "," + exch;
                            tabsel = 2;
                            tabControl1.SelectedIndex = 2;
                        }                        
                    }
                }
            }
        }
             

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            tabControl1.DoDragDrop(tokenno, DragDropEffects.Copy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String value = "";
            FillSubscriber.dictFillDetails.TryGetValue(txtOptionsDisplayQty.Text, out value);
            MessageBox.Show(value);
        }

        private void cmbStocksSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Symbol = cmbStocksSymbol.SelectedItem.ToString();
            string instrument = "EQ";
            this.tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument).Select(x => x.Key).Distinct().ToList().FirstOrDefault();
            tabsel = 0;
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
    }
}
