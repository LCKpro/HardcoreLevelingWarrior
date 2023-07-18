using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class LoadingManager : MonoBehaviour
{
    private AsyncOperation _async_Main;      // ���� ������ �Ѿ ��
    private AsyncOperation _async_Battle;   // ���� ������ �Ѿ ��
    private AsyncOperation _async_Title;    // ���� ���� ������ �Ѿ ��

    private string routeName;   // Crypto�� ����� �ε� ��Ʈ
    private float progress;     // ���൵
    private bool isLoad = false;    // �ε� ��������?

    public TextMeshProUGUI loadingTxt;
    public TextMeshProUGUI loadingProgressTxt;

    public Sprite[] loadingSprite;                  // �������� ���� ��������Ʈ 4��
    public string[] loadingCharacterNameList;       // ��������Ʈ�� ��ġ�Ǵ� ĳ���� �̸�
    public Image loadingImage;
    public TextMeshProUGUI loadingCharacterName;

    private void Start()
    {
        SetImage();     // �̹��� 4�� �������� ���� �� ����
        RouteDiv();     // ��� ������ �̵�����?
        DontDestroyOnLoad(this.gameObject);     // �ε� �Ŵ����� ������� �ȵ�
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
