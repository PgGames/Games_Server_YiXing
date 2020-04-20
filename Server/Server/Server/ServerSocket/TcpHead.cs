using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.ServerSocket
{
    class TcpHead
    {
        /// <summary>
        /// 主消息
        /// </summary>
        public byte mMian;
        /// <summary>
        /// 子消息
        /// </summary>
        public byte mSum;
        /// <summary>
        /// 消息内容大小
        /// </summary>
        public ushort mSize;
        /// <summary>
        /// 消息内容
        /// </summary>
        public byte[] mDate;


        public void ParsingContent(byte[] varBytes)
        {
            if (varBytes.Length < 4)
                return;
            mMian = varBytes[0];
            mSum = varBytes[1];
            mSize = System.BitConverter.ToUInt16(varBytes, 2);
            if (varBytes.Length < mSize + 4)
                return;
            Array.Copy(varBytes, 4, mDate, 0, (int)mSize);

        }
    }
}
