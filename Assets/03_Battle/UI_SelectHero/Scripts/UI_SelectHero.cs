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

    private bool isStart = false;

    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        if (isStart == false)
        {
            scroller.Delegate = this;
            scroller.lookAheadAfter = 500;
            scroller.lookAheadBefore = 500;

            SetCSV();
            isStart = true;
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
        return 162.3f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        UI_SelectHeroCellView cellView = null;

        cellView = scroller.GetCellView(selectHeroCellViewPrefab) as UI_SelectHeroCellView;

        cellView.SetData(_data_Squad[dataIndex], dataIndex);

        return cellView;
    }

    public int CellCount()
    {
        if (_data_Squad != null)
        {
            return _data_Squad.Count;
        }

        return 0;
    }
}
