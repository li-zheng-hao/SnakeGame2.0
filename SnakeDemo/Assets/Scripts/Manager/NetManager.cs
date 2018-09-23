using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;

namespace Game
{
    /// <summary>
    /// 网络模块
    /// </summary>
    public class NetManager : BaseManager
    {
        private Session session;
        public override void Update()
        {

        }

        public override void OnInit()
        {
            session = new Session("127.0.0.1", 55555);
            session.Connect();
            session.ReadCallback += OnRead;
        }

        private void OnRead(Session session, Message msg)
        {
            RequestCode recode = msg.RequestCode;
            switch (recode)
            {
                case RequestCode.Login:
                    LoginRequest req = new LoginRequest();
                    req.Response(msg);
                    break;

            }
        }

        public override void OnDestroy()
        {

        }
        /// <summary>
        /// 给服务器发送数据
        /// </summary>
        /// <param name="reCode"></param>
        /// <param name="opCode"></param>
        /// <param name="data"></param>
        public void Send(RequestCode reCode, object data)
        {

            session.Send(reCode, data);
        }
    }

}

