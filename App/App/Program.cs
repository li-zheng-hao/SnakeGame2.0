using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkCenter app=new NetworkCenter();
            app.server.Start();
            while (true)
            {
                
            }
        }
    }
}
