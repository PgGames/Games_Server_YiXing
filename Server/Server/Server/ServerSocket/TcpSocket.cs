using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.ServerSocket
{
    public class TcpSocket
    {
        public Socket mSocket;
        /// <summary>
        /// 客户端的IP和端口
        /// </summary>
        private IPEndPoint mIPEnd;
        /// <summary>
        /// 处理消息的缓存区
        /// </summary>
        private byte[] mMsg;
        /// <summary>
        /// 一次消息的最大字节量
        /// </summary>
        private int mMaxLeng = 1024;

        public TcpSocket()
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mMsg = new byte[mMaxLeng];
        }
        public TcpSocket(Socket varSocket)
        {
            mSocket = varSocket;

            mIPEnd = mSocket.RemoteEndPoint as IPEndPoint;

            Console.WriteLine("IP:" + mIPEnd.Address + "  Prot:" + mIPEnd.Port);
        }
        /// <summary>
        /// 启动Socket监听
        /// </summary>
        /// <param name="varIPAddress"></param>
        /// <param name="varPort"></param>
        /// <param name="MaxListen"></param>
        public void Bind(string varIPAddress, int varPort, int MaxListen)
        {
            if (string.IsNullOrEmpty(varIPAddress))
            {
                varIPAddress = "127.0.0.1";
            }
            IPAddress Ip = IPAddress.Parse(varIPAddress);
            IPEndPoint iPEnd = new IPEndPoint(Ip, varPort);

            mSocket.Bind(iPEnd);
            mSocket.Listen(MaxListen);
        }
        /// <summary>
        /// 启动消息接收
        /// </summary>
        public void Receive()
        {
            Thread thread = new Thread(ClientReceive);
            thread.IsBackground = true;
            thread.Start();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseSocket()
        {
            if (mSocket != null)
            {
                mSocket.Close();
            }
        }
        /// <summary>
        /// 发生消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string msg)
        {
            mSocket.Send(Encoding.UTF8.GetBytes(msg));
        }
        /// <summary>
        /// 发生消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(byte[] msg)
        {
            mSocket.Send(msg);
        }
        /// <summary>
        /// 接收来自客户端的消息
        /// </summary>
        private void ClientReceive()
        {
            try
            {
                while (true)
                {
                    //byte[] bytes = new byte[1024];
                    int meglen = mSocket.Receive(mMsg);
                    MessagHandle(mMsg, meglen);
                    //开启新的消息监听
                    ClientReceive();
                }
            }
            catch(System.Exception exp)
            {
                ServerManager.Manager.CloseClient(this);
                Console.WriteLine(exp.Message);
            }
        }
        /// <summary>
        /// 消息处理
        /// </summary>
        private void MessagHandle(byte[] msg,int meglen)
        {
            string value = Encoding.UTF8.GetString(msg, 0, meglen);

            Console.WriteLine(value);
        }
    }
}
