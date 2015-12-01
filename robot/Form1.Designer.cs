namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_go = new System.Windows.Forms.Button();
            this.text_login_count = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_interval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_ac_start = new System.Windows.Forms.TextBox();
            this.text_ac_ip = new System.Windows.Forms.TextBox();
            this.text_ac_port = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_ac_timeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ac_server = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_login_timeout = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.text_login_ip = new System.Windows.Forms.TextBox();
            this.text_login_port = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label10 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_quit = new System.Windows.Forms.Button();
            this.ac_server.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_go
            // 
            this.button_go.Location = new System.Drawing.Point(188, 571);
            this.button_go.Name = "button_go";
            this.button_go.Size = new System.Drawing.Size(75, 23);
            this.button_go.TabIndex = 0;
            this.button_go.Text = "开始";
            this.button_go.UseVisualStyleBackColor = true;
            this.button_go.Click += new System.EventHandler(this.button_go_Click);
            // 
            // text_login_count
            // 
            this.text_login_count.Location = new System.Drawing.Point(100, 44);
            this.text_login_count.Name = "text_login_count";
            this.text_login_count.Size = new System.Drawing.Size(137, 21);
            this.text_login_count.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "登陆人数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "登陆间隔(MS)：";
            // 
            // text_interval
            // 
            this.text_interval.Location = new System.Drawing.Point(100, 71);
            this.text_interval.Name = "text_interval";
            this.text_interval.Size = new System.Drawing.Size(137, 21);
            this.text_interval.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "起始帐号：";
            // 
            // text_ac_start
            // 
            this.text_ac_start.Location = new System.Drawing.Point(100, 18);
            this.text_ac_start.Name = "text_ac_start";
            this.text_ac_start.Size = new System.Drawing.Size(137, 21);
            this.text_ac_start.TabIndex = 7;
            // 
            // text_ac_ip
            // 
            this.text_ac_ip.Location = new System.Drawing.Point(100, 17);
            this.text_ac_ip.Name = "text_ac_ip";
            this.text_ac_ip.Size = new System.Drawing.Size(137, 21);
            this.text_ac_ip.TabIndex = 9;
            // 
            // text_ac_port
            // 
            this.text_ac_port.Location = new System.Drawing.Point(100, 44);
            this.text_ac_port.Name = "text_ac_port";
            this.text_ac_port.Size = new System.Drawing.Size(137, 21);
            this.text_ac_port.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Port：";
            // 
            // text_ac_timeout
            // 
            this.text_ac_timeout.Location = new System.Drawing.Point(100, 71);
            this.text_ac_timeout.Name = "text_ac_timeout";
            this.text_ac_timeout.Size = new System.Drawing.Size(137, 21);
            this.text_ac_timeout.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Timeout(S)：";
            // 
            // ac_server
            // 
            this.ac_server.Controls.Add(this.label6);
            this.ac_server.Controls.Add(this.text_ac_timeout);
            this.ac_server.Controls.Add(this.label2);
            this.ac_server.Controls.Add(this.text_ac_ip);
            this.ac_server.Controls.Add(this.text_ac_port);
            this.ac_server.Controls.Add(this.label5);
            this.ac_server.Location = new System.Drawing.Point(12, 12);
            this.ac_server.Name = "ac_server";
            this.ac_server.Size = new System.Drawing.Size(257, 100);
            this.ac_server.TabIndex = 14;
            this.ac_server.TabStop = false;
            this.ac_server.Text = "帐号服务器";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.text_login_timeout);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.text_login_ip);
            this.groupBox1.Controls.Add(this.text_login_port);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登陆服务器";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Timeout(S)：";
            // 
            // text_login_timeout
            // 
            this.text_login_timeout.Location = new System.Drawing.Point(100, 71);
            this.text_login_timeout.Name = "text_login_timeout";
            this.text_login_timeout.Size = new System.Drawing.Size(137, 21);
            this.text_login_timeout.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "IP：";
            // 
            // text_login_ip
            // 
            this.text_login_ip.Location = new System.Drawing.Point(100, 17);
            this.text_login_ip.Name = "text_login_ip";
            this.text_login_ip.Size = new System.Drawing.Size(137, 21);
            this.text_login_ip.TabIndex = 9;
            // 
            // text_login_port
            // 
            this.text_login_port.Location = new System.Drawing.Point(100, 44);
            this.text_login_port.Name = "text_login_port";
            this.text_login_port.Size = new System.Drawing.Size(137, 21);
            this.text_login_port.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "Port：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.text_login_count);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.text_interval);
            this.groupBox2.Controls.Add(this.text_ac_start);
            this.groupBox2.Location = new System.Drawing.Point(12, 222);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 100);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "登陆参数";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.treeView1);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 328);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 234);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "任务";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(146, 20);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(105, 214);
            this.treeView1.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(113, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "==>";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 20);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(92, 400);
            this.listBox1.TabIndex = 1;
            // 
            // btn_quit
            // 
            this.btn_quit.Location = new System.Drawing.Point(18, 571);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(75, 23);
            this.btn_quit.TabIndex = 18;
            this.btn_quit.Text = "退出";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 623);
            this.ControlBox = false;
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ac_server);
            this.Controls.Add(this.button_go);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器压力测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ac_server.ResumeLayout(false);
            this.ac_server.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_go;
        private System.Windows.Forms.TextBox text_login_count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_interval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_ac_start;
        private System.Windows.Forms.TextBox text_ac_ip;
        private System.Windows.Forms.TextBox text_ac_port;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_ac_timeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox ac_server;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_login_timeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox text_login_ip;
        private System.Windows.Forms.TextBox text_login_port;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.TreeView treeView1;
    }
}

