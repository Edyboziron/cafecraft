# ☕ Cafe Craft

[![Unity](https://img.shields.io/badge/Unity-6000.3+-black?logo=unity&logoColor=white)](https://unity.com/)
[![Render Pipeline](https://img.shields.io/badge/Render%20Pipeline-URP%202D-blue)](https://unity.com/features/srp/universal-render-pipeline)
[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20Android-green)](https://unity.com/)
[![Type](https://img.shields.io/badge/Project-School%20Project-orange)](https://github.com/Edyboziron/cafecraft)

**Cafe Craft** is a cozy, engaging 2D cafe management and crafting simulation game developed as a school project. Step into the shoes of a barista and cafe owner: serve incoming customers, craft personalized coffee recipes with precision, earn tips, unlock new upgrades, and expand your coffee shop!

---

## 📖 Table of Contents
- [☕ About the Game](#-about-the-game)
- [✨ Key Features](#-key-features)
- [🎮 Gameplay & Mechanics](#-gameplay--mechanics)
- [🛠️ Tech Stack](#️-tech-stack)
- [📁 Project Structure](#-project-structure)
- [🚀 Installation & Setup](#-installation--setup)
- [👥 Credits & Team](#-credits--team)

---

## ☕ About the Game

In **Cafe Craft**, your goal is to welcome unique customers walking into your cafe, review their order tickets, and prepare the right coffee beverages by combining ingredients at the coffee machine. Serve customers before their patience runs out to earn coins and tips! Reinvest your earnings to unlock new delicious recipes, purchase higher-tier ingredients, redecorate the cafe interior, and progress through challenging levels.

---

## ✨ Key Features

- 👥 **Dynamic Customer System:** Meet distinct customer characters (Pınar, Saliha, Yağmur, Şevket, etc.) with expressive emotional reactions (Happy, Smiling, Sad, Angry) depending on order accuracy and delivery speed.
- ☕ **Interactive Coffee Crafting:** Prepare a variety of coffee drinks—Espresso, Latte, Mocha, and more—by carefully blending ingredients and utilizing machine steps.
- 📜 **Recipe Book:** An in-game guide displaying required ingredients and step-by-step instructions for each unlocked beverage.
- 🏪 **Shop & Decoration System:** Spend hard-earned coins to upgrade coffee ingredients and customize cafe elements such as countertops, flooring, and trash bins.
- 📈 **Level Progression:** Satisfy customer orders to earn experience, level up, and unlock advanced recipes.
- 💾 **Persistent Save System:** Built-in save manager utilizing `PlayerPrefs` to retain player level, total balance, and purchased market upgrades across sessions.
- 🎵 **Audio & Music Controls:** Ambient background soundtrack, interactive button sounds, and customizable volume settings.

---

## 🎮 Gameplay & Mechanics

1. **Take Orders:** Inspect incoming customer order tickets at the counter.
2. **Head to the Coffee Station:** Switch to the coffee machine workspace.
3. **Craft the Drink:** Combine the requested ingredients in the cup and mix them according to the recipe.
4. **Serve with Drag & Drop:** Drag the prepared coffee cup and drop it onto the customer.
5. **Collect Tips & Expand:** Collect your earnings and visit the market to upgrade your cafe!

---

## 🛠️ Tech Stack

- **Game Engine:** Unity 6 (`6000.3.x`+)
- **Render Pipeline:** Universal Render Pipeline (URP 2D)
- **Programming Language:** C#
- **Animation & Tweening:** DOTween
- **UI Framework:** Unity UI (Canvas-based responsive layouts)
- **Target Platforms:** Android & PC (Windows)

---

## 📁 Project Structure

```plaintext
Assets/
├── Scenes/                  # Game scenes
│   ├── MainMenu.unity       # Main landing menu
│   ├── customer_screen.unity# Main cafe counter and customer area
│   ├── Coffe_machine.unity  # Coffee crafting and preparation view
│   ├── Options.unity        # Audio & display settings
│   └── Credit.unity         # Credits screen
├── Scripts/                 # C# gameplay logic and systems
│   ├── Customer_control.cs  # Customer spawner and economy management
│   ├── Customer.cs          # Customer behaviors, states, and emotions
│   ├── Crafting.cs          # Coffee mixing and brewing logic
│   ├── BuyIngredient.cs     # Ingredient purchasing system
│   ├── LevelManager.cs      # Level progression and recipe unlock manager
│   ├── MarketButton.cs      # Store and cosmetic purchase controller
│   ├── SaveSystem.cs        # Data persistence (PlayerPrefs)
│   └── MusicManager.cs      # BGM and SFX manager
└── sprites/                 # 2D character sprites, UI graphics, and backgrounds
```

---

## 🚀 Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Edyboziron/cafecraft.git
   ```
2. **Open in Unity:**
   - Launch **Unity Hub**.
   - Add and open the project using **Unity 6000.3.x** or a compatible version.
3. **Run the Game:**
   - In the Project window, navigate to `Assets/Scenes/MainMenu.unity`.
   - Double-click to open the scene.
   - Press the **Play** button in the Unity Editor toolbar.

---

## 👥 Credits & Team

| Role | Contributor |
| :--- | :--- |
| 💻 **Development & Programming** | **Enes Bozdemir** |
| 🎨 **2D Art & Visual Design** | **Muhammet Emin Yakut** |
| 🕹️ **Game Design & UI/UX** | **Enes Bozdemir & Muhammet Emin Yakut** |

---

*Developed as a school project.*
