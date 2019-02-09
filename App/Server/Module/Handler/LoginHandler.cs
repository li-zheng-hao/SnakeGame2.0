/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 22:02:57
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
    public class LoginHandler:IHandler
    {
        public void Handle(Session session, Message ms)
        {
            Console.WriteLine("Loginhandler");
//            var data=ms.Value as Account;
//            AccountDAO acc=new AccountDAO();
//            var res=acc.VerifyUser(DB.Instance.GetConnection(), data.username, data.password);
//            Message resms=new Message(RequestCode.Login,res);
//            Response(session,resms);
            Response(session,ms);
        }
        
        public void Response(Session session, Message ms)
        {
            var datas = MessageHelper.Serialize(ms);
            session.SendAsync(datas);
        }
    }
}
