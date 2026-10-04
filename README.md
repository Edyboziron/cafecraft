# ☕ Cafe Craft

[![Unity](https://img.shields.io/badge/Unity-6000.3+-black?logo=unity&logoColor=white)](https://unity.com/)
[![Render Pipeline](https://img.shields.io/badge/Render%20Pipeline-URP%202D-blue)](https://unity.com/features/srp/universal-render-pipeline)
[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20Android-green)](https://unity.com/)

**Cafe Craft**, Game Jam kapsamında geliştirilmiş, oyuncuların kendi kafelerini işlettikleri, gelen müşterilerin siparişlerine göre özel kahveler hazırladıkları ve kazandıkları paralarla kafelerini geliştirdikleri eğlenceli bir 2D kafe yönetim ve simülasyon oyunudur.

---

## 📖 İçindekiler / Table of Contents
- [☕ Oyun Hakkında / About the Game](#-oyun-hakkında--about-the-game)
- [✨ Özellikler / Features](#-özellikler--features)
- [🎮 Oynanış ve Mekanikler / Gameplay & Mechanics](#-oynanış-ve-mekanikler--gameplay--mechanics)
- [🛠️ Teknolojiler / Tech Stack](#️-teknolojiler--tech-stack)
- [📁 Proje Mimarisi / Project Architecture](#-proje-mimarisi--project-architecture)
- [🚀 Kurulum ve Çalıştırma / Installation & Setup](#-kurulum-ve-çalıştırma--installation--setup)
- [👥 Ekip ve Katkıda Bulunanlar / Credits & Team](#-ekip-ve-katkıda-bulunanlar--credits--team)

---

## ☕ Oyun Hakkında / About the Game

Cafe Craft'ta amacınız kafe kapısından içeri giren farklı karakterdeki müşterileri güler yüzle karşılamak, istedikleri özel tariflere sahip kahveleri kahve makinesinde doğru malzemeleri birleştirerek hazırlamak ve zamanında teslim etmektir. Müşterilerin memnuniyetine göre bahşiş ve para kazanabilir, bu kazançlarla yeni tariflerin kilitlerini açabilir, dükkanınızı görsel olarak dekore edebilir ve seviyenizi yükseltebilirsiniz.

---

## ✨ Özellikler / Features

- 👥 **Dinamik Müşteri Sistemi:** Farklı karakterler (Pınar, Saliha, Yağmur, Şevket vb.), sipariş bekleme süreleri ve ruh hallerine göre değişen görsel tepkiler (Mutlu, Tebessüm, Üzgün, Sinirli).
- ☕ **Kahve Hazırlama & Crafting:** Espresso, Latte, Mocha ve daha birçok tarifi doğru malzemelerle ve adımlarla hazırlama deneyimi.
- 📜 **Tarif Kitabı (Recipe Book):** Hangi kahvenin hangi bileşenlerden oluştuğunu gösteren interaktif rehber.
- 🏪 **Gelişmiş Market & Dekorasyon Sistemi:** Kazanılan paralarla tezgah, zemin, çöp kovası gibi kafe ögelerini ve yeni kahve malzemelerini özelleştirme.
- 📈 **Seviye ve İlerleme (Progression):** Tamamlanan başarılı siparişlerle seviye atlama ve yeni tarifleri devreye alma.
- 💾 **Kalıcı Kayıt Sistemi (Save System):** Oyuncunun kazandığı para, ulaştığı seviye ve satın aldığı geliştirmeleri saklayan PlayerPrefs entegrasyonu.
- 🎵 **Ses ve Müzik Yönetimi:** Ses seviyesi ayarları ve ambiyansa uygun müzikler.

---

## 🎮 Oynanış ve Mekanikler / Gameplay & Mechanics

1. **Siparişi Alın:** Müşteri kafeye geldiğinde sipariş biletini inceleyin.
2. **Kahve Makinesine Geçin:** İlgili butona tıklayarak kahve hazırlama ekranına geçiş yapın.
3. **Malzemeleri Birleştirin:** Bardağa doğru malzemeleri sırasıyla ekleyin ve karıştırın.
4. **Müşteriye Sunun:** Hazırladığınız içeceği sürükleyip müşteriye servis edin.
5. **Bahşişi Toplayın & Kafeyi Büyütün:** Kazandığınız paralarla marketten yeni geliştirmeler satın alın!

---

## 🛠️ Teknolojiler / Tech Stack

- **Oyun Motoru:** Unity 6 (6000.x)
- **Render Pipeline:** Universal Render Pipeline (URP 2D)
- **Programlama Dili:** C#
- **Animasyon & Tweening:** DOTween
- **UI:** Unity UI (Canvas tabanlı duyarlı arayüz)
- **Hedef Platformlar:** Android & PC

---

## 📁 Proje Mimarisi / Project Architecture

```plaintext
Assets/
├── Scenes/                  # Oyun sahneleri
│   ├── MainMenu.unity       # Ana menü
│   ├── customer_screen.unity# Müşteri ve kafe alanı
│   ├── Coffe_machine.unity  # Kahve hazırlama ekranı
│   ├── Options.unity        # Ayarlar ekranı
│   └── Credit.unity         # Yapımcılar / Jenerik
├── Scripts/                 # C# oyun mantığı ve mekanikler
│   ├── Customer_control.cs  # Müşteri spawn ve para yönetimi
│   ├── Customer.cs          # Müşteri davranışları ve tepkileri
│   ├── Crafting.cs          # Kahve hazırlama mantığı
│   ├── BuyIngredient.cs     # Malzeme satın alma sistemi
│   ├── LevelManager.cs      # Seviye ve kilit açma yönetimi
│   ├── MarketButton.cs      # Market satın alma kontrolleri
│   ├── SaveSystem.cs        # Veri kaydetme ve yükleme
│   └── MusicManager.cs      # Müzik ve ses yöneticisi
└── sprites/                 # 2D çizimler, karakterler ve UI varlıkları
```

---

## 🚀 Kurulum ve Çalıştırma / Installation & Setup

1. Depoyu bilgisayarınıza klonlayın:
   ```bash
   git clone https://github.com/Edyboziron/cafecraft.git
   ```
2. **Unity Hub** uygulamasını açın ve projeyi `Unity 6000.3.x` veya uyumlu bir sürümle ekleyin.
3. `Assets/Scenes/MainMenu.unity` sahnesini açın.
4. **Play** butonuna basarak oyunu çalıştırın.

---

## 👥 Ekip ve Katkıda Bulunanlar / Credits & Team

| Rol / Role | İsim / Name |
| :--- | :--- |
| 💻 **Development / Programlama** | **Enes Bozdemir** |
| 🎨 **2D Art & Görsel Tasarım** | **Muhammet Emin Yakut** |
| 🕹️ **Game Design & UI/UX** | **Enes Bozdemir & Muhammet Emin Yakut** |

---

*Game Jam projesi olarak geliştirilmiştir.*
