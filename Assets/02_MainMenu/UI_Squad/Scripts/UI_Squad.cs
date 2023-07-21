using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Squad : MonoBehaviour
{
    private UI_SquadScrollerController _controller = null;

    public TextMeshProUGUI text_Name;
    public Image image_Illerstration;

    void Start()
    {
    }

    public void SetSquad(SquadInfo squadInfo)
    {
        text_Name.text = squadInfo.characterName;
        //image_Illerstration.sprite = Resources.Load<Sprite>(squadInfo.illerstrationPath);
        image_Illerstration.sprite = Resources.Load<Sprite>(squadInfo.illerstrationPath + "_0");
    }

    public void SetData()
    {
        if (_controller == null)
            _controller = GetComponent<UI_SquadScrollerController>();
        _controller.SetData();
    }
}
