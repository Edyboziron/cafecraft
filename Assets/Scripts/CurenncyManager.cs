using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public int money = 0;
    public Text moneyText; // UI'da parayý gösteren Text

    void Start()
    {
        money = SaveSystem.LoadMoney(); // Kaydedilen parayý yükle
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SaveSystem.SaveMoney(money); // Yeni parayý kaydet
        UpdateUI();
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            SaveSystem.SaveMoney(money); // Güncellenmiþ parayý kaydet
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + money.ToString();
    }

}
