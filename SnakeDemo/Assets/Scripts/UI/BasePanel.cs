/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 14:07:34
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Game
{
    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// 进入
        /// </summary>
        public abstract void OnEnter();
        /// <summary>
        /// 退出
        /// </summary>
        public abstract void OnExit();
        /// <summary>
        /// 暂停
        /// </summary>
        public abstract void OnPause();
        /// <summary>
        /// 恢复
        /// </summary>
        public abstract void OnResume();

    }
}
