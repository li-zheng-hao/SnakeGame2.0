/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/21 21:47:31
*   描述说明：
*
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
   
    public class UIManager:BaseManager
    {
        private Canvas canvas;
        private Stack<BasePanel> panels;
        private Text message;
        private string showMsg="";
        public override void Update()
        {
            if (showMsg!=string.Empty)
            {
                message.text = showMsg;
                showMsg = "";

                message.CrossFadeAlpha(255,1,false);
                DOTween.Sequence().Append(message.DOColor(Color.red, 2))
                    .AppendInterval(1f)
                    .Append(message.DOColor(new Color(255,0,0,0), 1))
                    .Play();
                
            }
        }

        public override void OnInit()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            panels=new Stack<BasePanel>();
            PushPanel(PanelType.StartPanel);
            message = canvas.transform.Find("Message").GetComponent<Text>();
            message.color=new Color(message.color.r,message.color.g,message.color.b,0);

        }

        public override void OnDestroy()
        {
            panels.Clear();
        }
        /// <summary>
        /// 把panel压入栈
        /// </summary>
        /// <param name="panel"></param>
        public void PushPanel(BasePanel panel)
        {
            if (panels.Count > 0)
            {
                var top = panels.Peek();
                top.OnPause();
            }
            panels.Push(panel);
        }
        /// <summary>
        /// 把顶部panel移除
        /// </summary>
        public void PopPanel()
        {
            if (panels.Count>0)
            {
                var top=panels.Peek();
                top.OnExit();
                panels.Pop();
                top.gameObject.SetActive(false);
                var top2=panels.Peek();
                top2.OnResume();
            }

        }

        /// <summary>
        /// 获得顶层的panel
        /// </summary>
        /// <returns></returns>
        public BasePanel GetTopPanel()
        {
            if (panels.Count>0)
            {
                return panels.Peek();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据panel名字去激活面板
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public void PushPanel(PanelType type)
        {

            string str=Enum.GetName(type.GetType(),type);
            var go=canvas.transform.Find(str).gameObject;
            go.SetActive(true);
            PushPanel(go.GetComponent<BasePanel>());
        }

        public void ShowMessageAsync(string text)
        {
            showMsg = text;
        }

        /// <summary>
        /// 清除所有的panel
        /// </summary>
        public void ClearStack()
        {
            panels.Clear();
        }
    }
    
    
}
