using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainSystem : MonoBehaviour
{
    /// <summary>
    /// ��Ʋ ��ư Ŭ��
    /// </summary>
    public void OnClick_Battle()
    {
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }

    /// <summary>
    /// ������ ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_ManageSquad()
    {
        var popup = Core.Instance.uiPopUpManager.ShowAndGet<UI_Squad>("UI_Squad");
        popup.SetData();
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_Book()
    {

    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_Achievement()
    {

    }
}
