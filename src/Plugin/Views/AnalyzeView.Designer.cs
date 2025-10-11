namespace DataverseStorageCleaner.Views
{
    partial class AnalyzeView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.Button btnViewSample;
        private System.Windows.Forms.Button btnCreateRecipe;
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
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.btnViewSample = new System.Windows.Forms.Button();
            this.btnCreateRecipe = new System.Windows.Forms.Button();
            this.grpOrgSettings = new System.Windows.Forms.GroupBox();
            this.gridOrgSettings = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).BeginInit();
            this.SuspendLayout();
            //
            // flowCards
            //
            this.flowCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left))));
            this.flowCards.AutoScroll = true;
            this.flowCards.Location = new System.Drawing.Point(16, 56);
            this.flowCards.Name = "flowCards";
            this.flowCards.Size = new System.Drawing.Size(300, 320);
            this.flowCards.TabIndex = 0;
            //
            // btnViewSample
            //
            this.btnViewSample.Location = new System.Drawing.Point(16, 16);
            this.btnViewSample.Name = "btnViewSample";
            this.btnViewSample.Size = new System.Drawing.Size(120, 28);
            this.btnViewSample.TabIndex = 1;
            this.btnViewSample.Text = "View sample";
            this.btnViewSample.UseVisualStyleBackColor = true;
            this.btnViewSample.Click += new System.EventHandler(this.btnViewSample_Click);
            //
            // btnCreateRecipe
            //
            this.btnCreateRecipe.Location = new System.Drawing.Point(152, 16);
            this.btnCreateRecipe.Name = "btnCreateRecipe";
            this.btnCreateRecipe.Size = new System.Drawing.Size(140, 28);
            this.btnCreateRecipe.TabIndex = 2;
            this.btnCreateRecipe.Text = "Create recipe...";
            this.btnCreateRecipe.UseVisualStyleBackColor = true;
            this.btnCreateRecipe.Click += new System.EventHandler(this.btnCreateRecipe_Click);
            //
            // grpOrgSettings
            //
            this.grpOrgSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrgSettings.Location = new System.Drawing.Point(328, 48);
            this.grpOrgSettings.Name = "grpOrgSettings";
            this.grpOrgSettings.Size = new System.Drawing.Size(288, 328);
            this.grpOrgSettings.TabIndex = 3;
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
            // AnalyzeView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.grpOrgSettings.Controls.Add(this.gridOrgSettings);
            this.Controls.Add(this.grpOrgSettings);
            this.Controls.Add(this.btnCreateRecipe);
            this.Controls.Add(this.btnViewSample);
            this.Controls.Add(this.flowCards);
            this.Name = "AnalyzeView";
            this.Size = new System.Drawing.Size(632, 392);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrgSettings)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
