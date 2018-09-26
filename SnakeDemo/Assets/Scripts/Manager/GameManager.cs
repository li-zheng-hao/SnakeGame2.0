/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/21 21:27:35
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
    public class GameManager:MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!instance)
                {
                    GameObject go=new GameObject("GameManager");
                    instance=go.AddComponent<GameManager>();
                }

                return instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public NetManager netManager;

        public UIManager uiManager;

        public CharacterManager chaManager;

        public SceneManager scManager;

        public PlayerManager plManager;

//        public MTFunctionManager mtfManager;
        private void Start()
        {
            netManager=new NetManager();
            uiManager=new UIManager();
            chaManager=new CharacterManager();
            scManager=new SceneManager();
            plManager = new PlayerManager();
//            mtfManager=new MTFunctionManager();

            netManager.OnInit();
            uiManager.OnInit();
            chaManager.OnInit();
            scManager.OnInit();
            plManager?.OnInit();
        }

        private void Update()
        {
            netManager.Update();
            uiManager.Update();
            chaManager.Update();
            scManager.Update();
            plManager?.Update();
        }

        private void OnDestroy()
        {
            netManager.OnDestroy();
            uiManager.OnDestroy();
            chaManager.OnDestroy();
            scManager.OnDestroy();
            plManager?.OnDestroy();
        }
    }
}
