using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    #region SingleTon

    private static GamePlay _instance = null;

    public static GamePlay Instance
    {
        get
        {
            SetInstance();
            return _instance;
        }
    }

    public static void SetInstance()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("GamePlay");
            if (go != null)
            {
                _instance = go.GetComponent<GamePlay>();
                if (_instance == null)
                {
                    Debug.Log("GamePlay Instance Null");
                }
            }
        }
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void OnDisable()
    {
        _instance = null;
    }

    #endregion

    public SelectHeroManager selectHeroManager;
    public EnemySquadManager enemySquadManager;
    public BattleManager battleManager;
    public BattleUIManager battleUIManager;

    public void Cheat_ResetScene()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        CryptoPlayerPrefs.SetString("route", "battle");
        SceneManager.LoadScene("01_Loading");
    }
}
