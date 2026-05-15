using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [Header("Panel Referansı")]
    public GameObject popupPanel;

    private bool isOpen = false;
    public bool allowDrag = true; // 👈 Drag izni burada

    void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Popup panel atanmamış!");
        }

        allowDrag = true;
    }

    public void TogglePopup()
    {
        if (popupPanel == null)
        {
            Debug.LogWarning("Popup panel atanmamış!");
            return;
        }

        isOpen = !isOpen;
        popupPanel.SetActive(isOpen);
        allowDrag = !isOpen; // 👈 Drag izni panel durumuna göre
        Debug.Log("Popup durumu: " + (isOpen ? "Açıldı" : "Kapandı"));
    }

    public void OpenPopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
            isOpen = true;
            allowDrag = false; // 👈 Drag kapalı
            Debug.Log("Popup açıldı.");
        }
    }

    public void ClosePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            isOpen = false;
            allowDrag = true; // 👈 Drag açık
            Debug.Log("Popup kapatıldı.");
        }
    }

    // Eğer başka yerlerden okunacaksa hâlâ kullanabilirsin:
    public bool IsPopupOpen()
    {
        return isOpen;
    }
}
