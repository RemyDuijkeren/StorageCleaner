namespace StorageCleaner.Actions.CleanUpSystemJobs
{
    partial class CleanUpSystemJobsActionControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnViewSample;
        private System.Windows.Forms.GroupBox grpOrgSettings;
        private System.Windows.Forms.DataGridView gridOrgSettings;

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
            this.btnViewSample = new System.Windows.Forms.Button();
            this.grpOrgSettings = new System.Windows.Forms.GroupBox();
            this.gridOrgSettings = new System.Windows.Forms.DataGridView();
            this.grpOrgSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).BeginInit();
            this.SuspendLayout();
            //
            // btnViewSample
            //
            this.btnViewSample.Location = new System.Drawing.Point(112, 72);
            this.btnViewSample.Name = "btnViewSample";
            this.btnViewSample.Size = new System.Drawing.Size(120, 23);
            this.btnViewSample.TabIndex = 3;
            this.btnViewSample.Text = "View sample";
            this.btnViewSample.UseVisualStyleBackColor = true;
            this.btnViewSample.Click += new System.EventHandler(this.btnViewSample_Click);
            //
            // grpOrgSettings
            //
            this.grpOrgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrgSettings.Controls.Add(this.gridOrgSettings);
            this.grpOrgSettings.Location = new System.Drawing.Point(15, 112);
            this.grpOrgSettings.Name = "grpOrgSettings";
            this.grpOrgSettings.Size = new System.Drawing.Size(2750, 644);
            this.grpOrgSettings.TabIndex = 4;
            this.grpOrgSettings.TabStop = false;
            this.grpOrgSettings.Text = "Organization settings";
            //
            // gridOrgSettings
            //
            this.gridOrgSettings.AllowUserToAddRows = false;
            this.gridOrgSettings.AllowUserToDeleteRows = false;
            this.gridOrgSettings.AllowUserToResizeRows = false;
            this.gridOrgSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrgSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            var colSetting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSetting.HeaderText = "Setting";
            colSetting.Name = "colSetting";
            colSetting.FillWeight = 45F;

            var colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colValue.HeaderText = "Value";
            colValue.Name = "colValue";
            colValue.FillWeight = 55F;

            this.gridOrgSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colSetting,
                colValue
            });

            this.gridOrgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrgSettings.Location = new System.Drawing.Point(3, 16);
            this.gridOrgSettings.MultiSelect = false;
            this.gridOrgSettings.Name = "gridOrgSettings";
            this.gridOrgSettings.ReadOnly = true;
            this.gridOrgSettings.RowHeadersVisible = false;
            this.gridOrgSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrgSettings.Size = new System.Drawing.Size(2744, 625);
            this.gridOrgSettings.TabIndex = 0;
            //
            // CleanUpSystemJobsActionControl
            //
            this.Controls.Add(this.grpOrgSettings);
            this.Controls.Add(this.btnViewSample);
            this.Name = "CleanUpSystemJobsActionControl";
            this.Size = new System.Drawing.Size(2782, 776);
            this.grpOrgSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
