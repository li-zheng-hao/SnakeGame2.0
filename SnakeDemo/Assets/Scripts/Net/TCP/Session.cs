/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 19:28:46
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Game
{
    public class Session
    {
        public Socket client;
        private string ipadd;
        private int port;
        public SocketAsyncEventArgs readArgs;
        public SocketAsyncEventArgs writeArgs;
        //读或者写的操作
        private const int opsToPreAlloc = 2;

        private List<byte> msgQue=new List<byte>();
        private bool isResolveMsg = false;
        /// <summary>
        /// 初始化session
        /// </summary>
        /// <param name="client">连接的socket</param>
        /// <param name="server">服务端处理</param>
        public Session(string ipadd,int port)
        {
            this.client=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            this.ipadd = ipadd;
            this.port = port;

        }
        /// <summary>
        /// 连接服务器并且开始接收数据
        /// </summary>
        public void Connect()
        {
            client.Connect(IPAddress.Parse(ipadd), port);
            StartReceAsync(new SocketAsyncEventArgs());
            Debug.LogWarning("连接服务器成功");
        }
        #region 接收数据

        public void StartReceAsync(SocketAsyncEventArgs readEventArgs)
        {

            this.readArgs = readEventArgs;
            byte[] buffer=new byte[4096];
            readArgs.SetBuffer(buffer,0,buffer.Length);
            readArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IOCompleted);
            // As soon as the client is connected, post a receive to the connection
            bool willRaiseEvent = client.ReceiveAsync(readEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(readEventArgs);
            }

        }

        private Action<Session, Message> readCallback;

        public event Action<Session, Message> ReadCallback
        {
            add
            {
                this.readCallback += value;
            }
            remove
            {
                this.readCallback -= value;
            }
        }
        /// <summary>
        /// 接收数据完成
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (this.client.Connected == false)
            {
                return;
            }
           
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                var data = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                msgQue.AddRange(data);
//                if (isResolveMsg==false)
//                {
//                    isResolveMsg = true;
              
                BeginReSolveMsg();
                    
//                }
                if (!client.ReceiveAsync(e))
                {
                    ProcessReceive(e);
                }

            }
            else
            {
                //关闭连接并且回收资源
//                server.CloseClientSocket(this, e);
            }

        }
        /// <summary>
        /// 这里循环处理消息，如果消息能够被解析，则成功处理
        /// </summary>
        private void BeginReSolveMsg()
        {

            lock (msgQue)
            {
                if (msgQue.Count > 2)
                {
                    byte[] length = msgQue.GetRange(0, 2).ToArray();
                    var length2 = BitConverter.ToInt16(length, 0);
                    //此处需要先读取数据
                    if (msgQue.Count - 2 >= length2)
                    {
                        msgQue.RemoveRange(0, 2);
                        var data = msgQue.GetRange(0, length2).ToArray();
                        msgQue.RemoveRange(0, length2);
                        Message ms = MessageHelper.DeSerialize(data);

                        Console.Write("数据长度为" + length2);
                        readCallback?.Invoke(this, ms);
                    }
                }
            }


        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (client.Connected)
            {
                client.Close();
            }

        }

        #endregion


        #region 发送数据
        /// <summary>
        /// 异步发送数据完成
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            Socket s = (Socket)e.UserToken;
            if (e.SocketError == SocketError.Success)
            {


                //TODO 处理发送完成之后的事情
            }
            else
            {
//                //关闭连接并且回收资源
//                server.CloseClientSocket(this, e);
            }


        }


        /// <summary>
        /// 异步的发送数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public void SendAsync(byte[] data)
        {

            writeArgs = new SocketAsyncEventArgs();
            writeArgs.Completed += IOCompleted;

            writeArgs.UserToken = client;

            if (writeArgs.SocketError == SocketError.Success)
            {
                if (client.Connected)
                {
                    //                    Array.Copy(data, 0, writeArgs.Buffer, 0, data.Length);//设置发送数据

                    writeArgs.SetBuffer(data, 0, data.Length); //设置发送数据
                    if (!client.SendAsync(writeArgs))//投递发送请求，这个函数有可能同步发送出去，这时返回false，并且不会引发SocketAsyncEventArgs.Completed事件
                    {
                        // 同步发送时处理发送完成事件
                        ProcessSend(writeArgs);
                    }

                }
            }
        }


        /// <summary>
        /// 同步发送数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="timeout"></param>
        public void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            socket.SendTimeout = 0;
            int startTickCount = Environment.TickCount;
            int sent = 0; // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                {
                    //throw new Exception("Timeout.");
                }

                try
                {
                    sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably full, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                    {
                        throw ex; // any serious error occurr
                    }
                }
            } while (sent < size);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="subCode"></param>
        /// <param name="data"></param>
        public void Send(RequestCode reCode,object data)
        {
            Message ms=new Message(reCode,data);
            var datas = MessageHelper.Serialize(ms);
            client.Send(datas);
        }
        #endregion

        /// <summary>
        /// IO 完成时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IOCompleted(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }

        }
    }
}
