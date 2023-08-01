using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectHeroManager : MonoBehaviour
{
    // 아군 스쿼드 인덱스, 적군 스쿼드 인덱스 모음
    private List<int> allySquadIndexList = new List<int>();
    private List<int> enemySquadIndexList = new List<int>();

    // 아군 스쿼드 사거리 리스트
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

        // 사거리 데이터 추가
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
    /// 선택한 영웅들을 사거리에 맞게 세팅
    /// </summary>
    public void AddSquadByRange(int heroIndex)
    {
        // 이미 고른 영웅이 있으면 넘기기
        if (allySquadIndexList.Contains(heroIndex) == true)
        {
            Debug.Log("이미 고른 영웅이 리스트에 들어가있음");
            return;
        }

        if(allySquadImageList.Length <= selectHeroInfoList.Count)
        {
            Debug.Log("더 이상 영웅을 추가할 공간이 없음");
            return;
        }

        // 정렬용 리스트는 초기화
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
        // 만약 현재 적용된 캐릭터 수보다 인덱스가 같거나 크면 할 필요가 없음(해당 선택 구역에 캐릭터가 설정되어 있지 않음)
        if (selectHeroInfoList.Count <= index)
        {
            return;
        }

        var heroIndex = selectHeroInfoList[index].index;    // 제거하려는 구간의 캐릭터 인덱스
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
        // 만약 스쿼드를 1명 이상 채웠다면 시작 가능
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
/// 사거리에 맞게 배열하기 위한 클래스
/// </summary>
public class SelectHeroInfo
{
    public int index;
    public float range;
}
