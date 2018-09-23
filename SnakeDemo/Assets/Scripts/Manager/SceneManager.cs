/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 16:12:48
*   描述说明：
*
*****************************************************************/

namespace Game
{
    public class SceneManager:BaseManager
    {
        private int loadIndex = -1;
        public override void Update()
        {
            if (loadIndex!=-1)
            {
                
                UnityEngine.SceneManagement.SceneManager.LoadScene(loadIndex);
                loadIndex = -1;
            }
        }

        public override void OnInit()
        {
        }

        public override void OnDestroy()
        {

        }

        public void LoadScene(int index)
        {
            loadIndex = index;
        }

    }
}
