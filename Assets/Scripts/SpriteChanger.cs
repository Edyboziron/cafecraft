using UnityEngine;

public class SpriteShop : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int objectId;

    private bool[] isPurchased;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        isPurchased = new bool[sprites.Length];

        int savedIndex = SaveSystem.LoadSpriteIndex(objectId);
        ApplySprite(savedIndex);
    }

    public void TrySelectSprite(int index)
    {
        if (index < 0 || index >= sprites.Length) return;

        isPurchased[index] = true;
        ApplySprite(index);
        SaveSystem.SaveSpriteIndex(objectId, index);
    }

    private void ApplySprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }

    // Fonksiyonlar (butonlara bağlayabilmen için)
    public void SelectSprite0() => TrySelectSprite(0);
    public void SelectSprite1() => TrySelectSprite(1);
    public void SelectSprite2() => TrySelectSprite(2);
    public void SelectSprite3() => TrySelectSprite(3);
    public void SelectSprite4() => TrySelectSprite(4);
}
