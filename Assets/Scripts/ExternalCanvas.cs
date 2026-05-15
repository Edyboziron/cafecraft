using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExternalCoffeeDropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Drop Area Settings")]
    public Customer customer;

    [Header("Visual Feedback")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color dropColor = Color.green;

    private Image backgroundImage;
    private bool isHovering = false;

    private void Awake()
    {
        // Background image component'ini al veya oluþtur
        backgroundImage = GetComponent<Image>();
        if (backgroundImage == null)
        {
            backgroundImage = gameObject.AddComponent<Image>();
            backgroundImage.color = new Color(1, 1, 1, 0.1f); // Þeffaf beyaz
        }

        // Raycast Target olduðundan emin ol
        backgroundImage.raycastTarget = true;
    }

    private void Start()
    {
        // Customer referansý yoksa bul
        if (customer == null)
        {
            customer = GetComponentInParent<Customer>();
            if (customer == null)
            {
                Debug.LogWarning("Customer referansý bulunamadý: " + gameObject.name);
            }
        }

        SetVisualState(normalColor);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop çaðrýldý - Obje: " + eventData.pointerDrag?.name + " Drop Area: " + gameObject.name);

        if (customer == null)
        {
            Debug.LogWarning("Customer referansý yok! Drop Area: " + gameObject.name);
            return;
        }

        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null)
        {
            Debug.LogWarning("Dropped object null!");
            return;
        }

        // Kahve objesi mi kontrol et
        CoffeeItem coffee = droppedObj.GetComponent<CoffeeItem>();
        if (coffee == null)
        {
            Debug.LogWarning("Dropped object is not a coffee item!");
            return;
        }

        // Visual feedback
        SetVisualState(dropColor);

        // Sipariþ teslim et
        ProcessDrop(droppedObj);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Sadece drag iþlemi sýrasýnda hover efekti göster
        if (eventData.pointerDrag != null)
        {
            Debug.Log("Drop area'ya girdi: " + eventData.pointerDrag.name + " -> " + gameObject.name);
            isHovering = true;
            SetVisualState(hoverColor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHovering)
        {
            Debug.Log("Drop area'dan çýktý: " + eventData.pointerDrag?.name + " -> " + gameObject.name);
            isHovering = false;
            SetVisualState(normalColor);
        }
    }

    public void ProcessDrop(GameObject droppedObj)
    {
        if (customer == null)
        {
            Debug.LogWarning("Customer referansý yok! ProcessDrop: " + gameObject.name);
            return;
        }

        Debug.Log("ProcessDrop çaðrýldý: " + droppedObj.name + " -> " + customer.name);

        // Sipariþ teslim et
        customer.ReceiveOrder(droppedObj);

        // Visual state'i resetle
        isHovering = false;
        SetVisualState(normalColor);
    }

    private void SetVisualState(Color color)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = color;
        }
    }

    // Debug için
    private void OnValidate()
    {
        if (customer == null)
        {
            customer = GetComponentInParent<Customer>();
        }
    }
}