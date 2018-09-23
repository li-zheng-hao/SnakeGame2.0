/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/19 17:52:20
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
    public class UpdatePosMessage
    {
        [ProtoMember(1)]
        public string UserName;

        [ProtoMember(2)]
        public List<float> posX;

        [ProtoMember(3)]
        public List<float> posY;
    }
}
