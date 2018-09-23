/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 21:30:37
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
    public class EnterRoomRequest:BaseRequest
    {
        public override void SendRequest(RequestCode ReqCode, object data)
        {
            GameManager.Instance.netManager.Send(ReqCode,data);
        }

        public override void Response(Message msg)
        {
            List<PlayerInfo> infos = msg.Value as List<PlayerInfo>;
            if (infos!=null)
            {
                GameManager.Instance.chaManager.UpdatePlayers(infos);
            }

            GameManager.Instance.scManager.LoadScene(2);
           

        }
    }
}
