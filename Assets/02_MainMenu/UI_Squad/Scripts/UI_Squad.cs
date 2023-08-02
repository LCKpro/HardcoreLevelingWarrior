using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Squad : MonoBehaviour
{
    private UI_SquadScrollerController _controller = null;

    public TextMeshProUGUI text_Name;
    public Image image_Illerstration;

    public TextMeshProUGUI text_ATKValue;
    public TextMeshProUGUI text_VITValue;

    public void SetSquad(SquadInfo squadInfo)
    {
        text_Name.text = squadInfo.characterName;
        image_Illerstration.sprite = Resources.Load<Sprite>(squadInfo.illerstrationPath);
        //image_Illerstration.sprite = Resources.Load<Sprite>(squadInfo.illerstrationPath + "_0");

        var info = Core.Instance.characterManager.GetCharacterItemDataByIndex(squadInfo.index);
        text_ATKValue.text = info.atkStat.ToString();
        text_VITValue.text = info.vitStat.ToString();
    }

    public void SetData()
    {
        if (_controller == null)
            _controller = GetComponent<UI_SquadScrollerController>();
        _controller.SetData();
    }

    public void OnClick_Exit()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        Core.Instance.uiPopUpManager.Hide("UI_Squad");
    }
}
