using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    AsyncOperation async_main;
    AsyncOperation async_outside;
    AsyncOperation async_title;

    public TextMeshProUGUI loadingTxt;
    public TextMeshProUGUI loadingProgressTxt;

    string routeName;
    float progress;
    bool isLoad = false;
    void Start()
    {
        RouteDiv();
        DontDestroyOnLoad(this.gameObject);
    }

    void RouteDiv()
    {
        routeName = CryptoPlayerPrefs.GetString("route");
        switch (routeName)
        {
            case "main":
                async_main = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
                break;
            case "outside":
                async_outside = SceneManager.LoadSceneAsync("Outside", LoadSceneMode.Single);
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
