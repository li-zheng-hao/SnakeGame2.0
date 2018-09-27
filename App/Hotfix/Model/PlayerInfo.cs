/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 21:25:29
*   描述说明：
*
*****************************************************************/


using System.Collections.Generic;
using ProtoBuf;

namespace Game
{
    [ProtoContract]
    public class PlayerInfo
    {
        [ProtoMember(1)]
        public string username;
        [ProtoMember(2)]
        public List<Position> pos;

        [ProtoMember(3)]
        public float time;

        public PlayerInfo()
        {
            pos=new List<Position>();
        }
    }
}
