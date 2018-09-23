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
        }

        public void Response(Session session, Message ms)
        {
        }
    }
}
