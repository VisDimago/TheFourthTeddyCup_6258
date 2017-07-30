namespace DataReader
{
    partial class Form1
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
            if (disposing && ( components != null ))
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地区车站信息导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地区信息导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.车站信息导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.天气信息导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.列车运行信息导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从文件导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从文件夹导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传缓存数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.站点间日客流数据导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.数据整理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预测数据整理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.地区车站信息导入ToolStripMenuItem,
            this.天气信息导入ToolStripMenuItem,
            this.列车运行信息导入ToolStripMenuItem,
            this.导出数据ToolStripMenuItem,
            this.数据整理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(712, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 地区车站信息导入ToolStripMenuItem
            // 
            this.地区车站信息导入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地区信息导入ToolStripMenuItem,
            this.车站信息导入ToolStripMenuItem});
            this.地区车站信息导入ToolStripMenuItem.Name = "地区车站信息导入ToolStripMenuItem";
            this.地区车站信息导入ToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.地区车站信息导入ToolStripMenuItem.Text = "地区、车站信息导入";
            // 
            // 地区信息导入ToolStripMenuItem
            // 
            this.地区信息导入ToolStripMenuItem.Name = "地区信息导入ToolStripMenuItem";
            this.地区信息导入ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.地区信息导入ToolStripMenuItem.Text = "地区信息导入";
            this.地区信息导入ToolStripMenuItem.Click += new System.EventHandler(this.地区信息导入ToolStripMenuItem_Click);
            // 
            // 车站信息导入ToolStripMenuItem
            // 
            this.车站信息导入ToolStripMenuItem.Name = "车站信息导入ToolStripMenuItem";
            this.车站信息导入ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.车站信息导入ToolStripMenuItem.Text = "车站信息导入";
            this.车站信息导入ToolStripMenuItem.Click += new System.EventHandler(this.车站信息导入ToolStripMenuItem_Click);
            // 
            // 天气信息导入ToolStripMenuItem
            // 
            this.天气信息导入ToolStripMenuItem.Name = "天气信息导入ToolStripMenuItem";
            this.天气信息导入ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.天气信息导入ToolStripMenuItem.Text = "天气信息导入";
            this.天气信息导入ToolStripMenuItem.Click += new System.EventHandler(this.天气信息导入ToolStripMenuItem_Click);
            // 
            // 列车运行信息导入ToolStripMenuItem
            // 
            this.列车运行信息导入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.从文件导入ToolStripMenuItem,
            this.从文件夹导入ToolStripMenuItem,
            this.上传缓存数据ToolStripMenuItem});
            this.列车运行信息导入ToolStripMenuItem.Name = "列车运行信息导入ToolStripMenuItem";
            this.列车运行信息导入ToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.列车运行信息导入ToolStripMenuItem.Text = "列车运行信息导入";
            // 
            // 从文件导入ToolStripMenuItem
            // 
            this.从文件导入ToolStripMenuItem.Name = "从文件导入ToolStripMenuItem";
            this.从文件导入ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.从文件导入ToolStripMenuItem.Text = "从文件导入";
            this.从文件导入ToolStripMenuItem.Click += new System.EventHandler(this.从文件导入ToolStripMenuItem_Click);
            // 
            // 从文件夹导入ToolStripMenuItem
            // 
            this.从文件夹导入ToolStripMenuItem.Name = "从文件夹导入ToolStripMenuItem";
            this.从文件夹导入ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.从文件夹导入ToolStripMenuItem.Text = "从文件夹导入";
            this.从文件夹导入ToolStripMenuItem.Click += new System.EventHandler(this.从文件夹导入ToolStripMenuItem_Click);
            // 
            // 上传缓存数据ToolStripMenuItem
            // 
            this.上传缓存数据ToolStripMenuItem.Name = "上传缓存数据ToolStripMenuItem";
            this.上传缓存数据ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.上传缓存数据ToolStripMenuItem.Text = "上传缓存数据";
            this.上传缓存数据ToolStripMenuItem.Click += new System.EventHandler(this.上传缓存数据ToolStripMenuItem_Click);
            // 
            // 导出数据ToolStripMenuItem
            // 
            this.导出数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.站点间日客流数据导出ToolStripMenuItem});
            this.导出数据ToolStripMenuItem.Name = "导出数据ToolStripMenuItem";
            this.导出数据ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.导出数据ToolStripMenuItem.Text = "导出数据";
            // 
            // 站点间日客流数据导出ToolStripMenuItem
            // 
            this.站点间日客流数据导出ToolStripMenuItem.Name = "站点间日客流数据导出ToolStripMenuItem";
            this.站点间日客流数据导出ToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.站点间日客流数据导出ToolStripMenuItem.Text = "站点间日客流数据导出";
            this.站点间日客流数据导出ToolStripMenuItem.Click += new System.EventHandler(this.站点间日客流数据导出ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(712, 24);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(712, 343);
            this.textBox1.TabIndex = 3;
            // 
            // 数据整理ToolStripMenuItem
            // 
            this.数据整理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.预测数据整理ToolStripMenuItem});
            this.数据整理ToolStripMenuItem.Name = "数据整理ToolStripMenuItem";
            this.数据整理ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.数据整理ToolStripMenuItem.Text = "数据整理";
            // 
            // 预测数据整理ToolStripMenuItem
            // 
            this.预测数据整理ToolStripMenuItem.Name = "预测数据整理ToolStripMenuItem";
            this.预测数据整理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.预测数据整理ToolStripMenuItem.Text = "预测数据整理";
            this.预测数据整理ToolStripMenuItem.Click += new System.EventHandler(this.预测数据整理ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 395);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(631, 442);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "列车运行系统数据导入器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 天气信息导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 列车运行信息导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从文件导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从文件夹导入ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 地区车站信息导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地区信息导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 车站信息导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传缓存数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 站点间日客流数据导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据整理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预测数据整理ToolStripMenuItem;
    }
}

