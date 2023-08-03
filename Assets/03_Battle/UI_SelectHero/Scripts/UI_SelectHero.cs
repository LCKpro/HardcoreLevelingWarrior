using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class UI_SelectHero : MonoBehaviour, IEnhancedScrollerDelegate
{
    // Enhanced Scroller
    public EnhancedScroller scroller;

    // 화면에 나타나는 셀뷰 프리팹
    public UI_SelectHeroCellView selectHeroCellViewPrefab;

    private List<Dictionary<string, object>> _data_Squad;
    private List<int> squadIndexList = new List<int>();     // 가지고 있는 영웅의 인덱스 리스트

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
    /// CSV 파일 리딩
    /// </summary>
    private void SetCSV()
    {
        _data_Squad = CSVReader.Read("Squad_DB");
    }

    /// <summary>
    /// 가지고 있는 캐릭터의 인덱스를 세팅
    /// </summary>
    private void SetPlayableSquadIndex()
    {
        squadIndexList.Clear();
        string key = "Squad_";
        for (int i = 0; i < _data_Squad.Count; i++)
        {
            // 0이면 가지고 있지 않음. 가지고 있으면 
            if (CryptoPlayerPrefs.GetInt((key + i), 0) == 1)
            {
                squadIndexList.Add(i);
            }
        }
    }

    /// <summary>
    /// 가지고 있는 캐릭터의 인덱스 순서 반환
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetPlayableSquadIndex(int index)
    {
        if (squadIndexList.Count <= index)
        {
            Debug.LogError("스쿼드 인덱스의 카운트보다 인덱스가 높음");
            return 9999999;
        }

        return squadIndexList[index];
    }

    public void LoadSquadData()
    {
        // 가지고 있는 데이터 재배치
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

        var index = GetPlayableSquadIndex(dataIndex);       // 인덱스 0부터 쭉 가는게 아니라 있는 캐릭터 순서대로 쭉 가야 함
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
