using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public Image[] miniIcon_Ally;

    public Image[] miniIcon_Enemy;

    public Image[] characterIcon_Skill;   // �� �����ʺ��� �ε��� 0 ����

    public Image[] skillIcon;

    public void SetInfo(EnemySquad enemySquadInfo, List<int> allySquadInfo)
    {
        for (int i = 0; i < miniIcon_Ally.Length; i++)
        {
            if(i < allySquadInfo.Count)
            {
                var characterInfo = Core.Instance.characterInfoManager.GetCharacterInfoDataByIndex(allySquadInfo[i]);

                string key = string.Format("Mini/Mini_{0:D3}", allySquadInfo[i]);
                miniIcon_Ally[i].sprite = Resources.Load<Sprite>(key);  // �츮�� ��� �̴� ������

                characterIcon_Skill[i].sprite = Resources.Load<Sprite>(characterInfo.iconPath);     // ��ų ĳ���� ������

                skillIcon[i * 2].sprite = Resources.Load<Sprite>(characterInfo.skill_IconPath_0);
                skillIcon[i * 2 + 1].sprite = Resources.Load<Sprite>(characterInfo.skill_IconPath_1);
            }
            else
            {
                miniIcon_Ally[i].gameObject.SetActive(false);
                characterIcon_Skill[i].gameObject.SetActive(false);
                skillIcon[i * 2].gameObject.SetActive(false);
                skillIcon[i * 2 + 1].gameObject.SetActive(false);

                /*miniIcon_Ally[i].color = new Color(1, 1, 1, 0);
                skillIcon[i * 2].color = new Color(1, 1, 1, 0);
                skillIcon[i * 2 + 1].color = new Color(1, 1, 1, 0);*/
            }

            if(i < enemySquadInfo.squadMemberCount)
            {
                miniIcon_Enemy[i].sprite = Resources.Load<Sprite>(enemySquadInfo.miniIconPath[i]);  // ���� ��� �̴� ������
            }
            else
            {
                miniIcon_Enemy[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
