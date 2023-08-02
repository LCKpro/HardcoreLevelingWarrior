using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectHeroManager : MonoBehaviour
{
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
}

/// <summary>
/// ��Ÿ��� �°� �迭�ϱ� ���� Ŭ����
/// </summary>
public class SelectHeroInfo
{
    public int index;
    public float range;
}
