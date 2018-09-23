/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:11:21
*   描述说明：
*
*****************************************************************/

using ProtoBuf;


namespace Game
{
    [ProtoContract]
    public class RegisterMessage
    {
        [ProtoMember(1)]
        public string Username;

        [ProtoMember(2)]
        public string Password;
    }
}

