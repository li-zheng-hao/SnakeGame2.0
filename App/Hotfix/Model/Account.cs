/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 17:49:12
*   描述说明：
*
*****************************************************************/


using ProtoBuf;

namespace Game
{
    [ProtoContract]
    public class Account
    {
        [ProtoMember(1)]
        public int id { get; set; }
        [ProtoMember(2)]
        public string username { get; set; }
        [ProtoMember(3)]
        public string password { get; set; }
        [ProtoMember(4)]
        public int goldcount { get; set; }

    }
}
