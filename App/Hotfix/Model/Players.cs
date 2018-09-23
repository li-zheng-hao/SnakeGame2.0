/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 22:10:58
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Game
{
    [ProtoContract]
    public class Players
    {
        [ProtoMember(1)]
        public List<PlayerInfo> playerInfos;
    }
}
