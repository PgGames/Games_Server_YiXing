using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server.ServerSocket
{
    public class ServerManager
    {
        private static ServerManager mManager;

        public static ServerManager Manager
        {
            get
            {
                if (mManager == null)
                    mManager = new ServerManager();
                return mManager;
            }
        }
        /// <summary>
        /// 服务器Socket
        /// </summary>
        TcpSocket mServer;
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        private string mIP;
        /// <summary>
        /// 服务器端口
        /// </summary>
        private int mPort = 1111;
        /// <summary>
        /// 服务器最大连接数量
        /// </summary>
        private int mListen = 100;
        /// <summary>
        /// 服务器状态
        /// </summary>
        private bool mServerState;
        /// <summary>
        /// 所有的客户端连接
        /// </summary>
        private List<TcpSocket> mClients = new List<TcpSocket>();

        /// <summary>
        /// 获取服务器状态
        /// </summary>
        public bool ServerState { get { return mServerState; } }

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void StartServer()
        {
            mServer = new TcpSocket();
            mServer.Bind(mIP, mPort, mListen);

            Thread thread = new Thread(ClientContent);
            thread.IsBackground = true;
            thread.Start();
            mServerState = true;
        }
        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void StopServer()
        {
            for (int i = 0; i < mClients.Count; i++)
            {
                mClients[i].CloseSocket();
            }
            mClients.Clear();
            mServer.CloseSocket();
            mServerState = false;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        private void ClientContent()
        {
            var TempClient = mServer.mSocket.Accept();

            TcpSocket Client = new TcpSocket(TempClient);
            mClients.Add(Client);
            Client.Receive();
            //继续进行---连接监听
            ClientContent();
        }


        /// <summary>
        /// 发生消息给所有客户端
        /// </summary>
        public void SendAllClient(byte[] msg)
        {
            for (int i = 0; i < mClients.Count; i++)
            {
                mClients[i].Send(msg);
            }
        }
    }
}
