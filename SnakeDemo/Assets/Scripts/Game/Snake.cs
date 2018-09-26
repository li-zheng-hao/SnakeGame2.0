/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 10:26:08
*   描述说明：
*
*****************************************************************/


using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Snake:MonoBehaviour
    {
        private GameObject headPrefab;
        private GameObject bodyPrefab;
        public string username;
        private GameObject RootParent;
        private GameObject head;

        private  GameObject bodys;
        private List<Position> positions;
        private List<GameObject> bodysList;
        private GameObject initbody;
        private void Awake()
        {
            headPrefab = (GameObject)Resources.Load("Prefabs/Body/skin1_1");
            bodyPrefab= (GameObject)Resources.Load("Prefabs/Body/skin1_2");
            bodys=new GameObject("bodys");
            bodysList=new List<GameObject>();
            bodys.transform.SetParent(this.transform);

        }
        /// <summary>
        /// 初始化对象的数据
        /// </summary>
        /// <param name="playerInfo"></param>
        public void Init(PlayerInfo playerInfo)
        {
//            headPrefab = (GameObject)Resources.Load("Prefabs/Body/skin1_1");
//            bodyPrefab = (GameObject)Resources.Load("Prefabs/Body/skin1_2");
//            bodys = new GameObject("bodys");
//            bodysList = new List<GameObject>();
//            bodys.transform.SetParent(this.transform);
            username = playerInfo.username;
            positions = playerInfo.pos;
            for (int i = 0; i < playerInfo.pos.Count; i++)
            {
                Vector2 vec = new Vector2(playerInfo.pos[i].posx, playerInfo.pos[i].posy);
                if (i == 0)
                {
                    InitHead(vec);
                }
                else
                {
                    InitBody(vec);
                }
            }
        }
        private void InitBody(Vector2 vec)
        {
            initbody = Instantiate(bodyPrefab);
            
            this.bodysList.Add(initbody);
            initbody.transform.SetParent(bodys.transform);
            initbody.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            initbody.transform.position.Set(vec.x,vec.y,0);
        }

        private void InitHead(Vector2 pos)
        {
            head = Instantiate(headPrefab);
            head.transform.localScale=new Vector3(0.5f,0.5f,1f);
            head.transform.SetParent(this.transform);
            head.transform.position.Set(pos.x,pos.y,0);
        }


        private PlayerInfo info;

        private List<PlayerInfo>playerInfo=new List<PlayerInfo>();

        
        /// <summary>
        /// 更新位置信息或者长度
        /// </summary>
        /// <param name="playerInfo"></param>
        public void UpdatePos(PlayerInfo playerInfo)
        {
            this.playerInfo.Add(playerInfo);
            info = playerInfo;
         
            //            //说明变长了
            //            if (playerInfo.pos.Count - bodysList.Count > 1)
            //            {
            //                int i = 0, j = bodysList.Count + 1;
            //                for (; i<j; i++)
            //                {
            //                    Vector2 vec = new Vector2(playerInfo.pos[i].posx, playerInfo.pos[i].posy);
            //                    if (i == 0)
            //                    {
            //                        head.transform.position.Set(vec.x, vec.y, 0);
            //                    }
            //                    else
            //                    {
            //                        bodysList[i].transform.position.Set(vec.x, vec.y, 0);
            //                    }
            //                }
            //
            //                for (;i<playerInfo.pos.Count;i++)
            //                {
            //                    Vector2 vec = new Vector2(playerInfo.pos[i].posx, playerInfo.pos[i].posy);
            //                    GameObject go = Instantiate(bodyPrefab);
            //                    bodysList.Add(go);
            //                    go.transform.SetParent(bodys.transform);
            //                    go.transform.position.Set(vec.x, vec.y, 0);
            //
            //                }
            //            }
            //            //没有变长
            //            else
            //            {
            //                for (int i = 0; i < playerInfo.pos.Count; i++)
            //                {
            //                    Vector2 vec = new Vector2(playerInfo.pos[i].posx, playerInfo.pos[i].posy);
            //                    Vector3 pos = new Vector3(vec.x, vec.y, 0f);
            //                    if (i == 0)
            //                    {
            //                        
            //                        var res=Vector3.Lerp(head.transform.position, pos, Time.deltaTime);
            //                        head.transform.position = new Vector3(vec.x,vec.y,0);
            //                    }
            //                    else
            //                    {
            //                        var res = Vector3.Lerp(bodysList[i-1].transform.position, pos,Time.deltaTime);
            //                        bodysList[i - 1].transform.position = new Vector3(vec.x,vec.y,0);
            //                    }
            //                }
            //            }
        }

        private float startTime=0f;
        private float lerpRate = 17f;
        private float closeEnough = 0.11f;
        private Vector3 headEnd;
        List<Vector3> playerPos = new List<Vector3>();
        List<Vector3> posEnd = new List<Vector3>();
        
        private void Update()
        {
            if (startTime<0.001f)
            {
                startTime = Time.time;
            }
            startTime = Time.time;
            if (playerInfo.Count > 0)
            {
                playerPos.Clear();
                playerPos.Add(head.transform.position);
                foreach (var body in bodysList)
                {
                    playerPos.Add(body.transform.position);
                }
                posEnd.Clear();
                
                foreach (var pos in playerInfo[0].pos)
                {
                    posEnd.Add(new Vector3(pos.posx,pos.posy,0f));
                }
                //                MoveToPos(playerPos,posEnd);
                for (int i = 0; i < playerPos.Count; i++)
                {
                    if (i == 0)
                    {
                        head.transform.position = Vector3.Lerp(playerPos[i], posEnd[i], (Time.time-startTime) * lerpRate);
                        Debug.LogWarning(Time.time+"Time");
                        Debug.LogWarning(startTime+"starTIme");
                    }
                    else
                    {

                        bodysList[i - 1].transform.position = Vector3.Lerp(playerPos[i], posEnd[i], (Time.deltaTime) * lerpRate);
                    }


                }
//                Debug.LogWarning(playerInfo.Count+"个");
                float distance = Vector3.Distance(head.transform.position, posEnd[0]);
                if (distance < closeEnough)
                {
                    startTime = Time.time;
                    playerInfo.RemoveAt(0);
                }
                
//                if (playerInfo.Count>5)
//                {
//                    lerpRate = 18.0f;
//                }
//                else
                 {
//                    lerpRate = 15.0f;
                 }
            } 

             
//            if (this.playerInfo.Count>1)
//            {
//                var pos = playerInfo[0].pos;
//               
//                for (int i = 0; i < pos.Count; i++)
//                {
//                    Vector2 vec= new Vector2(pos[i].posx,pos[i].posy);
//
//                    if (i == 0)
//                    {
//                        head.transform.position = Vector3.Lerp(new Vector3(vec.x,vec.y,0), new Vector3(vec.x, vec.y, 0f), Time.deltaTime*lerpRate);
//                        
//                    }
//                    else
//                    {
//
//                        bodysList[i - 1].transform.position = Vector3.Lerp(new Vector3(vec.x,vec.y,0), new Vector3(vec.x, vec.y, 0f), Time.deltaTime*lerpRate);         
//                    }
//
//                  
//   
//                }
//
//                if (playerInfo.Count > 5)
//                {
//                    lerpRate = 18.0f;
//                }
//                else
//                {
//                    lerpRate = 14.0f;
//                }
//                float distance = Vector3.Distance(head.transform.position, new Vector3(pos2[0].posx, pos2[0].posy, 0f));
//                
//                if (distance < closeEnough)
//                {
//                    playerInfo.RemoveAt(0);
//                }
//
//                Debug.Log(distance+"_______"+playerInfo.Count);
//            }
//            

        }

        public void MoveToPos(List<Vector3> posBegin, List<Vector3> posEnd)
        {
            startTime = Time.time;
            while (true)
            {
                for (int i = 0; i < posBegin.Count; i++)
                {
                    if (i == 0)
                    {
                        head.transform.position = Vector3.Lerp(posBegin[i], posEnd[i], (Time.time - startTime) * lerpRate); 
                    }
                    else
                    {
                        bodysList[i - 1].transform.position = Vector3.Lerp(posBegin[i], posEnd[i], (Time.time - startTime) * lerpRate);
                    }
                }

                float distance = Vector3.Distance(head.transform.position, posEnd[0]);
                if (distance < closeEnough)
                {
                    break;
                }
            }
            
        }
    }
}
