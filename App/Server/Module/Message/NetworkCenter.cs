/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:15:47
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Game
{
    public class NetworkCenter
    {
        private static NetworkCenter instance=new NetworkCenter();

        public static NetworkCenter Instance
        {
            get { return instance; }
        }
        public Service server;
        public List<Session> clients=new List<Session>();
        public List<Session> roomList=new List<Session>();

        public Room room=new Room();

        public NetworkCenter()
        {
            server=new Service("47.106.238.197",55555);
            server.OnAccept += OnAccept;
        }

        /// <summary>
        /// 处理用户发来的连接
        /// </summary>
        /// <param name="session"></param>
        private void OnAccept(Session session)
        {
            session.ReadCallback += OnRead;
            clients.Add(session);
        }
       
        
        
        /// <summary>
        /// 处理用户发来的数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="msg"></param>
        private void OnRead(Session session, Message msg)
        {
            //todo 处理消息的派发  分发给各个handler
            Console.WriteLine("这里需要将接收到的数据给分发");
            
            IHandler handler = HandlerFactory.CreateHandler(msg.RequestCode);
            handler.Handle(session,msg);
        }


        public void BroadCastMsg(Message msg)
        {
            var datas = MessageHelper.Serialize(msg);
            foreach (var client in roomList)
            {  
                client.Send(datas);
            }

        }


    }
}
