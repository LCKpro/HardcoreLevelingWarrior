using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LoadingManager : MonoBehaviour
{
    private AsyncOperation _async_Main;      // 메인 씬으로 넘어갈 때
    private AsyncOperation _async_Battle;   // 전투 씬으로 넘어갈 때
    private AsyncOperation _async_Title;    // 게임 시작 씬으로 넘어갈 때

    private string routeName;   // Crypto에 저장된 로딩 루트
    private float progress;     // 진행도
    private bool isLoad = false;    // 로딩 끝났는지?

    public TextMeshProUGUI loadingTxt;
    public TextMeshProUGUI loadingProgressTxt;

    public Sprite[] loadingSprite;                  // 랜덤으로 나올 스프라이트 4종
    public string[] loadingCharacterNameList;       // 스프라이트와 매치되는 캐릭터 이름
    public Image loadingImage;
    public TextMeshProUGUI loadingCharacterName;

    private void Start()
    {
        SetImage();     // 이미지 4종 랜덤으로 선택 후 적용
        RouteDiv();     // 어느 씬으로 이동할지?
        DontDestroyOnLoad(this.gameObject);     // 로딩 매니저는 사라지면 안됨
    }

    private void SetImage()
    {
        System.Random _sysRand = new System.Random();

        int index = _sysRand.Next(0, loadingSprite.Length);
        loadingImage.sprite = loadingSprite.ElementAt(index);
        loadingCharacterName.text = loadingCharacterNameList.ElementAt(index);
    }

    private void RouteDiv()
    {
        routeName = CryptoPlayerPrefs.GetString("route");
        switch (routeName)
        {
            case "main":
                _async_Main = SceneManager.LoadSceneAsync("02_MainMenu", LoadSceneMode.Single);
                break;
            case "battle":
                _async_Battle = SceneManager.LoadSceneAsync("03_Battle", LoadSceneMode.Single);
                break;
            case "title":
                _async_Title = SceneManager.LoadSceneAsync("Title", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        switch (routeName)
        {
            case "main":
                {
                    if (!_async_Main.isDone)
                    {
                        progress = _async_Main.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (_async_Main.isDone)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            case "outside":
                {
                    if (!_async_Battle.isDone)
                    {
                        progress = _async_Battle.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (_async_Battle.isDone)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            case "title":
                {
                    if (!_async_Title.isDone)
                    {
                        progress = _async_Title.progress * 100;
                        loadingProgressTxt.text = progress.ToString();
                    }
                    if (_async_Title.isDone)
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

    private IEnumerator Loading()
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
