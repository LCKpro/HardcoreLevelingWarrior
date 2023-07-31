using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectCharacterBook : MonoBehaviour
{
    public Transform selectZone;
    public UI_CharacterBook_Icon iconPrefab;

    private bool isOpen = false;
    public void SetData()
    {
        if(isOpen == true)
        {
            return;
        }

        isOpen = true;

        var characterInfo = Core.Instance.characterManager.GetCharacterItemData();

        for (int index = 0; index < characterInfo.Count; index++)
        {
            var obj = Instantiate(iconPrefab, selectZone);
            obj.SetData(index);
        }
    }

    public void OnClick_Exit()
    {
        SoundManager.instance.PlayUIButtonClickSound();
        Core.Instance.uiPopUpManager.Hide("UI_SelectCharacterBook");
    }
}
