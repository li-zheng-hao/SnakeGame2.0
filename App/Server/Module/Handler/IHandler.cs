
namespace Game
{
    public interface IHandler
    {
        /// <summary>
        /// 处理用户特定的请求
        /// </summary>
        void Handle(Session session, Message ms);

        /// <summary>
        /// 处理完消息的响应
        /// </summary>
        void Response(Session session, Message ms);
    }
}
