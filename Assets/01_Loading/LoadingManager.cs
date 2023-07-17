using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LoadingManager : MonoBehaviour
{
    AsyncOperation async_main;
    AsyncOperation async_outside;
    AsyncOperation async_title;

    public TextMeshProUGUI loadingTxt;
    public TextMeshProUGUI loadingProgressTxt;

    public Sprite[] loadingSprite;
    public string[] loadingCharacterNameList;
    public Image loadingImage;
    public TextMeshProUGUI loadingCharacterName;

    string routeName;
    float progress;
    bool isLoad = false;
    void Start()
    {
        SetImage();
        RouteDiv();
        DontDestroyOnLoad(this.gameObject);
    }

    private void SetImage()
    {
        System.Random _sysRand = new System.Random();

        int index = _sysRand.Next(0, loadingSprite.Length);
        loadingImage.sprite = loadingSprite.ElementAt(index);
        loadingCharacterName.text = loadingCharacterNameList.ElementAt(index);
    }

    
    public static T RandomItem<T>(IEnumerable<T> list)
    {
        System.Random _sysRand = new System.Random();

        if (!list.Any())
            return default(T);
        int index = _sysRand.Next(0, list.Count());
        return list.ElementAt(index);
    }

    void RouteDiv()
    {
        routeName = CryptoPlayerPrefs.GetString("route");
        switch (routeName)
        {
            case "main":
                async_main = SceneManager.LoadSceneAsync("02_MainMenu", LoadSceneMode.Single);
                break;
            case "outside":
                async_outside = SceneManager.LoadSceneAsync("03_Battle", LoadSceneMode.Single);
                break;
            case "title":
                async_title = SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
    void Update()
    {
        switch (routeName)
        {
            case "main":
                {
                    if (!async_main.isDone)
                    {
                        progress = async_main.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (async_main.isDone)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            case "outside":
                {
                    if (!async_outside.isDone)
                    {
                        progress = async_outside.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (async_outside.isDone)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            case "title":
                {
                    if (!async_title.isDone)
                    {
                        progress = async_title.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (async_title.isDone)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            default:
                break;
        }
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        if (!isLoad)
        {
            isLoad = true;
            loadingTxt.text = "Loading.";
            yield return new WaitForSeconds(1.0f);
            loadingTxt.text = "Loading..";
            yield return new WaitForSeconds(1.0f);
            loadingTxt.text = "Loading...";
            yield return new WaitForSeconds(1.0f);
            isLoad = false;
        }
    }
}
