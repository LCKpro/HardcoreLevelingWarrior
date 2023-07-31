using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainSystem : MonoBehaviour
{
    public GameObject selectBattle;

    public void InitMainSystem()
    {
        selectBattle.SetActive(false);
    }

    /// <summary>
    /// 배틀 버튼 클릭
    /// </summary>
    public void OnClick_Battle()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        selectBattle.SetActive(true);
    }

    /// <summary>
    /// 전투 버튼 클릭 후 취소시
    /// </summary>
    public void OnClick_CancelSelect()
    {
        selectBattle.SetActive(false);
    }

    public void OnClick_SelectStory()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }

    public void OnClick_SelectBattle()
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
        var popup = Core.Instance.uiPopUpManager.ShowAndGet<UI_SelectCharacterBook>("UI_SelectCharacterBook");
        popup.SetData();
    }

    /// <summary>
    /// 업적 버튼 클릭
    /// </summary>
    public void OnClick_Achievement()
    {
        SoundManager.instance.PlayUIButtonClickSound();
    }
}
