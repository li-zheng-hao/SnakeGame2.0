using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;
using GameManager = Game.GameManager;

public class StartPanel : BasePanel
{
    private Button login;
    private Button regist;
    private Button cancel;

    private void Awake()
    {
        login = GameObject.Find("loginButton").GetComponent<Button>();
        regist = GameObject.Find("registButton").GetComponent<Button>();
        cancel= GameObject.Find("cancelButton").GetComponent<Button>();
        login.onClick.AddListener(LoginClick);
        regist.onClick.AddListener(RegistClick);
        cancel.onClick.AddListener(CancelClick);

    }

    private void CancelClick()
    {
        

    }

    private void RegistClick()
    {
        GameManager.Instance.uiManager.PushPanel(PanelType.RegistPanel);
    }

    private void LoginClick()
    {
        GameManager.Instance.uiManager.PushPanel(PanelType.LoginPanel);
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
