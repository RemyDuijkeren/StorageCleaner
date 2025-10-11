namespace DataverseStorageCleaner.Views
{
    partial class RecipesView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listRecipes;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colEnabled;
        private System.Windows.Forms.ColumnHeader colRetention;
        private System.Windows.Forms.ColumnHeader colScope;
        private System.Windows.Forms.Button btnCreateOrUpdateJobs;

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
            this.listRecipes = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colEnabled = new System.Windows.Forms.ColumnHeader();
            this.colRetention = new System.Windows.Forms.ColumnHeader();
            this.colScope = new System.Windows.Forms.ColumnHeader();
            this.btnCreateOrUpdateJobs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listRecipes
            // 
            this.listRecipes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listRecipes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colEnabled,
            this.colRetention,
            this.colScope});
            this.listRecipes.FullRowSelect = true;
            this.listRecipes.GridLines = true;
            this.listRecipes.HideSelection = false;
            this.listRecipes.Location = new System.Drawing.Point(16, 16);
            this.listRecipes.MultiSelect = false;
            this.listRecipes.Name = "listRecipes";
            this.listRecipes.Size = new System.Drawing.Size(600, 328);
            this.listRecipes.TabIndex = 0;
            this.listRecipes.UseCompatibleStateImageBehavior = false;
            this.listRecipes.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Recipe";
            this.colName.Width = 220;
            // 
            // colEnabled
            // 
            this.colEnabled.Text = "Enabled";
            this.colEnabled.Width = 80;
            // 
            // colRetention
            // 
            this.colRetention.Text = "Retention";
            this.colRetention.Width = 140;
            // 
            // colScope
            // 
            this.colScope.Text = "Scope";
            this.colScope.Width = 140;
            // 
            // btnCreateOrUpdateJobs
            // 
            this.btnCreateOrUpdateJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateOrUpdateJobs.Location = new System.Drawing.Point(436, 352);
            this.btnCreateOrUpdateJobs.Name = "btnCreateOrUpdateJobs";
            this.btnCreateOrUpdateJobs.Size = new System.Drawing.Size(180, 28);
            this.btnCreateOrUpdateJobs.TabIndex = 1;
            this.btnCreateOrUpdateJobs.Text = "Create/Update jobs";
            this.btnCreateOrUpdateJobs.UseVisualStyleBackColor = true;
            this.btnCreateOrUpdateJobs.Click += new System.EventHandler(this.btnCreateOrUpdateJobs_Click);
            // 
            // RecipesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreateOrUpdateJobs);
            this.Controls.Add(this.listRecipes);
            this.Name = "RecipesView";
            this.Size = new System.Drawing.Size(632, 392);
            this.ResumeLayout(false);

        }
    }
}
