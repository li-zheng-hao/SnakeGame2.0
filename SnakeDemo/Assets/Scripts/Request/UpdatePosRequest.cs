/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 8:25:10
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
    public class UpdatePosRequest:BaseRequest
    {
        public override void SendRequest(RequestCode ReqCode, object data)
        {
            GameManager.Instance.netManager.Send(ReqCode,data);
        }

        public override void Response(Message msg)
        {
            //这里收到的是所有在房间内的用户的信息
            var info = msg.Value as PlayerInfo;
            //在这里根据服务器发来的信息更新本地的玩家信息
            GameManager.Instance.plManager.temp = info;
            GameManager.Instance.plManager.UpdateFunc += GameManager.Instance.plManager.UpdatePlayer;
            
        }
    }
}
