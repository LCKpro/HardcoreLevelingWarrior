using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterBooks : MonoBehaviour
{
    public CharacterBookInfo characterBookInfo;

    public TextMeshProUGUI text_Name;
    public Image image_Illerstration;

    public TextMeshProUGUI text_ATKValue;
    public TextMeshProUGUI text_VITValue;

    public TextMeshProUGUI text_Skill_Title_0;
    public TextMeshProUGUI text_Skill_Desc_0;

    public TextMeshProUGUI text_Skill_Title_1;
    public TextMeshProUGUI text_Skill_Desc_1;

    public Image image_SkillIcon_0;
    public Image image_SkillIcon_1;

    public TextMeshProUGUI text_CharacterBGDesc;

    private string illerstCode = "";

    public void SetCharacterBook(int index)
    {
        var info = characterBookInfo.GetCharacterInfoDataByIndex(index);

        text_Name.text = info.characterName;

        // 일러스트는 각성용으로 따로 저장
        illerstCode = info.illerstrationPath;
        image_Illerstration.sprite = Resources.Load<Sprite>(info.illerstrationPath);

        text_ATKValue.text = info.atkValue.ToString();
        text_VITValue.text = info.vitValue.ToString();

        text_Skill_Title_0.text = info.skill_Title_0;
        text_Skill_Desc_0.text = info.skill_Desc_0;
        image_SkillIcon_0.sprite = Resources.Load<Sprite>(info.skill_IconPath_0);

        text_Skill_Title_1.text = info.skill_Title_1;
        text_Skill_Desc_1.text = info.skill_Desc_1;
        image_SkillIcon_1.sprite = Resources.Load<Sprite>(info.skill_IconPath_1);

        text_CharacterBGDesc.text = info.characterBGDesc;
    }

    public void OnClick_ChangeAwakenIllerst()
    {
        image_Illerstration.sprite = Resources.Load<Sprite>(illerstCode + "_0");
    }

    public void OnClick_Exit()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        Core.Instance.uiPopUpManager.Hide("UI_CharacterBooks");
    }
}

public class CharacterBooks
{
    public int index;
    public string characterName;
    public string illerstrationPath;
}
