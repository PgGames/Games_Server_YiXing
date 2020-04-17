using Client.Cliendview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientInfo minfo = new ClientInfo();
            Console.WriteLine("输入Exit退出");
            while (true)
            {
                string consolestr = Console.ReadLine();
                if (consolestr == "Exit")
                {
                    minfo.Close();
                    break;
                }
                else
                {
                    minfo.Send(consolestr);
                }
            }
        }
    }
}
