using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static String iniFile = "config.ini";

        public Form1()
        {
            InitializeComponent();
        }

        class TaskItem
        {
            public TaskItem(String _name, String _value)
            {
                this.name = _name;
                this.value = _value;
            }
            public String name;
            public String value;

            public override String ToString()
            {
                return this.name;
            }
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            //在listBox1添加项
            this.listBox1.Items.Add(new TaskItem("打开所有邮件", "task1"));
            this.listBox1.Items.Add(new TaskItem("魂匣",      "task2"));
            this.listBox1.Items.Add(new TaskItem("金币商店",   "task3"));
            this.listBox1.Items.Add(new TaskItem("pk商店",     "task4"));
            this.listBox1.Items.Add(new TaskItem("副本扫荡", "task6"));
            this.listBox1.Items.Add(new TaskItem("英雄升星", "task7"));
            this.listBox1.Items.Add(new TaskItem("吃经验药升级", "task8"));
            this.listBox1.Items.Add(new TaskItem("镶钻", "task10"));
            this.listBox1.Items.Add(new TaskItem("装备强化", "task11"));
            this.listBox1.Items.Add(new TaskItem("装备合成", "task12"));
            this.listBox1.Items.Add(new TaskItem("技能升级", "task13"));
            this.listBox1.Items.Add(new TaskItem("世界聊天", "task14"));
            this.listBox1.Items.Add(new TaskItem("竞技场", "task15"));
            this.listBox1.Items.Add(new TaskItem("打副本", "task16"));
            this.listBox1.Items.Add(new TaskItem("商队", "task18"));
            this.listBox1.Items.Add(new TaskItem("公会", "task19"));

            //设置treeview的AllowDrop属性(允许放置属性)为true
            try
            {
                this.treeView1.AllowDrop = true;
            }catch(Exception eee) {
                String v = eee.Message;
            }
            

            this.listBox1.MouseDown += new MouseEventHandler(listBox1_MouseDown);
            this.treeView1.DragEnter += new DragEventHandler(treeView1_DragEnter);
            this.treeView1.DragDrop += new DragEventHandler(treeView1_DragDrop);

            IniConfig ini = new IniConfig(iniFile);

            String ac_ip = ini.get("ac_ip");
            String ac_port = ini.get("ac_port");
            String ac_timeout = ini.get("ac_timeout");

            String login_ip = ini.get("login_ip");
            String login_port = ini.get("login_port");
            String login_timeout = ini.get("login_timeout");

            String ac_start = ini.get("ac_start");
            String login_count = ini.get("login_count");
            String interval = ini.get("interval");

            if (ac_ip != null)
                this.text_ac_ip.Text = ac_ip;
            if (ac_port != null)
                this.text_ac_port.Text = ac_port;
            if (ac_timeout != null)
                this.text_ac_timeout.Text = ac_timeout;

            if (login_ip != null)
                this.text_login_ip.Text = login_ip;
            if (login_port != null)
                this.text_login_port.Text = login_port;
            if (login_timeout != null)
                this.text_login_timeout.Text = login_timeout;

            if (ac_start != null)
                this.text_ac_start.Text = ac_start;

            if (login_count != null)
                this.text_login_count.Text = login_count;

            if (interval != null)
                this.text_interval.Text = interval;
        }
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //调用DoDragDrop方法
            if (this.listBox1.SelectedItem != null)
            {
                this.listBox1.DoDragDrop(this.listBox1.SelectedItem, DragDropEffects.Copy);
            }
        }
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            //设置拖拽类型(这里是复制拖拽)
            e.Effect = DragDropEffects.Copy;
        }
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            TaskItem ti = (TaskItem)e.Data.GetData(typeof(TaskItem));
            //获取值
            //string item = (string)e.Data.GetData(e.Data.GetFormats()[0]);
            this.treeView1.Nodes.Add(ti.name);

            robot.Program.task_list.Add(ti.value);
        }

        private void button_go_Click(object sender, EventArgs e)
        {
            String ac_ip = "192.168.1.2";
            short ac_port = 9000;
            int ac_timeout = 30;

            String login_ip = "192.168.1.2";
            short login_port = 9005;
            int login_timeout = 30;

            int ac_start = 1000;
            int login_count = 1;
            int interval = 1000;  //ms

            try
            {
                ac_ip = this.text_ac_ip.Text;
                ac_port = short.Parse(this.text_ac_port.Text);
                ac_timeout = int.Parse(this.text_ac_timeout.Text);

                login_ip = this.text_login_ip.Text;
                login_port = short.Parse(this.text_login_port.Text);
                login_timeout = int.Parse(this.text_login_timeout.Text);

                ac_start = int.Parse(this.text_ac_start.Text);
                login_count = int.Parse(this.text_login_count.Text);
                interval = int.Parse(this.text_interval.Text);
            }
            catch
            {
                Console.WriteLine("Error");
            }

            IniConfig ini = new IniConfig(iniFile);

            ini.set("ac_ip", ac_ip);
            ini.set("ac_port", ac_port.ToString());
            ini.set("ac_timeout", ac_timeout.ToString());

            ini.set("login_ip", login_ip);
            ini.set("login_port", login_port.ToString());
            ini.set("login_timeout", login_timeout.ToString());

            ini.set("ac_start", ac_start.ToString());
            ini.set("login_count", login_count.ToString());
            ini.set("interval", interval.ToString());
            ini.save();

            //account
            robot.Program.ACCOUNT_IP = ac_ip;
            robot.Program.ACCOUNT_PORT = ac_port;
            robot.Program.ACCOUNT_TIMEOUT = ac_timeout;

            //login
            robot.Program.LOGIN_IP = login_ip;
            robot.Program.LOGIN_PORT = login_port;
            robot.Program.LOGIN_TIMEOUT = login_timeout;

            robot.Program.ACCOUNT_START = ac_start;
            robot.Program.COUNT = login_count;
            robot.Program.LOGIN_INTERVAL = interval;

            this.Dispose();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            robot.Program.isQuit = true;
            this.Dispose();
        }
    }
}
