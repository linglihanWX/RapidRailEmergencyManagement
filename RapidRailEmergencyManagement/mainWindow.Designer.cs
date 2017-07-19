namespace RapidRailEmergencyManagement
{
    partial class mainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始发送数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止发送数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务器地址设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户信息设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(938, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接服务器ToolStripMenuItem,
            this.开始发送数据ToolStripMenuItem,
            this.停止发送数据ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 连接服务器ToolStripMenuItem
            // 
            this.连接服务器ToolStripMenuItem.Name = "连接服务器ToolStripMenuItem";
            this.连接服务器ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.连接服务器ToolStripMenuItem.Text = "连接服务器";
            this.连接服务器ToolStripMenuItem.Click += new System.EventHandler(this.连接服务器ToolStripMenuItem_Click);
            // 
            // 开始发送数据ToolStripMenuItem
            // 
            this.开始发送数据ToolStripMenuItem.Name = "开始发送数据ToolStripMenuItem";
            this.开始发送数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.开始发送数据ToolStripMenuItem.Text = "开始发送数据";
            this.开始发送数据ToolStripMenuItem.Click += new System.EventHandler(this.开始发送数据ToolStripMenuItem_Click);
            // 
            // 停止发送数据ToolStripMenuItem
            // 
            this.停止发送数据ToolStripMenuItem.Name = "停止发送数据ToolStripMenuItem";
            this.停止发送数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.停止发送数据ToolStripMenuItem.Text = "停止发送数据";
            this.停止发送数据ToolStripMenuItem.Click += new System.EventHandler(this.停止发送数据ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务器地址设置ToolStripMenuItem,
            this.用户信息设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 服务器地址设置ToolStripMenuItem
            // 
            this.服务器地址设置ToolStripMenuItem.Name = "服务器地址设置ToolStripMenuItem";
            this.服务器地址设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.服务器地址设置ToolStripMenuItem.Text = "服务器地址设置";
            this.服务器地址设置ToolStripMenuItem.Click += new System.EventHandler(this.服务器地址设置ToolStripMenuItem_Click);
            // 
            // 用户信息设置ToolStripMenuItem
            // 
            this.用户信息设置ToolStripMenuItem.Name = "用户信息设置ToolStripMenuItem";
            this.用户信息设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.用户信息设置ToolStripMenuItem.Text = "用户信息设置";
            this.用户信息设置ToolStripMenuItem.Click += new System.EventHandler(this.用户信息设置ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 535);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(938, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "正在后台运行";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "正在后台运行";
            this.notifyIcon1.Visible = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 510);
            this.panel1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(938, 510);
            this.textBox1.TabIndex = 0;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 557);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "快轨应急数据处理";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止发送数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务器地址设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户信息设置ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 连接服务器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始发送数据ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

