using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CollisionDetect : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Food"))
            {
                Debug.LogWarning("吃到食物了");
                FoodPoolManager.Instance.changeFoodState(other.gameObject);
                SnakeController.Instance.EatenFood();
            }

            if (other.CompareTag("Border"))
            {
                Debug.LogWarning("撞到墙了");
                SnakeController.Instance.Dead();
            }
        }
    }

}

