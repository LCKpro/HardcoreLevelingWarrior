using System;
using UnityEngine;

public partial class Core : MonoBehaviour
{
    #region SingleTon

    private static Core _instance = null;

    public static Core Instance
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
            GameObject go = GameObject.Find("Core");
            if (go != null)
            {
                _instance = go.GetComponent<Core>();
                if (_instance == null)
                {
                    Debug.Log("Core Instance Null");
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

    public UIPopUpManager uiPopUpManager;
    public CurrencyManager currencyManager;
    public CryptoManager cryptoManager;
    public UserSecureDataManager userSecureDataManager;
    public SkillManager skillManager;
    public ItemManager itemManager;
    public CharacterManager characterManager;
    public CharacterInfoManager characterInfoManager;

    /// <summary>
    /// Init Logic
    /// </summary>
    public void Init()
    {
        SetInstance();
    }
}