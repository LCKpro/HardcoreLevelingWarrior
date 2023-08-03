using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject messageBox;

    private void Awake()
    {
        CryptoPlayerPrefs.SetInt("Squad_32", 1);
        CryptoPlayerPrefs.SetInt("Squad_33", 1);
        CryptoPlayerPrefs.SetInt("Squad_34", 1);

        /*int info_Day = 1;
        int info_Gold = 0;
        int info_LongHammer = 0;
        int info_Food = 0;
        int info_AmmoBundle = 0;
        int info_MedicalFill = 0;
        int info_BlueFill = 0;
        int info_Wood = 0;
        int info_Rifle = 0;
        // 郴何 单捞磐 积己
        CryptoPlayerPrefs_HasKeyIntFind("info_Day", info_Day);
        CryptoPlayerPrefs_HasKeyIntFind("info_Gold", info_Gold);
        CryptoPlayerPrefs_HasKeyIntFind("info_LongHammer", info_LongHammer);
        CryptoPlayerPrefs_HasKeyIntFind("info_Food", info_Food);
        CryptoPlayerPrefs_HasKeyIntFind("info_AmmoBundle", info_AmmoBundle);
        CryptoPlayerPrefs_HasKeyIntFind("info_MedicalFill", info_MedicalFill);
        CryptoPlayerPrefs_HasKeyIntFind("info_BlueFill", info_BlueFill);
        CryptoPlayerPrefs_HasKeyIntFind("info_Wood", info_Wood);
        CryptoPlayerPrefs_HasKeyIntFind("info_Rifle", info_Rifle);*/

        //CryptoPlayerPrefs_HasKeyStringFind("route", "main");
    }

    private void Start()
    {
        SoundManager.instance.PlayBGM("Title");
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

    
}
