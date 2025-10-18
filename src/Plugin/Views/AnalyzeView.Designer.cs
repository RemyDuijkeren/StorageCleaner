namespace StorageCleaner.Views
{
    partial class AnalyzeView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private System.Windows.Forms.Button btnCreateAction;

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
            this.btnCreateAction = new System.Windows.Forms.Button();
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
            // btnCreateAction
            //
            this.btnCreateAction.Location = new System.Drawing.Point(16, 16);
            this.btnCreateAction.Name = "btnCreateAction";
            this.btnCreateAction.Size = new System.Drawing.Size(140, 28);
            this.btnCreateAction.TabIndex = 2;
            this.btnCreateAction.Text = "Create action...";
            this.btnCreateAction.UseVisualStyleBackColor = true;
            this.btnCreateAction.Click += new System.EventHandler(this.btnCreateAction_Click);
            //
            // AnalyzeView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreateAction);
            this.Controls.Add(this.flowCards);
            this.Name = "AnalyzeView";
            this.Size = new System.Drawing.Size(632, 392);
            this.ResumeLayout(false);
        }
    }
}
