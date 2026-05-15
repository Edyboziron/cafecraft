using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
     private int currentLevel;
    public int CurrentLevel => currentLevel;

    [Header("Referanslar")]
    public CraftingSystem craftingSystem;
    public CustomerManager customerManager;

    [Header("Level Tanımları")]
    public List<LevelData> levels = new List<LevelData>();

    [Header("🧪 TEST UI")]
    public Text levelText;
    public Button levelUpButton;
    public Button resetButton;

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public List<int> enabledButtonIndexes;
        public List<string> allowedRecipes;
    }

    private int correctOrdersInLevel = 0;
    public int ordersToLevelUp = 3;

    void Awake()
    {
        currentLevel = SaveSystem.LoadLevel();
        Debug.Log($"🔁 Kayıtlı seviye yüklendi: {currentLevel}");

        Debug.Log("🎯 PlayerPrefs Key Kontrolü:");
        foreach (var key in new string[] { "SavedLevel", "SavedMoney" })
        {
            Debug.Log($"{key} => {PlayerPrefs.GetInt(key, -999)}");
        }
    }

    void Start()
    {
        ApplyLevelSettings();
        UpdateUI();

        
    }

    public void ApplyLevelSettings()
    {
        for (int i = 0; i < craftingSystem.optionButtons.Length; i++)
        {
            craftingSystem.optionButtons[i].interactable = false;
        }

        var level = levels.Find(l => l.level == currentLevel);
        if (level != null)
        {
            foreach (int index in level.enabledButtonIndexes)
            {
                if (index >= 0 && index < craftingSystem.optionButtons.Length)
                    craftingSystem.optionButtons[index].interactable = true;
            }

            customerManager.possibleOrders = craftingSystem.recipePrefabs
                .Where(prefab =>
                {
                    var item = prefab.GetComponent<CoffeeItem>();
                    return item != null && level.allowedRecipes.Contains(item.coffeeName);
                })
                .ToArray();
        }
        else
        {
            Debug.LogWarning("⚠️ Level tanımı bulunamadı!");
        }
    }

    public void CorrectOrder()
    {
        correctOrdersInLevel++;
        Debug.Log($"✔️ Doğru sipariş sayısı: {correctOrdersInLevel} / {ordersToLevelUp}");

        if (correctOrdersInLevel >= ordersToLevelUp)
        {
            currentLevel++;
            correctOrdersInLevel = 0;

            Debug.Log($"🎉 Seviye atlandı! Yeni Seviye: {currentLevel}");
        }

        SaveSystem.SaveLevel(currentLevel); // 💾 Her doğru siparişte kayıt yapılır
        Debug.Log($"💾 Seviye kaydedildi: {currentLevel}");

        ApplyLevelSettings();
        UpdateUI();
    }

    public void WrongOrder()
    {
        // Hatalı siparişlerde ceza sistemi eklenebilir
    }

    public void SetLevel(int newLevel)
    {
        currentLevel = newLevel;
        ApplyLevelSettings();
        SaveSystem.SaveLevel(currentLevel);
        Debug.Log("SetLevel çağrıldı, seviye ayarlandı: " + currentLevel);
        UpdateUI();
    }

    // 🧪 Test UI fonksiyonları
    private void TestLevelUp()
    {
        currentLevel++;
        SaveSystem.SaveLevel(currentLevel);
        Debug.Log($"🟢 Test Level Up: Seviye {currentLevel} kaydedildi.");
        ApplyLevelSettings();
        UpdateUI();
    }

   /* private void TestReset()
    {
        SaveSystem.ClearAll();
        currentLevel = 1;
        Debug.Log("🔴 PlayerPrefs sıfırlandı.");
        ApplyLevelSettings();
        UpdateUI();
    }*/

    private void UpdateUI()
    {
        if (levelText != null)
            levelText.text = $"Level: {currentLevel}";
    }
}
