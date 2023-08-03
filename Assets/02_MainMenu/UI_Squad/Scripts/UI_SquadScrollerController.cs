using System;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class UI_SquadScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    // Enhanced Scroller
    public EnhancedScroller scroller;

    // ȭ�鿡 ��Ÿ���� ���� ������
    public UI_SquadCellView squadCellViewPrefab;

    private List<Dictionary<string, object>> _data_Squad;
    private List<int> squadIndexList = new List<int>();     // ������ �ִ� ������ �ε��� ����Ʈ

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
        if(squadIndexList.Count <= index)
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
        return 162.3f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        UI_SquadCellView cellView = null;

        cellView = scroller.GetCellView(squadCellViewPrefab) as UI_SquadCellView;

        var index = GetPlayableSquadIndex(dataIndex);       // �ε��� 0���� �� ���°� �ƴ϶� �ִ� ĳ���� ������� �� ���� ��
        cellView.SetData(_data_Squad[index], index);
        CheckFirstSquad(_data_Squad[index]);

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

    private bool isFirst = false;
    private void CheckFirstSquad(Dictionary<string, object> data)
    {
        if (isFirst == false)
        {
            isFirst = true;
            var popup = Core.Instance.uiPopUpManager.Get<UI_Squad>("UI_Squad");
            var charName = Convert.ToString(data["CharacterName"]);
            var iller = Convert.ToString(data["IllerstrationPath"]);

            Debug.Log("1 : " + charName);
            Debug.Log("2 : " + iller);

            popup.SetSquad(new SquadInfo() { index = 0, characterName = charName, illerstrationPath = iller });
        }
    }
}
