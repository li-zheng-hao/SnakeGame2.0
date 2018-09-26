/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 8:47:39
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
    public class UpdatePosHandler:IHandler
    {
        public void Handle(Session session, Message ms)
        {
//            Console.WriteLine("_________________________________");
            var info=ms.Value as PlayerInfo;
//            foreach (var pos in info.pos)
//            {
//                Console.WriteLine("X:{0}     Y:{1}",pos.posx,pos.posy);
//            }
//            Console.WriteLine();
//            Console.WriteLine("_________________________________");
            NetworkCenter.Instance.room.UpdatePlayer(info,session);
            Response(session,null);
        }

        public void Response(Session session, Message ms)
        {
            Players players=new Players();
            players.playerInfos=new List<PlayerInfo>();
            
            players.playerInfos = NetworkCenter.Instance.room.GetPlayer();
            Console.WriteLine("____________当前服务器存在的玩家数量____"+players.playerInfos.Count);
            Message mss=new Message(RequestCode.UpdatePos,players);

            var datas=MessageHelper.Serialize(mss);
            session.Send(datas);
        }
    }
}
