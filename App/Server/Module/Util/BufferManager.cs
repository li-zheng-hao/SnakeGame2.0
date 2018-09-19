/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:01:58
*   描述说明：
*
*****************************************************************/
using System.Collections.Generic;
using System.Net.Sockets;



namespace Game
{
    public class BufferManager
    {
        /// <summary>
        /// 缓冲池控制的总的字节数量
        /// </summary>
        int m_numBytes;
        /// <summary>
        /// BufferManager保持的所有的字节数组
        /// </summary>
        byte[] m_buffer;
        /// <summary>
        /// 仍然可用的内存池索引
        /// </summary>
        Stack<int> m_freeIndexPool;
        /// <summary>
        /// 当前缓冲区的索引 单位是字节 从0-m_numBytes
        /// </summary>
        int m_currentIndex;
        /// <summary>
        /// 每个缓冲区的大小
        /// </summary>
        int m_bufferSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalBytes">总的管理的字节数</param>
        /// <param name="bufferSize">每一个缓冲区的字节数</param>
        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }

        // Allocates buffer space used by the buffer pool
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object


            //创建一个大型的缓冲区并且把它分配给每个SocketAsyncEventArg对象使用
            m_buffer = new byte[m_numBytes];
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        //
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                if ((m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    return false;
                }
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }

        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
