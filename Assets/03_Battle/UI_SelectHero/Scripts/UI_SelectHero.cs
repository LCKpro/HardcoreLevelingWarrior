using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectHero : MonoBehaviour, IEnhancedScrollerDelegate
{
    // Enhanced Scroller
    public EnhancedScroller scroller;

    // ȭ�鿡 ��Ÿ���� ���� ������
    public UI_SelectHeroCellView selectHeroCellViewPrefab;

    public Image[] sortButtonList;

    private List<Dictionary<string, object>> _data_Squad;
    private List<int> squadIndexList = new List<int>();     // ������ �ִ� ������ �ε��� ����Ʈ

    private List<int> squadIndexSortList = new List<int>();     // ������ �ִ� ������ �ε��� ����Ʈ�� ����

    private GameDefine.SelectHeroSortType selectHeroSortType = GameDefine.SelectHeroSortType.None;

    private bool isStart = false;

    private void Start()
    {
        CryptoPlayerPrefs.SetInt("Squad_2", 1);
        CryptoPlayerPrefs.SetInt("Squad_5", 1);
        CryptoPlayerPrefs.SetInt("Squad_9", 1);
        CryptoPlayerPrefs.SetInt("Squad_10", 1);
        CryptoPlayerPrefs.SetInt("Squad_11", 1);
        CryptoPlayerPrefs.SetInt("Squad_15", 1);
        CryptoPlayerPrefs.SetInt("Squad_19", 1);
        CryptoPlayerPrefs.SetInt("Squad_24", 1);
        CryptoPlayerPrefs.SetInt("Squad_25", 1);
        CryptoPlayerPrefs.SetInt("Squad_26", 1);
        CryptoPlayerPrefs.SetInt("Squad_29", 1);
        CryptoPlayerPrefs.SetInt("Squad_30", 1);


        CryptoPlayerPrefs.SetInt("Squad_32", 1);
        CryptoPlayerPrefs.SetInt("Squad_33", 1);
        CryptoPlayerPrefs.SetInt("Squad_34", 1);
        SoundManager.instance.PlayBGM("SelectHero");

        SetData();
    }

    public void SetData()
    {
        GamePlay.Instance.selectHeroManager.InitData();

        if (isStart == false)
        {
            scroller.Delegate = this;
            scroller.lookAheadAfter = 500;
            scroller.lookAheadBefore = 500;

            SetCSV();
            isStart = true;
        }

        SetPlayableSquadIndex();

        if(selectHeroSortType != GameDefine.SelectHeroSortType.None)
        {
            SetSortIndex();
        }
        else
        {
            SetPlayableSquadIndex();
        }

        LoadSquadData();
    }

    /// <summary>
    /// CSV ���� ����
    /// </summary>
    private void SetCSV()
    {
        _data_Squad = CSVReader.Read("Squad_DB");
    }

    /// <summary>
    /// ������ �ִ� ĳ������ �ε����� ����
    /// </summary>
    private void SetPlayableSquadIndex()
    {
        squadIndexList.Clear();
        string key = "Squad_";
        for (int i = 0; i < _data_Squad.Count; i++)
        {
            // 0�̸� ������ ���� ����. ������ ������ 
            if (CryptoPlayerPrefs.GetInt((key + i), 0) == 1)
            {
                squadIndexList.Add(i);
            }
        }
    }

    /// <summary>
    /// ������ �ִ� ĳ���� �� ���Ŀ� �ش��ϴ� ĳ���� �ε����� ���� ����
    /// </summary>
    private void SetSortIndex()
    {
        squadIndexSortList.Clear();
        for (int i = 0; i < squadIndexList.Count; i++)
        {
            var info = Core.Instance.characterInfoManager.GetCharacterInfoDataByIndex(squadIndexList[i]);
            
            if(info.selectHeroSortType == selectHeroSortType)
            {
                squadIndexSortList.Add(squadIndexList[i]);
            }
        }
    }

    /// <summary>
    /// ������ �ִ� ĳ������ �ε��� ���� ��ȯ
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetPlayableSquadIndex(int index)
    {
        if (squadIndexList.Count <= index)
        {
            Debug.LogError("������ �ε����� ī��Ʈ���� �ε����� ����");
            return 9999999;
        }

        return squadIndexList[index];
    }

    public int GetSortSquadIndex(int index)
    {
        if (squadIndexSortList.Count <= index)
        {
            Debug.LogError("������ �ε����� ī��Ʈ���� �ε����� ����");
            return 9999999;
        }

        return squadIndexSortList[index];
    }

    public void LoadSquadData()
    {
        // ������ �ִ� ������ ���ġ
        scroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return CellCount();
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 150f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        UI_SelectHeroCellView cellView = null;

        cellView = scroller.GetCellView(selectHeroCellViewPrefab) as UI_SelectHeroCellView;

        int index = 0;

        if(selectHeroSortType == GameDefine.SelectHeroSortType.None)
        {
            index = GetPlayableSquadIndex(dataIndex);       // �ε��� 0���� �� ���°� �ƴ϶� �ִ� ĳ���� ������� �� ���� ��
        }
        else
        {
            index = GetSortSquadIndex(dataIndex);       // ���� ���Ĺ�ư�� �������� ���Ŀ� �´� �ε����� �����;� ��
        }

        cellView.SetData(_data_Squad[index], index);

        return cellView;
    }

    public int CellCount()
    {
        if (squadIndexList != null)
        {
            if(selectHeroSortType == GameDefine.SelectHeroSortType.None)
            {
                return squadIndexList.Count;
            }
            else
            {
                return squadIndexSortList.Count;
            }
        }

        return 0;
    }

    #region Button

    private bool[] isClick = new bool[5];
    public void OnClick_SortType(int index)
    {
        SoundManager.instance.PlayUIButtonClickSound();

        for (int i = 0; i < isClick.Length; i++)
        {
            if(index == i)
            {
                continue;
            }

            isClick[i] = false;
            sortButtonList[i].color = new Color(1, 1, 1, 1);
        }

        if(isClick[index] == false)
        {
            SetSortType(index);
            sortButtonList[index].color = new Color(1, 0, 0, 1);
            isClick[index] = true;
        }
        else
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.None;
            sortButtonList[index].color = new Color(1, 1, 1, 1);
            isClick[index] = false;
        }

        SetData();
    }

    private void SetSortType(int index)
    {
        if(index == 0)
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.Tanker;
        }
        else if(index == 1)
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.Dealer_Melee;
        }
        else if (index == 2)
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.Dealer_Range;
        }
        else if (index == 3)
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.Supporter;
        }
        else if (index == 4)
        {
            selectHeroSortType = GameDefine.SelectHeroSortType.Healer;
        }
        else
        {
            Debug.Log("���� �ε���.. Ȯ�� �ʿ�");
            selectHeroSortType = GameDefine.SelectHeroSortType.None;
        }
    }

    #endregion
}
