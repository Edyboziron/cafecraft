using UnityEngine;
using UnityEngine.UI;

public class BuyIngredient : MonoBehaviour
{
    [Header("Satın Alma Ayarları")]
    public Button buyButton;
    public int cost = 50;
    public int requiredLevel = 1;

    private LevelManager levelManager;
    private CustomerManager customerManager;

    void Start()
    {
        if (buyButton == null)
            buyButton = GetComponent<Button>();

        levelManager = FindFirstObjectByType<LevelManager>();
        customerManager = FindFirstObjectByType<CustomerManager>();

        if (buyButton != null)
        {
            buyButton.onClick.AddListener(Buy);
        }
    }

    void Update()
    {
        if (buyButton == null) return;

        int currentMoney = customerManager != null ? customerManager.GetMoney() : Customer.money;
        int currentLevel = levelManager != null ? levelManager.CurrentLevel : 1;

        bool enoughMoney = currentMoney >= cost;
        bool levelUnlocked = currentLevel >= requiredLevel;

        buyButton.interactable = enoughMoney && levelUnlocked;
    }

    void Buy()
    {
        int currentMoney = customerManager != null ? customerManager.GetMoney() : Customer.money;
        int currentLevel = levelManager != null ? levelManager.CurrentLevel : 1;

        if (currentMoney >= cost && currentLevel >= requiredLevel)
        {
            if (customerManager != null)
            {
                customerManager.AddMoney(-cost);
            }
            Customer.money = Mathf.Max(0, Customer.money - cost);

            Debug.Log($"✅ Malzeme alındı. Kalan para: {(customerManager != null ? customerManager.GetMoney() : Customer.money)}");
        }
        else
        {
            Debug.LogWarning("⚠️ Seviye veya para yetersiz!");
        }
    }
}
