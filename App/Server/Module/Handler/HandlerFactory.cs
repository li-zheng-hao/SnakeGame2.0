/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:26:35
*   描述说明：
*
*****************************************************************/


using System;
using System.Reflection;

namespace Game
{
    public static class HandlerFactory
    {
        public const string dllName = "Server";
        public const string classTail = "Handler";
        /// <summary>
        /// 通过反射来创建相应的处理类
        /// </summary>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public static IHandler CreateHandler(OpCode opCode)
        {
            string name = Enum.GetName(typeof(OpCode), opCode);
            string className = name + classTail;
            Assembly assem = Assembly.Load(dllName);
            Type type = assem.GetType(dllName + "." + className);
            return Activator.CreateInstance(type) as IHandler;
        }
    }
}
