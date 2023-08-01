using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquadManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemySquad> enemySquadDataList;

    public EnemySquad GetCharacterInfoDataByIndex(int index)
    {
        if (enemySquadDataList == null)
        {
            GameUtils.Log("Character Info Data Null");
            return null;
        }

        if (enemySquadDataList.Count <= index)
        {
            GameUtils.Log("Character Info Count Error");
            return null;
        }

        return enemySquadDataList[index];
    }
}

[System.Serializable]
public class EnemySquad
{
    public int index;
    public string squadName;
    
    public int squadMemberCount;    // 스쿼드에 몇명 있는지

    public List<string> characterNameList;
    public List<string> illerstrationPathList;
    public List<string> flagPathList;

    public List<int> atkValue;
    public List<int> vitValue;

    // 적은 스킬을 하나씩만 가짐
    public List<string> skill_TitleList;
}