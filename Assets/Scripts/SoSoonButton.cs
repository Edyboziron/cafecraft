using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SoSoonButton : MonoBehaviour
{
    public TMP_Text messageText; // Text alaný (baþta boþ olacak)

    private void Start()
    {
        messageText.gameObject.SetActive(false); // Baþta görünmesin
    }

    public void ShowMessage()
    {
        StartCoroutine(ShowTextCoroutine());
    }

    private IEnumerator ShowTextCoroutine()
    {
        messageText.text = "So Soon";
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f); // 2 saniye bekle

        messageText.gameObject.SetActive(false); // Yazýyý gizle
    }
}
