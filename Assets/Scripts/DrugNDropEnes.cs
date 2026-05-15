using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropWorld : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private TrashCanUI trashCan;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        trashCan = FindObjectOfType<TrashCanUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag başladı: " + gameObject.name);
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag başladı: " + gameObject.name);
        canvasGroup.blocksRaycasts = true;

        // Önce çöp kutusu kontrolü
        if (trashCan != null && trashCan.IsOverlapping(rectTransform))
        {
            Debug.Log("Kahve çöpe atıldı.");
            trashCan.SetHoveredVisual();
            Destroy(this.gameObject);
            return;
        }

        // UI Drop kontrolü - Unity'nin built-in sistemi
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        Debug.Log("UI Raycast sonuçları: " + results.Count + " obje bulundu");

        foreach (var result in results)
        {
            Debug.Log("Raycast bulduğu obje: " + result.gameObject.name);

            // Drop area kontrolü
            ExternalCoffeeDropArea dropArea = result.gameObject.GetComponent<ExternalCoffeeDropArea>();
            if (dropArea != null)
            {
                Debug.Log("DropArea bulundu! Customer: " + (dropArea.customer != null ? dropArea.customer.name : "NULL"));

                if (dropArea.customer != null)
                {
                    Debug.Log("Kahve teslim ediliyor: " + this.gameObject.name + " -> " + dropArea.customer.name);
                    dropArea.ProcessDrop(this.gameObject);
                    return; // Başarılı teslim
                }
                else
                {
                    Debug.LogWarning("DropArea var ama Customer null!");
                }
            }
        }

        // 3D Dünya kontrolü (gerekirse)
        if (TryDrop3D(eventData))
        {
            return;
        }

        // Hiçbir drop area bulunamadı
        Debug.Log("Hiçbir drop area bulunamadı, orijinal pozisyona dönüyor");
        rectTransform.anchoredPosition = originalPosition;
    }

    private bool TryDrop3D(PointerEventData eventData)
    {
        Camera raycastCamera = GetRaycastCamera(eventData);

        if (raycastCamera != null)
        {
            Debug.Log("3D Raycast kamera bulundu: " + raycastCamera.name);
            Ray ray = raycastCamera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Debug.Log("3D Raycast hit: " + hit.collider.name);
                ExternalCoffeeDropArea dropArea = hit.collider.GetComponent<ExternalCoffeeDropArea>();

                if (dropArea != null && dropArea.customer != null)
                {
                    Debug.Log("3D DropArea bulundu: " + hit.collider.name);
                    dropArea.ProcessDrop(this.gameObject);
                    return true;
                }
            }
            else
            {
                Debug.Log("3D Raycast hiçbir şey bulamadı");
            }
        }
        else
        {
            Debug.LogWarning("Raycast kamerası bulunamadı!");
        }

        return false;
    }

    private Camera GetRaycastCamera(PointerEventData eventData)
    {
        // Önce eventData'dan kamerayı al
        if (eventData.pressEventCamera != null)
            return eventData.pressEventCamera;

        // Canvas'ın render mode'una göre kamera bul
        if (canvas != null)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera != null)
                return canvas.worldCamera;

            if (canvas.renderMode == RenderMode.WorldSpace && canvas.worldCamera != null)
                return canvas.worldCamera;
        }

        // Son çare olarak ana kamerayı kullan
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
            return mainCamera;

        // Hiçbir kamera bulunamadıysa sahne içindeki ilk kamerayı bul
        Camera firstCamera = FindObjectOfType<Camera>();
        if (firstCamera != null)
            return firstCamera;

        Debug.LogWarning("Raycast için uygun kamera bulunamadı!");
        return null;
    }
}