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
using System.Globalization;
using System.IO;

namespace ScannerWindowApplication
{
    public partial class ScannerBox : Form
    {
        public static Queue<Feed> qfeed = new Queue<Feed>();
        public delegate void AddListItem();
        public AddListItem myDelegate;
        private Thread myThread;
        public static Boolean openedMainForm = true;
        ScannerDashboard parentSD;

        public static DataTable dtFeed = new DataTable();        

        public ScannerBox(ScannerDashboard sd)
        {
            InitializeComponent();
            if (dtFeed.Columns.Contains("TokenNo") == false)
            {
                var colTokenNo = dtFeed.Columns.Add("TokenNo");
                var colTime = dtFeed.Columns.Add("Time");
                var colSymbol = dtFeed.Columns.Add("Symbol");
                var colExpiry = dtFeed.Columns.Add("Expiry");
                var colStrike = dtFeed.Columns.Add("Strike");
                var colCallPut = dtFeed.Columns.Add("CallPut");
                var colLTP = dtFeed.Columns.Add("LTP", typeof(double));
                var colLTQ = dtFeed.Columns.Add("LTQ", typeof(double));
                var colBidSize = dtFeed.Columns.Add("BidSize", typeof(int));
                var colBidPrice = dtFeed.Columns.Add("BidPrice", typeof(double));
                var colAskPrice = dtFeed.Columns.Add("AskPrice", typeof(double));
                var colAskSize = dtFeed.Columns.Add("AskSize");
                var colSpread = dtFeed.Columns.Add("Spread", typeof(double));

                // set primary key constain so we can search for specific rows
                dtFeed.PrimaryKey = new[] { colTokenNo};
            }

            dataGridView1.DataSource = dtFeed;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            parentSD = sd;            
            ScannerBox.openedMainForm = true;
            myDelegate = new AddListItem(AddListItemMethod);

            myThread = new Thread(new ThreadStart(ThreadFunction));
            myThread.Start();
        }

        private void applyFilterVolume()
        {

        }

        private void ThreadFunction()
        {
            MyThreadClass myThreadClassObject = new MyThreadClass(this);
            myThreadClassObject.Run();
        }

        public void AddListItemMethod()
        {
            //String myItem;
            if (ScannerBox.qfeed.Count > 0)
            {
                Feed feed = ScannerBox.qfeed.Dequeue();
                try
                {                    
                    string symbol = "";
                    string expiry = "";
                    string strike = "";
                    string callput = "";

                    if(ScannerDashboard.dictSecurityMaster.ContainsKey(feed.tokenno))
                    {
                        SecurityMaster secMaster = ScannerDashboard.dictSecurityMaster[feed.tokenno];
                        symbol = secMaster.Symbol;
                        if (secMaster.Instrument != "EQ") // its a future or option stock,then we will get expirydate
                        {
                            expiry = secMaster.ExpiryDate;
                        }
                        if (secMaster.Instrument == "OPTIDX" || secMaster.Instrument == "OPTSTK")
                        {
                            strike = secMaster.StrikePrice;
                            callput = secMaster.OptType;
                        }
                    }

                    var exisiting = dtFeed.Rows.Find(new Object[] { feed.tokenno });
                    if (exisiting != null)
                        exisiting.ItemArray = new object[] { feed.tokenno, feed.feedtime, symbol, expiry, strike, callput, feed.ltp, feed.ltq, feed.bidsize, feed.bidprice, feed.askprice, feed.asksize, Convert.ToString(Math.Round((Convert.ToDouble(feed.askprice) - Convert.ToDouble(feed.bidprice)), 2)) };
                    else
                    {
                        if (ScannerDashboard.dictSecurityMaster.ContainsKey(feed.tokenno))
                            dtFeed.Rows.Add(new Object[] { feed.tokenno, feed.feedtime, symbol, expiry, strike, callput, feed.ltp, feed.ltq, feed.bidsize, feed.bidprice, feed.askprice, feed.asksize, Convert.ToString(Math.Round((Convert.ToDouble(feed.askprice) - Convert.ToDouble(feed.bidprice)), 2)) });
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private double round(string value, int decNumber)
        {
            double val = Convert.ToDouble(value);
            val = Math.Round(val, decNumber);
            return val;
        }

        public void loadClosePrices()
        {
            string[] lines = File.ReadAllLines(@"C:\s2trading\zmqhubresource\contractdetails\ClosePriceTokenList.csv");
            foreach (string line in lines)
            {
                string[] arr = line.Split(',');
                string TokenNo = arr[0];
                double closePrice = Convert.ToDouble(arr[1]);

                ScannerDashboard.dictSecurityCloseMaster[TokenNo] = closePrice;
            }
        }

        private void ScannerBox_Load(object sender, EventArgs e)
        {
            loadClosePrices();
            cmbLtpCondition.SelectedIndex = 0;
            cmbLtqCondition.SelectedIndex = 0;
            cmbSpreadCondition.SelectedIndex = 0;

            rbtnAnd1.Checked = true;
            rbtnAnd2.Checked = true;

            Subscriber sc = new Subscriber(parentSD);
            Thread th = new Thread(new ThreadStart(sc.ThreadB));

            FillSubscriber scFill = new FillSubscriber();
            Thread thFill = new Thread(new ThreadStart(scFill.ThreadB));

            Console.WriteLine("Threads started :");

            // Start thread B
            th.Start();
            thFill.Start();
        }

        private void ScannerBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            openedMainForm = false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {
                //do you staff.
                DataGridViewRow row = dgv.CurrentRow;

                DataGridViewCell dgvCell1 = row.Cells[0];
                string tokenno = dgvCell1.Value.ToString();
                
                TradingBoxV2 box1 = new TradingBoxV2(tokenno);
                box1.Show();
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    DataRowView view = (DataRowView)
                           dataGridView1.Rows[info.RowIndex].DataBoundItem;
                    if (view != null)
                        dataGridView1.DoDragDrop(view, DragDropEffects.Copy);
                }
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            string rowFilter = "";

            string ltpFilterValue = txtLTP.Text;
            
            if (ltpFilterValue.Trim().Length > 0)
            {
                string ltpFilterCond = cmbLtpCondition.SelectedItem.ToString();
                rowFilter = string.Format("[{0}] {1} '{2}'", "LTP", ltpFilterCond, ltpFilterValue);
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }

            string ltqFilterValue = txtLTQ.Text;
            if (ltqFilterValue.Trim().Length > 0)
            {
                string ltqFilterCond = cmbLtqCondition.SelectedItem.ToString();
                string concatenateCond = "AND";
                if (rbtnOr1.Checked == true)
                    concatenateCond = "OR";

                if(rowFilter.Length>0)
                    rowFilter += string.Format(" {0} [{1}] {2} '{3}'", concatenateCond, "LTQ",  ltqFilterCond, ltqFilterValue);
                else
                    rowFilter = string.Format("[{0}] {1} '{2}'", "LTQ", ltqFilterCond, ltqFilterValue);                
            }

            string spreadFilterValue = txtSpread.Text;
            if (spreadFilterValue.Trim().Length > 0)
            {
                string spreadFilterCond = cmbSpreadCondition.SelectedItem.ToString();
                string concatenateCond = "AND";
                if (rbtnOr2.Checked == true)
                    concatenateCond = "OR";

                if (rowFilter.Length > 0)
                    rowFilter += string.Format(" {0} [{1}] {2} '{3}'", concatenateCond, "Spread", spreadFilterCond, spreadFilterValue);
                else
                    rowFilter = string.Format("[{0}] {1} '{2}'", "Spread", spreadFilterCond, spreadFilterValue);
            }

            if (rowFilter.Length > 0)
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            else
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Empty;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtLTP.Text = "";
            txtLTQ.Text = "";
            txtSpread.Text = "";
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Empty;
        }
    }

    public class MyThreadClass
    {
        ScannerBox myFormControl1;

        public MyThreadClass(ScannerBox myForm)
        {
            myFormControl1 = myForm;
        }

        public void Run()
        {
            // Execute the specified delegate on the thread that owns
            // 'myFormControl1' control's underlying window handle.

            while (ScannerBox.openedMainForm)
            {
                try
                {
                    myFormControl1.Invoke(myFormControl1.myDelegate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
