namespace StorageCleaner.Actions
{
    partial class ActionControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel layoutRoot;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.GroupBox grpHelp;
        private System.Windows.Forms.RichTextBox rtbHelp;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnClean;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.grpHelp = new System.Windows.Forms.GroupBox();
            this.rtbHelp = new System.Windows.Forms.RichTextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.layoutRoot.SuspendLayout();
            this.grpHelp.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutRoot
            // 
            this.layoutRoot.ColumnCount = 1;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.Controls.Add(this.pnlContent, 0, 0);
            this.layoutRoot.Controls.Add(this.grpHelp, 0, 1);
            this.layoutRoot.Controls.Add(this.tlpButtons, 0, 2);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.RowCount = 3;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutRoot.Size = new System.Drawing.Size(600, 400);
            this.layoutRoot.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(594, 212);
            this.pnlContent.TabIndex = 0;
            // 
            // grpHelp
            // 
            this.grpHelp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpHelp.Controls.Add(this.rtbHelp);
            this.grpHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHelp.Location = new System.Drawing.Point(3, 221);
            this.grpHelp.Name = "grpHelp";
            this.grpHelp.Padding = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this.grpHelp.Size = new System.Drawing.Size(594, 134);
            this.grpHelp.TabIndex = 1;
            this.grpHelp.TabStop = false;
            this.grpHelp.Text = "Help";
            // 
            // rtbHelp
            // 
            this.rtbHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHelp.Location = new System.Drawing.Point(8, 19);
            this.rtbHelp.MinimumSize = new System.Drawing.Size(0, 100);
            this.rtbHelp.Name = "rtbHelp";
            this.rtbHelp.ReadOnly = true;
            this.rtbHelp.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbHelp.Size = new System.Drawing.Size(578, 107);
            this.rtbHelp.TabIndex = 0;
            this.rtbHelp.Text = "";
            // 
            // tlpButtons
            // 
            this.tlpButtons.AutoSize = true;
            this.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpButtons.ColumnCount = 3;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.btnAnalyze, 0, 0);
            this.tlpButtons.Controls.Add(this.btnClean, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 361);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpButtons.Size = new System.Drawing.Size(594, 33);
            this.tlpButtons.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyze.AutoSize = true;
            this.btnAnalyze.Location = new System.Drawing.Point(3, 7);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 23);
            this.btnAnalyze.TabIndex = 0;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnClean
            // 
            this.btnClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClean.AutoSize = true;
            this.btnClean.Enabled = false;
            this.btnClean.Location = new System.Drawing.Point(471, 7);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(120, 23);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // ActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutRoot);
            this.Name = "ActionControl";
            this.Size = new System.Drawing.Size(600, 400);
            this.layoutRoot.ResumeLayout(false);
            this.layoutRoot.PerformLayout();
            this.grpHelp.ResumeLayout(false);
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
