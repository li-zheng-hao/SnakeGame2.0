/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 21:07:01
*   描述说明：
*
*****************************************************************/


using System.Collections.Generic;

namespace Game
{
    public class Room
    {
        private List<Player> users;

        public Room()
        {
            users=new List<Player>();
            
        }

        public List<PlayerInfo> GetPlayer()
        {
            List<PlayerInfo>  pl=new List<PlayerInfo>();
            foreach (var u in users)
            {
                PlayerInfo player=new PlayerInfo();
                player.username = u.username;
                player.pos = u.pos;
                pl.Add(player);
            }

            return pl;
        }
    }
}
