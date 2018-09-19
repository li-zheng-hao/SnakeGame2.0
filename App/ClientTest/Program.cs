using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect("127.0.0.1", 55555);
                //                LoginMessage loginMessage = new LoginMessage() { Password = "111", Username = "111" };
                UpdatePosMessage data=new UpdatePosMessage(){posX = new List<float>{1,2,3},posY = new List<float>{1,2,3}};
                Message ms = new Message(0, data);

                client.Send(MessageHelper.Serialize(ms));
            }
           
        }
    }
}
