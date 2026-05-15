using UnityEngine;
using UnityEngine.UI;

public class MusicButtonConnector : MonoBehaviour
{
    public Button musicButton;

    void Start()
    {
        if (musicButton != null && MusicManager.Instance != null)
        {
            musicButton.onClick.RemoveAllListeners();
            musicButton.onClick.AddListener(MusicManager.Instance.ToggleMusic);
        }
    }
}
