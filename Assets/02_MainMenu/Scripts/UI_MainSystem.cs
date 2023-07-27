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
        SoundManager.instance.PlayUIButtonClickSound();
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }

    /// <summary>
    /// ������ ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_ManageSquad()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        var popup = Core.Instance.uiPopUpManager.ShowAndGet<UI_Squad>("UI_Squad");
        popup.SetData();
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_Book()
    {
        SoundManager.instance.PlayUIButtonClickSound();
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_Achievement()
    {
        SoundManager.instance.PlayUIButtonClickSound();
    }
}
