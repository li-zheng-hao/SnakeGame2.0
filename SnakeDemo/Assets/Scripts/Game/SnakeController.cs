using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine.UI;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    private static SnakeController instance;

    public static SnakeController Instance
    {
        get
        {
            if (instance==null)
            {
                 instance = FindObjectOfType<SnakeController>();
            }
            return instance;
        }
    }
    [Tooltip("左侧虚拟摇杆")]
    public EasyJoystick easyJoystick;
    [Tooltip("蛇移动的速度")]
    public int speed;
    [Tooltip("蛇头")]
    public Sprite snakeHeads;
    [Tooltip("蛇身体")]
    public Sprite snakeBodys;
    [Tooltip("初始身体的数量")]
    public int initBodyNum = 5;

    [Tooltip("蛇头对象")]
    public GameObject snakeHead;
    [Tooltip("蛇身体对象")]
    public GameObject snakeBody;
    private Quaternion direction;
    //蛇头产生的一些坐标
    private List<Vector2> oldPositionList;
    //蛇身体移动的步数
    private int positionLength = 5;
    //生成的蛇身体
    public List<GameObject> _bodys;
    //皮肤的编号
    private int skinNum;
    // Use this for initialization
    public int addLengthNeedFood = 20000;
    public int addLengthNeedFoodReset = 20000;
    [Tooltip("击杀的敌人")]
    public int killEnemyNum = 0;
    public bool isSpeedUp = false;
    public Text gameOverLengthText;
    public Text killEnemyText;

    //下面这些是长度和击杀敌人个数
    private GameObject gj;
    private GameObject gj2;
    private Text text;
    private Text text2;

    [Header("Ship Movement Properties")]
    public bool canSendNetworkMovement;
    public float networkSendRate = 5;
    public float timeBetweenMovementStart;
    public float timeBetweenMovementEnd;

    void Start()
    {
        //Debug.Log("游戏开始了，蛇头的名字是" + "skin" + StaticData.Instance.usingSkinName + "head");
        gj = GameObject.Find("length_text");
        gj2 = GameObject.Find("kill_text");
        text = gj.GetComponent<Text>();
        text2= gj2.GetComponent<Text>();
        
        InitHead();
        InitBody();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "长度:  " + @"<color=blue>" + _bodys.Count.ToString() + @"</color>";
        text2.text = "击杀:  " + @"<color=blue>" + killEnemyNum.ToString() + @"</color>";
        UpdateRotationAndMove();

        if(!canSendNetworkMovement)
        {
            canSendNetworkMovement = true;
            StartCoroutine("UpdatePos");
        }

    }

    IEnumerator UpdatePos()
    {
        timeBetweenMovementStart = Time.time;
        yield return new WaitForSeconds(1/networkSendRate);
        SendMoveRequest();


    }
    public void SendMoveRequest()
    {
        timeBetweenMovementEnd = Time.time;

        UpdatePosRequest re = new UpdatePosRequest();

        PlayerInfo info = new PlayerInfo();
        info.username = GameManager.Instance.chaManager.GetLocalPlayerName();
        Position headpos = new Position();
        headpos.posx = snakeHead.transform.position.x;
        headpos.posy = snakeHead.transform.position.y;
        info.pos.Add(headpos);
        foreach (var body in _bodys)
        {
            Position p = new Position();
            p.posx = body.transform.position.x;
            p.posy = body.transform.position.y;
            info.pos.Add(p);
        }
        info.time = (timeBetweenMovementEnd - timeBetweenMovementStart);

        Debug.LogWarning(info.time+"发出去的时间");
        re.SendRequest(RequestCode.UpdatePos, info);


        canSendNetworkMovement = false;

    }
    /// <summary>
    /// 更新头部的旋转角度
    /// </summary>
    private void UpdateRotationAndMove()
    {
        Vector3 joystickAxis = easyJoystick.JoystickAxis;
        int tempRunTime = 1;
        if (isSpeedUp == true)
            tempRunTime = 2;
        for (int i = 0; i < tempRunTime; i++)
        {
            oldPositionList.Insert(0, transform.position);
            if (joystickAxis == Vector3.zero)
            {
                //Vector3 vec = direction * Vector3.up;
                //transform.position += vec.normalized * speed * Time.deltaTime;

            }
            else
            {
                transform.position += joystickAxis.normalized * speed * Time.deltaTime;
                direction = Quaternion.FromToRotation(Vector2.up, joystickAxis);
                transform.rotation = direction;

            }

            FollowHead();
        }

    }

    /// <summary>
    /// 初始化头部
    /// </summary>
    private void InitHead()
    {

        snakeHead.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //创建蛇身体的存储
        oldPositionList = new List<Vector2>();
        //一开始有5个蛇身体，每个身体的间隔为positionLength个单元

        for (int i = 0; i < 6 * positionLength + 1; i++)
        {
            oldPositionList.Add(new Vector2(snakeHead.transform.position.x, snakeHead.transform.position.y - 0.07f * (i + 1)));
        }

    }
    /// <summary>
    /// 初始化身体
    /// </summary>
    private void InitBody()
    {
        _bodys = new List<GameObject>();
        for (int i = 0; i < initBodyNum; i++)
        {
            GameObject go = new GameObject("body");
            var comp = go.AddComponent<SpriteRenderer>();
            comp.sprite = snakeBodys;
            //因为父类对象缩小了0.5，所以这里要除以2
            go.transform.SetParent(snakeBody.transform);
            //go.transform.position = new Vector3(transform.position.x,
            //go.transform.position=new Vector3(transform.position.x,
            //-0.5f * (i + 1),
            //0);
            //    , transform.position.z);
            comp.sortingLayerName = "character";
            go.transform.localScale = new Vector3(1f, 1f, 1f);
            var collider = go.AddComponent<CircleCollider2D>();
            go.tag = "Player";
            _bodys.Add(go);

        }

    }
    /// <summary>
    /// 增加身体
    /// </summary>
    private void AddBody()
    {
        GameObject go = new GameObject("body");
        var comp = go.AddComponent<SpriteRenderer>();
        comp.sprite = snakeBodys;
        
        go.transform.SetParent(snakeBody.transform);
        comp.sortingLayerName = "character";
        go.transform.localScale = new Vector3(1f, 1f, 1f);
        go.transform.position = transform.position;
        var collider = go.AddComponent<CircleCollider2D>();
        go.tag = "Player";
        // AddAIBody(go.transform);
        _bodys.Add(go);

    }
    /// <summary>
    /// 跟随头部
    /// </summary>
    private void FollowHead()
    {

        for (int i = 0; i < _bodys.Count; i++)
        {

            _bodys[i].transform.position = oldPositionList[(i + 1) * (positionLength)];

        }


        if (oldPositionList.Count > _bodys.Count * positionLength + 40)
        {
            oldPositionList.RemoveAt(oldPositionList.Count - 1);
        }


        //}
        //if ((_bodys.Count+2)*positionLength> oldPositionList.Count)


    }
    /// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //撞到墙，游戏结束
        if (collision.tag == "Border")
        {
            //todo 游戏结束,给服务器发送自己死亡的信息
        }
        else if (collision.tag == "Food")
        {
            
          
        }
        else if (collision.tag == "Player")
        {
            //todo 给服务器发送自己死亡的信息
        }
        else if (collision.tag == "BigFood")
        {
            addLengthNeedFood -= 3;
            Destroy(collision.gameObject);
            if (addLengthNeedFood <= 0)
            {
                AddBody();
                addLengthNeedFood = addLengthNeedFoodReset;
            }
        }


    }
    /// <summary>
    /// 吃到食物了
    /// </summary>
    public void EatenFood()
    {
        addLengthNeedFood--;
        if (addLengthNeedFood == 0)
        {
            AddBody();
            //todo 需要不断地发送给服务器自己的身体信息
            addLengthNeedFood = addLengthNeedFoodReset;
        }
    }

    public void Dead()
    {
        Debug.LogWarning("游戏结束");
    }
    /// <summary>
    /// 击杀了敌人
    /// </summary>
    public void KillEnemy()
    {
        this.killEnemyNum++;
    }
    public void SetSpeedUp(bool result)
    {
        isSpeedUp = result;
    }
}
