using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterBookInfoData> characterItemDataList;

    public List<CharacterBookInfoData> GetCharacterInfoData()
    {
        if (characterItemDataList != null)
        {
            return characterItemDataList;
        }

        GameUtils.Log("CharacterInfoData Null");

        return null;
    }

    public CharacterBookInfoData GetCharacterInfoDataByIndex(int index)
    {
        if (characterItemDataList == null)
        {
            GameUtils.Log("CharacterInfoData Null");
            return null;
        }

        if (characterItemDataList.Count <= index)
        {
            GameUtils.Log("CharacterInfoData Count Error");
            return null;
        }

        return characterItemDataList[index];
    }
}

