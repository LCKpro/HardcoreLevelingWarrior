using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Currency Key
    // 골드 : gold
    // 티켓 : ticket
    // 보석 : diamond

    public void InitCurrency()
    {
        
    }


    public void AddCurrency(string key, int value)
    {
        var currentCurrency = CryptoPlayerPrefs.GetInt(key, 0);
        currentCurrency += value;
        CryptoPlayerPrefs.SetInt(key, currentCurrency);
    }

    public void SubCurrency(string key, int value)
    {
        var currentCurrency = CryptoPlayerPrefs.GetInt(key, 0);
        currentCurrency -= value;

        if(currentCurrency < 0)
        {
            currentCurrency = 0;
        }

        CryptoPlayerPrefs.SetInt(key, currentCurrency);
    }

    public bool CheckCurrency(int value)
    {
        var currentCurrency = CryptoPlayerPrefs.GetInt("gold", 0);
        if(currentCurrency >= value)
        {
            return true;
        }

        return false;
    }
}
