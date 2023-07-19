using System;
using System.Collections.Generic;
using UnityEngine;
using ZeroFormatter;

public partial class UserSecureDataManager : MonoBehaviour
{
    // 인벤토리 아이템 데이터 로드 (없으면 null 반환)
    public List<InventoryItem> GetInventoryItemData()
    {
        List<InventoryItem> data = null;
        string key = $"InventoryItem";
        var str = GetData(key);
        if (str != null && str.Equals("Empty") == false)
        {
            byte[] bytes = Convert.FromBase64String(str);
            data = ZeroFormatterSerializer.Deserialize<List<InventoryItem>>(bytes);
        }

        return data;
    }

    // 인벤토리 아이템 데이터 저장
    public void SetInventoryItemData(List<InventoryItem> dataset)
    {
        string key = $"InventoryItem";

        byte[] bytes = null;
        List<InventoryItem> item = dataset;
        bytes = ZeroFormatterSerializer.Serialize(item);
        string str = Convert.ToBase64String(bytes);
        SetData(key, str);
    }
}

[Serializable]
public class InventoryItem
{
    public string itemName;
    public string iconPath;
    public bool isEquip;
    public float atk;   // 공격력
    public float vit;   // 체력
    public GameDefine.ItemType itemType;
    public GameDefine.ItemOwner itemOwner;
    public GameDefine.ItemAbility itemAbility;
}
