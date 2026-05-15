using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    public CustomerManager manager;

    [Header("Sipariş Sistemi")]
    public GameObject currentOrderPrefab;
    public Text orderText;
    public Image orderImage;
    public Transform externalCoffeeCanvas;

    [Header("Tepki Sistemi")]
    public Text reactionText;

    [Header("Görsel Tepkiler")]
    public SpriteRenderer spriteRenderer;
    public Sprite happySprite;
    public Sprite angrySprite;
    public Sprite idleSprite;

    [Header("Canvas Yönü")]
    public Transform canvasTransform;

    [HideInInspector] public bool hasReceivedOrder = false;
    private static bool customerExists = false;

    public PlayerLevelSystem playerLevelSystem;
    public static int money = 0;

    private GameObject currentDropArea;

   

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        if (playerLevelSystem == null)
            playerLevelSystem = FindFirstObjectByType<PlayerLevelSystem>();

        if (spriteRenderer != null && idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }

    public void SetOrder(GameObject order)
    {
        currentOrderPrefab = order;

        CoffeeItem coffee = order.GetComponent<CoffeeItem>();
        if (coffee != null)
        {
            if (orderText != null)
                orderText.text = coffee.coffeeName;

            if (orderImage != null && coffee.coffeeIcon != null)
            {
                orderImage.sprite = coffee.coffeeIcon;
                orderImage.enabled = true;
            }
        }

        CreateOrderVisual(order);
    }

    private void CreateOrderVisual(GameObject order)
    {
        if (currentDropArea != null)
        {
            Destroy(currentDropArea);
        }

        GameObject coffeeVisual = Instantiate(order);
        coffeeVisual.name = order.name + "_OrderVisual_" + GetInstanceID();

        if (externalCoffeeCanvas != null && externalCoffeeCanvas.gameObject.scene.isLoaded)
        {
            coffeeVisual.transform.SetParent(externalCoffeeCanvas, false);
            coffeeVisual.transform.localPosition = Vector3.zero;
            coffeeVisual.transform.localScale = Vector3.one;

            ExternalCoffeeDropArea dropArea = coffeeVisual.GetComponent<ExternalCoffeeDropArea>();
            if (dropArea == null)
                dropArea = coffeeVisual.AddComponent<ExternalCoffeeDropArea>();
            dropArea.customer = this;

            DragNDropWorld dragComponent = coffeeVisual.GetComponent<DragNDropWorld>();
            if (dragComponent != null)
                Destroy(dragComponent);

            CanvasGroup canvasGroup = coffeeVisual.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = coffeeVisual.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = true;

            Image image = coffeeVisual.GetComponent<Image>();
            if (image != null)
                image.raycastTarget = true;

            currentDropArea = coffeeVisual;
        }
        else
        {
            Destroy(coffeeVisual);
        }
    }

    public void WalkToSpawn(Vector3 spawnTarget)
    {
        Sequence entrance = DOTween.Sequence();
        entrance.Append(transform.DOMove(spawnTarget, 0.8f).SetEase(Ease.Linear));
        entrance.Join(transform.DOScale(1f, 3f).SetEase(Ease.OutBack));
    }

    public void ReceiveOrder(GameObject givenOrder)
    {
        if (hasReceivedOrder)
        {
            Debug.Log("Bu müşteri zaten sipariş aldı!");
            return;
        }

        if (currentOrderPrefab == null)
        {
            Debug.LogWarning("Customer'un beklediği sipariş yok!");
            return;
        }

        var expected = currentOrderPrefab.GetComponent<CoffeeItem>();
        var given = givenOrder.GetComponent<CoffeeItem>();

        bool correct = expected != null && given != null && expected.coffeeName == given.coffeeName;

        React(correct);
        hasReceivedOrder = true;

        if (playerLevelSystem != null)
            playerLevelSystem.GiveCoffee(correct);

        if (correct)
            manager.AddMoney(150);
        else
            manager.AddMoney(30);

        Destroy(givenOrder);

        if (currentDropArea != null)
        {
            Destroy(currentDropArea);
            currentDropArea = null;
        }
    }

    public void React(bool correct)
    {
        if (reactionText == null) return;

        if (correct)
        {
            reactionText.text = "😄 Thank you!";
            reactionText.color = Color.green;
            if (spriteRenderer != null && happySprite != null)
                spriteRenderer.sprite = happySprite;
        }
        else
        {
            reactionText.text = "😠 that's not what I wanted.!";
            reactionText.color = Color.red;
            if (spriteRenderer != null && angrySprite != null)
                spriteRenderer.sprite = angrySprite;
        }

        Invoke(nameof(AfterReaction), 2f);
    }

    void AfterReaction()
    {
        ClearReaction();

        if (currentDropArea != null)
        {
            Destroy(currentDropArea);
            currentDropArea = null;
        }

        if (manager != null)
        {
            manager.CustomerLeft(false, false);
            manager.Invoke("SpawnCustomer", 1f);
        }

        customerExists = false;
        Destroy(gameObject);
    }

    void ClearReaction()
    {
        if (reactionText != null)
            reactionText.text = "";

        if (spriteRenderer != null && idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }

    void OnMouseDown()
    {
        Leave();
    }

    public void Leave()
    {
        if (hasReceivedOrder)
        {
            if (orderText != null) orderText.text = "";
            if (reactionText != null) reactionText.text = "";
            if (manager != null) manager.CustomerLeft(false, true);

            if (currentDropArea != null)
            {
                Destroy(currentDropArea);
                currentDropArea = null;
            }

            customerExists = false;
            Destroy(gameObject);
            return;
        }

        CustomerManager.ExitPoint availablePoint = null;
        foreach (var point in manager.exitPoints)
        {
            if (!point.isOccupied)
            {
                availablePoint = point;
                break;
            }
        }

        if (availablePoint == null)
        {
            if (reactionText != null)
            {
                reactionText.text = "😓 Yer yok!";
                reactionText.color = Color.yellow;
            }
            Invoke(nameof(ClearReaction), 2f);
            return;
        }

        availablePoint.isOccupied = true;

        Sequence sit = DOTween.Sequence();
        sit.Append(transform.DOMove(availablePoint.point.position, 0.75f).SetEase(Ease.InOutSine));
        sit.Join(transform.DOScale(0.4f, 0.2f).SetEase(Ease.InOutQuad));

        if (manager != null)
            manager.CustomerLeft(true, true);
    }

    public void LeaveSilently()
    {
        if (orderText != null) orderText.text = "";
        if (reactionText != null) reactionText.text = "";

        if (currentDropArea != null)
        {
            Destroy(currentDropArea);
            currentDropArea = null;
        }

        customerExists = false;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (currentDropArea != null)
        {
            Destroy(currentDropArea);
        }
    }
}
