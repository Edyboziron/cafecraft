using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource sharedAudioSource;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (clickSound != null && sharedAudioSource != null)
        {
            sharedAudioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("🔇 AudioSource veya ClickSound eksik!");
        }
    }
}
