using System;
using System.Windows.Forms;

namespace ScannerWindowApplication
{
    partial class TradingBoxV3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageStock = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnStockXAll = new System.Windows.Forms.Button();
            this.btnStockXBA = new System.Windows.Forms.Button();
            this.btnStockAPos = new System.Windows.Forms.Button();
            this.btnStockBPos = new System.Windows.Forms.Button();
            this.btnStockXBB = new System.Windows.Forms.Button();
            this.btnStockXLast = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnStocksCancel = new System.Windows.Forms.Button();
            this.btnStocksSell = new System.Windows.Forms.Button();
            this.btnStocksBuy = new System.Windows.Forms.Button();
            this.cmbStocksTif = new System.Windows.Forms.ComboBox();
            this.cmbStocksType = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbStocksVenue = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStocksLimit = new System.Windows.Forms.TextBox();
            this.txtStocksQty = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtStocksSpread = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtStocksClose = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStocksLTQ = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStocksLTP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStocksAskSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStocksAskPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStocksBidPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStocksBidSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStocksPercentChange = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStocksChange = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStocksSymbol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvStocksPrices = new System.Windows.Forms.DataGridView();
            this.BidSzStk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BidPxStk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskPxStk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskSzStk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStocksTrades = new System.Windows.Forms.DataGridView();
            this.tabPageFuture = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnFuturesXAll = new System.Windows.Forms.Button();
            this.btnFuturesXBA = new System.Windows.Forms.Button();
            this.btnFuturesAPos = new System.Windows.Forms.Button();
            this.btnFuturesBPos = new System.Windows.Forms.Button();
            this.btnFuturesXBB = new System.Windows.Forms.Button();
            this.btnFuturesXLast = new System.Windows.Forms.Button();
            this.btnFutureModify = new System.Windows.Forms.Button();
            this.txtFutClosePrice = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbFutExpiry = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnFuturesCancel = new System.Windows.Forms.Button();
            this.btnFuturesSell = new System.Windows.Forms.Button();
            this.btnFuturesBuy = new System.Windows.Forms.Button();
            this.cmbFuturesTif = new System.Windows.Forms.ComboBox();
            this.cmbFuturesType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFuturesVenue = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtFuturesLimit = new System.Windows.Forms.TextBox();
            this.txtFuturesQty = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFutSpread = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtFutLTQ = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtFutLTP = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtFutAskSz = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtFutAskPz = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFutBidPz = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtFutBidSz = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtFutPercentChange = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtFutChange = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbFutSymbol = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgvFuturesPrices = new System.Windows.Forms.DataGridView();
            this.BidSzFut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BidPxFut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskPxFut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskSzFut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFuturesTrade = new System.Windows.Forms.DataGridView();
            this.tabPageOption = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.btnOptionsXAll = new System.Windows.Forms.Button();
            this.btnOptionsXBA = new System.Windows.Forms.Button();
            this.btnOptionsAPos = new System.Windows.Forms.Button();
            this.btnOptionsBPos = new System.Windows.Forms.Button();
            this.btnOptionsXBB = new System.Windows.Forms.Button();
            this.btnOptionsXLast = new System.Windows.Forms.Button();
            this.txtOptionsDeltaPosition = new System.Windows.Forms.TextBox();
            this.txtOptionsImpliedVolatility = new System.Windows.Forms.TextBox();
            this.txtOptionsGamma = new System.Windows.Forms.TextBox();
            this.txtOptionsDelta = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderVolume = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderAskSize = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderAskPrice = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderBidPrice = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderLow = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderBidSize = new System.Windows.Forms.TextBox();
            this.rbtnUnderlying = new System.Windows.Forms.RadioButton();
            this.rbtnSymbol = new System.Windows.Forms.RadioButton();
            this.btnOptionCancel = new System.Windows.Forms.Button();
            this.btnOptionModify = new System.Windows.Forms.Button();
            this.cmbOptionsType = new System.Windows.Forms.ComboBox();
            this.txtOptionsUnderOpen = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderLTP = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderHigh = new System.Windows.Forms.TextBox();
            this.btnOptionsSell = new System.Windows.Forms.Button();
            this.txtOptionsPrice = new System.Windows.Forms.TextBox();
            this.txtOptionsQty = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.btnOptionsBuy = new System.Windows.Forms.Button();
            this.cmbOptionsDest = new System.Windows.Forms.ComboBox();
            this.cmbOptionsAccount = new System.Windows.Forms.ComboBox();
            this.cmbOptionsTif = new System.Windows.Forms.ComboBox();
            this.Type = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txtOptionsAskSize = new System.Windows.Forms.TextBox();
            this.txtOptionsAskPrice = new System.Windows.Forms.TextBox();
            this.txtOptionsBidPrice = new System.Windows.Forms.TextBox();
            this.txtOptionsBidSize = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderPercentChange = new System.Windows.Forms.TextBox();
            this.txtOptionsUnderChange = new System.Windows.Forms.TextBox();
            this.cmbOptionsCallPut = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.cmbOptionsStrike = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbOptionsExpiry = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbOptionsSymbol = new System.Windows.Forms.ComboBox();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.dgvOptionsPrices = new System.Windows.Forms.DataGridView();
            this.BidSzOpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BidPxOpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskPxOpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AskSzOpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOptionsTrade = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabelMessage1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPageStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksTrades)).BeginInit();
            this.tabPageFuture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuturesPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuturesTrade)).BeginInit();
            this.tabPageOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionsPrices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionsTrade)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabPageStock);
            this.tabControl1.Controls.Add(this.tabPageFuture);
            this.tabControl1.Controls.Add(this.tabPageOption);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(853, 513);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // tabPageStock
            // 
            this.tabPageStock.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStock.Controls.Add(this.splitContainer1);
            this.tabPageStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageStock.Location = new System.Drawing.Point(4, 24);
            this.tabPageStock.Name = "tabPageStock";
            this.tabPageStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStock.Size = new System.Drawing.Size(845, 485);
            this.tabPageStock.TabIndex = 0;
            this.tabPageStock.Text = "Stocks";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.btnStockXAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnStockXBA);
            this.splitContainer1.Panel1.Controls.Add(this.btnStockAPos);
            this.splitContainer1.Panel1.Controls.Add(this.btnStockBPos);
            this.splitContainer1.Panel1.Controls.Add(this.btnStockXBB);
            this.splitContainer1.Panel1.Controls.Add(this.btnStockXLast);
            this.splitContainer1.Panel1.Controls.Add(this.btnModify);
            this.splitContainer1.Panel1.Controls.Add(this.btnStocksCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnStocksSell);
            this.splitContainer1.Panel1.Controls.Add(this.btnStocksBuy);
            this.splitContainer1.Panel1.Controls.Add(this.cmbStocksTif);
            this.splitContainer1.Panel1.Controls.Add(this.cmbStocksType);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.cmbStocksVenue);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksLimit);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksQty);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksSpread);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksClose);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksLTQ);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksLTP);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksAskSize);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksAskPrice);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksBidPrice);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksBidSize);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksPercentChange);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtStocksChange);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cmbStocksSymbol);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer1.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(839, 479);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnStockXAll
            // 
            this.btnStockXAll.Location = new System.Drawing.Point(318, 169);
            this.btnStockXAll.Name = "btnStockXAll";
            this.btnStockXAll.Size = new System.Drawing.Size(58, 23);
            this.btnStockXAll.TabIndex = 89;
            this.btnStockXAll.Text = "xALL";
            this.btnStockXAll.UseVisualStyleBackColor = true;
            // 
            // btnStockXBA
            // 
            this.btnStockXBA.Location = new System.Drawing.Point(257, 169);
            this.btnStockXBA.Name = "btnStockXBA";
            this.btnStockXBA.Size = new System.Drawing.Size(58, 23);
            this.btnStockXBA.TabIndex = 88;
            this.btnStockXBA.Text = "xBA";
            this.btnStockXBA.UseVisualStyleBackColor = true;
            // 
            // btnStockAPos
            // 
            this.btnStockAPos.Location = new System.Drawing.Point(196, 169);
            this.btnStockAPos.Name = "btnStockAPos";
            this.btnStockAPos.Size = new System.Drawing.Size(58, 23);
            this.btnStockAPos.TabIndex = 87;
            this.btnStockAPos.Text = "APos";
            this.btnStockAPos.UseVisualStyleBackColor = true;
            this.btnStockAPos.Click += new System.EventHandler(this.btnStockAPos_Click);
            // 
            // btnStockBPos
            // 
            this.btnStockBPos.Location = new System.Drawing.Point(137, 169);
            this.btnStockBPos.Name = "btnStockBPos";
            this.btnStockBPos.Size = new System.Drawing.Size(58, 23);
            this.btnStockBPos.TabIndex = 86;
            this.btnStockBPos.Text = "BPos";
            this.btnStockBPos.UseVisualStyleBackColor = true;
            this.btnStockBPos.Click += new System.EventHandler(this.btnStockBPos_Click);
            // 
            // btnStockXBB
            // 
            this.btnStockXBB.Location = new System.Drawing.Point(77, 169);
            this.btnStockXBB.Name = "btnStockXBB";
            this.btnStockXBB.Size = new System.Drawing.Size(58, 23);
            this.btnStockXBB.TabIndex = 85;
            this.btnStockXBB.Text = "xBB";
            this.btnStockXBB.UseVisualStyleBackColor = true;
            // 
            // btnStockXLast
            // 
            this.btnStockXLast.Location = new System.Drawing.Point(17, 169);
            this.btnStockXLast.Name = "btnStockXLast";
            this.btnStockXLast.Size = new System.Drawing.Size(58, 23);
            this.btnStockXLast.TabIndex = 84;
            this.btnStockXLast.Text = "xLast";
            this.btnStockXLast.UseVisualStyleBackColor = true;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(384, 135);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(62, 23);
            this.btnModify.TabIndex = 70;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnStocksCancel
            // 
            this.btnStocksCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStocksCancel.Location = new System.Drawing.Point(452, 132);
            this.btnStocksCancel.Name = "btnStocksCancel";
            this.btnStocksCancel.Size = new System.Drawing.Size(63, 23);
            this.btnStocksCancel.TabIndex = 69;
            this.btnStocksCancel.Text = "Cancel";
            this.btnStocksCancel.UseVisualStyleBackColor = true;
            this.btnStocksCancel.Click += new System.EventHandler(this.btnStocksCancel_Click);
            // 
            // btnStocksSell
            // 
            this.btnStocksSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStocksSell.Location = new System.Drawing.Point(452, 102);
            this.btnStocksSell.Name = "btnStocksSell";
            this.btnStocksSell.Size = new System.Drawing.Size(63, 23);
            this.btnStocksSell.TabIndex = 67;
            this.btnStocksSell.Text = "Sell";
            this.btnStocksSell.UseVisualStyleBackColor = true;
            this.btnStocksSell.Click += new System.EventHandler(this.btnStocksSell_Click);
            // 
            // btnStocksBuy
            // 
            this.btnStocksBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStocksBuy.Location = new System.Drawing.Point(383, 102);
            this.btnStocksBuy.Name = "btnStocksBuy";
            this.btnStocksBuy.Size = new System.Drawing.Size(63, 23);
            this.btnStocksBuy.TabIndex = 66;
            this.btnStocksBuy.Text = "Buy";
            this.btnStocksBuy.UseVisualStyleBackColor = true;
            this.btnStocksBuy.Click += new System.EventHandler(this.btnStocksBuy_Click);
            // 
            // cmbStocksTif
            // 
            this.cmbStocksTif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocksTif.FormattingEnabled = true;
            this.cmbStocksTif.Items.AddRange(new object[] {
            "DAY",
            "IOC"});
            this.cmbStocksTif.Location = new System.Drawing.Point(306, 132);
            this.cmbStocksTif.Name = "cmbStocksTif";
            this.cmbStocksTif.Size = new System.Drawing.Size(47, 21);
            this.cmbStocksTif.TabIndex = 65;
            // 
            // cmbStocksType
            // 
            this.cmbStocksType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocksType.FormattingEnabled = true;
            this.cmbStocksType.Items.AddRange(new object[] {
            "LIMIT",
            "MARKET"});
            this.cmbStocksType.Location = new System.Drawing.Point(232, 132);
            this.cmbStocksType.Name = "cmbStocksType";
            this.cmbStocksType.Size = new System.Drawing.Size(61, 21);
            this.cmbStocksType.TabIndex = 64;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(179, 132);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 15);
            this.label16.TabIndex = 63;
            this.label16.Text = "Type";
            // 
            // cmbStocksVenue
            // 
            this.cmbStocksVenue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocksVenue.FormattingEnabled = true;
            this.cmbStocksVenue.Items.AddRange(new object[] {
            "NSE"});
            this.cmbStocksVenue.Location = new System.Drawing.Point(232, 102);
            this.cmbStocksVenue.Name = "cmbStocksVenue";
            this.cmbStocksVenue.Size = new System.Drawing.Size(121, 21);
            this.cmbStocksVenue.TabIndex = 62;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(179, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 15);
            this.label15.TabIndex = 61;
            this.label15.Text = "Venue";
            // 
            // txtStocksLimit
            // 
            this.txtStocksLimit.Location = new System.Drawing.Point(65, 132);
            this.txtStocksLimit.Name = "txtStocksLimit";
            this.txtStocksLimit.Size = new System.Drawing.Size(111, 20);
            this.txtStocksLimit.TabIndex = 60;
            // 
            // txtStocksQty
            // 
            this.txtStocksQty.Location = new System.Drawing.Point(65, 102);
            this.txtStocksQty.Name = "txtStocksQty";
            this.txtStocksQty.Size = new System.Drawing.Size(111, 20);
            this.txtStocksQty.TabIndex = 59;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(14, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 15);
            this.label14.TabIndex = 58;
            this.label14.Text = "Limit";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(14, 102);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 15);
            this.label13.TabIndex = 57;
            this.label13.Text = "Qty";
            // 
            // txtStocksSpread
            // 
            this.txtStocksSpread.Location = new System.Drawing.Point(559, 71);
            this.txtStocksSpread.Name = "txtStocksSpread";
            this.txtStocksSpread.Size = new System.Drawing.Size(111, 20);
            this.txtStocksSpread.TabIndex = 56;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(506, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 15);
            this.label12.TabIndex = 55;
            this.label12.Text = "Spread";
            // 
            // txtStocksClose
            // 
            this.txtStocksClose.Location = new System.Drawing.Point(394, 71);
            this.txtStocksClose.Name = "txtStocksClose";
            this.txtStocksClose.Size = new System.Drawing.Size(111, 20);
            this.txtStocksClose.TabIndex = 54;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(346, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 15);
            this.label11.TabIndex = 53;
            this.label11.Text = "Close";
            // 
            // txtStocksLTQ
            // 
            this.txtStocksLTQ.Location = new System.Drawing.Point(229, 71);
            this.txtStocksLTQ.Name = "txtStocksLTQ";
            this.txtStocksLTQ.Size = new System.Drawing.Size(111, 20);
            this.txtStocksLTQ.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(179, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 15);
            this.label10.TabIndex = 51;
            this.label10.Text = "LTQ";
            // 
            // txtStocksLTP
            // 
            this.txtStocksLTP.Location = new System.Drawing.Point(65, 71);
            this.txtStocksLTP.Name = "txtStocksLTP";
            this.txtStocksLTP.Size = new System.Drawing.Size(111, 20);
            this.txtStocksLTP.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(14, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 15);
            this.label9.TabIndex = 49;
            this.label9.Text = "LTP";
            // 
            // txtStocksAskSize
            // 
            this.txtStocksAskSize.Location = new System.Drawing.Point(559, 43);
            this.txtStocksAskSize.Name = "txtStocksAskSize";
            this.txtStocksAskSize.Size = new System.Drawing.Size(111, 20);
            this.txtStocksAskSize.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(511, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 47;
            this.label8.Text = "AskSz";
            // 
            // txtStocksAskPrice
            // 
            this.txtStocksAskPrice.Location = new System.Drawing.Point(394, 43);
            this.txtStocksAskPrice.Name = "txtStocksAskPrice";
            this.txtStocksAskPrice.Size = new System.Drawing.Size(111, 20);
            this.txtStocksAskPrice.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(346, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 45;
            this.label7.Text = "AskPx";
            // 
            // txtStocksBidPrice
            // 
            this.txtStocksBidPrice.Location = new System.Drawing.Point(229, 43);
            this.txtStocksBidPrice.Name = "txtStocksBidPrice";
            this.txtStocksBidPrice.Size = new System.Drawing.Size(111, 20);
            this.txtStocksBidPrice.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(179, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "BidPx";
            // 
            // txtStocksBidSize
            // 
            this.txtStocksBidSize.Location = new System.Drawing.Point(65, 43);
            this.txtStocksBidSize.Name = "txtStocksBidSize";
            this.txtStocksBidSize.Size = new System.Drawing.Size(111, 20);
            this.txtStocksBidSize.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(14, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 15);
            this.label5.TabIndex = 41;
            this.label5.Text = "BidSz";
            // 
            // txtStocksPercentChange
            // 
            this.txtStocksPercentChange.Location = new System.Drawing.Point(462, 13);
            this.txtStocksPercentChange.Name = "txtStocksPercentChange";
            this.txtStocksPercentChange.Size = new System.Drawing.Size(80, 20);
            this.txtStocksPercentChange.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(404, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 39;
            this.label4.Text = "% Chg";
            // 
            // txtStocksChange
            // 
            this.txtStocksChange.Location = new System.Drawing.Point(264, 13);
            this.txtStocksChange.Name = "txtStocksChange";
            this.txtStocksChange.Size = new System.Drawing.Size(75, 20);
            this.txtStocksChange.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(226, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "Chg";
            // 
            // cmbStocksSymbol
            // 
            this.cmbStocksSymbol.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStocksSymbol.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStocksSymbol.DropDownHeight = 110;
            this.cmbStocksSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocksSymbol.FormattingEnabled = true;
            this.cmbStocksSymbol.IntegralHeight = false;
            this.cmbStocksSymbol.ItemHeight = 13;
            this.cmbStocksSymbol.Location = new System.Drawing.Point(65, 13);
            this.cmbStocksSymbol.Name = "cmbStocksSymbol";
            this.cmbStocksSymbol.Size = new System.Drawing.Size(111, 21);
            this.cmbStocksSymbol.Sorted = true;
            this.cmbStocksSymbol.TabIndex = 36;
            this.cmbStocksSymbol.SelectedIndexChanged += new System.EventHandler(this.cmbStocksSymbol_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Sym";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvStocksPrices);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvStocksTrades);
            this.splitContainer2.Size = new System.Drawing.Size(839, 249);
            this.splitContainer2.SplitterDistance = 486;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvStocksPrices
            // 
            this.dgvStocksPrices.AllowUserToAddRows = false;
            this.dgvStocksPrices.AllowUserToDeleteRows = false;
            this.dgvStocksPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStocksPrices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BidSzStk,
            this.BidPxStk,
            this.AskPxStk,
            this.AskSzStk});
            this.dgvStocksPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStocksPrices.Location = new System.Drawing.Point(0, 0);
            this.dgvStocksPrices.Name = "dgvStocksPrices";
            this.dgvStocksPrices.ReadOnly = true;
            this.dgvStocksPrices.Size = new System.Drawing.Size(486, 249);
            this.dgvStocksPrices.TabIndex = 0;
            this.dgvStocksPrices.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStocksPrices_CellFormatting);
            // 
            // BidSzStk
            // 
            this.BidSzStk.HeaderText = "BidSz";
            this.BidSzStk.Name = "BidSzStk";
            this.BidSzStk.ReadOnly = true;
            // 
            // BidPxStk
            // 
            this.BidPxStk.HeaderText = "BidPx";
            this.BidPxStk.Name = "BidPxStk";
            this.BidPxStk.ReadOnly = true;
            // 
            // AskPxStk
            // 
            this.AskPxStk.HeaderText = "AskPx";
            this.AskPxStk.Name = "AskPxStk";
            this.AskPxStk.ReadOnly = true;
            // 
            // AskSzStk
            // 
            this.AskSzStk.HeaderText = "AskSz";
            this.AskSzStk.Name = "AskSzStk";
            this.AskSzStk.ReadOnly = true;
            // 
            // dgvStocksTrades
            // 
            this.dgvStocksTrades.AllowUserToAddRows = false;
            this.dgvStocksTrades.AllowUserToDeleteRows = false;
            this.dgvStocksTrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStocksTrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStocksTrades.Location = new System.Drawing.Point(0, 0);
            this.dgvStocksTrades.Name = "dgvStocksTrades";
            this.dgvStocksTrades.Size = new System.Drawing.Size(349, 249);
            this.dgvStocksTrades.TabIndex = 0;
            // 
            // tabPageFuture
            // 
            this.tabPageFuture.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageFuture.Controls.Add(this.splitContainer3);
            this.tabPageFuture.Location = new System.Drawing.Point(4, 24);
            this.tabPageFuture.Name = "tabPageFuture";
            this.tabPageFuture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFuture.Size = new System.Drawing.Size(845, 485);
            this.tabPageFuture.TabIndex = 1;
            this.tabPageFuture.Text = "Futures";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.AllowDrop = true;
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesXAll);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesXBA);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesAPos);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesBPos);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesXBB);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesXLast);
            this.splitContainer3.Panel1.Controls.Add(this.btnFutureModify);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutClosePrice);
            this.splitContainer3.Panel1.Controls.Add(this.label21);
            this.splitContainer3.Panel1.Controls.Add(this.cmbFutExpiry);
            this.splitContainer3.Panel1.Controls.Add(this.label31);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesCancel);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesSell);
            this.splitContainer3.Panel1.Controls.Add(this.btnFuturesBuy);
            this.splitContainer3.Panel1.Controls.Add(this.cmbFuturesTif);
            this.splitContainer3.Panel1.Controls.Add(this.cmbFuturesType);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.cmbFuturesVenue);
            this.splitContainer3.Panel1.Controls.Add(this.label17);
            this.splitContainer3.Panel1.Controls.Add(this.txtFuturesLimit);
            this.splitContainer3.Panel1.Controls.Add(this.txtFuturesQty);
            this.splitContainer3.Panel1.Controls.Add(this.label18);
            this.splitContainer3.Panel1.Controls.Add(this.label19);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutSpread);
            this.splitContainer3.Panel1.Controls.Add(this.label20);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutLTQ);
            this.splitContainer3.Panel1.Controls.Add(this.label22);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutLTP);
            this.splitContainer3.Panel1.Controls.Add(this.label23);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutAskSz);
            this.splitContainer3.Panel1.Controls.Add(this.label24);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutAskPz);
            this.splitContainer3.Panel1.Controls.Add(this.label25);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutBidPz);
            this.splitContainer3.Panel1.Controls.Add(this.label26);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutBidSz);
            this.splitContainer3.Panel1.Controls.Add(this.label27);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutPercentChange);
            this.splitContainer3.Panel1.Controls.Add(this.label28);
            this.splitContainer3.Panel1.Controls.Add(this.txtFutChange);
            this.splitContainer3.Panel1.Controls.Add(this.label29);
            this.splitContainer3.Panel1.Controls.Add(this.cmbFutSymbol);
            this.splitContainer3.Panel1.Controls.Add(this.label30);
            this.splitContainer3.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer3.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(839, 479);
            this.splitContainer3.SplitterDistance = 184;
            this.splitContainer3.TabIndex = 0;
            // 
            // btnFuturesXAll
            // 
            this.btnFuturesXAll.Location = new System.Drawing.Point(318, 159);
            this.btnFuturesXAll.Name = "btnFuturesXAll";
            this.btnFuturesXAll.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesXAll.TabIndex = 115;
            this.btnFuturesXAll.Text = "xALL";
            this.btnFuturesXAll.UseVisualStyleBackColor = true;
            // 
            // btnFuturesXBA
            // 
            this.btnFuturesXBA.Location = new System.Drawing.Point(257, 159);
            this.btnFuturesXBA.Name = "btnFuturesXBA";
            this.btnFuturesXBA.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesXBA.TabIndex = 114;
            this.btnFuturesXBA.Text = "xBA";
            this.btnFuturesXBA.UseVisualStyleBackColor = true;
            // 
            // btnFuturesAPos
            // 
            this.btnFuturesAPos.Location = new System.Drawing.Point(196, 159);
            this.btnFuturesAPos.Name = "btnFuturesAPos";
            this.btnFuturesAPos.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesAPos.TabIndex = 113;
            this.btnFuturesAPos.Text = "APos";
            this.btnFuturesAPos.UseVisualStyleBackColor = true;
            this.btnFuturesAPos.Click += new System.EventHandler(this.btnFuturesAPos_Click);
            // 
            // btnFuturesBPos
            // 
            this.btnFuturesBPos.Location = new System.Drawing.Point(137, 159);
            this.btnFuturesBPos.Name = "btnFuturesBPos";
            this.btnFuturesBPos.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesBPos.TabIndex = 112;
            this.btnFuturesBPos.Text = "BPos";
            this.btnFuturesBPos.UseVisualStyleBackColor = true;
            this.btnFuturesBPos.Click += new System.EventHandler(this.btnFuturesBPos_Click);
            // 
            // btnFuturesXBB
            // 
            this.btnFuturesXBB.Location = new System.Drawing.Point(77, 159);
            this.btnFuturesXBB.Name = "btnFuturesXBB";
            this.btnFuturesXBB.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesXBB.TabIndex = 111;
            this.btnFuturesXBB.Text = "xBB";
            this.btnFuturesXBB.UseVisualStyleBackColor = true;
            // 
            // btnFuturesXLast
            // 
            this.btnFuturesXLast.Location = new System.Drawing.Point(17, 159);
            this.btnFuturesXLast.Name = "btnFuturesXLast";
            this.btnFuturesXLast.Size = new System.Drawing.Size(58, 23);
            this.btnFuturesXLast.TabIndex = 110;
            this.btnFuturesXLast.Text = "xLast";
            this.btnFuturesXLast.UseVisualStyleBackColor = true;
            // 
            // btnFutureModify
            // 
            this.btnFutureModify.Location = new System.Drawing.Point(431, 134);
            this.btnFutureModify.Name = "btnFutureModify";
            this.btnFutureModify.Size = new System.Drawing.Size(63, 23);
            this.btnFutureModify.TabIndex = 109;
            this.btnFutureModify.Text = "Modify";
            this.btnFutureModify.UseVisualStyleBackColor = true;
            this.btnFutureModify.Click += new System.EventHandler(this.btnFutureModify_Click);
            // 
            // txtFutClosePrice
            // 
            this.txtFutClosePrice.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutClosePrice.Enabled = false;
            this.txtFutClosePrice.Location = new System.Drawing.Point(431, 72);
            this.txtFutClosePrice.Name = "txtFutClosePrice";
            this.txtFutClosePrice.Size = new System.Drawing.Size(68, 21);
            this.txtFutClosePrice.TabIndex = 108;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(382, 74);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(43, 15);
            this.label21.TabIndex = 107;
            this.label21.Text = "Close";
            // 
            // cmbFutExpiry
            // 
            this.cmbFutExpiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFutExpiry.FormattingEnabled = true;
            this.cmbFutExpiry.Location = new System.Drawing.Point(241, 13);
            this.cmbFutExpiry.Name = "cmbFutExpiry";
            this.cmbFutExpiry.Size = new System.Drawing.Size(124, 23);
            this.cmbFutExpiry.Sorted = true;
            this.cmbFutExpiry.TabIndex = 106;
            this.cmbFutExpiry.SelectedIndexChanged += new System.EventHandler(this.cmbFuturesExpiry_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(188, 14);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 15);
            this.label31.TabIndex = 105;
            this.label31.Text = "Exp";
            // 
            // btnFuturesCancel
            // 
            this.btnFuturesCancel.Location = new System.Drawing.Point(500, 137);
            this.btnFuturesCancel.Name = "btnFuturesCancel";
            this.btnFuturesCancel.Size = new System.Drawing.Size(63, 23);
            this.btnFuturesCancel.TabIndex = 104;
            this.btnFuturesCancel.Text = "Cancel";
            this.btnFuturesCancel.UseVisualStyleBackColor = true;
            this.btnFuturesCancel.Click += new System.EventHandler(this.btnFuturesCancel_Click);
            // 
            // btnFuturesSell
            // 
            this.btnFuturesSell.Location = new System.Drawing.Point(500, 105);
            this.btnFuturesSell.Name = "btnFuturesSell";
            this.btnFuturesSell.Size = new System.Drawing.Size(63, 23);
            this.btnFuturesSell.TabIndex = 102;
            this.btnFuturesSell.Text = "Sell";
            this.btnFuturesSell.UseVisualStyleBackColor = true;
            this.btnFuturesSell.Click += new System.EventHandler(this.btnFuturesSell_Click);
            // 
            // btnFuturesBuy
            // 
            this.btnFuturesBuy.Location = new System.Drawing.Point(431, 105);
            this.btnFuturesBuy.Name = "btnFuturesBuy";
            this.btnFuturesBuy.Size = new System.Drawing.Size(63, 23);
            this.btnFuturesBuy.TabIndex = 101;
            this.btnFuturesBuy.Text = "Buy";
            this.btnFuturesBuy.UseVisualStyleBackColor = true;
            this.btnFuturesBuy.Click += new System.EventHandler(this.btnFuturesBuy_Click);
            // 
            // cmbFuturesTif
            // 
            this.cmbFuturesTif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuturesTif.FormattingEnabled = true;
            this.cmbFuturesTif.Items.AddRange(new object[] {
            "DAY",
            "IOC"});
            this.cmbFuturesTif.Location = new System.Drawing.Point(308, 132);
            this.cmbFuturesTif.Name = "cmbFuturesTif";
            this.cmbFuturesTif.Size = new System.Drawing.Size(57, 23);
            this.cmbFuturesTif.TabIndex = 100;
            // 
            // cmbFuturesType
            // 
            this.cmbFuturesType.BackColor = System.Drawing.SystemColors.Control;
            this.cmbFuturesType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuturesType.FormattingEnabled = true;
            this.cmbFuturesType.Items.AddRange(new object[] {
            "LIMIT",
            "MARKET"});
            this.cmbFuturesType.Location = new System.Drawing.Point(241, 132);
            this.cmbFuturesType.Name = "cmbFuturesType";
            this.cmbFuturesType.Size = new System.Drawing.Size(61, 23);
            this.cmbFuturesType.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 98;
            this.label2.Text = "Type";
            // 
            // cmbFuturesVenue
            // 
            this.cmbFuturesVenue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuturesVenue.FormattingEnabled = true;
            this.cmbFuturesVenue.Items.AddRange(new object[] {
            "NSE"});
            this.cmbFuturesVenue.Location = new System.Drawing.Point(241, 102);
            this.cmbFuturesVenue.Name = "cmbFuturesVenue";
            this.cmbFuturesVenue.Size = new System.Drawing.Size(124, 23);
            this.cmbFuturesVenue.TabIndex = 97;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(180, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 15);
            this.label17.TabIndex = 96;
            this.label17.Text = "Venue";
            // 
            // txtFuturesLimit
            // 
            this.txtFuturesLimit.Location = new System.Drawing.Point(65, 132);
            this.txtFuturesLimit.Name = "txtFuturesLimit";
            this.txtFuturesLimit.Size = new System.Drawing.Size(111, 21);
            this.txtFuturesLimit.TabIndex = 95;
            // 
            // txtFuturesQty
            // 
            this.txtFuturesQty.Location = new System.Drawing.Point(65, 102);
            this.txtFuturesQty.Name = "txtFuturesQty";
            this.txtFuturesQty.Size = new System.Drawing.Size(111, 21);
            this.txtFuturesQty.TabIndex = 94;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 132);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 15);
            this.label18.TabIndex = 93;
            this.label18.Text = "Limit";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 102);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 15);
            this.label19.TabIndex = 92;
            this.label19.Text = "Qty";
            // 
            // txtFutSpread
            // 
            this.txtFutSpread.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutSpread.Enabled = false;
            this.txtFutSpread.Location = new System.Drawing.Point(569, 71);
            this.txtFutSpread.Name = "txtFutSpread";
            this.txtFutSpread.Size = new System.Drawing.Size(81, 21);
            this.txtFutSpread.TabIndex = 91;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(502, 75);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 15);
            this.label20.TabIndex = 90;
            this.label20.Text = "Spread";
            // 
            // txtFutLTQ
            // 
            this.txtFutLTQ.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutLTQ.Enabled = false;
            this.txtFutLTQ.Location = new System.Drawing.Point(241, 71);
            this.txtFutLTQ.Name = "txtFutLTQ";
            this.txtFutLTQ.Size = new System.Drawing.Size(124, 21);
            this.txtFutLTQ.TabIndex = 87;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(182, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(33, 15);
            this.label22.TabIndex = 86;
            this.label22.Text = "LTQ";
            // 
            // txtFutLTP
            // 
            this.txtFutLTP.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutLTP.Enabled = false;
            this.txtFutLTP.Location = new System.Drawing.Point(65, 71);
            this.txtFutLTP.Name = "txtFutLTP";
            this.txtFutLTP.Size = new System.Drawing.Size(111, 21);
            this.txtFutLTP.TabIndex = 85;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(14, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(32, 15);
            this.label23.TabIndex = 84;
            this.label23.Text = "LTP";
            // 
            // txtFutAskSz
            // 
            this.txtFutAskSz.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutAskSz.Enabled = false;
            this.txtFutAskSz.Location = new System.Drawing.Point(569, 43);
            this.txtFutAskSz.Name = "txtFutAskSz";
            this.txtFutAskSz.Size = new System.Drawing.Size(81, 21);
            this.txtFutAskSz.TabIndex = 83;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(508, 43);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(45, 15);
            this.label24.TabIndex = 82;
            this.label24.Text = "AskSz";
            // 
            // txtFutAskPz
            // 
            this.txtFutAskPz.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutAskPz.Enabled = false;
            this.txtFutAskPz.Location = new System.Drawing.Point(431, 43);
            this.txtFutAskPz.Name = "txtFutAskPz";
            this.txtFutAskPz.Size = new System.Drawing.Size(68, 21);
            this.txtFutAskPz.TabIndex = 81;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(382, 43);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(45, 15);
            this.label25.TabIndex = 80;
            this.label25.Text = "AskPx";
            // 
            // txtFutBidPz
            // 
            this.txtFutBidPz.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutBidPz.Enabled = false;
            this.txtFutBidPz.Location = new System.Drawing.Point(241, 43);
            this.txtFutBidPz.Name = "txtFutBidPz";
            this.txtFutBidPz.Size = new System.Drawing.Size(124, 21);
            this.txtFutBidPz.TabIndex = 79;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(183, 43);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 15);
            this.label26.TabIndex = 78;
            this.label26.Text = "BidPx";
            // 
            // txtFutBidSz
            // 
            this.txtFutBidSz.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutBidSz.Enabled = false;
            this.txtFutBidSz.Location = new System.Drawing.Point(65, 43);
            this.txtFutBidSz.Name = "txtFutBidSz";
            this.txtFutBidSz.Size = new System.Drawing.Size(111, 21);
            this.txtFutBidSz.TabIndex = 77;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(14, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(44, 15);
            this.label27.TabIndex = 76;
            this.label27.Text = "BidSz";
            // 
            // txtFutPercentChange
            // 
            this.txtFutPercentChange.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutPercentChange.Enabled = false;
            this.txtFutPercentChange.Location = new System.Drawing.Point(569, 13);
            this.txtFutPercentChange.Name = "txtFutPercentChange";
            this.txtFutPercentChange.Size = new System.Drawing.Size(81, 21);
            this.txtFutPercentChange.TabIndex = 75;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(504, 14);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(48, 15);
            this.label28.TabIndex = 74;
            this.label28.Text = "% Chg";
            // 
            // txtFutChange
            // 
            this.txtFutChange.BackColor = System.Drawing.SystemColors.Control;
            this.txtFutChange.Enabled = false;
            this.txtFutChange.Location = new System.Drawing.Point(431, 13);
            this.txtFutChange.Name = "txtFutChange";
            this.txtFutChange.Size = new System.Drawing.Size(68, 21);
            this.txtFutChange.TabIndex = 73;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(382, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(32, 15);
            this.label29.TabIndex = 72;
            this.label29.Text = "Chg";
            // 
            // cmbFutSymbol
            // 
            this.cmbFutSymbol.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFutSymbol.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFutSymbol.DropDownHeight = 110;
            this.cmbFutSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFutSymbol.FormattingEnabled = true;
            this.cmbFutSymbol.IntegralHeight = false;
            this.cmbFutSymbol.ItemHeight = 15;
            this.cmbFutSymbol.Location = new System.Drawing.Point(65, 13);
            this.cmbFutSymbol.Name = "cmbFutSymbol";
            this.cmbFutSymbol.Size = new System.Drawing.Size(111, 23);
            this.cmbFutSymbol.Sorted = true;
            this.cmbFutSymbol.TabIndex = 71;
            this.cmbFutSymbol.SelectedIndexChanged += new System.EventHandler(this.cmbFuturesSymbol_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(14, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(34, 15);
            this.label30.TabIndex = 70;
            this.label30.Text = "Sym";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dgvFuturesPrices);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.dgvFuturesTrade);
            this.splitContainer4.Size = new System.Drawing.Size(839, 291);
            this.splitContainer4.SplitterDistance = 444;
            this.splitContainer4.TabIndex = 0;
            // 
            // dgvFuturesPrices
            // 
            this.dgvFuturesPrices.AllowUserToAddRows = false;
            this.dgvFuturesPrices.AllowUserToDeleteRows = false;
            this.dgvFuturesPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFuturesPrices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BidSzFut,
            this.BidPxFut,
            this.AskPxFut,
            this.AskSzFut});
            this.dgvFuturesPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFuturesPrices.Location = new System.Drawing.Point(0, 0);
            this.dgvFuturesPrices.MultiSelect = false;
            this.dgvFuturesPrices.Name = "dgvFuturesPrices";
            this.dgvFuturesPrices.ReadOnly = true;
            this.dgvFuturesPrices.Size = new System.Drawing.Size(444, 291);
            this.dgvFuturesPrices.TabIndex = 0;
            this.dgvFuturesPrices.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFuturesPrices_CellFormatting);
            // 
            // BidSzFut
            // 
            this.BidSzFut.HeaderText = "BidSz";
            this.BidSzFut.Name = "BidSzFut";
            this.BidSzFut.ReadOnly = true;
            // 
            // BidPxFut
            // 
            this.BidPxFut.HeaderText = "BidPx";
            this.BidPxFut.Name = "BidPxFut";
            this.BidPxFut.ReadOnly = true;
            // 
            // AskPxFut
            // 
            this.AskPxFut.HeaderText = "AskPx";
            this.AskPxFut.Name = "AskPxFut";
            this.AskPxFut.ReadOnly = true;
            // 
            // AskSzFut
            // 
            this.AskSzFut.HeaderText = "AskSz";
            this.AskSzFut.Name = "AskSzFut";
            this.AskSzFut.ReadOnly = true;
            // 
            // dgvFuturesTrade
            // 
            this.dgvFuturesTrade.AllowUserToAddRows = false;
            this.dgvFuturesTrade.AllowUserToDeleteRows = false;
            this.dgvFuturesTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFuturesTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFuturesTrade.Location = new System.Drawing.Point(0, 0);
            this.dgvFuturesTrade.Name = "dgvFuturesTrade";
            this.dgvFuturesTrade.ReadOnly = true;
            this.dgvFuturesTrade.Size = new System.Drawing.Size(391, 291);
            this.dgvFuturesTrade.TabIndex = 0;
            // 
            // tabPageOption
            // 
            this.tabPageOption.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageOption.Controls.Add(this.splitContainer5);
            this.tabPageOption.Location = new System.Drawing.Point(4, 24);
            this.tabPageOption.Name = "tabPageOption";
            this.tabPageOption.Size = new System.Drawing.Size(845, 485);
            this.tabPageOption.TabIndex = 2;
            this.tabPageOption.Text = "Options";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.AllowDrop = true;
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsXAll);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsXBA);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsAPos);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsBPos);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsXBB);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsXLast);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsDeltaPosition);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsImpliedVolatility);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsGamma);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsDelta);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderVolume);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderAskSize);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderAskPrice);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderBidPrice);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderLow);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderBidSize);
            this.splitContainer5.Panel1.Controls.Add(this.rbtnUnderlying);
            this.splitContainer5.Panel1.Controls.Add(this.rbtnSymbol);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionCancel);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionModify);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsType);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderOpen);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderLTP);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderHigh);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsSell);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsPrice);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsQty);
            this.splitContainer5.Panel1.Controls.Add(this.label42);
            this.splitContainer5.Panel1.Controls.Add(this.label41);
            this.splitContainer5.Panel1.Controls.Add(this.btnOptionsBuy);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsDest);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsAccount);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsTif);
            this.splitContainer5.Panel1.Controls.Add(this.Type);
            this.splitContainer5.Panel1.Controls.Add(this.label39);
            this.splitContainer5.Panel1.Controls.Add(this.label38);
            this.splitContainer5.Panel1.Controls.Add(this.label37);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsAskSize);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsAskPrice);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsBidPrice);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsBidSize);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderPercentChange);
            this.splitContainer5.Panel1.Controls.Add(this.txtOptionsUnderChange);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsCallPut);
            this.splitContainer5.Panel1.Controls.Add(this.label35);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsStrike);
            this.splitContainer5.Panel1.Controls.Add(this.label34);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsExpiry);
            this.splitContainer5.Panel1.Controls.Add(this.label33);
            this.splitContainer5.Panel1.Controls.Add(this.cmbOptionsSymbol);
            this.splitContainer5.Panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragDrop);
            this.splitContainer5.Panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.splitContainer1_Panel1_DragEnter);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(845, 485);
            this.splitContainer5.SplitterDistance = 267;
            this.splitContainer5.TabIndex = 30;
            // 
            // btnOptionsXAll
            // 
            this.btnOptionsXAll.Location = new System.Drawing.Point(306, 241);
            this.btnOptionsXAll.Name = "btnOptionsXAll";
            this.btnOptionsXAll.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsXAll.TabIndex = 83;
            this.btnOptionsXAll.Text = "xALL";
            this.btnOptionsXAll.UseVisualStyleBackColor = true;
            // 
            // btnOptionsXBA
            // 
            this.btnOptionsXBA.Location = new System.Drawing.Point(245, 241);
            this.btnOptionsXBA.Name = "btnOptionsXBA";
            this.btnOptionsXBA.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsXBA.TabIndex = 82;
            this.btnOptionsXBA.Text = "xBA";
            this.btnOptionsXBA.UseVisualStyleBackColor = true;
            // 
            // btnOptionsAPos
            // 
            this.btnOptionsAPos.Location = new System.Drawing.Point(184, 241);
            this.btnOptionsAPos.Name = "btnOptionsAPos";
            this.btnOptionsAPos.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsAPos.TabIndex = 81;
            this.btnOptionsAPos.Text = "APos";
            this.btnOptionsAPos.UseVisualStyleBackColor = true;
            this.btnOptionsAPos.Click += new System.EventHandler(this.btnOptionsAPos_Click);
            // 
            // btnOptionsBPos
            // 
            this.btnOptionsBPos.Location = new System.Drawing.Point(125, 241);
            this.btnOptionsBPos.Name = "btnOptionsBPos";
            this.btnOptionsBPos.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsBPos.TabIndex = 80;
            this.btnOptionsBPos.Text = "BPos";
            this.btnOptionsBPos.UseVisualStyleBackColor = true;
            this.btnOptionsBPos.Click += new System.EventHandler(this.btnOptionsBPos_Click);
            // 
            // btnOptionsXBB
            // 
            this.btnOptionsXBB.Location = new System.Drawing.Point(65, 241);
            this.btnOptionsXBB.Name = "btnOptionsXBB";
            this.btnOptionsXBB.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsXBB.TabIndex = 79;
            this.btnOptionsXBB.Text = "xBB";
            this.btnOptionsXBB.UseVisualStyleBackColor = true;
            // 
            // btnOptionsXLast
            // 
            this.btnOptionsXLast.Location = new System.Drawing.Point(5, 241);
            this.btnOptionsXLast.Name = "btnOptionsXLast";
            this.btnOptionsXLast.Size = new System.Drawing.Size(58, 23);
            this.btnOptionsXLast.TabIndex = 78;
            this.btnOptionsXLast.Text = "xLast";
            this.btnOptionsXLast.UseVisualStyleBackColor = true;
            // 
            // txtOptionsDeltaPosition
            // 
            this.txtOptionsDeltaPosition.Location = new System.Drawing.Point(278, 110);
            this.txtOptionsDeltaPosition.Name = "txtOptionsDeltaPosition";
            this.txtOptionsDeltaPosition.Size = new System.Drawing.Size(86, 21);
            this.txtOptionsDeltaPosition.TabIndex = 77;
            // 
            // txtOptionsImpliedVolatility
            // 
            this.txtOptionsImpliedVolatility.Location = new System.Drawing.Point(187, 110);
            this.txtOptionsImpliedVolatility.Name = "txtOptionsImpliedVolatility";
            this.txtOptionsImpliedVolatility.Size = new System.Drawing.Size(86, 21);
            this.txtOptionsImpliedVolatility.TabIndex = 76;
            // 
            // txtOptionsGamma
            // 
            this.txtOptionsGamma.Location = new System.Drawing.Point(96, 110);
            this.txtOptionsGamma.Name = "txtOptionsGamma";
            this.txtOptionsGamma.Size = new System.Drawing.Size(86, 21);
            this.txtOptionsGamma.TabIndex = 75;
            // 
            // txtOptionsDelta
            // 
            this.txtOptionsDelta.Location = new System.Drawing.Point(5, 110);
            this.txtOptionsDelta.Name = "txtOptionsDelta";
            this.txtOptionsDelta.Size = new System.Drawing.Size(86, 21);
            this.txtOptionsDelta.TabIndex = 74;
            // 
            // txtOptionsUnderVolume
            // 
            this.txtOptionsUnderVolume.Location = new System.Drawing.Point(302, 83);
            this.txtOptionsUnderVolume.Name = "txtOptionsUnderVolume";
            this.txtOptionsUnderVolume.Size = new System.Drawing.Size(63, 21);
            this.txtOptionsUnderVolume.TabIndex = 73;
            // 
            // txtOptionsUnderAskSize
            // 
            this.txtOptionsUnderAskSize.Location = new System.Drawing.Point(226, 83);
            this.txtOptionsUnderAskSize.Name = "txtOptionsUnderAskSize";
            this.txtOptionsUnderAskSize.Size = new System.Drawing.Size(70, 21);
            this.txtOptionsUnderAskSize.TabIndex = 72;
            // 
            // txtOptionsUnderAskPrice
            // 
            this.txtOptionsUnderAskPrice.Location = new System.Drawing.Point(150, 83);
            this.txtOptionsUnderAskPrice.Name = "txtOptionsUnderAskPrice";
            this.txtOptionsUnderAskPrice.Size = new System.Drawing.Size(70, 21);
            this.txtOptionsUnderAskPrice.TabIndex = 71;
            // 
            // txtOptionsUnderBidPrice
            // 
            this.txtOptionsUnderBidPrice.Location = new System.Drawing.Point(77, 83);
            this.txtOptionsUnderBidPrice.Name = "txtOptionsUnderBidPrice";
            this.txtOptionsUnderBidPrice.Size = new System.Drawing.Size(70, 21);
            this.txtOptionsUnderBidPrice.TabIndex = 70;
            // 
            // txtOptionsUnderLow
            // 
            this.txtOptionsUnderLow.Location = new System.Drawing.Point(247, 56);
            this.txtOptionsUnderLow.Name = "txtOptionsUnderLow";
            this.txtOptionsUnderLow.Size = new System.Drawing.Size(55, 21);
            this.txtOptionsUnderLow.TabIndex = 69;
            // 
            // txtOptionsUnderBidSize
            // 
            this.txtOptionsUnderBidSize.Location = new System.Drawing.Point(5, 83);
            this.txtOptionsUnderBidSize.Name = "txtOptionsUnderBidSize";
            this.txtOptionsUnderBidSize.Size = new System.Drawing.Size(70, 21);
            this.txtOptionsUnderBidSize.TabIndex = 68;
            // 
            // rbtnUnderlying
            // 
            this.rbtnUnderlying.AutoSize = true;
            this.rbtnUnderlying.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnUnderlying.Location = new System.Drawing.Point(58, 6);
            this.rbtnUnderlying.Name = "rbtnUnderlying";
            this.rbtnUnderlying.Size = new System.Drawing.Size(59, 19);
            this.rbtnUnderlying.TabIndex = 2;
            this.rbtnUnderlying.Text = "Under";
            this.rbtnUnderlying.UseVisualStyleBackColor = true;
            // 
            // rbtnSymbol
            // 
            this.rbtnSymbol.AutoSize = true;
            this.rbtnSymbol.Checked = true;
            this.rbtnSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSymbol.Location = new System.Drawing.Point(8, 6);
            this.rbtnSymbol.Name = "rbtnSymbol";
            this.rbtnSymbol.Size = new System.Drawing.Size(49, 19);
            this.rbtnSymbol.TabIndex = 1;
            this.rbtnSymbol.TabStop = true;
            this.rbtnSymbol.Text = "Sym";
            this.rbtnSymbol.UseVisualStyleBackColor = true;
            // 
            // btnOptionCancel
            // 
            this.btnOptionCancel.Location = new System.Drawing.Point(452, 209);
            this.btnOptionCancel.Name = "btnOptionCancel";
            this.btnOptionCancel.Size = new System.Drawing.Size(75, 23);
            this.btnOptionCancel.TabIndex = 67;
            this.btnOptionCancel.Text = "Cancel";
            this.btnOptionCancel.UseVisualStyleBackColor = true;
            this.btnOptionCancel.Click += new System.EventHandler(this.btnOptionCancel_Click);
            // 
            // btnOptionModify
            // 
            this.btnOptionModify.Location = new System.Drawing.Point(371, 209);
            this.btnOptionModify.Name = "btnOptionModify";
            this.btnOptionModify.Size = new System.Drawing.Size(75, 23);
            this.btnOptionModify.TabIndex = 66;
            this.btnOptionModify.Text = "Modify";
            this.btnOptionModify.UseVisualStyleBackColor = true;
            this.btnOptionModify.Click += new System.EventHandler(this.btnOptionModify_Click);
            // 
            // cmbOptionsType
            // 
            this.cmbOptionsType.FormattingEnabled = true;
            this.cmbOptionsType.Items.AddRange(new object[] {
            "LIMIT",
            "MARKET"});
            this.cmbOptionsType.Location = new System.Drawing.Point(269, 179);
            this.cmbOptionsType.Name = "cmbOptionsType";
            this.cmbOptionsType.Size = new System.Drawing.Size(95, 23);
            this.cmbOptionsType.TabIndex = 65;
            // 
            // txtOptionsUnderOpen
            // 
            this.txtOptionsUnderOpen.Location = new System.Drawing.Point(125, 56);
            this.txtOptionsUnderOpen.Name = "txtOptionsUnderOpen";
            this.txtOptionsUnderOpen.Size = new System.Drawing.Size(55, 21);
            this.txtOptionsUnderOpen.TabIndex = 64;
            // 
            // txtOptionsUnderLTP
            // 
            this.txtOptionsUnderLTP.Location = new System.Drawing.Point(308, 56);
            this.txtOptionsUnderLTP.Name = "txtOptionsUnderLTP";
            this.txtOptionsUnderLTP.Size = new System.Drawing.Size(56, 21);
            this.txtOptionsUnderLTP.TabIndex = 63;
            // 
            // txtOptionsUnderHigh
            // 
            this.txtOptionsUnderHigh.Location = new System.Drawing.Point(186, 56);
            this.txtOptionsUnderHigh.Name = "txtOptionsUnderHigh";
            this.txtOptionsUnderHigh.Size = new System.Drawing.Size(55, 21);
            this.txtOptionsUnderHigh.TabIndex = 61;
            // 
            // btnOptionsSell
            // 
            this.btnOptionsSell.Location = new System.Drawing.Point(302, 209);
            this.btnOptionsSell.Name = "btnOptionsSell";
            this.btnOptionsSell.Size = new System.Drawing.Size(63, 23);
            this.btnOptionsSell.TabIndex = 59;
            this.btnOptionsSell.Text = "Sell";
            this.btnOptionsSell.UseVisualStyleBackColor = true;
            this.btnOptionsSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // txtOptionsPrice
            // 
            this.txtOptionsPrice.Location = new System.Drawing.Point(226, 210);
            this.txtOptionsPrice.Name = "txtOptionsPrice";
            this.txtOptionsPrice.Size = new System.Drawing.Size(70, 21);
            this.txtOptionsPrice.TabIndex = 58;
            // 
            // txtOptionsQty
            // 
            this.txtOptionsQty.Location = new System.Drawing.Point(121, 209);
            this.txtOptionsQty.Name = "txtOptionsQty";
            this.txtOptionsQty.Size = new System.Drawing.Size(48, 21);
            this.txtOptionsQty.TabIndex = 57;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(176, 212);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(44, 15);
            this.label42.TabIndex = 56;
            this.label42.Text = "Price:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(84, 212);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(31, 15);
            this.label41.TabIndex = 55;
            this.label41.Text = "Qty:";
            // 
            // btnOptionsBuy
            // 
            this.btnOptionsBuy.Location = new System.Drawing.Point(5, 208);
            this.btnOptionsBuy.Name = "btnOptionsBuy";
            this.btnOptionsBuy.Size = new System.Drawing.Size(67, 23);
            this.btnOptionsBuy.TabIndex = 54;
            this.btnOptionsBuy.Text = "Buy";
            this.btnOptionsBuy.UseVisualStyleBackColor = true;
            this.btnOptionsBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // cmbOptionsDest
            // 
            this.cmbOptionsDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsDest.FormattingEnabled = true;
            this.cmbOptionsDest.Items.AddRange(new object[] {
            "NSE"});
            this.cmbOptionsDest.Location = new System.Drawing.Point(184, 179);
            this.cmbOptionsDest.Name = "cmbOptionsDest";
            this.cmbOptionsDest.Size = new System.Drawing.Size(61, 23);
            this.cmbOptionsDest.TabIndex = 52;
            // 
            // cmbOptionsAccount
            // 
            this.cmbOptionsAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsAccount.FormattingEnabled = true;
            this.cmbOptionsAccount.Location = new System.Drawing.Point(87, 179);
            this.cmbOptionsAccount.Name = "cmbOptionsAccount";
            this.cmbOptionsAccount.Size = new System.Drawing.Size(83, 23);
            this.cmbOptionsAccount.TabIndex = 51;
            // 
            // cmbOptionsTif
            // 
            this.cmbOptionsTif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsTif.FormattingEnabled = true;
            this.cmbOptionsTif.Items.AddRange(new object[] {
            "DAY",
            "IOC"});
            this.cmbOptionsTif.Location = new System.Drawing.Point(5, 179);
            this.cmbOptionsTif.Name = "cmbOptionsTif";
            this.cmbOptionsTif.Size = new System.Drawing.Size(67, 23);
            this.cmbOptionsTif.TabIndex = 50;
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Location = new System.Drawing.Point(265, 161);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(37, 15);
            this.Type.TabIndex = 48;
            this.Type.Text = "Type";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(184, 161);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(36, 15);
            this.label39.TabIndex = 47;
            this.label39.Text = "Dest";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(87, 161);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(57, 15);
            this.label38.TabIndex = 46;
            this.label38.Text = "Account";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(8, 161);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(19, 15);
            this.label37.TabIndex = 45;
            this.label37.Text = "tif";
            // 
            // txtOptionsAskSize
            // 
            this.txtOptionsAskSize.Location = new System.Drawing.Point(279, 137);
            this.txtOptionsAskSize.Name = "txtOptionsAskSize";
            this.txtOptionsAskSize.Size = new System.Drawing.Size(85, 21);
            this.txtOptionsAskSize.TabIndex = 43;
            // 
            // txtOptionsAskPrice
            // 
            this.txtOptionsAskPrice.Location = new System.Drawing.Point(187, 137);
            this.txtOptionsAskPrice.Name = "txtOptionsAskPrice";
            this.txtOptionsAskPrice.Size = new System.Drawing.Size(86, 21);
            this.txtOptionsAskPrice.TabIndex = 42;
            // 
            // txtOptionsBidPrice
            // 
            this.txtOptionsBidPrice.Location = new System.Drawing.Point(97, 137);
            this.txtOptionsBidPrice.Name = "txtOptionsBidPrice";
            this.txtOptionsBidPrice.Size = new System.Drawing.Size(85, 21);
            this.txtOptionsBidPrice.TabIndex = 41;
            // 
            // txtOptionsBidSize
            // 
            this.txtOptionsBidSize.Location = new System.Drawing.Point(5, 137);
            this.txtOptionsBidSize.Name = "txtOptionsBidSize";
            this.txtOptionsBidSize.Size = new System.Drawing.Size(85, 21);
            this.txtOptionsBidSize.TabIndex = 40;
            // 
            // txtOptionsUnderPercentChange
            // 
            this.txtOptionsUnderPercentChange.Location = new System.Drawing.Point(51, 56);
            this.txtOptionsUnderPercentChange.Name = "txtOptionsUnderPercentChange";
            this.txtOptionsUnderPercentChange.Size = new System.Drawing.Size(68, 21);
            this.txtOptionsUnderPercentChange.TabIndex = 39;
            // 
            // txtOptionsUnderChange
            // 
            this.txtOptionsUnderChange.Location = new System.Drawing.Point(5, 56);
            this.txtOptionsUnderChange.Name = "txtOptionsUnderChange";
            this.txtOptionsUnderChange.Size = new System.Drawing.Size(40, 21);
            this.txtOptionsUnderChange.TabIndex = 38;
            // 
            // cmbOptionsCallPut
            // 
            this.cmbOptionsCallPut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOptionsCallPut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOptionsCallPut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsCallPut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptionsCallPut.FormattingEnabled = true;
            this.cmbOptionsCallPut.Items.AddRange(new object[] {
            "CE",
            "PE"});
            this.cmbOptionsCallPut.Location = new System.Drawing.Point(214, 29);
            this.cmbOptionsCallPut.Name = "cmbOptionsCallPut";
            this.cmbOptionsCallPut.Size = new System.Drawing.Size(75, 21);
            this.cmbOptionsCallPut.Sorted = true;
            this.cmbOptionsCallPut.TabIndex = 37;
            this.cmbOptionsCallPut.SelectedIndexChanged += new System.EventHandler(this.cmbOptionsCallPut_SelectedIndexChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(211, 8);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(29, 15);
            this.label35.TabIndex = 36;
            this.label35.Text = "C/P:";
            // 
            // cmbOptionsStrike
            // 
            this.cmbOptionsStrike.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOptionsStrike.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOptionsStrike.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsStrike.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptionsStrike.FormattingEnabled = true;
            this.cmbOptionsStrike.Items.AddRange(new object[] {
            "10000"});
            this.cmbOptionsStrike.Location = new System.Drawing.Point(296, 29);
            this.cmbOptionsStrike.Name = "cmbOptionsStrike";
            this.cmbOptionsStrike.Size = new System.Drawing.Size(69, 21);
            this.cmbOptionsStrike.Sorted = true;
            this.cmbOptionsStrike.TabIndex = 35;
            this.cmbOptionsStrike.SelectedIndexChanged += new System.EventHandler(this.cmbOptionsStrike_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(293, 8);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 15);
            this.label34.TabIndex = 34;
            this.label34.Text = "Strike:";
            // 
            // cmbOptionsExpiry
            // 
            this.cmbOptionsExpiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsExpiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptionsExpiry.FormattingEnabled = true;
            this.cmbOptionsExpiry.Location = new System.Drawing.Point(126, 29);
            this.cmbOptionsExpiry.Name = "cmbOptionsExpiry";
            this.cmbOptionsExpiry.Size = new System.Drawing.Size(81, 21);
            this.cmbOptionsExpiry.Sorted = true;
            this.cmbOptionsExpiry.TabIndex = 3;
            this.cmbOptionsExpiry.SelectedIndexChanged += new System.EventHandler(this.cmbOptionsExpiry_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(123, 8);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 15);
            this.label33.TabIndex = 32;
            this.label33.Text = "Expiration:";
            // 
            // cmbOptionsSymbol
            // 
            this.cmbOptionsSymbol.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOptionsSymbol.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOptionsSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptionsSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptionsSymbol.FormattingEnabled = true;
            this.cmbOptionsSymbol.ItemHeight = 13;
            this.cmbOptionsSymbol.Location = new System.Drawing.Point(5, 29);
            this.cmbOptionsSymbol.Name = "cmbOptionsSymbol";
            this.cmbOptionsSymbol.Size = new System.Drawing.Size(114, 21);
            this.cmbOptionsSymbol.Sorted = true;
            this.cmbOptionsSymbol.TabIndex = 2;
            this.cmbOptionsSymbol.SelectedIndexChanged += new System.EventHandler(this.cmbOptionsSymbol_SelectedIndexChanged);
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.dgvOptionsPrices);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.dgvOptionsTrade);
            this.splitContainer6.Size = new System.Drawing.Size(845, 214);
            this.splitContainer6.SplitterDistance = 477;
            this.splitContainer6.TabIndex = 0;
            // 
            // dgvOptionsPrices
            // 
            this.dgvOptionsPrices.AllowUserToAddRows = false;
            this.dgvOptionsPrices.AllowUserToDeleteRows = false;
            this.dgvOptionsPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOptionsPrices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BidSzOpt,
            this.BidPxOpt,
            this.AskPxOpt,
            this.AskSzOpt});
            this.dgvOptionsPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOptionsPrices.Location = new System.Drawing.Point(0, 0);
            this.dgvOptionsPrices.Name = "dgvOptionsPrices";
            this.dgvOptionsPrices.ReadOnly = true;
            this.dgvOptionsPrices.Size = new System.Drawing.Size(477, 214);
            this.dgvOptionsPrices.TabIndex = 1;
            this.dgvOptionsPrices.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOptionsPrices_CellFormatting);
            // 
            // BidSzOpt
            // 
            this.BidSzOpt.HeaderText = "BidSz";
            this.BidSzOpt.Name = "BidSzOpt";
            this.BidSzOpt.ReadOnly = true;
            // 
            // BidPxOpt
            // 
            this.BidPxOpt.HeaderText = "BidPx";
            this.BidPxOpt.Name = "BidPxOpt";
            this.BidPxOpt.ReadOnly = true;
            // 
            // AskPxOpt
            // 
            this.AskPxOpt.HeaderText = "AskPx";
            this.AskPxOpt.Name = "AskPxOpt";
            this.AskPxOpt.ReadOnly = true;
            // 
            // AskSzOpt
            // 
            this.AskSzOpt.HeaderText = "AskSz";
            this.AskSzOpt.Name = "AskSzOpt";
            this.AskSzOpt.ReadOnly = true;
            // 
            // dgvOptionsTrade
            // 
            this.dgvOptionsTrade.AllowUserToAddRows = false;
            this.dgvOptionsTrade.AllowUserToDeleteRows = false;
            this.dgvOptionsTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOptionsTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOptionsTrade.Location = new System.Drawing.Point(0, 0);
            this.dgvOptionsTrade.Name = "dgvOptionsTrade";
            this.dgvOptionsTrade.ReadOnly = true;
            this.dgvOptionsTrade.Size = new System.Drawing.Size(364, 214);
            this.dgvOptionsTrade.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.richTextBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(845, 485);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(222, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(20, 33);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(615, 359);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelMessage1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(853, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabelMessage1
            // 
            this.StatusLabelMessage1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLabelMessage1.Name = "StatusLabelMessage1";
            this.StatusLabelMessage1.Size = new System.Drawing.Size(118, 17);
            this.StatusLabelMessage1.Text = "toolStripStatusLabel1";
            // 
            // TradingBoxV3
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 513);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "TradingBoxV3";
            this.Text = "TradingBoxV3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradingBoxV3_FormClosing);
            this.Load += new System.EventHandler(this.TradingBoxV3_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageStock.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStocksTrades)).EndInit();
            this.tabPageFuture.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuturesPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuturesTrade)).EndInit();
            this.tabPageOption.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionsPrices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptionsTrade)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageStock;
        private System.Windows.Forms.TabPage tabPageFuture;
        private System.Windows.Forms.TabPage tabPageOption;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnStocksCancel;
        private System.Windows.Forms.Button btnStocksSell;
        private System.Windows.Forms.Button btnStocksBuy;
        private System.Windows.Forms.ComboBox cmbStocksTif;
        private System.Windows.Forms.ComboBox cmbStocksType;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbStocksVenue;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtStocksLimit;
        private System.Windows.Forms.TextBox txtStocksQty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtStocksSpread;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtStocksClose;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStocksLTQ;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStocksLTP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStocksAskSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStocksAskPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStocksBidPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStocksBidSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStocksPercentChange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStocksChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbStocksSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvStocksPrices;
        private System.Windows.Forms.DataGridView dgvStocksTrades;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnFuturesCancel;
        private System.Windows.Forms.Button btnFuturesSell;
        private System.Windows.Forms.Button btnFuturesBuy;
        private System.Windows.Forms.ComboBox cmbFuturesTif;
        private System.Windows.Forms.ComboBox cmbFuturesType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFuturesVenue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtFuturesLimit;
        private System.Windows.Forms.TextBox txtFuturesQty;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFutSpread;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtFutLTQ;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtFutLTP;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtFutBidPz;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtFutBidSz;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtFutPercentChange;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtFutChange;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbFutSymbol;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgvFuturesPrices;
        private System.Windows.Forms.DataGridView dgvFuturesTrade;
        private System.Windows.Forms.ComboBox cmbFutExpiry;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Button btnOptionsSell;
        private System.Windows.Forms.TextBox txtOptionsPrice;
        private System.Windows.Forms.TextBox txtOptionsQty;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button btnOptionsBuy;
        private System.Windows.Forms.ComboBox cmbOptionsDest;
        private System.Windows.Forms.ComboBox cmbOptionsAccount;
        private System.Windows.Forms.ComboBox cmbOptionsTif;
        private System.Windows.Forms.Label Type;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtOptionsAskSize;
        private System.Windows.Forms.TextBox txtOptionsAskPrice;
        private System.Windows.Forms.TextBox txtOptionsBidPrice;
        private System.Windows.Forms.TextBox txtOptionsBidSize;
        private System.Windows.Forms.TextBox txtOptionsUnderPercentChange;
        private System.Windows.Forms.TextBox txtOptionsUnderChange;
        private System.Windows.Forms.ComboBox cmbOptionsCallPut;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cmbOptionsStrike;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmbOptionsExpiry;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cmbOptionsSymbol;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.DataGridView dgvOptionsPrices;
        private System.Windows.Forms.DataGridView dgvOptionsTrade;
        private TextBox txtFutAskSz;
        private Label label24;
        private TextBox txtFutAskPz;
        private Label label25;
        private TabPage tabPage1;
        private RichTextBox richTextBox2;
        private TextBox textBox1;
        private Button button2;
        private TextBox txtOptionsUnderHigh;
        private TextBox txtFutClosePrice;
        private Label label21;
        private TextBox txtOptionsUnderOpen;
        private TextBox txtOptionsUnderLTP;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabelMessage1;
        private ComboBox cmbOptionsType;
        private Button btnModify;
        private Button btnFutureModify;
        private Button btnOptionCancel;
        private Button btnOptionModify;
        private RadioButton rbtnSymbol;
        private RadioButton rbtnUnderlying;
        private TextBox txtOptionsUnderBidSize;
        private TextBox txtOptionsUnderLow;
        private TextBox txtOptionsUnderAskSize;
        private TextBox txtOptionsUnderAskPrice;
        private TextBox txtOptionsUnderBidPrice;
        private TextBox txtOptionsUnderVolume;
        private TextBox txtOptionsDeltaPosition;
        private TextBox txtOptionsImpliedVolatility;
        private TextBox txtOptionsGamma;
        private TextBox txtOptionsDelta;
        private Button btnOptionsXLast;
        private Button btnOptionsXBB;
        private Button btnOptionsBPos;
        private Button btnOptionsXBA;
        private Button btnOptionsAPos;
        private Button btnOptionsXAll;
        private Button btnStockXAll;
        private Button btnStockXBA;
        private Button btnStockAPos;
        private Button btnStockBPos;
        private Button btnStockXBB;
        private Button btnStockXLast;
        private Button btnFuturesXAll;
        private Button btnFuturesXBA;
        private Button btnFuturesAPos;
        private Button btnFuturesBPos;
        private Button btnFuturesXBB;
        private Button btnFuturesXLast;
        private DataGridViewTextBoxColumn BidSzStk;
        private DataGridViewTextBoxColumn BidPxStk;
        private DataGridViewTextBoxColumn AskPxStk;
        private DataGridViewTextBoxColumn AskSzStk;
        private DataGridViewTextBoxColumn BidSzFut;
        private DataGridViewTextBoxColumn BidPxFut;
        private DataGridViewTextBoxColumn AskPxFut;
        private DataGridViewTextBoxColumn AskSzFut;
        private DataGridViewTextBoxColumn BidSzOpt;
        private DataGridViewTextBoxColumn BidPxOpt;
        private DataGridViewTextBoxColumn AskPxOpt;
        private DataGridViewTextBoxColumn AskSzOpt;
    }
}