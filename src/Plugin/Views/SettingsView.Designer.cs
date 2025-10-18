namespace StorageCleaner.Views
{
    partial class SettingsView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NumericUpDown numBatchSize;
        private System.Windows.Forms.NumericUpDown numInterBatchDelayMs;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.Label lblInterBatchDelay;
        private System.Windows.Forms.Label lblInclude;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.Button btnExportManifest;
        private System.Windows.Forms.Button btnImportManifest;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numBatchSize = new System.Windows.Forms.NumericUpDown();
            this.numInterBatchDelayMs = new System.Windows.Forms.NumericUpDown();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.lblInterBatchDelay = new System.Windows.Forms.Label();
            this.lblInclude = new System.Windows.Forms.Label();
            this.lblExclude = new System.Windows.Forms.Label();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.btnExportManifest = new System.Windows.Forms.Button();
            this.btnImportManifest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterBatchDelayMs)).BeginInit();
            this.SuspendLayout();
            // 
            // numBatchSize
            // 
            this.numBatchSize.Location = new System.Drawing.Point(160, 16);
            this.numBatchSize.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numBatchSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numBatchSize.Name = "numBatchSize";
            this.numBatchSize.Size = new System.Drawing.Size(120, 20);
            this.numBatchSize.TabIndex = 0;
            this.numBatchSize.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numInterBatchDelayMs
            // 
            this.numInterBatchDelayMs.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            this.numInterBatchDelayMs.Location = new System.Drawing.Point(160, 48);
            this.numInterBatchDelayMs.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            this.numInterBatchDelayMs.Name = "numInterBatchDelayMs";
            this.numInterBatchDelayMs.Size = new System.Drawing.Size(120, 20);
            this.numInterBatchDelayMs.TabIndex = 1;
            this.numInterBatchDelayMs.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.AutoSize = true;
            this.lblBatchSize.Location = new System.Drawing.Point(16, 18);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(112, 13);
            this.lblBatchSize.TabIndex = 2;
            this.lblBatchSize.Text = "Batch size (records)";
            // 
            // lblInterBatchDelay
            // 
            this.lblInterBatchDelay.AutoSize = true;
            this.lblInterBatchDelay.Location = new System.Drawing.Point(16, 50);
            this.lblInterBatchDelay.Name = "lblInterBatchDelay";
            this.lblInterBatchDelay.Size = new System.Drawing.Size(126, 13);
            this.lblInterBatchDelay.TabIndex = 3;
            this.lblInterBatchDelay.Text = "Inter-batch delay (ms)";
            // 
            // lblInclude
            // 
            this.lblInclude.AutoSize = true;
            this.lblInclude.Location = new System.Drawing.Point(16, 88);
            this.lblInclude.Name = "lblInclude";
            this.lblInclude.Size = new System.Drawing.Size(120, 13);
            this.lblInclude.TabIndex = 4;
            this.lblInclude.Text = "Include entities (one per line)";
            // 
            // lblExclude
            // 
            this.lblExclude.AutoSize = true;
            this.lblExclude.Location = new System.Drawing.Point(320, 88);
            this.lblExclude.Name = "lblExclude";
            this.lblExclude.Size = new System.Drawing.Size(122, 13);
            this.lblExclude.TabIndex = 5;
            this.lblExclude.Text = "Exclude entities (one per line)";
            // 
            // txtInclude
            // 
            this.txtInclude.AcceptsReturn = true;
            this.txtInclude.AcceptsTab = true;
            this.txtInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInclude.Location = new System.Drawing.Point(16, 108);
            this.txtInclude.Multiline = true;
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInclude.Size = new System.Drawing.Size(280, 260);
            this.txtInclude.TabIndex = 6;
            // 
            // txtExclude
            // 
            this.txtExclude.AcceptsReturn = true;
            this.txtExclude.AcceptsTab = true;
            this.txtExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExclude.Location = new System.Drawing.Point(320, 108);
            this.txtExclude.Multiline = true;
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExclude.Size = new System.Drawing.Size(280, 260);
            this.txtExclude.TabIndex = 7;
            // 
            // btnExportManifest
            // 
            this.btnExportManifest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportManifest.Location = new System.Drawing.Point(424, 376);
            this.btnExportManifest.Name = "btnExportManifest";
            this.btnExportManifest.Size = new System.Drawing.Size(84, 28);
            this.btnExportManifest.TabIndex = 8;
            this.btnExportManifest.Text = "Export";
            this.btnExportManifest.UseVisualStyleBackColor = true;
            this.btnExportManifest.Click += new System.EventHandler(this.btnExportManifest_Click);
            // 
            // btnImportManifest
            // 
            this.btnImportManifest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportManifest.Location = new System.Drawing.Point(516, 376);
            this.btnImportManifest.Name = "btnImportManifest";
            this.btnImportManifest.Size = new System.Drawing.Size(84, 28);
            this.btnImportManifest.TabIndex = 9;
            this.btnImportManifest.Text = "Import";
            this.btnImportManifest.UseVisualStyleBackColor = true;
            this.btnImportManifest.Click += new System.EventHandler(this.btnImportManifest_Click);
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnImportManifest);
            this.Controls.Add(this.btnExportManifest);
            this.Controls.Add(this.txtExclude);
            this.Controls.Add(this.txtInclude);
            this.Controls.Add(this.lblExclude);
            this.Controls.Add(this.lblInclude);
            this.Controls.Add(this.lblInterBatchDelay);
            this.Controls.Add(this.lblBatchSize);
            this.Controls.Add(this.numInterBatchDelayMs);
            this.Controls.Add(this.numBatchSize);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(632, 412);
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterBatchDelayMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
