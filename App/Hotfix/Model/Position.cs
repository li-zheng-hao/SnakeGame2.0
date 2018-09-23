/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 21:19:47
*   描述说明：
*
*****************************************************************/
using ProtoBuf;


namespace Game
{
    [ProtoContract]
    public class Position
    {
        [ProtoMember(1)]
        public float pox;
        [ProtoMember(2)]
        public float posy;
    }
}
