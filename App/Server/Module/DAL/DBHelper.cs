///****************************************************************
//*   作者：李正浩
//*   联系方式: QQ 1263212577
//*   CLR版本：4.0.30319.42000
//*   创建时间： 2018/9/22 17:48:24
//*   描述说明：
//*
//*****************************************************************/
//
//
//using MongoDB.Driver;
//
//namespace Game
//{
//    public class DBHelper
//    {
//        private static DBHelper instance;
//
//        public static DBHelper Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new DBHelper();
//                }
//
//                return instance;
//            }
//        }
//
//        private MongoClient client;
//        private IMongoDatabase db;
//        private DBHelper()
//        {
//           client = new MongoClient("mongodb://localhost:27017");
//           db = client.GetDatabase("gamedb");
//        }
//
//        public void Insert<T>(T data,string colstr)
//        {
//            var col=db.GetCollection<T>(colstr);
//            col.InsertOne(data);
//        }
//
//
//    }
//}
