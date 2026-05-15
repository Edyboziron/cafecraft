using UnityEngine;
using UnityEngine.UI;

public class TrashCanUI : MonoBehaviour
{
    public Image trashCanImage;
    public Sprite normalSprite;
    public Sprite hoverSprite;

    public void SetHoveredVisual()
    {
        if (trashCanImage != null && hoverSprite != null)
        {
            trashCanImage.sprite = hoverSprite;
            Invoke(nameof(ResetVisual), 2f); // 2 saniye sonra eski sprite'a dön
        }
    }

    public void ResetVisual()
    {
        if (trashCanImage != null && normalSprite != null)
            trashCanImage.sprite = normalSprite;
    }

    public bool IsOverlapping(RectTransform draggedRect)
    {
        RectTransform thisRect = GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(
            thisRect,
            RectTransformUtility.WorldToScreenPoint(null, draggedRect.position),
            null
        );
    }
}
