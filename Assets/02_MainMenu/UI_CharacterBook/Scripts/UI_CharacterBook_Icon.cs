using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterBook_Icon : MonoBehaviour
{
    public Image image_Icon;
    private int _index;
    public void SetData(int index)
    {
        _index = index;
        string key = string.Format("Icons/Icon_Character_{0:D3}", index);
        Debug.Log(key);
        image_Icon.sprite = Resources.Load<Sprite>(key);
    }

    public void OnClick_ShowInfo()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        var popup = Core.Instance.uiPopUpManager.ShowAndGet<UI_CharacterBooks>("UI_CharacterBooks");
        popup.SetCharacterBook(_index);
    }

}
