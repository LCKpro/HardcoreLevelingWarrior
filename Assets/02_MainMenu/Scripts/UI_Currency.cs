using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI currency_Gold;
    public TextMeshProUGUI currency_Ticket;
    public TextMeshProUGUI currency_Diamond;

    public void InitCurrency()
    {
        var gold = CryptoPlayerPrefs.GetInt("gold", 0);
        var ticket = CryptoPlayerPrefs.GetInt("ticket", 0);
        var diamond = CryptoPlayerPrefs.GetInt("diamond", 0);

        gold = 10000000;
        ticket = 100;
        diamond = 3000;

        currency_Gold.text = gold.ToString("#,##0");
        currency_Ticket.text = ticket.ToString("#,##0");
        currency_Diamond.text = diamond.ToString("#,##0");
    }
}
