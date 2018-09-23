/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 17:48:24
*   描述说明：
*
*****************************************************************/


using MongoDB.Driver;

namespace Game
{
    public class DBHelper
    {
        private static DBHelper instance;

        public static DBHelper Instance
        {
            get
            {
                if (instance==null)
                {
                    instance=new DBHelper();
                }

                return instance;
            }
        }
        private const string conn = "mongodb://localhost";
        private const string database = "gamedb";
        private string collection = "account";
        private MongoDatabase db;
        private MongoServer server;

        public void Connect()
        {
           server= MongoDB.Driver.MongoServer.Create(conn);
            //创建数据库链接 MongoServer server = MongoDB.Driver.MongoServer.Create(strconn);
           db= server.GetDatabase(database);

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">插入的对象类型</typeparam>
        /// <param name="obj">插入的对象</param>
        /// <param name="ColName">要插入的集合</param>
        public void Insert<T>(T obj,string ColName)
        {
            //创建数据库链接 MongoServer server = MongoDB.Driver.MongoServer.Create(strconn);
            //获得数据库cnblogs MongoDatabase db = server.GetDatabase(dbName);
            MongoCollection col = db.GetCollection(ColName);
            col.Insert<T>(obj);
        }
        /// <summary>
        ///  更新数据
        ///  注意：千万要注意查询条件和数据库内的字段类型保持一致，否则将无法查找到数据。如：如果Sex是整型，一点要记得将值转换为整型。
        /// </summary>
        /// <param name="collection">集合名</param>
        /// <param name="query">查询条件</param>
        /// <param name="update">更新内容</param>
        public void Update(string collection,QueryDocument query,UpdateDocument update)
        {
            MongoCollection col = db.GetCollection(collection);
            //定义获取“Name”值为“xumingxiang”的查询条件
            //var query = new QueryDocument { { "Name", "xumingxiang" } };
            //定义更新文档
            //var update = new UpdateDocument { { "$set", new QueryDocument { { "Sex", "wowen" } } } };
            col.Update(query, update);//   执行更新操作
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="query">删除条件</param>
        public void Delete(string collection,QueryDocument query)
        {
            //获取Users集合
            MongoCollection col = db.GetCollection(collection);
            //定义获取“Name”值为“xumingxiang”的查询条件
            //var query = new QueryDocument { { "Name", "xumingxiang" } };
            //执行删除操作 
            col.Remove(query);
        }
        public void Query<T>(string collection,QueryDocument query)
        {

            //获取Users集合
            MongoCollection col = db.GetCollection(collection);
            //定义获取“Name”值为“xumingxiang”的查询条件
            //var query = new QueryDocument { { "Name", "xumingxiang" } };

            //查询全部集合里的数据
            var result1 = col.FindAllAs<T>();
            //查询指定查询条件的第一条数据，查询条件可缺省。 var result2 = col.FindOneAs<Users>();
            //查询指定查询条件的全部数据 var result3 = col.FindAs<Users>(query);
        }


    }
}
