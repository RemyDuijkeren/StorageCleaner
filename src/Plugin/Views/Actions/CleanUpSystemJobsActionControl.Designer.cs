using System.Drawing;
using System.Windows.Forms;

namespace DataverseStorageCleaner.Views.Actions
{
    partial class CleanUpSystemJobsActionControl
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUrl;
        private Button btnSave;
        private Label lblUrl;
        private Button btnViewSample;
        private GroupBox grpOrgSettings;
        private DataGridView gridOrgSettings;

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
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnViewSample = new System.Windows.Forms.Button();
            this.grpOrgSettings = new System.Windows.Forms.GroupBox();
            this.gridOrgSettings = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).BeginInit();
            this.SuspendLayout();
            //
            // CleanUpSystemJobsActionControl
            //
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            //
            // lblUrl
            //
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(12, 16);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(114, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Last used Org URL:";
            //
            // txtUrl
            //
            this.txtUrl.Location = new System.Drawing.Point(15, 36);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(400, 20);
            this.txtUrl.TabIndex = 1;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(15, 72);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.grpOrgSettings.Location = new System.Drawing.Point(15, 112);
            this.grpOrgSettings.Name = "grpOrgSettings";
            this.grpOrgSettings.Size = new System.Drawing.Size(600, 260);
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
            this.gridOrgSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Setting", Name = "colSetting", FillWeight = 45 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Value", Name = "colValue", FillWeight = 55 }
            });
            this.gridOrgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrgSettings.Location = new System.Drawing.Point(3, 16);
            this.gridOrgSettings.MultiSelect = false;
            this.gridOrgSettings.Name = "gridOrgSettings";
            this.gridOrgSettings.ReadOnly = true;
            this.gridOrgSettings.RowHeadersVisible = false;
            this.gridOrgSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrgSettings.TabIndex = 0;
            //
            // CleanUpSystemJobsActionControl
            //
            this.grpOrgSettings.Controls.Add(this.gridOrgSettings);
            this.Controls.Add(this.grpOrgSettings);
            this.Controls.Add(this.btnViewSample);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnSave);
            this.Name = "CleanUpSystemJobsActionControl";
            this.Size = new System.Drawing.Size(632, 392);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
