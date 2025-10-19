namespace StorageCleaner.Actions.SystemJobCleanup
{
    partial class SystemJobCleanupActionControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.GroupBox grpOrgSettings;
        private System.Windows.Forms.DataGridView gridOrgSettings;
        private System.Windows.Forms.GroupBox grpSystemJobCleanup;
        private System.Windows.Forms.CheckBox chkEnableSystemJobCleanup;
        private System.Windows.Forms.Label lblSucceeded;
        private System.Windows.Forms.NumericUpDown numSucceededDays;
        private System.Windows.Forms.Label lblCanceled;
        private System.Windows.Forms.NumericUpDown numCanceledDays;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.NumericUpDown numFailedDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;

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
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.grpOrgSettings = new System.Windows.Forms.GroupBox();
            this.gridOrgSettings = new System.Windows.Forms.DataGridView();
            this.colSetting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpSystemJobCleanup = new System.Windows.Forms.GroupBox();
            this.numFailedDays = new System.Windows.Forms.NumericUpDown();
            this.lblFailed = new System.Windows.Forms.Label();
            this.numCanceledDays = new System.Windows.Forms.NumericUpDown();
            this.lblCanceled = new System.Windows.Forms.Label();
            this.numSucceededDays = new System.Windows.Forms.NumericUpDown();
            this.lblSucceeded = new System.Windows.Forms.Label();
            this.chkEnableSystemJobCleanup = new System.Windows.Forms.CheckBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.grpOrgSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).BeginInit();
            this.grpSystemJobCleanup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFailedDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCanceledDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSucceededDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(15, 238);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 23);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.AnalyzeClick);
            // 
            // grpOrgSettings
            // 
            this.grpOrgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrgSettings.Controls.Add(this.gridOrgSettings);
            this.grpOrgSettings.Location = new System.Drawing.Point(15, 337);
            this.grpOrgSettings.Name = "grpOrgSettings";
            this.grpOrgSettings.Size = new System.Drawing.Size(604, 419);
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
            this.gridOrgSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colSetting, this.colValue });
            this.gridOrgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrgSettings.Location = new System.Drawing.Point(3, 16);
            this.gridOrgSettings.MultiSelect = false;
            this.gridOrgSettings.Name = "gridOrgSettings";
            this.gridOrgSettings.ReadOnly = true;
            this.gridOrgSettings.RowHeadersVisible = false;
            this.gridOrgSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrgSettings.Size = new System.Drawing.Size(598, 400);
            this.gridOrgSettings.TabIndex = 0;
            // 
            // colSetting
            // 
            this.colSetting.FillWeight = 45F;
            this.colSetting.HeaderText = "Setting";
            this.colSetting.Name = "colSetting";
            this.colSetting.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.FillWeight = 55F;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // grpSystemJobCleanup
            // 
            this.grpSystemJobCleanup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSystemJobCleanup.Controls.Add(this.numFailedDays);
            this.grpSystemJobCleanup.Controls.Add(this.lblFailed);
            this.grpSystemJobCleanup.Controls.Add(this.numCanceledDays);
            this.grpSystemJobCleanup.Controls.Add(this.lblCanceled);
            this.grpSystemJobCleanup.Controls.Add(this.numSucceededDays);
            this.grpSystemJobCleanup.Controls.Add(this.lblSucceeded);
            this.grpSystemJobCleanup.Controls.Add(this.chkEnableSystemJobCleanup);
            this.grpSystemJobCleanup.Location = new System.Drawing.Point(15, 48);
            this.grpSystemJobCleanup.Name = "grpSystemJobCleanup";
            this.grpSystemJobCleanup.Size = new System.Drawing.Size(604, 174);
            this.grpSystemJobCleanup.TabIndex = 5;
            this.grpSystemJobCleanup.TabStop = false;
            this.grpSystemJobCleanup.Text = "System Job Cleanup";
            // 
            // numFailedDays
            // 
            this.numFailedDays.Location = new System.Drawing.Point(270, 109);
            this.numFailedDays.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            this.numFailedDays.Name = "numFailedDays";
            this.numFailedDays.Size = new System.Drawing.Size(80, 20);
            this.numFailedDays.TabIndex = 6;
            this.numFailedDays.ValueChanged += new System.EventHandler(this.numFailedDays_ValueChanged);
            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.Location = new System.Drawing.Point(13, 111);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(215, 13);
            this.lblFailed.TabIndex = 5;
            this.lblFailed.Text = "Failed persistence (days, 0 - 180, default 60)";
            // 
            // numCanceledDays
            // 
            this.numCanceledDays.Location = new System.Drawing.Point(270, 81);
            this.numCanceledDays.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            this.numCanceledDays.Name = "numCanceledDays";
            this.numCanceledDays.Size = new System.Drawing.Size(80, 20);
            this.numCanceledDays.TabIndex = 4;
            this.numCanceledDays.ValueChanged += new System.EventHandler(this.numCanceledDays_ValueChanged);
            // 
            // lblCanceled
            // 
            this.lblCanceled.AutoSize = true;
            this.lblCanceled.Location = new System.Drawing.Point(13, 83);
            this.lblCanceled.Name = "lblCanceled";
            this.lblCanceled.Size = new System.Drawing.Size(232, 13);
            this.lblCanceled.TabIndex = 3;
            this.lblCanceled.Text = "Canceled persistence (days, 0 - 180, default 60)";
            // 
            // numSucceededDays
            // 
            this.numSucceededDays.Location = new System.Drawing.Point(270, 50);
            this.numSucceededDays.Maximum = new decimal(new int[] { 90, 0, 0, 0 });
            this.numSucceededDays.Name = "numSucceededDays";
            this.numSucceededDays.Size = new System.Drawing.Size(80, 20);
            this.numSucceededDays.TabIndex = 2;
            this.numSucceededDays.ValueChanged += new System.EventHandler(this.numSucceededDays_ValueChanged);
            // 
            // lblSucceeded
            // 
            this.lblSucceeded.AutoSize = true;
            this.lblSucceeded.Location = new System.Drawing.Point(13, 52);
            this.lblSucceeded.Name = "lblSucceeded";
            this.lblSucceeded.Size = new System.Drawing.Size(236, 13);
            this.lblSucceeded.TabIndex = 1;
            this.lblSucceeded.Text = "Succeeded persistence (days, 0 - 90, default 30)";
            // 
            // chkEnableSystemJobCleanup
            // 
            this.chkEnableSystemJobCleanup.AutoSize = true;
            this.chkEnableSystemJobCleanup.Location = new System.Drawing.Point(16, 22);
            this.chkEnableSystemJobCleanup.Name = "chkEnableSystemJobCleanup";
            this.chkEnableSystemJobCleanup.Size = new System.Drawing.Size(206, 17);
            this.chkEnableSystemJobCleanup.TabIndex = 0;
            this.chkEnableSystemJobCleanup.Text = "Enable automatic System Job cleanup";
            this.chkEnableSystemJobCleanup.UseVisualStyleBackColor = true;
            this.chkEnableSystemJobCleanup.CheckedChanged += new System.EventHandler(this.chkEnableSystemJobCleanup_CheckedChanged);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(260, 238);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(120, 23);
            this.btnClean.TabIndex = 6;
            this.btnClean.Text = "Clean / Apply";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.CleanClick);
            // 
            // SystemJobCleanupActionControl
            // 
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.grpSystemJobCleanup);
            this.Controls.Add(this.grpOrgSettings);
            this.Controls.Add(this.btnAnalyze);
            this.Name = "SystemJobCleanupActionControl";
            this.Size = new System.Drawing.Size(636, 776);
            this.grpOrgSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).EndInit();
            this.grpSystemJobCleanup.ResumeLayout(false);
            this.grpSystemJobCleanup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFailedDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCanceledDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSucceededDays)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnClean;
    }
}
