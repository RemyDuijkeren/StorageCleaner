namespace StorageCleaner.Views
{
    partial class ActionsView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListBox lstActions;
        private System.Windows.Forms.Panel pnlHost;

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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lstActions = new System.Windows.Forms.ListBox();
            this.pnlHost = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.lstActions);
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.pnlHost);
            this.splitContainer.Size = new System.Drawing.Size(632, 344);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 0;
            //
            // lstActions
            //
            this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstActions.FormattingEnabled = true;
            this.lstActions.IntegralHeight = false;
            this.lstActions.Name = "lstActions";
            this.lstActions.TabIndex = 0;
            this.lstActions.DisplayMember = "Name";
            this.lstActions.SelectedIndexChanged += new System.EventHandler(this.lstActions_SelectedIndexChanged);
            //
            // pnlHost
            //
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.TabIndex = 0;
            //
            // ActionsView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ActionsView";
            this.Size = new System.Drawing.Size(632, 392);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
