using System;
using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SquadCellView : EnhancedScrollerCellView
{
    public Image icon_Squad;

    private string _characterName;
    private string _illerstrationPath;
    private string _iconPath;

    private int _index;

    private Dictionary<string, string> ingredientDic = new Dictionary<string, string>();

    public void SetData(Dictionary<string, object> data, int index)
    {
        ingredientDic.Clear();

        _index = index;
        _characterName = Convert.ToString(data["CharacterName"]);
        _illerstrationPath = Convert.ToString(data["IllerstrationPath"]);
        _iconPath = Convert.ToString(data["IconPath"]);

        //icon_Squad.sprite = Resources.Load<Sprite>(Convert.ToString(data["SquadPath"]));
        icon_Squad.sprite = Resources.Load<Sprite>(Convert.ToString(data["SquadPath"]) + "_0");
    }

    public void OnClick_Squad()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        var popup = Core.Instance.uiPopUpManager.Get<UI_Squad>("UI_Squad");
        popup.SetSquad(new SquadInfo() { index = _index, characterName = _characterName, illerstrationPath = _illerstrationPath });
    }
}

public class SquadInfo
{
    public int index;
    public string characterName;
    public string illerstrationPath;
}

