/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/27 19:53:18
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class OtherPlayer:MonoBehaviour
    {
        [Header("玩家的唯一标识")]
        public string playerID;

        [Header("移动的一些属性")]
        public bool canSendNetworkMovement;
        public float speed;
        public float networkSendRate = 5;
        public float timeBetweenMovementStart;
        public float timeBetweenMovementEnd;

        [Header("线性插值的属性")]
        public bool isLerpingPosition;
        public Vector3 realPosition;
        public Vector3 lastRealPosition;
        public float timeStartedLerping;
        public float timeToLerp;

        [Header("其他的属性")]
        public GameObject prefab;
        private void Start()
        {
            isLerpingPosition = false;
            realPosition = transform.position;
        }

        public void ReceiveMovementMessage(Vector3 _position, float _timeToLerp)
        {
            lastRealPosition = realPosition;
            realPosition = _position;
            timeToLerp = _timeToLerp;
            if (realPosition != transform.position)
            {
                isLerpingPosition = true;
            }
            timeStartedLerping = Time.time;
            Debug.LogWarning(_timeToLerp + "收到的执行时间");
        }

        private void FixedUpdate()
        {
                NetworkLerp();

        }

        private void NetworkLerp()
        {
            if (isLerpingPosition)
            {
                float lerpPercentage = (Time.time - timeStartedLerping) / timeToLerp;
                transform.position = Vector3.Lerp(lastRealPosition, realPosition, lerpPercentage);
            }

        }
    }
}
