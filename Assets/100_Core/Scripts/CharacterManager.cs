using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterData> characterItemDataList;

    public List<CharacterData> GetCharacterItemData()
    {
        if (characterItemDataList != null)
        {
            return characterItemDataList;
        }

        GameUtils.Log("CharacterData Null");

        return null;
    }

    public CharacterData GetCharacterItemDataByIndex(int index)
    {
        if (characterItemDataList == null)
        {
            GameUtils.Log("CharacterData Null");
            return null;
        }

        if (characterItemDataList.Count <= index)
        {
            GameUtils.Log("CharacterData Count Error");
            return null;
        }

        return characterItemDataList[index];
    }
}

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public string iconPath;
    public string illerstrationPath;
    public int atkStat = 150;
    public int vitStat = 1500;
    public float rangeStat = 1f;
}

