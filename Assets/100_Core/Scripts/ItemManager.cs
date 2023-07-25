using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> itemDataList;

    [SerializeField]
    private List<ItemData> weaponItemDataList;

    [SerializeField]
    private List<ItemData> armorItemDataList;

    public List<ItemData> GetWeaponItemData()
    {
        if (weaponItemDataList != null)
        {
            return weaponItemDataList;
        }

        GameUtils.Log("Weapon ItemData Null");

        return null;
    }

    public ItemData GetWeaponItemDataByUUID(int uuid)
    {
        foreach (var item in weaponItemDataList)
        {
            if (item.uuid == uuid)
            {
                return item;
            }
        }

        return null;
    }

    public List<ItemData> GetArmorItemData()
    {
        if (armorItemDataList != null)
        {
            return armorItemDataList;
        }

        GameUtils.Log("Armor ItemData Null");

        return null;
    }

    public ItemData GetArmorItemDataByUUID(int uuid)
    {
        foreach (var item in armorItemDataList)
        {
            if (item.uuid == uuid)
            {
                return item;
            }
        }

        return null;
    }
}

[System.Serializable]
public class ItemData
{
    public int uuid;
    public string itemName;
    public string description;
    public Sprite sprite;
    public int price;
}
