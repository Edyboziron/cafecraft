using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Level Data")]
    public int level = 1;
    public float currentXP = 0;
    public float xpToNextLevel = 3;

    [Header("XP Ayarları")]
    public float xpGainPerCorrect = 1f;
    public float xpLossPerWrong = 1f;

    [Header("Referanslar")]
    public LevelManager levelManager; // LevelManager referansı

    void Start()
    {
        level = SaveSystem.LoadLevel();  // Kayıtlı levelı yükle
        UpdateXPThreshold();
        ClampXP();

        Debug.Log("PlayerLevelSystem start - loaded level: " + level);

        if (levelManager != null)
            levelManager.SetLevel(level);
    }


    public void GiveCoffee(bool correct)
    {
        Debug.Log("GiveCoffee çağrıldı. Doğruluk: " + correct);

        if (correct)
        {
            currentXP += xpGainPerCorrect;
            CheckLevelUp();
        }
        else
        {
            currentXP -= xpLossPerWrong;
            ClampXP();
        }

        Debug.Log($"📈 Seviye: {level} | XP: {currentXP}/{xpToNextLevel}");
    }

    void CheckLevelUp()
    {
        bool leveledUp = false;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;
            leveledUp = true;
            UpdateXPThreshold();
        }

        if (leveledUp && levelManager != null)
        {
            levelManager.SetLevel(level);  // Burada da SetLevel ile bildiriyoruz
            Debug.Log("🆙 Seviye atladın! Yeni seviye: " + level);
        }
    }

    void ClampXP()
    {
        float minXPForThisLevel = GetMinimumXPForLevel(level);
        if (currentXP < minXPForThisLevel)
        {
            currentXP = minXPForThisLevel;
        }
    }

    void UpdateXPThreshold()
    {
        xpToNextLevel = Mathf.Ceil(3f * Mathf.Pow(1.5f, level - 1));
    }

    float GetMinimumXPForLevel(int currentLevel)
    {
        float totalXP = 0f;
        for (int i = 1; i < currentLevel; i++)
        {
            totalXP += Mathf.Ceil(3f * Mathf.Pow(1.5f, i - 1));
        }
        return totalXP;
    }
}
