using System;
using System.Collections.Generic;
using UnityEngine;
using ZeroFormatter;

public partial class UserSecureDataManager : MonoBehaviour
{
    // �κ��丮 ������ ������ �ε� (������ null ��ȯ)
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

    // �κ��丮 ������ ������ ����
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
    public float atk;   // ���ݷ�
    public float vit;   // ü��
    public GameDefine.ItemType itemType;
    public GameDefine.ItemOwner itemOwner;
    public GameDefine.ItemAbility itemAbility;
}
