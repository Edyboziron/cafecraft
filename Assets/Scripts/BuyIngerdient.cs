using UnityEngine;
using UnityEngine.UI;

public class BuyIngredient : MonoBehaviour
{
    public Button buyButton;
    public int cost = 50;
    public int requiredLevel = 1;

    private LevelManager levelManager;

    void Start()
    {
        if (buyButton == null)
            buyButton = GetComponent<Button>();

        levelManager = FindFirstObjectByType<LevelManager>();

        buyButton.onClick.AddListener(Buy);
    }

    void Update()
    {
        if (levelManager == null) return;

        bool enoughMoney = Customer.money >= cost;
        bool levelUnlocked = levelManager.CurrentLevel >= requiredLevel;

        buyButton.interactable = enoughMoney && levelUnlocked;
    }

    void Buy()
    {
        if (Customer.money >= cost && levelManager.CurrentLevel >= requiredLevel)
        {
            Customer.money -= cost;
            Debug.Log("Malzeme alındı. Kalan para: " + Customer.money);
        }
        else
        {
            Debug.LogWarning("Seviye veya para yetersiz!");
        }
    }
}
