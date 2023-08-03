using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // 만약 버튼 클릭 중 인덱스랑 리스트 카운트 비교해서 인덱스가 같거나 크면 return 시켜버리기

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
            miniIconPath = enemySquadInfo.miniIconPath,
        };

        foreach (var info in allySquadInfo)
        {
            var characterInfo = Core.Instance.characterInfoManager.GetCharacterInfoDataByIndex(info);
            characterBookInfoDatas.Add(characterInfo);
        }
    }

}
