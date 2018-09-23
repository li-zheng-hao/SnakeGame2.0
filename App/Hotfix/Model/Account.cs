/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 17:49:12
*   描述说明：
*
*****************************************************************/

using MongoDB.Bson;

namespace Game
{
    public class Account
    {
        public ObjectId _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        
    }
}
