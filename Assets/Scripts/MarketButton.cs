using UnityEngine;
using UnityEngine.UI;

public class MarketButton : MonoBehaviour
{
    public Button targetButton;
    public int objectId;        // Örneğin: 0, 1, 2 (hangi obje)
    public int spriteIndex;     // Örneğin: 0, 1, 2, 3, 4 (hangi sprite)
    public int price = 500;

    private CustomerManager customerManager;

    private void Awake()
    {
        customerManager = FindFirstObjectByType<CustomerManager>();

        if (targetButton == null)
            Debug.LogError("Button atanmadı!");
        if (customerManager == null)
            Debug.LogError("CustomerManager bulunamadı!");
    }

    private void Update()
    {
        if (customerManager == null) return;

        bool alreadyPaid = SaveSystem.HasPaidOnce(GetUniqueKey());
        int currentMoney = customerManager.GetMoney();

        if (alreadyPaid)
        {
            if (!targetButton.interactable)
                targetButton.interactable = true;
        }
        else
        {
            targetButton.interactable = (currentMoney >= price);
        }
    }

    public void OnButtonClicked()
    {
        int key = GetUniqueKey();
        bool alreadyPaid = SaveSystem.HasPaidOnce(key);

        if (!alreadyPaid && customerManager != null && customerManager.GetMoney() >= price)
        {
            customerManager.AddMoney(-price);
            SaveSystem.SavePaymentStatus(key, true);
            targetButton.interactable = true;

            Debug.Log($"✅ Ödeme yapıldı ve buton aktifleştirildi. ObjectId: {objectId}, Sprite: {spriteIndex}");
        }
        else if (alreadyPaid)
        {
            Debug.Log("✅ Ödeme zaten yapılmış.");
        }
        else
        {
            Debug.LogWarning("❌ Yetersiz para.");
        }
    }

    // Sprite başına farklı anahtar üret
    private int GetUniqueKey()
    {
        return objectId * 100 + spriteIndex;
        // Örn: objectId 2, spriteIndex 3 → 203
    }
}
