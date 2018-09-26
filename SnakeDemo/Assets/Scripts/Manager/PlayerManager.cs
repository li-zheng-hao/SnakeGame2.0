/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 10:08:40
*   描述说明：
*
*****************************************************************/


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerManager:BaseManager
    {
        public Dictionary<string,PlayerInfo> players=new Dictionary<string, PlayerInfo>();
        GameObject bodyPrefab= (GameObject) Resources.Load("Prefabs/Body/skin1_2");
        public List<Snake> snakes= new List<Snake>();
        public Action<Players> UpdateFunc;
        public Players temp;
        /// <summary>
        /// 改成在主线程中调用
        /// </summary>
        /// <param name="player"></param>
        private void UpdatePlayer(PlayerInfo player)
        {
            if (players.ContainsKey(player.username))
            {
                players[player.username] = player;
                //更新角色信息
                Snake snake=snakes.Find(s =>s.username == player.username);
                snake.UpdatePos(player);
            }
            else
            {
                //创建一个新角色
                players.Add(player.username,player);
                GameObject go=new GameObject("player");
                var root = GameObject.Find("Snakes");
                go.transform.SetParent(root.transform);
                Snake sn=go.AddComponent<Snake>();
                snakes.Add(sn);
                //初始化所有信息和数据
                sn.Init(player); 
            }
        }


        /// <summary>
        /// 更新所有的玩家
        /// </summary>
        /// <param name="players"></param>
        public void UpdatePlayers(Players players)
        {
            
            foreach (var pl in players.playerInfos)
            {
                

                if (pl.username!=GameManager.Instance.chaManager.GetLocalPlayerName())
                {
                    UpdatePlayer(pl);
                }
                
            }

        }

     
        public override void Update()
        {
            if (UpdateFunc?.Method!=null)
            {
                UpdateFunc.Invoke(temp);
                UpdateFunc -= UpdatePlayers;
            }
        }

        public override void OnInit()
        {
        }

        public override void OnDestroy()
        {
            
        }
    }
}
