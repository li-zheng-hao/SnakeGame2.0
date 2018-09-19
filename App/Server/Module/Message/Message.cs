/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:07:03
*   描述说明：
*
*****************************************************************/


using ProtoBuf;

namespace Game
{
    [ProtoContract]
    public class Message
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [ProtoMember(1,IsRequired = true)]
        public int OpCode { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [ProtoMember(2,DynamicType = true)]
        public object Value { get; set; }

        public Message()
        {

        }

        public Message(int opCode, int subCode, object value)
        {
            this.OpCode = opCode;
            this.Value = value;
        }
    }
}
