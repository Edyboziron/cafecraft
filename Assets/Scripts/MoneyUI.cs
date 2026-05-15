using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText; // Inspector üzerinden bağlanmalı
    public CustomerManager customerManager; // Inspector üzerinden bağlanmalı

    void Update()
    {
        if (customerManager != null)
        {
            moneyText.text = "💰 " + customerManager.GetMoney().ToString() ;
        }
        else
        {
            moneyText.text = "💰 ---";
        }
    }
}
