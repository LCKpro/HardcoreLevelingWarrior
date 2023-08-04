using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private EnemySquad enemySquad;
    private List<CharacterBookInfoData> characterBookInfoDatas = new List<CharacterBookInfoData>();

    private List<BattleStatus> allyStatusList = new List<BattleStatus>();
    private List<BattleStatus> enemyStatusList = new List<BattleStatus>();

    private int attacker_Index = 0;
    private int opponent_Index = 0;

    private bool[] isDead_Ally = new bool[4] { true, true, true, true };
    private bool[] isDead_Enemy = new bool[4] { true, true, true, true };

    private bool ourTurn = false;

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

        SetStatus();
        SetDeath(allySquadInfo.Count, enemySquadInfo.squadMemberCount);
        StartBattle();
    }

    private void SetStatus()
    {
        allyStatusList.Clear();
        enemyStatusList.Clear();

        // �츮�� ��/ü ����
        foreach (var data in characterBookInfoDatas)
        {
            allyStatusList.Add(new BattleStatus()
            {
                atkValue = data.atkValue,
                currentVitValue = (float)data.vitValue,
                maxVitValue = (float)data.vitValue
            });
        }

        // ���� ��/ü ����
        for (int i = 0; i < enemySquad.squadMemberCount; i++)
        {
            enemyStatusList.Add(new BattleStatus()
            {
                atkValue = enemySquad.atkValue[i],
                currentVitValue = (float)enemySquad.vitValue[i],
                maxVitValue = (float)enemySquad.vitValue[i]
            });
        }
    }

    private void SetDeath(int allyCount, int enemyCount)
    {
        for (int i = 0; i < allyCount; i++)
        {
            isDead_Ally[i] = false;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            isDead_Enemy[i] = false;
        }

        GamePlay.Instance.battleUIManager.SetBtn_SkillSelectList(isDead_Enemy);
    }

    public void StartBattle()
    {
        if (ourTurn == false)
        {
            ourTurn = true;
        }
        else
        {
            ourTurn = false;
        }
    }

    private void SetAttacker(int index)
    {
        isCanSwitch = true;
        attacker_Index = index;
        GamePlay.Instance.battleUIManager.SetActiveBtn_SkillSelect(true);
    }

    #region ����

    /// <summary>
    /// �츮�� ����/����� �ǰ�
    /// </summary>
    /// <param name="percent"></param>
    public void Attack_Ally(float percent)
    {
        var damage = allyStatusList[attacker_Index].atkValue * percent;

        enemyStatusList[opponent_Index].currentVitValue -= damage;

        if (enemyStatusList[opponent_Index].currentVitValue <= 0)
        {
            isDead_Enemy[opponent_Index] = true;
            GamePlay.Instance.battleUIManager.SetEnemyZeroHP(opponent_Index);
            GamePlay.Instance.battleUIManager.SetBtn_SkillSelectList(isDead_Enemy);
        }
        else
        {
            GamePlay.Instance.battleUIManager.SetEnemyHPUI(opponent_Index, enemyStatusList[opponent_Index].currentVitValue, enemyStatusList[opponent_Index].maxVitValue);
        }
    }

    /// <summary>
    /// ���� ����/�츮�� �ǰ�
    /// </summary>
    public void Attack_Enemy()
    {
        Debug.Log("Attack_Enemy Start");
        attacker_Index = 0;
        opponent_Index = 0;

        for (int i = 0; i < isDead_Ally.Length; i++)
        {
            if(isDead_Ally[i] == false)
            {
                opponent_Index = i;
                break;
            }
        }

        var damage = enemyStatusList[attacker_Index].atkValue * 1f;

        allyStatusList[opponent_Index].currentVitValue -= damage;

        if (allyStatusList[opponent_Index].currentVitValue <= 0)
        {
            isDead_Ally[opponent_Index] = true;
            GamePlay.Instance.battleUIManager.SetAllyZeroHP(opponent_Index);
        }
        else
        {
            GamePlay.Instance.battleUIManager.SetAllyHPUI(opponent_Index, allyStatusList[opponent_Index].currentVitValue, allyStatusList[opponent_Index].maxVitValue);
        }

        StartBattle();
    }

    #endregion

    #region ��ư

    /// <summary>
    /// N��° ��� Ŭ��(0~3)
    /// </summary>
    private bool isCanSwitch = false;
    /// 
    public void OnClick_Opponent(int index)
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (isCanSwitch == false)
        {
            Debug.Log("��� ���� �Ұ�");
            return;
        }

        opponent_Index = index;
        isCanSwitch = false;
        GamePlay.Instance.attackManager.OnClick_StartRGB();
        GamePlay.Instance.battleUIManager.SetActiveBtn_SkillSelect(false);
    }

    /// <summary>
    /// �÷��̾� 1�� ��ų 1, 2
    /// </summary>
    public void OnClick_Player1_Skill_0()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 1)
        {
            Debug.Log("�÷��̾� ī��Ʈ 1�� �̸��̶� �ȵ�");
            return;
        }
    }

    public void OnClick_Player1_Skill_1()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 1)
        {
            Debug.Log("�÷��̾� ī��Ʈ 1�� �̸��̶� �ȵ�");
            return;
        }

        if (isCanSwitch == true)
        {
            Debug.Log("�̹� ���� ��ų ��ư�� ����");
            return;
        }

        SetAttacker(0);
    }

    /// <summary>
    /// �÷��̾� 2�� ��ų 1, 2
    /// </summary>
    public void OnClick_Player2_Skill_0()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 2)
        {
            Debug.Log("�÷��̾� ī��Ʈ 2�� �̸��̶� �ȵ�");
            return;
        }
    }

    public void OnClick_Player2_Skill_1()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 2)
        {
            Debug.Log("�÷��̾� ī��Ʈ 2�� �̸��̶� �ȵ�");
            return;
        }

        if (isCanSwitch == true)
        {
            Debug.Log("�̹� ���� ��ų ��ư�� ����");
            return;
        }

        SetAttacker(1);
    }

    /// <summary>
    /// �÷��̾� 3�� ��ų 1, 2
    /// </summary>
    public void OnClick_Player3_Skill_0()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 3)
        {
            Debug.Log("�÷��̾� ī��Ʈ 3�� �̸��̶� �ȵ�");
            return;
        }
    }

    public void OnClick_Player3_Skill_1()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 3)
        {
            Debug.Log("�÷��̾� ī��Ʈ 3�� �̸��̶� �ȵ�");
            return;
        }

        if (isCanSwitch == true)
        {
            Debug.Log("�̹� ���� ��ų ��ư�� ����");
            return;
        }

        SetAttacker(2);
    }

    /// <summary>
    /// �÷��̾� 4�� ��ų 1, 2
    /// </summary>
    public void OnClick_Player4_Skill_0()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 4)
        {
            Debug.Log("�÷��̾� ī��Ʈ 4�� �̸��̶� �ȵ�");
            return;
        }
    }

    public void OnClick_Player4_Skill_1()
    {
        if (ourTurn == false)
        {
            Debug.Log("���� ��� ����");
            return;
        }

        if (characterBookInfoDatas.Count < 4)
        {
            Debug.Log("�÷��̾� ī��Ʈ 4�� �̸��̶� �ȵ�");
            return;
        }

        if (isCanSwitch == true)
        {
            Debug.Log("�̹� ���� ��ų ��ư�� ����");
            return;
        }

        SetAttacker(3);
    }

    #endregion
}

public class BattleStatus
{
    public int atkValue;
    public float currentVitValue;
    public float maxVitValue;
}