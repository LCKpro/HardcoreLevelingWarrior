using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class UI_AttackPattern_Gause : MonoBehaviour
{
    public Image gauseGuide;

    private IDisposable _gauseTimer = Disposable.Empty;

    public void Init()
    {
    }

    #region 게이지 공격

    private float _time = 0;
    public void StartGauseTimer()
    {
        _gauseTimer.Dispose();
        _gauseTimer = Disposable.Empty;

        _gauseTimer = Observable.EveryUpdate().TakeUntilDisable(gameObject)
            .TakeUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                var dt = Time.deltaTime;

                _time += dt;

                if (_time >= 1)
                {
                    _time = 0;
                }

                gauseGuide.fillAmount = _time;
            });
    }

    public void OnClick_StopGause()
    {
        _gauseTimer.Dispose();
        _gauseTimer = Disposable.Empty;

        float percent = 1f;
        if (_time >= 0.95f)
        {
            percent = 2.5f;
        }
        else if (_time >= 0.9f)
        {
            percent = 2f;
        }
        else if (_time >= 0.85f)
        {
            percent = 1.5f;
        }
        else if (_time >= 0.75f)
        {
            percent = 1.2f;
        }
        else
        {
            percent = 1f;
        }

        GamePlay.Instance.battleManager.Attack_Ally(percent);

        Invoke("DeActiveGause", 1.5f);
    }

    public void DeActiveGause()
    {
        GamePlay.Instance.battleManager.StartBattle();
        GamePlay.Instance.battleManager.Attack_Enemy();
        GamePlay.Instance.attackManager.attack_Gause.gameObject.SetActive(false);
    }

    #endregion
}
