namespace DataverseStorageCleaner.Views
{
    partial class JobsView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView gridJobs;

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
            this.gridJobs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).BeginInit();
            this.SuspendLayout();
            //
            // gridJobs
            //
            this.gridJobs.AllowUserToAddRows = false;
            this.gridJobs.AllowUserToDeleteRows = false;
            this.gridJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Name", Name = "colName", AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill },
            new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Status", Name = "colStatus", Width = 100 },
            new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Next run", Name = "colNextRun", Width = 140 },
            new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Last run", Name = "colLastRun", Width = 140 },
            new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Records affected", Name = "colRecords", Width = 120 }
            });
            this.gridJobs.Location = new System.Drawing.Point(16, 16);
            this.gridJobs.Name = "gridJobs";
            this.gridJobs.ReadOnly = true;
            this.gridJobs.RowHeadersVisible = false;
            this.gridJobs.Size = new System.Drawing.Size(600, 360);
            this.gridJobs.TabIndex = 0;
            //
            // JobsView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridJobs);
            this.Name = "JobsView";
            this.Size = new System.Drawing.Size(632, 392);
            ((System.ComponentModel.ISupportInitialize)(this.gridJobs)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
