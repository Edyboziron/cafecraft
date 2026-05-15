using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private InterstitialAd interstitialAd;
    private int clickCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob başlatıldı.");
            LoadInterstitial();
        });
    }

    public void OnMyButtonClick() // ← Bunu butonun OnClick() kısmına bağla
    {
        clickCount++;
        Debug.Log($"Butona {clickCount}. kez basıldı.");

        if (clickCount >= 10)
        {
            ShowInterstitial();
            clickCount = 0;
        }

        // Butona tıklanınca yapılacak başka şeyler varsa buraya yazabilirsin
        // Örneğin: Ses çal, skor artır, vs.
    }

    private void LoadInterstitial()
    {
        var adRequest = new AdRequest();

        // Buraya kendi Ad Unit ID'ni yazdık (seninki bu)
        string adUnitId = "ca-app-pub-2236184354051308/2722025317";

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Interstitial reklam yüklenemedi: " + error);
                    return;
                }

                interstitialAd = ad;
                Debug.Log("Interstitial reklam yüklendi.");

                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Reklam kapatıldı, tekrar yükleniyor.");
                    LoadInterstitial(); // Reklam kapanınca yeniden yükle
                };

                interstitialAd.OnAdFullScreenContentFailed += (AdError adError) =>
                {
                    Debug.LogError("Reklam gösterme başarısız: " + adError.GetMessage());
                };
            });
    }

    private void ShowInterstitial()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Reklam gösteriliyor.");
            interstitialAd.Show();
            interstitialAd = null; // Sonraki gösterim için null'a çek
        }
        else
        {
            Debug.Log("Reklam henüz hazır değil. Yeniden yükleniyor.");
            LoadInterstitial();
        }
    }
}
