using UnityEngine;
using System;
using UniRx;
using TMPro;

public class UI_AttackPattern_ContinuousBlow : MonoBehaviour
{
    public TextMeshProUGUI text_Timer;
    public TextMeshProUGUI count_Timer;

    private IDisposable _blowTimer = Disposable.Empty;
    private IDisposable _textTimer = Disposable.Empty;

    #region 연타 공격

    private float _time = 0;
    private int _count = 0;
    public void StartBlowTimer()
    {
        _time = 0;
        _count = 0;
        _blowTimer.Dispose();
        _blowTimer = Disposable.Empty;

        _blowTimer = Observable.EveryUpdate().TakeUntilDisable(gameObject)
            .TakeUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                if (Input.GetMouseButtonDown(0) == true)
                {
                    _count++;
                }

                var dt = Time.deltaTime;

                _time += dt;

                if (_time < 3f)
                {
                    count_Timer.text = _count.ToString();
                }
                else
                {
                    _blowTimer.Dispose();
                    _blowTimer = Disposable.Empty;
                    _textTimer.Dispose();
                    _textTimer = Disposable.Empty;

                    OnClick_StopBlow();
                    text_Timer.text = "3.00";
                }
            });

        _textTimer.Dispose();
        _textTimer = Disposable.Empty;

        _textTimer = Observable.Interval(TimeSpan.FromSeconds(0.1f)).TakeUntilDisable(gameObject)
            .TakeUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                text_Timer.text = string.Format("{0:N2}", _time);
            });
    }

    public void OnClick_StopBlow()
    {
        float percent = 1f;
        if (_count >= 50)
        {
            percent = 2.5f;
        }
        else if (_count >= 40)
        {
            percent = 2.0f;
        }
        else if (_count >= 30)
        {
            percent = 1.2f;
        }
        else
        {
            percent = 1f;
        }

        GamePlay.Instance.battleManager.Attack_Ally(percent);

        Invoke("DeActiveBlow", 1.5f);
    }

    public void DeActiveBlow()
    {
        GamePlay.Instance.battleManager.StartBattle();
        GamePlay.Instance.battleManager.Attack_Enemy();
        GamePlay.Instance.attackManager.attack_ContinuousBlow.gameObject.SetActive(false);
    }

    #endregion
}
