using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class BattleUIManager : MonoBehaviour
{
    public Image[] miniIcon_Ally;

    public Image[] miniIcon_Enemy;

    public Image[] characterIcon_Skill;   // 맨 오른쪽부터 인덱스 0 시작

    public Image[] skillIcon;

    public Image[] hpBar_Ally;
    public Image[] hpBar_Enemy;

    public GameObject[] btn_SkillSelectList;
    public GameObject btn_SkillSelect;

    private IDisposable _hpBarTimer = Disposable.Empty;

    public void SetInfo(EnemySquad enemySquadInfo, List<int> allySquadInfo)
    {
        for (int i = 0; i < miniIcon_Ally.Length; i++)
        {
            if(i < allySquadInfo.Count)
            {
                var characterInfo = Core.Instance.characterInfoManager.GetCharacterInfoDataByIndex(allySquadInfo[i]);

                string key = string.Format("Mini/Mini_{0:D3}", allySquadInfo[i]);
                miniIcon_Ally[i].sprite = Resources.Load<Sprite>(key);  // 우리팀 상단 미니 아이콘

                characterIcon_Skill[i].sprite = Resources.Load<Sprite>(characterInfo.iconPath);     // 스킬 캐릭터 아이콘

                skillIcon[i * 2].sprite = Resources.Load<Sprite>(characterInfo.skill_IconPath_0);
                skillIcon[i * 2 + 1].sprite = Resources.Load<Sprite>(characterInfo.skill_IconPath_1);
            }
            else
            {
                miniIcon_Ally[i].gameObject.SetActive(false);
                characterIcon_Skill[i].gameObject.SetActive(false);
                skillIcon[i * 2].gameObject.SetActive(false);
                skillIcon[i * 2 + 1].gameObject.SetActive(false);
            }

            if(i < enemySquadInfo.squadMemberCount)
            {
                miniIcon_Enemy[i].sprite = Resources.Load<Sprite>(enemySquadInfo.miniIconPath[i]);  // 적팀 상단 미니 아이콘
            }
            else
            {
                miniIcon_Enemy[i].color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void SetAllyHPUI(int index, float currentVit, float maxVit)
    {
        // 현재 체력 비율
        float rate = currentVit / maxVit;

        SetHPBarTimer(hpBar_Ally[index], rate);
    }

    public void SetAllyZeroHP(int index)
    {
        hpBar_Ally[index].fillAmount = 0;
        SetHPBarTimer(hpBar_Ally[index], 0);
    }

    public void SetEnemyHPUI(int index, float currentVit, float maxVit)
    {
        // 현재 체력 비율
        float rate = currentVit / maxVit;

        SetHPBarTimer(hpBar_Enemy[index], rate);
    }

    public void SetEnemyZeroHP(int index)
    {
        hpBar_Enemy[index].fillAmount = 0;
        SetHPBarTimer(hpBar_Enemy[index], 0);
    }

    private void SetHPBarTimer(Image hpBar, float rate)
    {
        _hpBarTimer.Dispose();
        _hpBarTimer = Disposable.Empty;

        float currentRate = hpBar.fillAmount;

        _hpBarTimer = Observable.EveryUpdate().TakeUntilDisable(gameObject)
            .TakeUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                var dt = Time.deltaTime;

                currentRate -= dt / 2;

                if (currentRate <= rate)
                {
                    hpBar.fillAmount = rate;
                    _hpBarTimer.Dispose();
                    _hpBarTimer = Disposable.Empty;
                }
                else
                {
                    hpBar.fillAmount = currentRate;
                }
            });
    }

    public void SetActiveBtn_SkillSelect(bool isActive)
    {
        btn_SkillSelect.SetActive(isActive);
    }

    public void SetBtn_SkillSelectList(bool[] isDead)
    {
        for (int i = 0; i < btn_SkillSelectList.Length; i++)
        {
            if(isDead[i] == false)
            {
                btn_SkillSelectList[i].SetActive(true);
            }
            else
            {
                btn_SkillSelectList[i].SetActive(false);
            }
        }
    }
}
