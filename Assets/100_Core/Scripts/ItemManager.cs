using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
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

    public ItemData GetWeaponItemDataByIndex(int index)
    {
        if (weaponItemDataList == null)
        {
            GameUtils.Log("Weapon ItemData Null");
            return null;
        }

        if(weaponItemDataList.Count <= index)
        {
            GameUtils.Log("Weapon ItemData Count Error");
            return null;
        }

        return weaponItemDataList[index];
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

    public ItemData GetArmorItemDataByIndex(int index)
    {
        if (armorItemDataList == null)
        {
            GameUtils.Log("Armor ItemData Null");
            return null;
        }

        if (armorItemDataList.Count <= index)
        {
            GameUtils.Log("Armor ItemData Count Error");
            return null;
        }

        return armorItemDataList[index];
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
    public int atkValue = 150;
    public int vitValue = 150;
}
