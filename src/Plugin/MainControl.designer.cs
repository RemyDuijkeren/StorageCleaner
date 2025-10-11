namespace DataverseStorageCleaner
{
    partial class MainControl
    {
        private System.ComponentModel.IContainer components = null;

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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSample = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabAnalyze = new System.Windows.Forms.TabPage();
            this.tabRecipes = new System.Windows.Forms.TabPage();
            this.tabJobs = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.analyzeView = new DataverseStorageCleaner.Views.AnalyzeView();
            this.recipesView = new DataverseStorageCleaner.Views.RecipesView();
            this.jobsView = new DataverseStorageCleaner.Views.JobsView();
            this.settingsView = new DataverseStorageCleaner.Views.SettingsView();
            this.toolStripMenu.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabAnalyze.SuspendLayout();
            this.tabRecipes.SuspendLayout();
            this.tabJobs.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStripMenu
            //
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tsbClose, this.tssSeparator1, this.tsbSample });
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(800, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            //
            // tsbClose
            //
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(95, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            //
            // tssSeparator1
            //
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            //
            // tsbSample
            //
            this.tsbSample.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSample.Name = "tsbSample";
            this.tsbSample.Size = new System.Drawing.Size(51, 22);
            this.tsbSample.Text = "Try me";
            this.tsbSample.Click += new System.EventHandler(this.tsbSample_Click);
            //
            // tabMain
            //
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabAnalyze);
            this.tabMain.Controls.Add(this.tabRecipes);
            this.tabMain.Controls.Add(this.tabJobs);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Location = new System.Drawing.Point(0, 28);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(800, 472);
            this.tabMain.TabIndex = 5;
            //
            // tabAnalyze
            //
            this.tabAnalyze.Controls.Add(this.analyzeView);
            this.tabAnalyze.Location = new System.Drawing.Point(4, 22);
            this.tabAnalyze.Name = "tabAnalyze";
            this.tabAnalyze.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnalyze.Size = new System.Drawing.Size(792, 446);
            this.tabAnalyze.TabIndex = 0;
            this.tabAnalyze.Text = "Analyze";
            this.tabAnalyze.UseVisualStyleBackColor = true;
            //
            // tabRecipes
            //
            this.tabRecipes.Controls.Add(this.recipesView);
            this.tabRecipes.Location = new System.Drawing.Point(4, 22);
            this.tabRecipes.Name = "tabRecipes";
            this.tabRecipes.Padding = new System.Windows.Forms.Padding(3);
            this.tabRecipes.Size = new System.Drawing.Size(792, 446);
            this.tabRecipes.TabIndex = 1;
            this.tabRecipes.Text = "Recipes";
            this.tabRecipes.UseVisualStyleBackColor = true;
            //
            // tabJobs
            //
            this.tabJobs.Controls.Add(this.jobsView);
            this.tabJobs.Location = new System.Drawing.Point(4, 22);
            this.tabJobs.Name = "tabJobs";
            this.tabJobs.Padding = new System.Windows.Forms.Padding(3);
            this.tabJobs.Size = new System.Drawing.Size(792, 446);
            this.tabJobs.TabIndex = 2;
            this.tabJobs.Text = "Jobs";
            this.tabJobs.UseVisualStyleBackColor = true;
            //
            // tabSettings
            //
            this.tabSettings.Controls.Add(this.settingsView);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(792, 446);
            this.tabSettings.TabIndex = 3;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            //
            // analyzeView
            //
            this.analyzeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeView.Location = new System.Drawing.Point(3, 3);
            this.analyzeView.Name = "analyzeView";
            this.analyzeView.Size = new System.Drawing.Size(786, 440);
            this.analyzeView.TabIndex = 0;
            //
            // recipesView
            //
            this.recipesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recipesView.Location = new System.Drawing.Point(3, 3);
            this.recipesView.Name = "recipesView";
            this.recipesView.Size = new System.Drawing.Size(786, 440);
            this.recipesView.TabIndex = 0;
            //
            // jobsView
            //
            this.jobsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobsView.Location = new System.Drawing.Point(3, 3);
            this.jobsView.Name = "jobsView";
            this.jobsView.Size = new System.Drawing.Size(786, 440);
            this.jobsView.TabIndex = 0;
            //
            // settingsView
            //
            this.settingsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsView.Location = new System.Drawing.Point(3, 3);
            this.settingsView.Name = "settingsView";
            this.settingsView.Size = new System.Drawing.Size(786, 440);
            this.settingsView.TabIndex = 0;
            //
            // MainControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.Load += new System.EventHandler(this.MainControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabAnalyze.ResumeLayout(false);
            this.tabRecipes.ResumeLayout(false);
            this.tabJobs.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabAnalyze;
        private System.Windows.Forms.TabPage tabRecipes;
        private System.Windows.Forms.TabPage tabJobs;
        private System.Windows.Forms.TabPage tabSettings;
        private DataverseStorageCleaner.Views.AnalyzeView analyzeView;
        private DataverseStorageCleaner.Views.RecipesView recipesView;
        private DataverseStorageCleaner.Views.JobsView jobsView;
        private DataverseStorageCleaner.Views.SettingsView settingsView;

        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSample;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
    }
}
