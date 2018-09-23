/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/21 21:31:45
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game
{
    public abstract class BaseManager
    {
        /// <summary>
        /// 实现更新功能
        /// </summary>
        public abstract void Update();

        public abstract void OnInit();

        public abstract void OnDestroy();
    }
}
