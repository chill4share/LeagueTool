namespace LeagueTool.Tabs
{
    partial class HomeTab
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.summonerNameLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.rankLabel = new System.Windows.Forms.Label();
            this.btnFindClient = new System.Windows.Forms.Button();
            this.findStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.Location = new System.Drawing.Point(16, 430);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(768, 22);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "LOLVN TOOLS";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelVersion.ForeColor = System.Drawing.Color.Black;
            this.labelVersion.Location = new System.Drawing.Point(16, 452);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(768, 18);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Phiên bản 1.1";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAuthor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelAuthor.ForeColor = System.Drawing.Color.Black;
            this.labelAuthor.Location = new System.Drawing.Point(16, 470);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(768, 18);
            this.labelAuthor.TabIndex = 2;
            this.labelAuthor.Text = "Tạo bởi Chill4Share";
            this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // summonerNameLabel
            // 
            this.summonerNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.summonerNameLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.summonerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.summonerNameLabel.Location = new System.Drawing.Point(16, 20);
            this.summonerNameLabel.Name = "summonerNameLabel";
            this.summonerNameLabel.Size = new System.Drawing.Size(768, 40);
            this.summonerNameLabel.TabIndex = 3;
            this.summonerNameLabel.Text = "Đang chờ...";
            this.summonerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.levelLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.levelLabel.ForeColor = System.Drawing.Color.DimGray;
            this.levelLabel.Location = new System.Drawing.Point(16, 64);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(768, 22);
            this.levelLabel.TabIndex = 4;
            this.levelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rankLabel
            // 
            this.rankLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rankLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rankLabel.ForeColor = System.Drawing.Color.DimGray;
            this.rankLabel.Location = new System.Drawing.Point(16, 88);
            this.rankLabel.Name = "rankLabel";
            this.rankLabel.Size = new System.Drawing.Size(768, 22);
            this.rankLabel.TabIndex = 7;
            this.rankLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFindClient
            // 
            this.btnFindClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindClient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFindClient.Location = new System.Drawing.Point(680, 486);
            this.btnFindClient.Name = "btnFindClient";
            this.btnFindClient.Size = new System.Drawing.Size(104, 30);
            this.btnFindClient.TabIndex = 5;
            this.btnFindClient.Text = "Tìm client";
            this.btnFindClient.UseVisualStyleBackColor = true;
            this.btnFindClient.Click += new System.EventHandler(this.btnFindClient_Click);
            // 
            // findStatusLabel
            // 
            this.findStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findStatusLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.findStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.findStatusLabel.Location = new System.Drawing.Point(16, 494);
            this.findStatusLabel.Name = "findStatusLabel";
            this.findStatusLabel.Size = new System.Drawing.Size(648, 20);
            this.findStatusLabel.TabIndex = 6;
            this.findStatusLabel.Text = "🔍 Vào tab Home và nhấn \'Tìm client\' để dò LCU";
            this.findStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HomeTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.Controls.Add(this.btnFindClient);
            this.Controls.Add(this.findStatusLabel);
            this.Controls.Add(this.rankLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.summonerNameLabel);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelTitle);
            this.Name = "HomeTab";
            this.Size = new System.Drawing.Size(800, 522);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label summonerNameLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label rankLabel;
        private System.Windows.Forms.Button btnFindClient;
        private System.Windows.Forms.Label findStatusLabel;
    }
}
