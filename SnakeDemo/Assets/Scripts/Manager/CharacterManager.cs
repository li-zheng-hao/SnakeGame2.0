/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 15:19:45
*   描述说明：
*
*****************************************************************/


using System.Collections.Generic;


namespace Game
{
    public class CharacterManager:BaseManager
    {
        private List<PlayerInfo> players;
        private AccountRT localPlayer;

        public override void Update()
        {
            
        }

        public override void OnInit()
        {
            players=new List<PlayerInfo>();
        }

        public override void OnDestroy()
        {
        }

        public void SetLocalPlayer(AccountRT ac)
        {
            localPlayer = ac;
        }

        public string GetLocalPlayerName()
        {
            return localPlayer.username;
        }

        public void UpdatePlayers(List<PlayerInfo> playersInfos)
        {
            players.Clear();
            foreach (var pl in playersInfos)
            {
                if (pl.username!=localPlayer.username)
                {
                    players.Add(pl);
                }
            }
        }
    
    }
}
