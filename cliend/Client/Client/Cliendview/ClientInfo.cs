
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Cliendview
{
    class ClientInfo
    {
        Socket mSocket;
        public ClientInfo()
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            mSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1111));
        }

        public void Send(string content)
        {
            mSocket.Send(Encoding.UTF8.GetBytes(content));
        }
        public void Close()
        {
            mSocket.Close();
        }
    }
}
