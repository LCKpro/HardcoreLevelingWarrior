using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class UI_SquadScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    // Enhanced Scroller
    public EnhancedScroller scroller;

    // 화면에 나타나는 셀뷰 프리팹
    public UI_SquadCellView squadCellViewPrefab;

    private List<Dictionary<string, object>> _data_Squad;

    private bool isStart = false;


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
    /// CSV 파일 리딩
    /// </summary>
    private void SetCSV()
    {
        _data_Squad = CSVReader.Read("Squad_DB");
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
        return 350f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        UI_SquadCellView cellView = null;

        cellView = scroller.GetCellView(squadCellViewPrefab) as UI_SquadCellView;

        if(dataIndex == 3 || dataIndex == 7 || dataIndex == 24 || dataIndex == 4)
        {
            cellView.SetData(_data_Squad[dataIndex], dataIndex);

            CheckFirstSquad(_data_Squad[dataIndex]);
        }

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

    private bool isFirst = false;
    private void CheckFirstSquad(Dictionary<string, object> data)
    {
        if (isFirst == false)
        {
            isFirst = true;
            var popup = Core.Instance.uiPopUpManager.Get<UI_Squad>("UI_Squad");
            var charName = Convert.ToString(data["CharacterName"]);
            var iller = Convert.ToString(data["IllerstrationPath"]);

            popup.SetSquad(new SquadInfo() { characterName = charName, illerstrationPath = iller });
        }
    }
}
