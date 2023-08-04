using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using TMPro;

public class UI_AttackPattern_RGB : MonoBehaviour
{
    public Image[] rgbImageList;
    private List<int> numberList = new List<int>() { 0, 1, 2 };
    private List<int> imageNumberList = new List<int>();

    public TextMeshProUGUI text_Timer;

    public Animator[] anim_RGB;

    private int currentIndex = 0;

    private IDisposable _rgbTimer = Disposable.Empty;
    private IDisposable _textTimer = Disposable.Empty;

    public void SetImage()
    {
        isEnd = false;
        currentIndex = 0;
        imageNumberList.Clear();

        for (int i = 0; i < rgbImageList.Length; i++)
        {
            var num = GameUtils.RandomItem(numberList);

            if(num == 0)
            {
                rgbImageList[i].color = Color.red;
                imageNumberList.Add(0);
            }
            else if(num == 1)
            {
                rgbImageList[i].color = Color.green;
                imageNumberList.Add(1);
            }
            else if (num == 2)
            {
                rgbImageList[i].color = Color.blue;
                imageNumberList.Add(2);
            }
        }
    }

    #region RGB 공격

    private float _time = 0;
    public void StartRGBTimer()
    {
        _time = 0;
        _rgbTimer.Dispose();
        _rgbTimer = Disposable.Empty;

        _rgbTimer = Observable.EveryUpdate().TakeUntilDisable(gameObject)
            .TakeUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                var dt = Time.deltaTime;

                _time += dt;

                if(_time >= 5f)
                {
                    OnClick_StopRGB();
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

    public void OnClick_StopRGB()
    {
        _rgbTimer.Dispose();
        _rgbTimer = Disposable.Empty;
        _textTimer.Dispose();
        _textTimer = Disposable.Empty;

        float percent = 1f;
        if (_time >= 5f)
        {
            percent = 1f;
        }
        else if (_time >= 3f)
        {
            percent = 1.2f;
        }
        else if (_time >= 1.5f)
        {
            percent = 2.0f;
        }
        else
        {
            percent = 2.5f;
        }

        GamePlay.Instance.battleManager.Attack_Ally(percent);

        Invoke("DeActiveRGB", 1.5f);
    }

    public void DeActiveRGB()
    {
        GamePlay.Instance.battleManager.StartBattle();
        GamePlay.Instance.battleManager.Attack_Enemy();
        GamePlay.Instance.attackManager.attack_RGB.gameObject.SetActive(false);
    }

    private bool isEnd = false;
    private void CheckEnd()
    {
        if(currentIndex == rgbImageList.Length)
        {
            isEnd = true;
            OnClick_StopRGB();
        }
    }

    #endregion

    #region 버튼


    public void OnClick_Red()
    {
        if(isEnd == true)
        {
            return;
        }

        if(imageNumberList[currentIndex] == 0)
        {
            SoundManager.instance.PlaySound("RGBSuccess");
            rgbImageList[currentIndex].color = new Color(1, 1, 1, 1);
            currentIndex++;
        }
        else
        {
            SoundManager.instance.PlaySound("RGBFail");
            anim_RGB[0].SetTrigger("Incorrect");
        }

        CheckEnd();
    }

    public void OnClick_Green()
    {
        if (isEnd == true)
        {
            return;
        }

        if (imageNumberList[currentIndex] == 1)
        {
            SoundManager.instance.PlaySound("RGBSuccess");
            rgbImageList[currentIndex].color = new Color(1, 1, 1, 1);
            currentIndex++;
        }
        else
        {
            SoundManager.instance.PlaySound("RGBFail");
            anim_RGB[1].SetTrigger("Incorrect");
        }

        CheckEnd();
    }

    public void OnClick_Blue()
    {
        if (isEnd == true)
        {
            return;
        }

        if (imageNumberList[currentIndex] == 2)
        {
            SoundManager.instance.PlaySound("RGBSuccess");
            rgbImageList[currentIndex].color = new Color(1, 1, 1, 1);
            currentIndex++;
        }
        else
        {
            SoundManager.instance.PlaySound("RGBFail");
            anim_RGB[2].SetTrigger("Incorrect");
        }

        CheckEnd();
    }

    #endregion

}
