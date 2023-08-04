using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject ui_Attack;
    public UI_AttackPattern_Gause attack_Gause;                 // ������ ���� ���
    public UI_AttackPattern_RGB attack_RGB;                 // ȭ��ǥ ���߱� ���
    public UI_AttackPattern_ContinuousBlow attack_ContinuousBlow;        // ��Ÿ ���� ���

    public void Init()
    {
    }

    public void OnClick_StartGause()
    {
        attack_Gause.gameObject.SetActive(true);
        attack_Gause.StartGauseTimer();
    }

    public void OnClick_StartRGB()
    {
        attack_RGB.gameObject.SetActive(true);
        attack_RGB.SetImage();
        attack_RGB.StartRGBTimer();
    }

    public void OnClick_StartBlow()
    {
        attack_ContinuousBlow.gameObject.SetActive(true);
        attack_ContinuousBlow.StartBlowTimer();
    }
}
