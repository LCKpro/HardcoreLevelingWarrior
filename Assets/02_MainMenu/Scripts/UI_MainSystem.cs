using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainSystem : MonoBehaviour
{
    /// <summary>
    /// 배틀 버튼 클릭
    /// </summary>
    public void OnClick_Battle()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }

    /// <summary>
    /// 스쿼드 관리 버튼 클릭
    /// </summary>
    public void OnClick_ManageSquad()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        var popup = Core.Instance.uiPopUpManager.ShowAndGet<UI_Squad>("UI_Squad");
        popup.SetData();
    }

    /// <summary>
    /// 도감 버튼 클릭
    /// </summary>
    public void OnClick_Book()
    {
        SoundManager.instance.PlayUIButtonClickSound();
    }

    /// <summary>
    /// 업적 버튼 클릭
    /// </summary>
    public void OnClick_Achievement()
    {
        SoundManager.instance.PlayUIButtonClickSound();
    }
}
