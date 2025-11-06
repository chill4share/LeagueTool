namespace LeagueTool.Tabs
{
    partial class ResetTab
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.resetButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();

            this.lblHeaderChals = new System.Windows.Forms.Label();
            this.picChal1 = new System.Windows.Forms.PictureBox();
            this.picChal2 = new System.Windows.Forms.PictureBox();
            this.picChal3 = new System.Windows.Forms.PictureBox();
            this.lblChal1 = new System.Windows.Forms.Label();
            this.lblChal2 = new System.Windows.Forms.Label();
            this.lblChal3 = new System.Windows.Forms.Label();

            this.groupDecor = new System.Windows.Forms.GroupBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitleValue = new System.Windows.Forms.Label();
            this.lblCrest = new System.Windows.Forms.Label();
            this.picCrestBorder = new System.Windows.Forms.PictureBox();
            this.lblBanner = new System.Windows.Forms.Label();
            this.picBanner = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.picChal1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChal2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChal3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrestBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).BeginInit();
            this.groupDecor.SuspendLayout();
            this.SuspendLayout();

            // ResetTab container
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "ResetTab";
            this.Size = new System.Drawing.Size(800, 522);

            // resetButton
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.resetButton.Location = new System.Drawing.Point(24, 24);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(752, 48);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Đặt lại Thử thách đã đeo";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);

            // progressBar
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(24, 82);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(752, 12);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;

            // statusLabel
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusLabel.ForeColor = System.Drawing.Color.Black;
            this.statusLabel.Location = new System.Drawing.Point(24, 98);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(752, 22);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblHeaderChals
            this.lblHeaderChals.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeaderChals.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHeaderChals.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderChals.Location = new System.Drawing.Point(24, 132);
            this.lblHeaderChals.Name = "lblHeaderChals";
            this.lblHeaderChals.Size = new System.Drawing.Size(752, 22);
            this.lblHeaderChals.TabIndex = 4;
            this.lblHeaderChals.Text = "Thử thách đang đeo";
            this.lblHeaderChals.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // picChal1
            this.picChal1.Location = new System.Drawing.Point(196, 168);
            this.picChal1.Name = "picChal1";
            this.picChal1.Size = new System.Drawing.Size(92, 92);
            this.picChal1.TabIndex = 5;
            this.picChal1.TabStop = false;

            // lblChal1
            this.lblChal1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChal1.ForeColor = System.Drawing.Color.Black;
            this.lblChal1.Location = new System.Drawing.Point(176, 262);
            this.lblChal1.Name = "lblChal1";
            this.lblChal1.Size = new System.Drawing.Size(132, 18);
            this.lblChal1.TabIndex = 6;
            this.lblChal1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // picChal2
            this.picChal2.Location = new System.Drawing.Point(354, 168);
            this.picChal2.Name = "picChal2";
            this.picChal2.Size = new System.Drawing.Size(92, 92);
            this.picChal2.TabIndex = 7;
            this.picChal2.TabStop = false;

            // lblChal2
            this.lblChal2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChal2.ForeColor = System.Drawing.Color.Black;
            this.lblChal2.Location = new System.Drawing.Point(334, 262);
            this.lblChal2.Name = "lblChal2";
            this.lblChal2.Size = new System.Drawing.Size(132, 18);
            this.lblChal2.TabIndex = 8;
            this.lblChal2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // picChal3
            this.picChal3.Location = new System.Drawing.Point(512, 168);
            this.picChal3.Name = "picChal3";
            this.picChal3.Size = new System.Drawing.Size(92, 92);
            this.picChal3.TabIndex = 9;
            this.picChal3.TabStop = false;

            // lblChal3
            this.lblChal3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChal3.ForeColor = System.Drawing.Color.Black;
            this.lblChal3.Location = new System.Drawing.Point(492, 262);
            this.lblChal3.Name = "lblChal3";
            this.lblChal3.Size = new System.Drawing.Size(132, 18);
            this.lblChal3.TabIndex = 10;
            this.lblChal3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // groupDecor (nhóm Trang trí: Title, Crest Border, Banner Accent)
            this.groupDecor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDecor.Controls.Add(this.lblTitle);
            this.groupDecor.Controls.Add(this.lblTitleValue);
            this.groupDecor.Controls.Add(this.lblCrest);
            this.groupDecor.Controls.Add(this.picCrestBorder);
            this.groupDecor.Controls.Add(this.lblBanner);
            this.groupDecor.Controls.Add(this.picBanner);
            this.groupDecor.ForeColor = System.Drawing.Color.Black;
            this.groupDecor.Location = new System.Drawing.Point(24, 304);
            this.groupDecor.Name = "groupDecor";
            this.groupDecor.Size = new System.Drawing.Size(752, 160);
            this.groupDecor.TabIndex = 11;
            this.groupDecor.TabStop = false;
            this.groupDecor.Text = "Trang trí hồ sơ";

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTitle.Location = new System.Drawing.Point(24, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(80, 20);
            this.lblTitle.Text = "Danh hiệu:";

            // lblTitleValue
            this.lblTitleValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleValue.Location = new System.Drawing.Point(110, 30);
            this.lblTitleValue.Name = "lblTitleValue";
            this.lblTitleValue.Size = new System.Drawing.Size(600, 20);
            this.lblTitleValue.Text = "—";

            // lblCrest
            this.lblCrest.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCrest.Location = new System.Drawing.Point(24, 68);
            this.lblCrest.Name = "lblCrest";
            this.lblCrest.Size = new System.Drawing.Size(120, 20);
            this.lblCrest.Text = "Crest Border:";

            // picCrestBorder
            this.picCrestBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCrestBorder.Location = new System.Drawing.Point(150, 60);
            this.picCrestBorder.Name = "picCrestBorder";
            this.picCrestBorder.Size = new System.Drawing.Size(96, 64);
            this.picCrestBorder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCrestBorder.TabStop = false;

            // lblBanner
            this.lblBanner.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBanner.Location = new System.Drawing.Point(280, 68);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(120, 20);
            this.lblBanner.Text = "Banner Accent:";

            // picBanner
            this.picBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBanner.Location = new System.Drawing.Point(406, 60);
            this.picBanner.Name = "picBanner";
            this.picBanner.Size = new System.Drawing.Size(180, 64);
            this.picBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBanner.TabStop = false;

            // Add controls
            this.Controls.Add(this.groupDecor);
            this.Controls.Add(this.lblChal3);
            this.Controls.Add(this.picChal3);
            this.Controls.Add(this.lblChal2);
            this.Controls.Add(this.picChal2);
            this.Controls.Add(this.lblChal1);
            this.Controls.Add(this.picChal1);
            this.Controls.Add(this.lblHeaderChals);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.resetButton);

            ((System.ComponentModel.ISupportInitialize)(this.picChal1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChal2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChal3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCrestBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).EndInit();
            this.groupDecor.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;

        private System.Windows.Forms.Label lblHeaderChals;
        private System.Windows.Forms.PictureBox picChal1;
        private System.Windows.Forms.PictureBox picChal2;
        private System.Windows.Forms.PictureBox picChal3;
        private System.Windows.Forms.Label lblChal1;
        private System.Windows.Forms.Label lblChal2;
        private System.Windows.Forms.Label lblChal3;

        private System.Windows.Forms.GroupBox groupDecor;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTitleValue;
        private System.Windows.Forms.Label lblCrest;
        private System.Windows.Forms.PictureBox picCrestBorder;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.PictureBox picBanner;
    }
}
