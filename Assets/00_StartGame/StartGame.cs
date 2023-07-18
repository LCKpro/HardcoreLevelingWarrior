using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject messageBox;

    private void Awake()
    {
        int info_Day = 1;
        int info_Gold = 0;
        int info_LongHammer = 0;
        int info_Food = 0;
        int info_AmmoBundle = 0;
        int info_MedicalFill = 0;
        int info_BlueFill = 0;
        int info_Wood = 0;
        int info_Rifle = 0;
        // 내부 데이터 생성
        CryptoPlayerPrefs_HasKeyIntFind("info_Day", info_Day);
        CryptoPlayerPrefs_HasKeyIntFind("info_Gold", info_Gold);
        CryptoPlayerPrefs_HasKeyIntFind("info_LongHammer", info_LongHammer);
        CryptoPlayerPrefs_HasKeyIntFind("info_Food", info_Food);
        CryptoPlayerPrefs_HasKeyIntFind("info_AmmoBundle", info_AmmoBundle);
        CryptoPlayerPrefs_HasKeyIntFind("info_MedicalFill", info_MedicalFill);
        CryptoPlayerPrefs_HasKeyIntFind("info_BlueFill", info_BlueFill);
        CryptoPlayerPrefs_HasKeyIntFind("info_Wood", info_Wood);
        CryptoPlayerPrefs_HasKeyIntFind("info_Rifle", info_Rifle);

        CryptoPlayerPrefs_HasKeyStringFind("route", "main");
    }

    private void Start()
    {
        //messageBox.SetActive(false);
    }

    public void YesBtn()
    {
        Application.Quit();
    }
    public void NoBtn()
    {
        messageBox.SetActive(false);
    }

    public void OnClick_TapToStart()
    {
        CryptoPlayerPrefs.SetString("route", "main");
        SceneManager.LoadScene("01_Loading");
    }

    /*public void NewStartOnClick()
    {
        CryptoPlayerPrefs.DeleteAll();
        route = "main";
        CryptoPlayerPrefs.SetString("route", "main");
        SceneManager.LoadScene("01_Loading");
    }*/

    public void QuitOnClick()
    {
        messageBox.SetActive(true);
    }

    public void CryptoPlayerPrefs_HasKeyIntFind(string key, int value)
    {
        if (!CryptoPlayerPrefs.HasKey(key))
        {
            CryptoPlayerPrefs.SetInt(key, value);
        }
        else
        {
            CryptoPlayerPrefs.GetInt(key, 0);
        }
    }

    public void CryptoPlayerPrefs_HasKeyFloatFind(string key, float value)
    {
        if (!CryptoPlayerPrefs.HasKey(key))
        {
            CryptoPlayerPrefs.SetFloat(key, value);
        }
        else
        {
            CryptoPlayerPrefs.GetFloat(key, 0);
        }
    }


    public void CryptoPlayerPrefs_HasKeyStringFind(string key, string value)
    {
        if (!CryptoPlayerPrefs.HasKey(key))
        {
            CryptoPlayerPrefs.SetString(key, value);
        }
        else
        {
            CryptoPlayerPrefs.GetString(key, "0");
        }
    }
}
