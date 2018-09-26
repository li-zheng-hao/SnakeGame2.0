/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/8/29 18:31:09
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using UnityEngine;


/// <summary>
/// 管理需要在主线程调用的一些方法
/// </summary>
public class MTFunctionManager:BaseManager
{
    private Queue<Action> queueWaitAction;
    
    public void AddAction(Action action)
    {
        queueWaitAction.Enqueue(action);
    }
    public void AddAction(Action<object> action)
    {
//        queueWaitAction2.Enqueue(action);
    }


    public override void Update()
    {
        while (queueWaitAction.Count!=0)
        {
            var func=queueWaitAction.Dequeue();
            func.Invoke();
        }

       
    }

    public override void OnInit()
    {
        queueWaitAction = new Queue<Action>();
    
    }

    public override void OnDestroy()
    {
        
    }
}
