using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public UI_Currency ui_Currency;
    public UI_MainSystem ui_MainSystem;

    public void Init()
    {
        
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_Setting()
    {
        Core.Instance.uiPopUpManager.Show("UI_Setting");
    }

    public void OnClick_Event()
    {

    }

}
