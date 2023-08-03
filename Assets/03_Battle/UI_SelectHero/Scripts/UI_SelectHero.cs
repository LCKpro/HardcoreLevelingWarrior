using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class UI_SelectHero : MonoBehaviour, IEnhancedScrollerDelegate
{
    // Enhanced Scroller
    public EnhancedScroller scroller;

    // ȭ�鿡 ��Ÿ���� ���� ������
    public UI_SelectHeroCellView selectHeroCellViewPrefab;

    private List<Dictionary<string, object>> _data_Squad;
    private List<int> squadIndexList = new List<int>();     // ������ �ִ� ������ �ε��� ����Ʈ

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

        var index = GetPlayableSquadIndex(dataIndex);       // �ε��� 0���� �� ���°� �ƴ϶� �ִ� ĳ���� ������� �� ���� ��
        cellView.SetData(_data_Squad[index], index);

        return cellView;
    }

    public int CellCount()
    {
        if (squadIndexList != null)
        {
            return squadIndexList.Count;
        }

        return 0;
    }
}
