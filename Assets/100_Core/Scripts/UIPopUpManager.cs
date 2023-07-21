using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUpManager : MonoBehaviour
{
    public Dictionary<string, GameObject> popUpBufferList = new Dictionary<string, GameObject>();
    public Transform safeArea;
    private PopUpPools poolManager;

    public const string uiWorldPrefabName = "UI_PopUp_World";

    public void Awake()
    {
        poolManager = GetComponent<PopUpPools>();
        popUpBufferList.Clear();
    }

    /// <summary>
    /// 아무 팝업이나 떠있는지?
    /// </summary>
    public bool IsViewPopUps()
    {
        bool isView = false;
        for (int i = 0; i < Core.Instance.uiPopUpManager.safeArea.childCount; i++)
        {
            if (Core.Instance.uiPopUpManager.safeArea.GetChild(i).gameObject.activeSelf == true)
            {
                isView = true;
                break;
            }
        }

        return isView;
    }

    // 팝업 클래스 가져오기
    public T Get<T>(string key)
    {
        if (popUpBufferList.ContainsKey(key) == true)
        {
            return popUpBufferList[key].GetComponent<T>();
        }

        return default(T);
    }

    // 팝업 열기
    public GameObject Show(string popUpName, bool isView = true)
    {
        GameUtils.Log("UIPopUpManager / Show () " + popUpName);

        try
        {
            // 강제로 퍼즈 띄우기
            //SetPause(popUpName);

            bool contains = popUpBufferList.ContainsKey(popUpName);

            if (contains == false)
            {
                popUpBufferList.Add(popUpName, poolManager.Spawn(popUpName, isView).gameObject);
            }

            bool contains2 = popUpBufferList.ContainsKey(popUpName);
            popUpBufferList[popUpName].transform.SetParent(safeArea);
            popUpBufferList[popUpName].transform.localPosition = new Vector3(0, 0, 0);
            popUpBufferList[popUpName].transform.localScale = new Vector3(1, 1, 1);
            popUpBufferList[popUpName].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            popUpBufferList[popUpName].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            popUpBufferList[popUpName].GetComponent<RectTransform>().SetSiblingIndex(safeArea.childCount - 1);
            popUpBufferList[popUpName].GetComponent<UIPopUpVisible>().Show();
        }
        catch (Exception e)
        {
            GameUtils.LogException(e);
        }

        if (popUpBufferList.ContainsKey(popUpName) == true)
        {
            return popUpBufferList[popUpName];
        }
        else
        {
            return null;
        }
    }

    public T ShowAndGet<T>(string popUpName, bool isView = true)
    {
        Show(popUpName, isView);
        if (popUpBufferList.ContainsKey(popUpName) == true)
        {
            return popUpBufferList[popUpName].GetComponent<T>();
        }

        return default(T);
    }

    // 순차적으로 닫기
    public void Hide()
    {
        // 비활성화 된 팝업 UI뎁스 예외처리
        for (int i = safeArea.childCount - 1; i >= 0; i--)
        {
            if (safeArea.GetChild(i).gameObject.activeSelf == false)
            {
                safeArea.GetChild(i).GetComponent<RectTransform>().SetSiblingIndex(0);
            }
        }

        if (popUpBufferList.Count > 0)
        {
            var pGO = safeArea.GetChild(safeArea.childCount - 1).gameObject;
            var items = popUpBufferList.GetEnumerator();
            while (items.MoveNext())
            {
                if (items.Current.Value.Equals(pGO) == true)
                {
                    if (poolManager.IsSpawned(popUpBufferList[items.Current.Key].transform) == true)
                    {
                        poolManager.Despawn(popUpBufferList[items.Current.Key].transform);
                    }

                    popUpBufferList[items.Current.Key].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    popUpBufferList[items.Current.Key].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    popUpBufferList[items.Current.Key].GetComponent<RectTransform>().SetSiblingIndex(0);
                    var p = popUpBufferList[items.Current.Key].GetComponent<UIPopUpVisible>();
                    popUpBufferList.Remove(items.Current.Key);
                    p.Hide();
                    break;
                }
            }
        }
    }

    // 특정 팝업 닫기
    public void Hide(string popUpName)
    {
        if (popUpBufferList.Count > 0)
        {
            if (popUpBufferList.ContainsKey(popUpName) == true)
            {
                var p = popUpBufferList[popUpName].GetComponent<UIPopUpVisible>();
                if (poolManager.IsSpawned(p.transform) == true)
                {
                    poolManager.Despawn(p.transform);
                }

                popUpBufferList.Remove(popUpName);
                p.Hide();
            }
        }
    }

    // 특정 팝업 빼고 모두 닫기 
    public void HideOthers(string name)
    {
        List<string> removeKeys = new List<string>();
        var i = popUpBufferList.GetEnumerator();
        while (i.MoveNext())
        {
            if (i.Current.Key.Equals(name) == false)
            {
                if (poolManager.IsSpawned(popUpBufferList[i.Current.Key].transform) == true)
                {
                    poolManager.Despawn(popUpBufferList[i.Current.Key].transform);
                    removeKeys.Add(i.Current.Key);
                }
            }
        }

        foreach (var key in removeKeys)
        {
            popUpBufferList.Remove(key);
        }
    }

    public void HideOthers(string[] name)
    {
        List<string> removeKeys = new List<string>();
        var i = popUpBufferList.GetEnumerator();
        while (i.MoveNext())
        {
            bool checker = true;
            foreach (var item in name)
            {
                if (i.Current.Key.Equals(item)) checker = false;
            }

            if (checker)
            {
                if (poolManager.IsSpawned(popUpBufferList[i.Current.Key].transform) == true)
                {
                    poolManager.Despawn(popUpBufferList[i.Current.Key].transform);
                    removeKeys.Add(i.Current.Key);
                }
            }
        }

        foreach (var key in removeKeys)
        {
            popUpBufferList.Remove(key);
        }
    }

    //모두 닫기 
    public void HideAll()
    {
        List<string> removeKeys = new List<string>();
        var i = popUpBufferList.GetEnumerator();
        while (i.MoveNext())
        {
            if (poolManager.IsSpawned(popUpBufferList[i.Current.Key].transform) == true)
            {
                poolManager.Despawn(popUpBufferList[i.Current.Key].transform);
                removeKeys.Add(i.Current.Key);
            }
        }

        foreach (var key in removeKeys)
        {
            popUpBufferList.Remove(key);
        }
    }

    public void Dispose(string name)
    {
        if (popUpBufferList.ContainsKey(name))
        {
            popUpBufferList.Remove(name);
        }

        poolManager.Dispose(name);
    }

    //특정 팝업 열려있는지 확인
    public bool CheckIsView(string name)
    {
        var i = popUpBufferList.GetEnumerator();
        while (i.MoveNext())
        {
            //if (AppSwitch.IsLog) GameUtils.Log("UIPopUpManager PopUpName : " + i.Current.Key);
            if (i.Current.Key.Equals(name) == true)
            {
                if (poolManager.IsSpawned(popUpBufferList[i.Current.Key].transform) == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //특정 팝업만 열려있는지 확인
    public bool CheckHideOthers(string name)
    {
        var i = popUpBufferList.GetEnumerator();
        while (i.MoveNext())
        {
            if (i.Current.Key.Equals(name) == false)
            {
                if (poolManager.IsSpawned(popUpBufferList[i.Current.Key].transform) == true)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // 비활성화된 상태의 모든 팝업 강제 하이드 
    public void HideAllDisabledPopups()
    {
        HashSet<string> targets = new HashSet<string>();
        foreach (var pair in popUpBufferList)
        {
            if (pair.Value.activeSelf == false)
                targets.Add(pair.Key);
        }

        foreach (var target in targets)
            Hide(target);
    }
}