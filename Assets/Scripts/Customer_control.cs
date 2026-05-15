using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CustomerManager : MonoBehaviour
{
    [Serializable]
    public class ExitPoint
    {
        public Transform point;
        public bool isOccupied;
    }

    public static event Action<int> OnMoneyChanged;

    [Header("Müşteri ve Sipariş Ayarları")]
    public GameObject[] customerPrefabs;
    public GameObject[] possibleOrders;
    public Transform spawnPoint;
    public Transform doorPoint;
    public float spawnDelay = 2f;
    public ExitPoint[] exitPoints;

    [Header("Seviye ve Yönetim")]
    public LevelManager levelManager;
    public PlayerLevelSystem playerLevelSystem;

    private bool isCustomerPresent = false;
    private Customer currentCustomer;

    private int money;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            Debug.Log("🎮 Sahne yüklendi, müşteri spawn kontrolü yapılıyor.");
            LoadMoneyAndLevel();

            if (currentCustomer == null)
            {
                isCustomerPresent = false;
                SpawnCustomerWithDelay();
            }
            else
            {
                Debug.Log("Mevcut müşteri sahnede kaldı, yeni müşteri spawn edilmedi.");
                isCustomerPresent = true;
            }
        }
    }

    private void Awake()
    {
        money = SaveSystem.LoadMoney();

        if (playerLevelSystem != null)
            playerLevelSystem.level = SaveSystem.LoadLevel();
    }

    void Start()
    {
        OnMoneyChanged?.Invoke(money);

        if (!isCustomerPresent && currentCustomer == null)
        {
            Invoke(nameof(SpawnCustomer), spawnDelay);
            isCustomerPresent = true;
        }

        Debug.Log($"💾 Başlangıç Parası: {money} | Seviye: {(playerLevelSystem != null ? playerLevelSystem.level : 1)}");
    }

    void LoadMoneyAndLevel()
    {
        money = SaveSystem.LoadMoney();

        if (playerLevelSystem != null)
            playerLevelSystem.level = SaveSystem.LoadLevel();

        OnMoneyChanged?.Invoke(money);

        Debug.Log($"💾 Paralar yüklendi: {money} | Seviye: {(playerLevelSystem != null ? playerLevelSystem.level : 1)}");
    }

    public void SpawnCustomerWithDelay()
    {
        if (!isCustomerPresent)
        {
            Invoke(nameof(SpawnCustomer), spawnDelay);
            isCustomerPresent = true;
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefabs == null || customerPrefabs.Length == 0)
        {
            Debug.LogWarning("Müşteri prefabları atanmamış.");
            return;
        }

        if (possibleOrders == null || possibleOrders.Length == 0)
        {
            Debug.LogWarning("Sipariş prefabları atanmamış.");
            return;
        }

        int index = UnityEngine.Random.Range(0, customerPrefabs.Length);
        GameObject chosenPrefab = customerPrefabs[index];

        // Burada direkt Instantiate değil, önce sahnede kalıcı Customer var mı kontrolü yapıyoruz.
        if (currentCustomer != null)
        {
            Debug.Log("Müşteri zaten sahnede mevcut, spawn edilmedi.");
            return;
        }

        GameObject customerGO = Instantiate(chosenPrefab, doorPoint.position, Quaternion.identity);

        currentCustomer = customerGO.GetComponent<Customer>();

        if (currentCustomer == null)
        {
            Debug.LogError("Müşteri prefabında Customer scripti yok!");
            return;
        }

        currentCustomer.manager = this;

        GameObject chosenOrder = possibleOrders[UnityEngine.Random.Range(0, possibleOrders.Length)];

        currentCustomer.SetOrder(chosenOrder);

        Debug.Log("☕ Müşteri geldi: " + chosenOrder.name);

        currentCustomer.WalkToSpawn(spawnPoint.position);
    }

    public void CustomerLeft(bool seated, bool manuallySeated)
    {
        if (!seated || manuallySeated)
        {
            isCustomerPresent = false;
            currentCustomer = null;
            SpawnCustomerWithDelay();
        }
    }

    public void OnOrderResult(bool correct)
    {
        if (correct)
        {
            money += 150;
            levelManager?.CorrectOrder();
            playerLevelSystem?.GiveCoffee(true);
        }
        else
        {
            money += 30;
            levelManager?.WrongOrder();
            playerLevelSystem?.GiveCoffee(false);
        }

        SaveSystem.SaveMoney(money);

        if (playerLevelSystem != null)
            SaveSystem.SaveLevel(playerLevelSystem.level);

        OnMoneyChanged?.Invoke(money);

        Debug.Log($"💾 Kaydedilen Para: {money} | Seviye: {(playerLevelSystem != null ? playerLevelSystem.level : -1)}");
    }

    public int GetMoney() => money;

    public void AddMoney(int amount)
    {
        money += amount;
        if (money < 0) money = 0;

        SaveSystem.SaveMoney(money);
        OnMoneyChanged?.Invoke(money);

        Debug.Log("💰 Para güncellendi: " + money);
    }

    public int GetLevel() => playerLevelSystem != null ? playerLevelSystem.level : 1;
}
