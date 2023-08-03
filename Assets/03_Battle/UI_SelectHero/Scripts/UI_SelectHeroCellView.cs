using System;
using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectHeroCellView : EnhancedScrollerCellView
{
    public Image flag_Squad;

    private int _index;

    public void SetData(Dictionary<string, object> data, int index)
    {
        _index = index;

        flag_Squad.sprite = Resources.Load<Sprite>(Convert.ToString(data["FlagPath"]));
    }

    public void OnClick_SelectHero()
    {
        SoundManager.instance.PlaySound("Choose");
        GamePlay.Instance.selectHeroManager.AddSquadByRange(_index);
    }
}
