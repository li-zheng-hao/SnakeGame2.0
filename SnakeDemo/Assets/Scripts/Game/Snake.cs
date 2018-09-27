/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/25 10:26:08
*   描述说明：
*
*****************************************************************/


using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Snake : MonoBehaviour
    {
        //private GameObject headPrefab;
        //    private GameObject bodyPrefab;
        //public string username;
        //    private GameObject RootParent;
        //    private GameObject head;

        //    private  GameObject bodys;
        //    private List<Position> positions;
        //    private List<GameObject> bodysList;
        //    private GameObject initbody;




        //    [Header("Lerping Properties")]
        //    public bool isLerpingPosition;
        //    public List<Vector3> realPosition=new List<Vector3>();
        //    public List<Vector3> lastRealPosition=new List<Vector3>();
        //    public float timeStartedLerping;
        //    public float timeToLerp;

        //    private void Awake()
        //    {
        //        headPrefab = (GameObject)Resources.Load("Prefabs/Body/skin1_1");
        //        bodyPrefab= (GameObject)Resources.Load("Prefabs/Body/skin1_2");
        //        bodys=new GameObject("bodys");
        //        bodysList=new List<GameObject>();
        //        bodys.transform.SetParent(this.transform);

        //    }
        //    /// <summary>
        //    /// 初始化对象的数据
        //    /// </summary>
        //    /// <param name="playerInfo"></param>
        //    public void Init(PlayerInfo playerInfo)
        //    {
        //        username = playerInfo.username;
        //        positions = playerInfo.pos;
        //        for (int i = 0; i < playerInfo.pos.Count; i++)
        //        {
        //            Vector2 vec = new Vector2(playerInfo.pos[i].posx, playerInfo.pos[i].posy);
        //            if (i == 0)
        //            {
        //                InitHead(vec);
        //            }
        //            else
        //            {
        //                InitBody(vec);
        //            }
        //        }

        //        isLerpingPosition = false;

        //        realPosition.Add(head.transform.position);
        //        foreach (var body in bodysList)
        //        {
        //            realPosition.Add(body.transform.position);
        //        }

        //    }
        //    private void InitBody(Vector2 vec)
        //    {
        //        initbody = Instantiate(bodyPrefab);

        //        this.bodysList.Add(initbody);
        //        initbody.transform.SetParent(bodys.transform);
        //        initbody.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        //        initbody.transform.position.Set(vec.x,vec.y,0);
        //    }

        //    private void InitHead(Vector2 pos)
        //    {
        //        head = Instantiate(headPrefab);
        //        head.transform.localScale=new Vector3(0.5f,0.5f,1f);
        //        head.transform.SetParent(this.transform);
        //        head.transform.position.Set(pos.x,pos.y,0);
        //    }




        //    private List<PlayerInfo> playerInfo = new List<PlayerInfo>();


        //    /// <summary>
        //    /// 更新位置信息或者长度
        //    /// </summary>
        //    /// <param name="playerInfo"></param>
        //    public void UpdatePos(PlayerInfo playerInfo)
        //    {
        //        //this.playerInfo.Add(playerInfo);
        //        //isLerpingPosition = true;
        //        lastRealPosition = realPosition;
        //        realPosition.Clear();

        //        foreach (var pos in playerInfo.pos)
        //        {
        //            realPosition.Add(new Vector3(pos.posx, pos.posy, 0f));
        //        }

        //        timeToLerp = playerInfo.time;
        //        if (realPosition[0] != head.transform.position)
        //        {
        //            isLerpingPosition = true;
        //        }
        //        timeStartedLerping = Time.time;
        //    }

        //    //public void UpdateInfo()
        //    //{
        //    //    if(playerInfo.Count>0)
        //    //    {
        //    //        lastRealPosition = realPosition;
        //    //        realPosition.Clear();

        //    //        foreach (var pos in playerInfo[0].pos)
        //    //        {
        //    //            realPosition.Add(new Vector3(pos.posx, pos.posy, 0f));
        //    //        }

        //    //        timeToLerp = playerInfo[0].time;
        //    //        //if (realPosition[0] != head.transform.position)
        //    //        //{
        //    //        //    isLerpingPosition = true;
        //    //        //}
        //    //        timeStartedLerping = Time.time;
        //    //    }
        //    //}
        //    private void FixedUpdate()
        //    {
        //        NetworkLerp();  
        //    }
        //    //private float lerpRate = 1f;

        //    private void NetworkLerp()
        //    {
        //        if(isLerpingPosition)
        //        {
        //            float lerpPercentage = (Time.time - timeStartedLerping)/ timeToLerp;
        //            Debug.LogWarning(lerpPercentage);
        //            for(int i=0;i<realPosition.Count;i++)
        //            {
        //                if(i==0)
        //                {
        //                    head.transform.position = Vector3.Lerp(lastRealPosition[i], realPosition[i], lerpPercentage);
        //                }
        //                else
        //                {
        //                    bodysList[i - 1].transform.position = Vector3.Lerp(lastRealPosition[i], realPosition[i], lerpPercentage);
        //                }
        //            }
        //            if(lerpPercentage>=1f)
        //            {
        //                isLerpingPosition = false;
        //            }

        //        }

        //    }
        //}
    }
}