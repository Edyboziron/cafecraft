using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    [Header("Tariflerin Gösterileceği Textler (Inspector'dan 30 tane)")]
    public Text[] recipeTexts;
    [Header("Kahve İsimleri (Inspector'dan 30 tane, aynı sırada)")]
    public string[] coffeeNames;

    // Singleton pattern ile diğer scriptlerden erişim
    public static RecipeBookManager Instance;

    void Awake()
    {
        // Singleton kurulumu
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        DisplayRecipes();
    }

    void OnEnable()
    {
        // Panel her açıldığında tarifleri güncelle
        DisplayRecipes();
    }

    public void DisplayRecipes()
    {
        Debug.Log("📋 DisplayRecipes çalışıyor...");

        // Önce tüm textleri gizle
        foreach (var txt in recipeTexts)
        {
            if (txt != null)
                txt.gameObject.SetActive(false);
        }

        // Açılan tariflere göre ilgili Text'i aç
        for (int i = 0; i < coffeeNames.Length; i++)
        {
            Debug.Log($"🔍 Kontrol ediliyor: '{coffeeNames[i]}' - Açık mı: {SaveSystem.IsRecipeUnlocked(coffeeNames[i])}");

            if (SaveSystem.IsRecipeUnlocked(coffeeNames[i]))
            {
                if (i < recipeTexts.Length && recipeTexts[i] != null)
                {
                    recipeTexts[i].gameObject.SetActive(true);
                    Debug.Log($"✅ Tarif açıldı: {coffeeNames[i]} (index: {i})");
                }
                else
                {
                    Debug.LogWarning("recipeTexts dizisi coffeeNames dizisinden kısa: " + i);
                }
            }
        }
    }

    // Bu fonksiyonu kahve yapıldığında diğer scriptlerden çağırın
    public static void RefreshRecipes()
    {
        if (Instance != null)
        {
            Instance.DisplayRecipes();
        }
    }

    // Non-static versiyonu da ekleyelim
    public void RefreshRecipesNonStatic()
    {
        DisplayRecipes();
    }
}