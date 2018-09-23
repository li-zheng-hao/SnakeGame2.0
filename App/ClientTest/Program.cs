using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Game;

namespace ClientTest
{
    class Program
    {

        static void Main(string[] args)
        {
   
//            DBHelper helper=new DBHelper();
//            helper.Connect();
//            helper.Insert();
            Account ac=new Account();
            ac.username = "123";
            ac.password = "123";
//            DBHelper.Instance.Insert(ac,"account");

        }

    }
}
