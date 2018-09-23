/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 21:14:03
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
    public class EnterRoomHandler:IHandler
    {
        public void Handle(Session session, Message ms)
        {
            NetworkCenter.Instance.roomList.Add(session);
            Response(session,ms);
        }

        public void Response(Session session, Message ms)
        {
            List<PlayerInfo>pl=NetworkCenter.Instance.room.GetPlayer();
            Players pls = new Players(){playerInfos = pl};
            Message mss=new Message(RequestCode.EnterRoom,pls);
            var datas = MessageHelper.Serialize(mss);
            session.Send(datas);
        }
    }
}
