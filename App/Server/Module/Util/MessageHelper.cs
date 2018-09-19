/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:05:32
*   描述说明：
*
*****************************************************************/


using System;
using System.IO;

namespace Game
{
    public class MessageHelper
    {
        // 将消息序列化为二进制的方法
        // < param name="model">要序列化的对象< /param>
        public static byte[] Serialize(Message model)
        {
            try
            {
                //涉及格式转换，需要用到流，将二进制序列化到流中
                using (MemoryStream ms = new MemoryStream())
                {
                    //使用ProtoBuf工具的序列化方法
                    ProtoBuf.Serializer.Serialize<Message>(ms, model);
                    //定义二级制数组，保存序列化后的结果
                    byte[] result = new byte[ms.Length];
                    //将流的位置设为0，起始点
                    ms.Position = 0;
                    //将流中的内容读取到二进制数组中
                    ms.Read(result, 0, result.Length);
                    short length = (short)result.Length;
                    var datalength=BitConverter.GetBytes(length);
                    byte[] c = new byte[datalength.Length + result.Length];
                    datalength.CopyTo(c, 0);
                    result.CopyTo(c, datalength.Length);
                    //此处需要将数据包转换成字节数组并且在开头加上 2个字节来表示数据包的大小
                    return c;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("序列化失败" + ex.Message);
                return null;
            }
        }

        // 将收到的消息反序列化成对象
        // < returns>The serialize.< /returns>
        // < param name="msg">收到的消息.</param>
        public static Message DeSerialize(byte[] msg)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //将消息写入流中
                    ms.Write(msg, 0, msg.Length);
                    //将流的位置归0
                    ms.Position = 0;
                    //使用工具反序列化对象
                    return  ProtoBuf.Serializer.Deserialize<Message>(ms);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("反序列化失败+" + ex.Message);
                return null;
            }
        }
    }
}
