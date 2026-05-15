using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    [Header("Hedef UI Image")]
    public Image targetImage;

    [Header("Kullanýlacak Spritelar")]
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    public void ChangeToSprite1()
    {
        if (targetImage != null && sprite1 != null)
        {
            targetImage.sprite = sprite1;
        }
    }

    public void ChangeToSprite2()
    {
        if (targetImage != null && sprite2 != null)
        {
            targetImage.sprite = sprite2;
        }
    }

    public void ChangeToSprite3()
    {
        if (targetImage != null && sprite3 != null)
        {
            targetImage.sprite = sprite3;
        }
    }
}
