/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 19:33:32
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Game
{
    /// <summary>
    /// service类只负责接收客户端发来的连接，建立连接后所有session发来的数据和业务在上层NetWorkCenter类进行处理
    /// </summary>
    public class Service
    {
       
        #region 属性


        //读或者写的操作
        const int opsToPreAlloc = 2;

        private Socket serverSocket;

        private readonly IPEndPoint ipEndPoint;

        public const int maxConnection = 100;

        private Semaphore semaphore;

        /// <summary>
        /// 缓冲区管理
        /// </summary>
        public BufferManager bufferManager;

        /// <summary>
        /// 是否已经销毁
        /// </summary>
        public bool disposed { get; set; } = false;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;
        /// <summary>
        /// 每个IO Socket缓冲区的大小
        /// </summary>
        private int bufferSize = 1024;
        /// <summary>
        /// 当前连接了的session
        /// </summary>
        public List<Session> battleList = new List<Session>();
        /// <summary>
        /// 对象池
        /// </summary>
        public SocketAsyncEventArgsPool eventArgsPool;

        List<Session> clientList=new List<Session>();
        
        #endregion



        #region 构造
        public Service(string ipadd, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipadd), port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bufferManager = new BufferManager(bufferSize * maxConnection * opsToPreAlloc, bufferSize);

            eventArgsPool = new SocketAsyncEventArgsPool(maxConnection);
            semaphore = new Semaphore(maxConnection, maxConnection);
            

            Init();
        }

        private void Init()
        {
            bufferManager.InitBuffer();

            SocketAsyncEventArgs readWriteEventArg;

            for (int i = 0; i < maxConnection; i++)
            {
                //提前分配好可重复使用的SocketAsyncEventArgs
                readWriteEventArg = new SocketAsyncEventArgs();

                readWriteEventArg.UserToken = null;

                // assign a byte buffer from the buffer pool to the SocketAsyncEventArg object
                bufferManager.SetBuffer(readWriteEventArg);

                //初始化eventArgsPool池
                eventArgsPool.InitPool(readWriteEventArg);
            }

        }


        #endregion


        #region 监听
        /// <summary>
        /// 开启服务器并且监听
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(maxConnection);
                Console.WriteLine("server start listening...");
                StartAccept(null);
            }



        }
        /// <summary>
        /// 开始接收客户端
        /// </summary>
        /// <param name="args"></param>
        private void StartAccept(SocketAsyncEventArgs args)
        {
            if (args == null)
            {
                args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            }
            else
            {
                args.AcceptSocket = null;
            }

            semaphore.WaitOne();
            if (!serverSocket.AcceptAsync(args))
            {
                ProcessAccept(args);
                //如果I/O挂起等待异步则触发AcceptAsyn_Asyn_Completed事件
                //此时I/O操作同步完成，不会触发Asyn_Completed事件，所以指定BeginAccept()方法
            }

        }

        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private Action<Session> onAccept;
        public event Action<Session> OnAccept
        {
            add { this.onAccept += value; }
            remove { this.onAccept -= value; }
        }

        private void ProcessAccept(SocketAsyncEventArgs args)
        {
            Console.WriteLine("A client Coming");
            // Get the socket for the accepted client connection and put it into the 
            //ReadEventArg object user token
            SocketAsyncEventArgs readEventArgs = eventArgsPool.Pop();

            Session session = new Session(args.AcceptSocket, this);
            readEventArgs.UserToken = args.AcceptSocket;

            session.StartReceAsync(readEventArgs);

            onAccept(session);

            // Accept the next connection request
            StartAccept(args);
        }



        #endregion


        
        /// <summary>
        /// 关闭用户的连接
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public void CloseClientSocket(Session s, SocketAsyncEventArgs e)
        {
            Console.WriteLine("一个客户端断开连接");
            try
            {
                s.Close();
               
                if (clientList.Contains(s))
                {
                    clientList.Remove(s);
                }

            }
            catch (Exception ex)
            {
                // Throw if client has closed, so it is not necessary to catch.
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            semaphore.Release();
            eventArgsPool.Push(e);//SocketAsyncEventArg 对象被释放，压入可重用队列。
        }

        #region 关闭服务


        /// <summary>
        /// 关闭服务
        /// </summary>
        public void Close()
        {
            Console.WriteLine("关闭所有服务");
            if (IsRunning)
            {
                IsRunning = false;
                serverSocket.Close();
                //todo 关闭对所有客户端的连接
                foreach (var session in clientList)
                {
                    session.Close();
                    eventArgsPool.Push(session.readArgs);
                    //todo 回收写
                }


            }
        }

        #endregion


      


       
    }
}
