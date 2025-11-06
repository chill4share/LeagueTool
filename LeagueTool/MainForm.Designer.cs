namespace LeagueTool
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.navPanel = new System.Windows.Forms.Panel();
            this.flowNav = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnBiography = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.navPanel.SuspendLayout();
            this.flowNav.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // navPanel
            // 
            this.navPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.navPanel.Controls.Add(this.flowNav);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.navPanel.Size = new System.Drawing.Size(800, 56);
            this.navPanel.TabIndex = 0;
            this.navPanel.Visible = true;
            // 
            // flowNav
            // 
            this.flowNav.AutoSize = false;
            this.flowNav.BackColor = System.Drawing.Color.Transparent;
            this.flowNav.Controls.Add(this.btnHome);
            this.flowNav.Controls.Add(this.btnReset);
            this.flowNav.Controls.Add(this.btnStatus);
            this.flowNav.Controls.Add(this.btnBiography);
            this.flowNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNav.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowNav.Location = new System.Drawing.Point(12, 8);
            this.flowNav.Margin = new System.Windows.Forms.Padding(0);
            this.flowNav.Name = "flowNav";
            this.flowNav.Padding = new System.Windows.Forms.Padding(0);
            this.flowNav.Size = new System.Drawing.Size(776, 40);
            this.flowNav.TabIndex = 0;
            this.flowNav.WrapContents = false;
            // 
            // btnHome
            // 
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Location = new System.Drawing.Point(3, 3);
            this.btnHome.Margin = new System.Windows.Forms.Padding(3);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(110, 34);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(119, 3);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(110, 34);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStatus
            // 
            this.btnStatus.FlatAppearance.BorderSize = 0;
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStatus.ForeColor = System.Drawing.Color.Black;
            this.btnStatus.Location = new System.Drawing.Point(235, 3);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(3);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(110, 34);
            this.btnStatus.TabIndex = 2;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = false;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnBiography
            // 
            this.btnBiography.FlatAppearance.BorderSize = 0;
            this.btnBiography.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBiography.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBiography.ForeColor = System.Drawing.Color.Black;
            this.btnBiography.Location = new System.Drawing.Point(351, 3);
            this.btnBiography.Margin = new System.Windows.Forms.Padding(3);
            this.btnBiography.Name = "btnBiography";
            this.btnBiography.Size = new System.Drawing.Size(110, 34);
            this.btnBiography.TabIndex = 3;
            this.btnBiography.Text = "Biography";
            this.btnBiography.UseVisualStyleBackColor = false;
            this.btnBiography.Click += new System.EventHandler(this.btnBiography_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 56);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(800, 522);
            this.contentPanel.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 578);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.navPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LeagueTool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.navPanel.ResumeLayout(false);
            this.flowNav.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.FlowLayoutPanel flowNav;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnBiography;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
    }
}
