using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ScannerWindowApplication
{    

    public partial class SecurityMasterGenerator : Form
    {
        public static long TimeOffset = 315532800000L;

        public SecurityMasterGenerator()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //
                // The user selected a folder and pressed the OK button.
                // We print the number of files found.
                //
                string filePath = folderBrowserDialog1.SelectedPath;
                txtFolderPath.Text = filePath;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            string contractsFilePath = folderPath + "\\contract.gz";
            string securitiesFilePath = folderPath + "\\security.gz";

            //if both the files does not exist then
            //we will not delete the old SecurityMaster.csv file and generate new file

            if (File.Exists(contractsFilePath) == false)
            {
                lblMessage.AppendText("contract.gz file not found at location " + folderPath + Environment.NewLine);
            }
            else if (File.Exists(securitiesFilePath) == false)
            {
                lblMessage.AppendText("security.gz file not found at location " + folderPath + Environment.NewLine);
            }
            else
            {
                // unzip the contract.gz file
                // creates contract.txt file 

                try
                {
                    LinkedList<string> formattedLines = new LinkedList<string>();
                    Dictionary<Int32, string> dictunderlyingtoken = new Dictionary<Int32, string>();

                    String header = "ScripNo,underlyingScripNo,Instrument,symbol,tradeSymbol,MLot,expiryDate,StrikePrice,OptType,fullname";
                    formattedLines.AddLast(header);

                    //processing the equities Security.gz file

                    FileInfo fiSecurity = new FileInfo(securitiesFilePath);
                    FileStream inFileSecurity = fiSecurity.OpenRead();

                    string securityZipFile = fiSecurity.FullName;
                    string securityNewFile = securityZipFile.Remove(securityZipFile.Length - fiSecurity.Extension.Length) + ".txt";

                    if (File.Exists(securityNewFile))
                    {
                        File.Delete(securityNewFile);
                    }

                    Stream outFileSecurity = File.Create(securityNewFile);

                    using (GZipStream Dc = new GZipStream(inFileSecurity, CompressionMode.Decompress))
                    {
                        Dc.CopyTo(outFileSecurity);
                        Dc.Flush();
                        Dc.Close();

                        outFileSecurity.Close();
                    }

                    lblMessage.AppendText("Unzip Security.gz Done" + Environment.NewLine);

                    // parse the contracts.txt and add the contents in SecurityMaster.csv

                    string []lines = File.ReadAllLines(securityNewFile);
                    foreach (string line in lines)
                    {
                        if (line.Split('|').Length < 10)
                        {
                            continue;
                        }

                        string[] arr = line.Split('|');

                        if (arr[2] == "EQ")
                        {
                            string ScripNo = arr[0];
                            string underlyingScripNo = "";
                            string Instrument = arr[2];
                            string symbol = arr[1];
                            string expiryDate = "";
                            string StrikePrice = "";
                            string OptType = "";
                            string MLot = "";
                            string tradeSymbol = "";
                            string fullname = arr[21];

                            String newLine = ScripNo + "," + underlyingScripNo + "," + Instrument + "," + symbol + "," + tradeSymbol + "," + MLot + "," + expiryDate + "," + StrikePrice + "," + OptType + "," + fullname;
                            dictunderlyingtoken[Convert.ToInt32(ScripNo)] = fullname;
                            formattedLines.AddLast(newLine);
                        }
                    }

                    lblMessage.AppendText("Successfully processed Security.txt file" + Environment.NewLine);

                    FileInfo fi = new FileInfo(contractsFilePath);
                    FileStream inFile = fi.OpenRead();

                    string contractZipFile = fi.FullName;
                    string contractNewFile = contractZipFile.Remove(contractZipFile.Length - fi.Extension.Length) + ".txt";

                    if (File.Exists(contractNewFile))
                    {
                        File.Delete(contractNewFile);
                    }
                    FileStream outFile = File.Create(contractNewFile);

                    using (GZipStream Dc = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        Dc.CopyTo(outFile);
                        Dc.Flush();
                        Dc.Close();

                        outFile.Close();
                    }

                    lblMessage.AppendText("Unzip Contracts.gz Done" + Environment.NewLine);

                    // parse the contracts.txt and add the contents in SecurityMaster.csv

                    string SecurityMasterFilePath = folderPath + "\\SecurityMaster.csv";

                    lines = File.ReadAllLines(contractNewFile);
                    foreach (string line in lines)
                    {
                        if (line.Split('|').Length < 10)
                        {
                            continue;
                        }

                        string[] arr = line.Split('|');

                        string ScripNo = arr[0];
                        string underlyingScripNo = arr[1];
                        string Instrument = arr[2];
                        string symbol = arr[3];
                        long val = Convert.ToUInt32(arr[6]);

                        DateTime dt = new DateTime((val * 1000L) + TimeOffset);

                        double ticks = double.Parse(arr[6]) * 1000;
                        TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                        DateTime startdate = new DateTime(1980, 1, 1) + time;

                        string expiryDate = startdate.ToString("yyyy-MM-dd");
                        if (arr[7] != "-1")
                            arr[7] = (Convert.ToInt32(arr[7]) / 100).ToString();
                        string StrikePrice = arr[7];
                        string OptType = arr[8];
                        string MLot = arr[30];
                        string tradeSymbol = arr[53];
                        string fullname = tradeSymbol;
                        if (dictunderlyingtoken.ContainsKey(Convert.ToInt32(underlyingScripNo)))
                            fullname = dictunderlyingtoken[Convert.ToInt32(underlyingScripNo)];
                        else
                            lblMessage.AppendText("Token not Found : " + underlyingScripNo);

                        String newLine = ScripNo + "," + underlyingScripNo + "," + Instrument + "," + symbol + "," + tradeSymbol + "," + MLot + "," + expiryDate + "," + StrikePrice + "," + OptType + "," + fullname;
                        formattedLines.AddLast(newLine);
                    }

                    lblMessage.AppendText("Successfully processed contract.txt file" + Environment.NewLine);
                                        
                    string[] the_array = formattedLines.Select(i => i.ToString()).ToArray();

                    File.WriteAllLines(SecurityMasterFilePath, the_array);

                    //Generated the SecurityMaster.csv file
                }
                catch(Exception ex)
                {
                    lblMessage.AppendText(ex.StackTrace + Environment.NewLine);
                }
            }
        }

        public void DownloadFile(string filename)
        {
            string ResponseDescription = "";
            try
            {
                string folderPath = txtFolderPath.Text;

                string FileNameToDownload = "COMMON/ntneat/" + filename;
                string PureFileName = new FileInfo(FileNameToDownload).Name;
                string DownloadedFilePath = folderPath + "/" + PureFileName;
                string FtpUrl = "ftp://ftp.connect2nse.com";
                string downloadUrl = String.Format("{0}/{1}", FtpUrl, FileNameToDownload);
                FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(downloadUrl);
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");
                req.UseBinary = true;
                req.Proxy = null;

                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[2048];
                FileStream fs = new FileStream(DownloadedFilePath, FileMode.Create);
                int ReadCount = stream.Read(buffer, 0, buffer.Length);
                while (ReadCount > 0)
                {
                    fs.Write(buffer, 0, ReadCount);
                    ReadCount = stream.Read(buffer, 0, buffer.Length);
                }
                ResponseDescription = response.StatusDescription;
                fs.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                lblMessage.AppendText(ex.Message + Environment.NewLine);
            }
            lblMessage.AppendText(ResponseDescription);
            
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            lblMessage.AppendText("Downloading contract: "+Environment.NewLine);
            DownloadFile("contract.gz");
            lblMessage.AppendText("Downloading security : " + Environment.NewLine);
            DownloadFile("security.gz");
        }
    }
}
