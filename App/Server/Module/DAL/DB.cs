/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 8:37:25
*   描述说明：
*
*****************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Game
{
    public class DB : IDisposable
    {
        private static DB instance = new DB();

        public static DB Instance
        {
            get { return instance; }
        }


        public const string CONNECTIONSTRING =
            "datasource=127.0.0.1;port=3306;database=gamedb;user=root;pwd=123;SSLMode=None;CharSet=utf8";

        private Queue<MySqlConnection> connections;
        private bool isDispose = false;

        private DB()
        {
            connections = new Queue<MySqlConnection>();
            for (int i = 0; i < 50; i++)
            {
                MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
                Connect(conn);
                connections.Enqueue(conn);
            }
        }

        /// <summary>
        /// 获取一个connection 用完记得返还就行
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            return connections.Dequeue();
        }

        private MySqlConnection Connect(MySqlConnection conn)
        {

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("链接数据库的时候实现异常：" + e);
                return null;
            }

        }

        /// <summary>
        /// 返还这个conn
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
                Instance.connections.Enqueue(conn);
            else
            {
                Console.WriteLine("MySqlConnection不能为空");
            }
        }

        #region 资源销毁

        public virtual void Dispose()
        {
            if (!isDispose)
            {
                try
                {
                    foreach (var conn in connections)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    isDispose = true;
                    GC.SuppressFinalize(this);
                }

            }
        }

        ~DB()
        {
            Dispose();
        }

        #endregion

    }
}
