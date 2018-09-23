/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/22 14:40:46
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class RegistPanel:BasePanel
    {
        private InputField username;
        private InputField password;
        private Button regist;
        private Button cancel;

        private void Awake()
        {
            username = transform.Find("username").GetComponent<InputField>();
            password = transform.Find("password").GetComponent<InputField>();

            regist = transform.Find("regist").GetComponent<Button>();
            cancel = transform.Find("cancel").GetComponent<Button>();
            regist.onClick.AddListener(RegistClick);
            cancel.onClick.AddListener(CancelClick);
        }

        private void CancelClick()
        {
            GameManager.Instance.uiManager.PopPanel();
        }

        private void RegistClick()
        {
            //todo

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
