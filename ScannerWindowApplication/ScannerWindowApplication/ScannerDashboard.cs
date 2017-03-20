using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace ScannerWindowApplication
{
    public partial class ScannerDashboard : Form
    {
        //string connectionString = @"Data Source=HSTBHSVAMDS\SQLEXPRESS;Initial Catalog=LPIntraDay;Persist Security Info=True;User ID=sa;Password=sa@123";
        //string testString = @"Data Source=.\SQLServerr2;Initial Catalog=LPIntraDay;Persist Security Info=True;User ID=sa;Password=sa123";    
        //string connectionString = "";
        
        public string topics { get; set; }
        //public Dictionary<string, SymbolFilter> dictFilters = new Dictionary<string, SymbolFilter>();
        public Dictionary<string, List<SymbolFilter>> dictFilters = new Dictionary<string, List<SymbolFilter>>();
        public static Dictionary<string, SecurityMaster> dictSecurityMaster = new Dictionary<string, SecurityMaster>();
        public static Dictionary<string, double> dictSecurityCloseMaster = new Dictionary<string, double>();

        ScannerBox scannerBox = null;
        OrderBlotter orderBlotter = null;
        SecurityMasterGenerator securityMasterGenerator = null;

        public void loadSecurityMaster()
        {
            string []lines = File.ReadAllLines(@"C:\s2trading\zmqhubresource\contractdetails\SecurityMaster.csv");
            foreach(string line in lines)
            {
                if (line.Contains("ScripNo")) continue;
                //ScripNo,Exch,series,symbol,opttype,StrikePrice,ExpiryDate,MLot               
                //ScripNo	underlyingScripNo	Instrument	symbol	tradeSymbol	MLot	expiryDate	StrikePrice	OptType

                string[] arr = line.Split(',');
                string TokenNo = arr[0];
                string UnderlyingScripNo = arr[1];
                string Instrument = arr[2];
                string Symbol = arr[3];
                string TradeSymbol = arr[4];
                string MLot = arr[5];
                string ExpiryDate = arr[6];
                string StrikePrice = arr[7];
                string OptType = arr[8];

                SecurityMaster data = new SecurityMaster(TokenNo, UnderlyingScripNo, Instrument, Symbol, TradeSymbol, MLot, ExpiryDate, StrikePrice, OptType);
                dictSecurityMaster[TokenNo] = data;
            }
        }

        //public void loadSecurityClosePrices()
        //{
        //    string[] lines = File.ReadAllLines(@"C:\s2trading\zmqhubresource\contractdetails\cm20FEB2017bhav.csv");

        //    List<SecurityMaster> listEQSecMaster = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Instrument == "EQ").Select(x => x.Value).ToList();

        //    foreach (string line in lines)
        //    {
        //        string[] arr = line.Split(',');

        //        if (arr[0].Equals("SYMBOL"))
        //            continue;

        //        string Symbol = arr[0];
        //        string instrument = arr[1];
        //        string last = arr[6];
                
        //        if (instrument.Equals("EQ"))
        //        {
        //            try
        //            {
        //                //string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument).Select(x => x.Key).First();
        //                string tokenno = listEQSecMaster.Where(x => x.Symbol == Symbol && x.Instrument == instrument).Select(x => x.TokenNo).First();
        //                if (dictSecurityMaster.ContainsKey(tokenno))
        //                {                            
        //                    dictSecurityCloseMasterStocks[tokenno] = Convert.ToDouble(last);
        //                }
        //            }
        //            catch(Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //    }

        //    return;

        //    lines = File.ReadAllLines(@"C:\s2trading\zmqhubresource\contractdetails\fo20FEB2017bhav.csv");
        //    int ctr = 0;
        //    foreach (string line in lines)
        //    {
        //        string[] arr = line.Split(',');
                
        //        if (arr[0].Equals("INSTRUMENT"))
        //            continue;

        //        string Symbol = arr[1];
        //        string instrument = arr[0];
        //        string expiry = arr[2];
        //        string strike = arr[3];
        //        string opttype = arr[4];

        //        string last = arr[8];
        //        if (instrument == "FUTIDX" || instrument == "FUTSTK")
        //        {
        //            try
        //            {
        //                DateTime dtExpiry = DateTime.ParseExact(expiry, "dd-MMM-yyyy", null);
        //                string expiryDate = dtExpiry.ToString("yyyy-MM-dd");

        //                string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument && x.Value.ExpiryDate == expiryDate).Select(x => x.Key).Distinct().ToList().FirstOrDefault();

        //                //if (dictSecurityMaster.ContainsKey(tokenno))
        //                if(tokenno != null)
        //                {
        //                    ctr++;
        //                    dictSecurityCloseMasterFutures[tokenno] = Convert.ToDouble(last);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //        if (instrument == "OPTSTK" || instrument == "OPTIDX")                
        //        {
        //            try
        //            {
        //                DateTime dtExpiry = DateTime.ParseExact(expiry, "dd-MMM-yyyy", null);
        //                string expiryDate = dtExpiry.ToString("yyyy-MM-dd");

        //                string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument && x.Value.ExpiryDate == expiryDate && x.Value.StrikePrice == strike && x.Value.OptType == opttype).Select(x => x.Key).Distinct().ToList().FirstOrDefault();

        //                //if (dictSecurityMaster.ContainsKey(tokenno))
        //                if (tokenno != null)
        //                {
        //                    ctr++;
        //                    dictSecurityCloseMasterOptions[tokenno] = Convert.ToDouble(last);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }                
        //    }
            
        //    Console.WriteLine("ctr = " + ctr);
        //}

        //static DateTime epoch = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //public class TimeandSales
        //{
        //    public string ftime;            
        //    public double price;
        //    public string quantity;

        //    public TimeandSales(string time1, string price, string qty)
        //    {
        //        long lft = Convert.ToInt64(time1);
        //        DateTime dt = epoch.AddSeconds((lft / 1000));
        //        this.ftime = dt.ToString("HH:mm:ss");
        //        double pr = Convert.ToInt32(price) / 100.0;
        //        this.price = pr;
        //        this.quantity = qty;
        //    }
        //}

        //public static Dictionary<string, TimeandSales> dictTimeandSales = new Dictionary<string, TimeandSales>();
        //public static List<TimeandSales> listTrades = new List<TimeandSales>();
            
        //public void loadTimeAndSales()
        //{
        //    string []lines = File.ReadAllLines(@"C:\App\nseServer\TimeandSales.csv");
        //    foreach(string line in lines)
        //    {
        //        string []arr = line.Split(',');
        //        if (arr[1].Equals("56711"))
        //        {
        //            dictTimeandSales[arr[2]] = new TimeandSales(arr[2], arr[3], arr[4]);
        //            listTrades.Add(new TimeandSales(arr[2], arr[3], arr[4]));
        //        }
        //    }

            

        //}

        public ScannerDashboard()
        {
            //removeing the dependency from the db, so we can run Scanner from any machine
            //connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            //connectionString = @"Data Source=VPS104139\SQLEXPRESS;Initial Catalog=LPIntraDay;Persist Security Info=True;User ID=sa;Password=sa123";           
            //MySqlHelper.Initialize(connectionString);

            loadSecurityMaster();
            //loadSecurityClosePrices();
            //loadTimeAndSales();
            InitializeComponent();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scannerBox == null || scannerBox.Text == "")
            {
                scannerBox = new ScannerBox(this);                
                scannerBox.MdiParent = this;
                scannerBox.Dock = DockStyle.Fill;
                scannerBox.Show();
            }
            else if (CheckOpened(scannerBox.Text))
            {
                scannerBox.WindowState = FormWindowState.Normal;
                scannerBox.Dock = DockStyle.Fill;
                scannerBox.Show();
                scannerBox.Focus();
            }
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void orderBlotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (orderBlotter == null || orderBlotter.Text == "")
            {
                orderBlotter = new OrderBlotter(this);
                orderBlotter.MdiParent = this;
                orderBlotter.Dock = DockStyle.Fill;
                orderBlotter.Show();
            }
            else if (CheckOpened(orderBlotter.Text))
            {
                orderBlotter.WindowState = FormWindowState.Normal;
                orderBlotter.Dock = DockStyle.Fill;
                orderBlotter.Show();
                orderBlotter.Focus();
            }
        }

        private void ScannerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void securityMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (securityMasterGenerator == null || securityMasterGenerator.Text == "")
            {
                securityMasterGenerator = new SecurityMasterGenerator();
                securityMasterGenerator.MdiParent = this;
                securityMasterGenerator.Dock = DockStyle.Fill;
                securityMasterGenerator.Show();
            }
            else if (CheckOpened(securityMasterGenerator.Text))
            {
                securityMasterGenerator.WindowState = FormWindowState.Normal;
                securityMasterGenerator.Dock = DockStyle.Fill;
                securityMasterGenerator.Show();
                securityMasterGenerator.Focus();
            }
        }

        private void closePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosePriceForm closePriceForm = new ClosePriceForm();
            closePriceForm.Show();
        }
    }
}
