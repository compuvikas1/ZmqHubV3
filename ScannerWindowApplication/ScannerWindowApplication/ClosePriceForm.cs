using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace ScannerWindowApplication
{
    public partial class ClosePriceForm : Form
    {

        public static Dictionary<string, double> dictionaryClosePrices = new Dictionary<string, double>();

        string mainDirectoryPath = @"c:\windows\s2trading\zmqhubresource\contractdetails\";

        public ClosePriceForm()
        {
            InitializeComponent();
        }
                
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt = dtCloseDate.Value;
            string dateString = dtCloseDate.Text.ToUpper();
            string dateMonth = dateString.Substring(2, 3);
            String dateYear = dateString.Substring(5, 4);
            string cmfilename = "cm" + dateString + "bhav.csv";
            string fofilename = "fo"+ dateString + "bhav.csv";

            string cmZipfilename = cmfilename + ".zip";
            string foZipfilename = fofilename + ".zip";

            //https://www.nseindia.com/content/historical/EQUITIES/2017/FEB/cm21FEB2017bhav.csv.zip
            //webClient.DownloadFile(new Uri("https://www.nseindia.com/content/historical/EQUITIES/2017/FEB/" + filename), @"c:\windows\s2trading\zmqhubresource\contractdetails\" + filename);
            // Defines the URL and destination directory for the downloaded file

            string cmURI = "https://www.nseindia.com/content/historical/EQUITIES/" + dateYear+ "/" + dateMonth+"/";
            string foURI = "https://www.nseindia.com/content/historical/DERIVATIVES/" + dateYear + "/" + dateMonth + "/";

            txtMessages.AppendText("Downloading the file for " + dateString +"\r\n");

            bool cmflag = DownloadDataZipFile(cmURI, cmZipfilename);
            bool foflag = DownloadDataZipFile(foURI, foZipfilename);

            if(cmflag == false)
            {
                MessageBox.Show("EOD process Stopped, Since file yet not available on server. Please Try after sometime");
                return;
            }
            if (foflag == false)
            {
                MessageBox.Show("EOD process Stopped, Since file yet not available on server. Please Try after sometime");
                return;
            }

            if (cmflag && foflag)
            {

                FileInfo cmFileInfo = new FileInfo(mainDirectoryPath + cmZipfilename);
                string outfilename = mainDirectoryPath + cmfilename;
                if (File.Exists(outfilename))
                    File.Delete(outfilename);
                if (cmFileInfo.Exists)
                {
                    ZipFile.ExtractToDirectory(cmFileInfo.FullName, cmFileInfo.DirectoryName);
                    processSecurityClosePricesEquity(outfilename);
                    //Generate the File containing equities file with Tokenno & closeprice
                }

                FileInfo foFileInfo = new FileInfo(mainDirectoryPath + foZipfilename);
                outfilename = mainDirectoryPath + fofilename;
                if (File.Exists(outfilename))
                    File.Delete(outfilename);
                if (foFileInfo.Exists)
                {
                    ZipFile.ExtractToDirectory(foFileInfo.FullName, foFileInfo.DirectoryName);
                    processSecurityClosePricesFutures(outfilename);
                    //Generate the File containing equities file with Tokenno & closeprice
                }

                txtMessages.AppendText("Generating the file  ClosePriceTokenList.csv" + "\r\n");
                string closePriceFilePath = mainDirectoryPath + "ClosePriceTokenList.csv";
                if (File.Exists(closePriceFilePath))
                {
                    File.Delete(closePriceFilePath);
                    //File.Create(closePriceFilePath);
                }

                StreamWriter closeFileStream = new StreamWriter(closePriceFilePath);
                foreach (KeyValuePair<string, double> entry in dictionaryClosePrices)
                {
                    closeFileStream.WriteLine(entry.Key + "," + entry.Value);
                }

                txtMessages.AppendText("EOD Process Complete" + "\r\n");

                closeFileStream.Close();
            }
            else
            {

            }
        }
        
        public bool DownloadDataZipFile(String uri, String zipfilename)
        {
            bool flagstatus = true;

            WebClient webClient = new WebClient();

            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
            webClient.Headers.Add(HttpRequestHeader.Upgrade, "1");
            webClient.Headers.Add(HttpRequestHeader.Referer, "https://www.nseindia.com/products/content/all_daily_reports.htm");
            webClient.Headers.Add(HttpRequestHeader.Host, "www.nseindia.com");
            webClient.Headers.Add(HttpRequestHeader.Connection, "1");
            webClient.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
            webClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch, br");
            webClient.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

            try
            {
                byte[] data = webClient.DownloadData(new Uri(uri + zipfilename));

                FileStream writer = null;
                using (writer = File.Create(mainDirectoryPath + zipfilename))
                {
                    try
                    {
                        writer.Write(data, 0, data.Length);
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Close();
                    }
                }

                txtMessages.AppendText("Downloaded the file " + zipfilename + "\r\n");                
            }
            catch(WebException ex)
            {
                flagstatus = false;
                txtMessages.AppendText("WebException : " + zipfilename + ex.Message +"\r\n");
            }
            catch(Exception ex)
            {
                flagstatus = false;
                txtMessages.AppendText("Other Exception : " + zipfilename + ex.Message + "\r\n");
            }
            return flagstatus;
        }


        public void processSecurityClosePricesFutures(String filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            int totalLines = lines.Length;
            int perc = totalLines / 100;
            int ctr = 0;
            foreach (string line in lines)
            {
                string[] arr = line.Split(',');

                if (arr[0].Equals("INSTRUMENT"))
                    continue;

                string Symbol = arr[1];
                string instrument = arr[0];
                string expiry = arr[2];
                string strike = arr[3];
                string opttype = arr[4];

                string last = arr[8];
                if (instrument == "FUTIDX" || instrument == "FUTSTK")
                {
                    try
                    {
                        DateTime dtExpiry = DateTime.ParseExact(expiry, "dd-MMM-yyyy", null);
                        string expiryDate = dtExpiry.ToString("yyyy-MM-dd");

                        string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument && x.Value.ExpiryDate == expiryDate).Select(x => x.Key).Distinct().ToList().FirstOrDefault();

                        //if (dictSecurityMaster.ContainsKey(tokenno))
                        if (tokenno != null)
                        {
                            ctr++;
                            dictionaryClosePrices[tokenno] = Convert.ToDouble(last);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (instrument == "OPTSTK" || instrument == "OPTIDX")
                {
                    try
                    {
                        DateTime dtExpiry = DateTime.ParseExact(expiry, "dd-MMM-yyyy", null);
                        string expiryDate = dtExpiry.ToString("yyyy-MM-dd");

                        string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument && x.Value.ExpiryDate == expiryDate && x.Value.StrikePrice == strike && x.Value.OptType == opttype).Select(x => x.Key).Distinct().ToList().FirstOrDefault();

                        //if (dictSecurityMaster.ContainsKey(tokenno))
                        if (tokenno != null)
                        {
                            ctr++;
                            dictionaryClosePrices[tokenno] = Convert.ToDouble(last);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                if (ctr % perc == 0)
                {
                    string[] newText = new string[this.txtMessages.Lines.Length];
                    Array.Copy(this.txtMessages.Lines, newText, this.txtMessages.Lines.Length - 1);
                    newText[this.txtMessages.Lines.Length - 1] = "Processed " + (ctr / perc) +"%";
                    this.txtMessages.Text = string.Join(Environment.NewLine, newText);
                    Application.DoEvents();
                }
            }
            txtMessages.AppendText("\r\nProcessing of Futures & Options Close Price is complete" +"\r\n");
        }

        public void processSecurityClosePricesEquity(String filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            List<SecurityMaster> listEQSecMaster = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Instrument == "EQ").Select(x => x.Value).ToList();

            foreach (string line in lines)
            {
                string[] arr = line.Split(',');

                if (arr[0].Equals("SYMBOL"))
                    continue;

                string Symbol = arr[0];
                string instrument = arr[1];
                string last = arr[6];

                if (instrument.Equals("EQ"))
                {
                    try
                    {
                        //string tokenno = ScannerDashboard.dictSecurityMaster.Where(x => x.Value.Symbol == Symbol && x.Value.Instrument == instrument).Select(x => x.Key).First();
                        string tokenno = listEQSecMaster.Where(x => x.Symbol == Symbol && x.Instrument == instrument).Select(x => x.TokenNo).First();
                        if (ScannerDashboard.dictSecurityMaster.ContainsKey(tokenno))
                        {
                            dictionaryClosePrices[tokenno] = Convert.ToDouble(last);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            txtMessages.AppendText("Processing of ClosePrice Equity is complete" + "\r\n");
        }

        private void ClosePriceForm_Load(object sender, EventArgs e)
        {
            DateTime result = DateTime.Today;
            dtCloseDate.Value = result;
        }
    }
}
