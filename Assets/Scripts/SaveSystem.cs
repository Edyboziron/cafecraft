using UnityEngine;

public static class SaveSystem
{
    private const string LevelKey = "SavedLevel";
    private const string MoneyKey = "SavedMoney";

    public static void SaveSpriteIndex(int objectId, int spriteIndex)
    {
        PlayerPrefs.SetInt($"SpriteIndex_{objectId}", spriteIndex);
        PlayerPrefs.Save();
    }

    public static int LoadSpriteIndex(int objectId)
    {
        return PlayerPrefs.GetInt($"SpriteIndex_{objectId}", 0);
    }

    public static void SavePaymentStatus(int objectId, bool hasPaid)
    {
        PlayerPrefs.SetInt($"HasPaid_{objectId}", hasPaid ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool HasPaidOnce(int objectId)
    {
        return PlayerPrefs.GetInt($"HasPaid_{objectId}", 0) == 1;
    }

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.Save();
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, 1);
    }

    public static void SaveMoney(int money)
    {
        PlayerPrefs.SetInt(MoneyKey, money);
        PlayerPrefs.Save();
    }

    public static int LoadMoney()
    {
        return PlayerPrefs.GetInt(MoneyKey, 0);
    }

    public static void UnlockRecipe(string recipeName)
    {
        PlayerPrefs.SetInt("Recipe_" + recipeName, 1);
        PlayerPrefs.Save();
    }

    public static bool IsRecipeUnlocked(string recipeName)
    {
        return PlayerPrefs.GetInt("Recipe_" + recipeName, 0) == 1;
    }

    public static void ResetSaveData()
    {
        SaveLevel(1);
        SaveMoney(0);

        for (int objectId = 0; objectId <= 6; objectId++)
        {
            SaveSpriteIndex(objectId, 0);
            SavePaymentStatus(objectId, false);
        }

        Debug.Log("🧼 Kayıtlar sıfırlandı.");
    }
}
