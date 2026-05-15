using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CraftingSystem : MonoBehaviour
{
    public Image[] selectionSlots;
    public Button[] optionButtons;
    public Sprite[] itemSprites;
    public string[] itemNames;
    public Sprite emptySprite;

    public Transform spawnPoint;
    public GameObject[] recipePrefabs;

    public CustomerManager customerManager;
    public RecipeBookManager recipeBookManager;

    public AudioSource sfxAudioSource;
    public AudioClip wrongRecipeSound;
    public AudioClip correctRecipeSound; // ✅ Doğru tarif sesi

    private string[] selectedItems = new string[3];
    private int currentSlot = 0;

    [System.Serializable]
    public class Recipe
    {
        public string name;
        public string[] ingredients;
        public GameObject prefab;

        public Recipe(string name, string[] ingredients, GameObject prefab)
        {
            this.name = name;
            this.ingredients = ingredients;
            this.prefab = prefab;
        }

        public bool Matches(string[] input)
        {
            if (input.Length != ingredients.Length) return false;
            var sortedInput = input.OrderBy(x => x).ToArray();
            var sortedIngredients = ingredients.OrderBy(x => x).ToArray();

            for (int i = 0; i < sortedInput.Length; i++)
            {
                if (sortedInput[i] != sortedIngredients[i]) return false;
            }
            return true;
        }
    }

    private List<Recipe> recipes = new List<Recipe>();

    void Start()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnItemClick(index));
        }

        // Tarifleri ekle
        recipes.Add(new Recipe("Americano", new[] { "A", "B", "B" }, recipePrefabs[0]));
        recipes.Add(new Recipe("Cappuccino", new[] { "A", "C", "C" }, recipePrefabs[1]));
        recipes.Add(new Recipe("Latte", new[] { "A", "C", "H" }, recipePrefabs[2]));
        recipes.Add(new Recipe("Mocha", new[] { "A", "C", "E" }, recipePrefabs[3]));
        recipes.Add(new Recipe("Iced Coffee Float", new[] { "A", "C", "H" }, recipePrefabs[4]));
        recipes.Add(new Recipe("Chai Coffee", new[] { "A", "C", "M" }, recipePrefabs[5]));
        recipes.Add(new Recipe("Sweet Coffee", new[] { "A", "C", "F" }, recipePrefabs[6]));
        recipes.Add(new Recipe("Iced Latte", new[] { "A", "C", "G" }, recipePrefabs[7]));
        recipes.Add(new Recipe("Vanilla Latte", new[] { "A", "C", "J" }, recipePrefabs[8]));
        recipes.Add(new Recipe("Hazelnut Coffee", new[] { "A", "C", "L" }, recipePrefabs[9]));
        recipes.Add(new Recipe("Sıcak Çikolata", new[] { "E", "E", "C" }, recipePrefabs[10]));
        recipes.Add(new Recipe("Vanilyalı Sıcak Çikolata", new[] { "E", "C", "J" }, recipePrefabs[11]));
        recipes.Add(new Recipe("Naneli Sıcak Çikolata", new[] { "E", "C", "R" }, recipePrefabs[12]));
        recipes.Add(new Recipe("Soğuk Çikolata", new[] { "E", "C", "G" }, recipePrefabs[13]));
        recipes.Add(new Recipe("Milkshake (Çikolatalı)", new[] { "E", "C", "H" }, recipePrefabs[14]));
        recipes.Add(new Recipe("Muzlu Milkshake", new[] { "E", "C", "Y" }, recipePrefabs[15]));
        recipes.Add(new Recipe("Reese's Shake", new[] { "E", "C", "K" }, recipePrefabs[16]));
        recipes.Add(new Recipe("Karamelli Sıcak Çikolata", new[] { "E", "C", "N" }, recipePrefabs[17]));
        recipes.Add(new Recipe("Bubble Tea (Classic)", new[] { "O", "C", "d" }, recipePrefabs[18]));
        recipes.Add(new Recipe("Limonlu Soğuk Çay", new[] { "O", "P", "G" }, recipePrefabs[19]));
        recipes.Add(new Recipe("Şeftalili Soğuk Çay", new[] { "O", "T", "G" }, recipePrefabs[20]));
        recipes.Add(new Recipe("Ballı Limonlu Çay", new[] { "O", "c", "P" }, recipePrefabs[21]));
        recipes.Add(new Recipe("Chai Tea Latte", new[] { "O", "C", "I" }, recipePrefabs[22]));
        recipes.Add(new Recipe("Detoks Çayı", new[] { "O", "R", "P" }, recipePrefabs[23]));
        recipes.Add(new Recipe("Çilekli Soğuk Çay", new[] { "O", "V", "G" }, recipePrefabs[24]));
        recipes.Add(new Recipe("Hindistan Cevizli Bubble Tea", new[] { "O", "b", "C" }, recipePrefabs[25]));
        recipes.Add(new Recipe("Mango Smoothie", new[] { "S", "C", "G" }, recipePrefabs[26]));
        recipes.Add(new Recipe("Strawberry-Banana Smoothie", new[] { "V", "Y", "C" }, recipePrefabs[27]));
        recipes.Add(new Recipe("Berry Smoothie", new[] { "Z", "a", "G" }, recipePrefabs[28]));
        recipes.Add(new Recipe("Limonata", new[] { "P", "R", "G" }, recipePrefabs[29]));
    }

    void OnItemClick(int index)
    {
        if (currentSlot >= 3) return;

        if (index > 1)
        {
            if (customerManager.GetMoney() >= 30)
            {
                customerManager.AddMoney(-30);
                Debug.Log($"💸 {itemNames[index]} alındı, 30 Coin düşüldü. Kalan: {customerManager.GetMoney()}");
            }
            else
            {
                Debug.LogWarning("❌ Yeterli para yok, malzeme alınamadı.");
                return;
            }
        }

        selectionSlots[currentSlot].sprite = itemSprites[index];
        selectedItems[currentSlot] = itemNames[index];
        selectionSlots[currentSlot].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        currentSlot++;
    }

    public void TryCraft()
    {
        Debug.Log("🧪 Üretim denemesi başlıyor");

        foreach (Recipe recipe in recipes)
        {
            if (recipe.Matches(selectedItems))
            {
                if (recipe.prefab != null)
                {
                    GameObject go = Instantiate(recipe.prefab, spawnPoint);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;

                    Debug.Log("☕ Tarif bulundu ve üretildi: " + recipe.name);

                    if (correctRecipeSound != null && sfxAudioSource != null)
                    {
                        sfxAudioSource.clip = correctRecipeSound;
                        sfxAudioSource.Play();
                        StartCoroutine(StopSoundAfterSeconds(1.5f));
                    }

                    bool wasAlreadyUnlocked = SaveSystem.IsRecipeUnlocked(recipe.name);
                    if (!wasAlreadyUnlocked)
                    {
                        SaveSystem.UnlockRecipe(recipe.name);
                        Debug.Log($"🆕 Yeni tarif açıldı: {recipe.name}");

                        if (recipeBookManager != null)
                        {
                            recipeBookManager.DisplayRecipes();
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("⚠ Tarif bulundu ama prefab atanmadı: " + recipe.name);
                }

                ResetSelection();
                return;
            }
        }

        Debug.Log("❌ Tarif bulunamadı.");

        if (wrongRecipeSound != null && sfxAudioSource != null)
        {
            sfxAudioSource.PlayOneShot(wrongRecipeSound);
        }

        StartCoroutine(ShakeScreen(0.2f, 0.3f));
        ResetSelection();
    }

    IEnumerator StopSoundAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sfxAudioSource.Stop();
    }

    IEnumerator ShakeScreen(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }

    void ResetSelection()
    {
        for (int i = 0; i < selectionSlots.Length; i++)
        {
            selectionSlots[i].sprite = emptySprite;
            selectionSlots[i].GetComponent<RectTransform>().localScale = Vector3.one;
        }

        selectedItems = new string[3];
        currentSlot = 0;
    }
}
