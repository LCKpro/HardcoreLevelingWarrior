using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBookInfo : MonoBehaviour
{
    [SerializeField]
    private List<CharacterBookInfoData> characterDataList;

    public CharacterBookInfoData GetCharacterInfoDataByIndex(int index)
    {
        if (characterDataList == null)
        {
            GameUtils.Log("Character Info Data Null");
            return null;
        }

        if (characterDataList.Count <= index)
        {
            GameUtils.Log("Character Info Count Error");
            return null;
        }

        return characterDataList[index];
    }
}

[System.Serializable]
public class CharacterBookInfoData
{
    public int index;
    public string characterName;
    public string iconPath;
    public string illerstrationPath;
    
    public int atkValue = 150;
    public int vitValue = 150;

    public string skill_Title_0;
    public string skill_Desc_0;
    public string skill_IconPath_0;

    public string skill_Title_1;
    public string skill_Desc_1;
    public string skill_IconPath_1;

    public string characterBGDesc;
}
