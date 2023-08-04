using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectHeroManager : MonoBehaviour
{
    public GameObject bg_Black;
    public GameObject ui_SelectHero;
    public GameObject image_BG;
    public GameObject ui_BattleInterface;
    public GameObject ui_SortBtn;

    public Image allyIllerstration;
    public Image enemyIllerstration;

    // �Ʊ� ������ �ε���, ���� ������ �ε��� ����
    private List<int> allySquadIndexList = new List<int>();
    private List<int> enemySquadIndexList = new List<int>();

    // �Ʊ� ������ ��Ÿ� ����Ʈ
    private List<float> allySquadRangeList = null;
    private List<SelectHeroInfo> selectHeroInfoList = new List<SelectHeroInfo>();
    private List<SelectHeroInfo> selectHeroSortList = new List<SelectHeroInfo>();

    public Image[] allySquadImageList = new Image[4];
    public Image[] enemySquadImageList = new Image[4];

    public Sprite blankImage;
    public Button nextButton;

    public void InitData()
    {
        ui_SortBtn.SetActive(true);
        ui_SelectHero.SetActive(true);
        image_BG.SetActive(true);
        bg_Black.SetActive(false);
        allySquadIndexList.Clear();
        enemySquadIndexList.Clear();

        nextButton.interactable = false;

        // ��Ÿ� ������ �߰�
        if (allySquadRangeList == null)
        {
            allySquadRangeList = new List<float>();
            var characterInfo = Core.Instance.characterManager.GetCharacterItemData();
            foreach (var info in characterInfo)
            {
                allySquadRangeList.Add(info.rangeStat);
            }
        }
    }

    /// <summary>
    /// ������ �������� ��Ÿ��� �°� ����
    /// </summary>
    public void AddSquadByRange(int heroIndex)
    {
        // �̹� �� ������ ������ �ѱ��
        if (allySquadIndexList.Contains(heroIndex) == true)
        {
            Debug.Log("�̹� �� ������ ����Ʈ�� ������");
            return;
        }

        if(allySquadImageList.Length <= selectHeroInfoList.Count)
        {
            Debug.Log("�� �̻� ������ �߰��� ������ ����");
            return;
        }

        // ���Ŀ� ����Ʈ�� �ʱ�ȭ
        selectHeroSortList.Clear();

        allySquadIndexList.Add(heroIndex);
        selectHeroInfoList.Add(new SelectHeroInfo() { index = heroIndex, range = allySquadRangeList[heroIndex] });

        var sort = selectHeroInfoList.OrderBy(a => a.range);

        foreach (var item in sort)
        {
            selectHeroSortList.Add(item);
        }

        for (int i = 0; i < selectHeroSortList.Count; i++)
        {
            string key = string.Format("Flags/Flag_Character_{0:D3}", selectHeroSortList[i].index);
            Debug.Log(key);
            allySquadImageList[i].sprite = Resources.Load<Sprite>(key);
        }

        selectHeroInfoList.Clear();

        foreach (var item in selectHeroSortList)
        {
            selectHeroInfoList.Add(item);
        }

        CheckStart();
    }

    public void OnClick_ExcludeHero(int index)
    {
        SoundManager.instance.PlaySound("Choose");
        // ���� ���� ����� ĳ���� ������ �ε����� ���ų� ũ�� �� �ʿ䰡 ����(�ش� ���� ������ ĳ���Ͱ� �����Ǿ� ���� ����)
        if (selectHeroInfoList.Count <= index)
        {
            return;
        }

        var heroIndex = selectHeroInfoList[index].index;    // �����Ϸ��� ������ ĳ���� �ε���
        selectHeroInfoList.RemoveAt(index);
        allySquadIndexList.Remove(heroIndex);
        Debug.Log("Excluded Index : " + heroIndex);
        Debug.Log("List Count " + (selectHeroInfoList.Count + 1) + " => " + selectHeroInfoList.Count);

        for (int i = 0; i < allySquadImageList.Length; i++)
        {
            if (i < selectHeroInfoList.Count)
            {
                string key = string.Format("Flags/Flag_Character_{0:D3}", selectHeroInfoList[i].index);
                Debug.Log(key);
                allySquadImageList[i].sprite = Resources.Load<Sprite>(key);
            }
            else
            {
                allySquadImageList[i].sprite = blankImage;
            }
        }

        CheckStart();
    }

    private void CheckStart()
    {
        // ���� �����带 1�� �̻� ä���ٸ� ���� ����
        if (selectHeroInfoList.Count >= 1)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }

    public void OnClick_SubmitSquad()
    {
        // ����� ���� ����
        var enemySquad = GamePlay.Instance.enemySquadManager.GetRandomEnemySquadData();

        // Ŭ���� ����
        SoundManager.instance.PlaySound("SelectHero");

        // ���� ���� �Ŵ����� ������
        SendBattleInfo(enemySquad);

        // ��ư�� ��Ȱ��ȭ
        nextButton.gameObject.SetActive(false);

        // ���� ���� �� ���� ����(�Ϸ���Ʈ ����)
        SetSquadIllerstration(enemySquad);

        Invoke("DeActiveBlackBG", 3f);
    }

    private void SendBattleInfo(EnemySquad enemySquad)
    {
        Debug.Log("EnemySquad Index : " + enemySquad.index);

        var indexList = new List<int>();

        foreach (var info in selectHeroInfoList)
        {
            indexList.Add(info.index);
        }

        GamePlay.Instance.battleManager.SetInfo(enemySquad, indexList);
        GamePlay.Instance.battleUIManager.SetInfo(enemySquad, indexList);
    }

    private void SetSquadIllerstration(EnemySquad enemySquad)
    {
        ui_SortBtn.SetActive(false);
        bg_Black.SetActive(true);

        string key = string.Format("Illerstration/Illerstration_Character_{0:D3}", selectHeroInfoList[0].index);
        allyIllerstration.sprite = Resources.Load<Sprite>(key);
        Debug.Log(key);

        enemyIllerstration.sprite = Resources.Load<Sprite>(enemySquad.illerstrationPathList[0]);
        Debug.Log(enemySquad.illerstrationPathList[0]);

        for (int i = 0; i < enemySquad.squadMemberCount; i++)
        {
            enemySquadImageList[i].sprite = Resources.Load<Sprite>(enemySquad.flagPathList[i]);
        }
    }

    public void DeActiveBlackBG()
    {
        bg_Black.SetActive(false);
        image_BG.SetActive(false);
        ui_SelectHero.SetActive(false);
        ui_BattleInterface.SetActive(true);
    }
}

/// <summary>
/// ��Ÿ��� �°� �迭�ϱ� ���� Ŭ����
/// </summary>
public class SelectHeroInfo
{
    public int index;
    public float range;
}
