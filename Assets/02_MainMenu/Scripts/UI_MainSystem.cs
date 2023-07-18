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
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }

    /// <summary>
    /// 스쿼드 관리 버튼 클릭
    /// </summary>
    public void OnClick_ManageSquad()
    {

    }

    /// <summary>
    /// 도감 버튼 클릭
    /// </summary>
    public void OnClick_Book()
    {

    }

    /// <summary>
    /// 업적 버튼 클릭
    /// </summary>
    public void OnClick_Achievement()
    {

    }
}
