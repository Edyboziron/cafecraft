using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public Button button1; // Panel1'deki buton

    void Start()
    {
        // Baþlangýçta panel1 açýk, panel2 kapalý
        panel1.SetActive(true);
        panel2.SetActive(false);

        // button1'e týklama iþlevi baðla
        button1.onClick.AddListener(SwitchToPanel2);
    }

    void SwitchToPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
}
