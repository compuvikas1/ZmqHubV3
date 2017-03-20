namespace ScannerWindowApplication
{
    partial class ScannerBox
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnAnd2 = new System.Windows.Forms.RadioButton();
            this.rbtnOr2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnAnd1 = new System.Windows.Forms.RadioButton();
            this.rbtnOr1 = new System.Windows.Forms.RadioButton();
            this.txtSpread = new System.Windows.Forms.TextBox();
            this.cmbSpreadCondition = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLTQ = new System.Windows.Forms.TextBox();
            this.cmbLtqCondition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.txtLTP = new System.Windows.Forms.TextBox();
            this.cmbLtpCondition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1048, 256);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnReset);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.txtSpread);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSpreadCondition);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtLTQ);
            this.splitContainer1.Panel1.Controls.Add(this.cmbLtqCondition);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnApplyFilter);
            this.splitContainer1.Panel1.Controls.Add(this.txtLTP);
            this.splitContainer1.Panel1.Controls.Add(this.cmbLtpCondition);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1048, 309);
            this.splitContainer1.SplitterDistance = 49;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnAnd2);
            this.groupBox2.Controls.Add(this.rbtnOr2);
            this.groupBox2.Location = new System.Drawing.Point(543, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 45);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // rbtnAnd2
            // 
            this.rbtnAnd2.AutoSize = true;
            this.rbtnAnd2.Location = new System.Drawing.Point(6, 16);
            this.rbtnAnd2.Name = "rbtnAnd2";
            this.rbtnAnd2.Size = new System.Drawing.Size(44, 17);
            this.rbtnAnd2.TabIndex = 9;
            this.rbtnAnd2.TabStop = true;
            this.rbtnAnd2.Text = "And";
            this.rbtnAnd2.UseVisualStyleBackColor = true;
            // 
            // rbtnOr2
            // 
            this.rbtnOr2.AutoSize = true;
            this.rbtnOr2.Location = new System.Drawing.Point(56, 16);
            this.rbtnOr2.Name = "rbtnOr2";
            this.rbtnOr2.Size = new System.Drawing.Size(36, 17);
            this.rbtnOr2.TabIndex = 10;
            this.rbtnOr2.TabStop = true;
            this.rbtnOr2.Text = "Or";
            this.rbtnOr2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnAnd1);
            this.groupBox1.Controls.Add(this.rbtnOr1);
            this.groupBox1.Location = new System.Drawing.Point(238, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 45);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // rbtnAnd1
            // 
            this.rbtnAnd1.AutoSize = true;
            this.rbtnAnd1.Location = new System.Drawing.Point(12, 16);
            this.rbtnAnd1.Name = "rbtnAnd1";
            this.rbtnAnd1.Size = new System.Drawing.Size(44, 17);
            this.rbtnAnd1.TabIndex = 7;
            this.rbtnAnd1.TabStop = true;
            this.rbtnAnd1.Text = "And";
            this.rbtnAnd1.UseVisualStyleBackColor = true;
            // 
            // rbtnOr1
            // 
            this.rbtnOr1.AutoSize = true;
            this.rbtnOr1.Location = new System.Drawing.Point(62, 16);
            this.rbtnOr1.Name = "rbtnOr1";
            this.rbtnOr1.Size = new System.Drawing.Size(36, 17);
            this.rbtnOr1.TabIndex = 8;
            this.rbtnOr1.TabStop = true;
            this.rbtnOr1.Text = "Or";
            this.rbtnOr1.UseVisualStyleBackColor = true;
            // 
            // txtSpread
            // 
            this.txtSpread.Location = new System.Drawing.Point(763, 16);
            this.txtSpread.Name = "txtSpread";
            this.txtSpread.Size = new System.Drawing.Size(100, 20);
            this.txtSpread.TabIndex = 13;
            // 
            // cmbSpreadCondition
            // 
            this.cmbSpreadCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpreadCondition.FormattingEnabled = true;
            this.cmbSpreadCondition.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<="});
            this.cmbSpreadCondition.Location = new System.Drawing.Point(715, 15);
            this.cmbSpreadCondition.Name = "cmbSpreadCondition";
            this.cmbSpreadCondition.Size = new System.Drawing.Size(33, 21);
            this.cmbSpreadCondition.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(668, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Spread";
            // 
            // txtLTQ
            // 
            this.txtLTQ.Location = new System.Drawing.Point(436, 14);
            this.txtLTQ.Name = "txtLTQ";
            this.txtLTQ.Size = new System.Drawing.Size(100, 20);
            this.txtLTQ.TabIndex = 6;
            // 
            // cmbLtqCondition
            // 
            this.cmbLtqCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLtqCondition.FormattingEnabled = true;
            this.cmbLtqCondition.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<="});
            this.cmbLtqCondition.Location = new System.Drawing.Point(388, 13);
            this.cmbLtqCondition.Name = "cmbLtqCondition";
            this.cmbLtqCondition.Size = new System.Drawing.Size(33, 21);
            this.cmbLtqCondition.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LTQ";
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(881, 14);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(75, 23);
            this.btnApplyFilter.TabIndex = 3;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // txtLTP
            // 
            this.txtLTP.Location = new System.Drawing.Point(117, 14);
            this.txtLTP.Name = "txtLTP";
            this.txtLTP.Size = new System.Drawing.Size(100, 20);
            this.txtLTP.TabIndex = 2;
            // 
            // cmbLtpCondition
            // 
            this.cmbLtpCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLtpCondition.FormattingEnabled = true;
            this.cmbLtpCondition.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<="});
            this.cmbLtpCondition.Location = new System.Drawing.Point(68, 12);
            this.cmbLtpCondition.Name = "cmbLtpCondition";
            this.cmbLtpCondition.Size = new System.Drawing.Size(33, 21);
            this.cmbLtpCondition.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "LTP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(962, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ScannerBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 309);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ScannerBox";
            this.Text = "ScannerBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScannerBox_FormClosing);
            this.Load += new System.EventHandler(this.ScannerBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtLTP;
        private System.Windows.Forms.ComboBox cmbLtpCondition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.TextBox txtLTQ;
        private System.Windows.Forms.ComboBox cmbLtqCondition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnOr1;
        private System.Windows.Forms.RadioButton rbtnAnd1;
        private System.Windows.Forms.RadioButton rbtnOr2;
        private System.Windows.Forms.RadioButton rbtnAnd2;
        private System.Windows.Forms.TextBox txtSpread;
        private System.Windows.Forms.ComboBox cmbSpreadCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReset;
    }
}