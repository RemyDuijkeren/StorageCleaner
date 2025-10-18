using System.Windows.Forms;

namespace StorageCleaner.Views
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

            var colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            var colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.Width = 100;

            var colNextRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNextRun.HeaderText = "Next run";
            colNextRun.Name = "colNextRun";
            colNextRun.Width = 140;

            var colLastRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLastRun.HeaderText = "Last run";
            colLastRun.Name = "colLastRun";
            colLastRun.Width = 140;

            var colRecords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRecords.HeaderText = "Records affected";
            colRecords.Name = "colRecords";
            colRecords.Width = 120;

            this.gridJobs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colName,
                colStatus,
                colNextRun,
                colLastRun,
                colRecords
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
