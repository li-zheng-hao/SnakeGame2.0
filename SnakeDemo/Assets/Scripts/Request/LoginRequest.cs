/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 22:26:58
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Game
{
    public class LoginRequest:BaseRequest
    {
        public override void SendRequest(RequestCode ReqCode, object data)
        {
            GameManager.Instance.netManager.Send(ReqCode,data);
        }

        public override void Response(Message msg)
        {
            if (msg.Value!=null)
            {
                var acc=msg.Value as Account;
                AccountRT acrt=new AccountRT(acc);
                GameManager.Instance.chaManager.SetLocalPlayer(acrt);
                Debug.LogWarning("登陆成功");
                GameManager.Instance.uiManager.ClearStack();
                GameManager.Instance.scManager.LoadScene(1);

            }
            else
            {
                GameManager.Instance.uiManager.ShowMessageAsync("提示：帐号密码输入错误");
            }
        }
    }
}
