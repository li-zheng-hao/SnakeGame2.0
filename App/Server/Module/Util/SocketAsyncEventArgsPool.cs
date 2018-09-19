/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/16 20:03:14
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    public class SocketAsyncEventArgsPool
    {
        //已使用记录
        private List<Int32> usedRecord;
        //未使用记录
        private List<Int32> unUsedRecord;
        //池子
        private List<SocketAsyncEventArgs> pool;
        //池子最大容量
        private int capacity;
        //是否动态扩展容量
        // private bool dynamic = false;

        /**池子初始化*/
        private void init()
        {
            this.pool = new List<SocketAsyncEventArgs>(this.capacity);
            this.usedRecord = new List<Int32>(this.capacity);
            this.unUsedRecord = new List<Int32>(this.capacity);
            //        for (int i = 0; i < this.capacity; i++)
            //        {
            //            this.unUsedRecord.Add(i);
            //            this.pool.Add(new SocketAsyncEventArgs());
            //        }
        }

        ///////////////////公开方法////////////////////////
        /**获取可使用数量**/
        public int GetUsedCount()
        {
            return this.capacity - this.usedRecord.Count;
        }
        /**获取可使用 SocketAsyncEventArgs */
        public SocketAsyncEventArgs Pop()
        {
            int index = 0;
            lock (this)
            {
                if (GetUsedCount() <= 0)
                {
                    extCapacity();
                }
                index = this.unUsedRecord[0];
                this.unUsedRecord.RemoveAt(0);
                this.usedRecord.Add(index);
                return pool[index];
            }
        }
        /**回收 SocketAsyncEventArgs */
        public void Push(SocketAsyncEventArgs args)
        {
            int index = 0;
            lock (this)
            {
                index = pool.FindIndex(item => item.Equals(args));

                this.unUsedRecord.Add(index);
                this.usedRecord.Remove(index);
            }
        }

        public void InitPool(SocketAsyncEventArgs args)
        {
            pool.Add(args);
            this.unUsedRecord.Add(pool.Count - 1);

        }
        /** 扩展容量   */
        private void extCapacity()
        {
            //        int minNewCapacity = 200;
            //        int newCapacity = Math.Min(this.capacity, minNewCapacity);
            //
            //        //每次以minNewCapacity倍数扩展
            //        if (newCapacity > minNewCapacity)
            //        {
            //            newCapacity += minNewCapacity;
            //        }
            //        else
            //        {
            //            //以自身双倍扩展空间
            //            newCapacity = 64;
            //            while (newCapacity < minNewCapacity)
            //            {
            //                newCapacity <<= 1;
            //            }
            //        }
            //
            //
            //        for (int i = this.capacity; i < newCapacity; i++)
            //        {
            //            this.unUsedRecord.Add(i);
            //            this.pool.Add(sock.valueOf(i));
            //        }
            //
            //        this.capacity = newCapacity;
        }


        //getter
        public int GetCapacity()
        {
            return this.capacity;
        }

        /**构建方法*/
        public SocketAsyncEventArgsPool(int maxCapacity)
        {
            capacity = maxCapacity;
            init();
        }
    }
}
