/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 16:07:13
*   描述说明：
*
*****************************************************************/

using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class AccountPanel:BasePanel
    {
        private Button multi;
        private Button bot;
        private Button shop;
        private Text username;
        
        private void Awake()
        {
            multi = transform.Find("multiMode").GetComponent<Button>();
            bot = transform.Find("botMode").GetComponent<Button>();
            shop = transform.Find("skin").GetComponent<Button>();
            username = transform.Find("username").GetComponent<Text>();
        }

        private void Start()
        {
            GameManager.Instance.uiManager.PushPanel(this);
            Debug.LogWarning($"进行到了这里");
            username.text = GameManager.Instance.chaManager.GetLocalPlayerName();
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
        }

        public override void OnPause()
        {
        }

        public override void OnResume()
        {

        }
    }
}
