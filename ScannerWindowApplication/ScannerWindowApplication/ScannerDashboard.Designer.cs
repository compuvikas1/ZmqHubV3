﻿namespace ScannerWindowApplication
{
    partial class ScannerDashboard
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bODToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderBlotterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eODToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closePriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eODPositionSnapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.bODToolStripMenuItem,
            this.scannerToolStripMenuItem,
            this.orderBlotterToolStripMenuItem,
            this.eODToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.configToolStripMenuItem.Text = "Filters";
            this.configToolStripMenuItem.Visible = false;
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // bODToolStripMenuItem
            // 
            this.bODToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.securityMasterToolStripMenuItem});
            this.bODToolStripMenuItem.Name = "bODToolStripMenuItem";
            this.bODToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.bODToolStripMenuItem.Text = "BOD";
            // 
            // securityMasterToolStripMenuItem
            // 
            this.securityMasterToolStripMenuItem.Name = "securityMasterToolStripMenuItem";
            this.securityMasterToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.securityMasterToolStripMenuItem.Text = "Security Master";
            this.securityMasterToolStripMenuItem.Click += new System.EventHandler(this.securityMasterToolStripMenuItem_Click);
            // 
            // scannerToolStripMenuItem
            // 
            this.scannerToolStripMenuItem.Name = "scannerToolStripMenuItem";
            this.scannerToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.scannerToolStripMenuItem.Text = "Scanner";
            this.scannerToolStripMenuItem.Click += new System.EventHandler(this.scannerToolStripMenuItem_Click);
            // 
            // orderBlotterToolStripMenuItem
            // 
            this.orderBlotterToolStripMenuItem.Name = "orderBlotterToolStripMenuItem";
            this.orderBlotterToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.orderBlotterToolStripMenuItem.Text = "OrderBlotter";
            this.orderBlotterToolStripMenuItem.Click += new System.EventHandler(this.orderBlotterToolStripMenuItem_Click);
            // 
            // eODToolStripMenuItem
            // 
            this.eODToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closePriceToolStripMenuItem,
            this.eODPositionSnapshotToolStripMenuItem});
            this.eODToolStripMenuItem.Name = "eODToolStripMenuItem";
            this.eODToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.eODToolStripMenuItem.Text = "EOD";
            // 
            // closePriceToolStripMenuItem
            // 
            this.closePriceToolStripMenuItem.Name = "closePriceToolStripMenuItem";
            this.closePriceToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.closePriceToolStripMenuItem.Text = "Close Price";
            this.closePriceToolStripMenuItem.Click += new System.EventHandler(this.closePriceToolStripMenuItem_Click);
            // 
            // eODPositionSnapshotToolStripMenuItem
            // 
            this.eODPositionSnapshotToolStripMenuItem.Name = "eODPositionSnapshotToolStripMenuItem";
            this.eODPositionSnapshotToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.eODPositionSnapshotToolStripMenuItem.Text = "EOD Position Snapshot";
            this.eODPositionSnapshotToolStripMenuItem.Click += new System.EventHandler(this.eODPositionSnapshotToolStripMenuItem_Click);
            // 
            // ScannerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 262);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScannerDashboard";
            this.Text = "Scanner Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ScannerDashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderBlotterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eODToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bODToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closePriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eODPositionSnapshotToolStripMenuItem;
    }
}

