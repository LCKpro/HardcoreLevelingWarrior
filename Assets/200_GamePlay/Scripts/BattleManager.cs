using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private EnemySquad enemySquad;
    private List<CharacterBookInfoData> characterBookInfoDatas = new List<CharacterBookInfoData>();

    public void SetInfo(EnemySquad enemySquadInfo, List<int> allySquadInfo)
    {
        enemySquad = null;
        characterBookInfoDatas = new List<CharacterBookInfoData>();

        enemySquad = new EnemySquad()
        {
            index = enemySquadInfo.index,
            squadName = enemySquadInfo.squadName,

            squadMemberCount = enemySquadInfo.squadMemberCount,

            characterNameList = enemySquadInfo.characterNameList,
            illerstrationPathList = enemySquadInfo.illerstrationPathList,
            flagPathList = enemySquadInfo.flagPathList,

            atkValue = enemySquadInfo.atkValue,
            vitValue = enemySquadInfo.vitValue,
            skill_TitleList = enemySquadInfo.skill_TitleList,
        };

        foreach (var info in allySquadInfo)
        {
            var characterInfo = Core.Instance.characterInfoManager.GetCharacterInfoDataByIndex(info);
            characterBookInfoDatas.Add(characterInfo);
        }
    }

}
