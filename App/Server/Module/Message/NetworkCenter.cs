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
using System.Threading.Tasks;


namespace Game
{
    public class NetworkCenter
    {
        public Service server;
        public List<Session> clients=new List<Session>();

        public NetworkCenter()
        {
            server=new Service("127.0.0.1",55555);
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
            //todo 处理消息的派发
        }
    }
}
