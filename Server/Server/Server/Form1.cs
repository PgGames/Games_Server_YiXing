using Server.ServerSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(ServerManager.Manager.State);
            if (ServerManager.Manager.ServerState)
            {
                button1.Text = "启动服务器";
                ServerManager.Manager.StopServer();
            }
            else
            {
                button1.Text = "关闭服务器";
                ServerManager.Manager.StartServer();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ServerManager.Manager.ServerState)
            {
                string value = this.textBox1.Text;
                byte[] msg = Encoding.UTF8.GetBytes(value);
                //发送公告
                ServerManager.Manager.SendAllClient(msg);
            }
        }
    }
}
