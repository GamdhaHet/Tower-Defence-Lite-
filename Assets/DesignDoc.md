# [YOUR GAME NAME] — Design Document

**Version:** 1.0  
**Last Updated:** March 2026  
**Developer:** [Your Name / Team Name]  
**Platform:** Android  

---

## 1. Game Overview

### What is the game?
[Write 2–3 sentences describing your game here.  
Example: "A tower defense game where players place and upgrade towers to defend their base against endless waves of enemies."]

### Genre
Tower Defense

### Target Audience
[e.g.  casual mobile gamers, strategy fans]

### Core Fantasy (How should the player FEEL?)
[e.g. "The player should feel like a powerful commander outsmarting waves of enemies."]

---

## 2. Core Gameplay Loop

```
Place Towers → Survive Wave → Earn Coins & Gems → Buy More Towers → Next Wave
```

---

## 3. Game Systems

### 3.1 Currency System

| Currency | How Earned | How Spent |
|---|---|---|
| Coins | Completing waves, IAP | Buying towers |
| Gems | IAP (Gem Packs) | Buying towers |

### 3.2 IAP (In-App Purchases)

| Product | What the Player Gets |
|---|---|
| Gem Pack (Small) | [e.g. 100 Gems] |
| Gem Pack (Medium) | [e.g. 500 Gems] |
| Gem Pack (Large) | [e.g. 1200 Gems] |
| Coin Pack (Small) | [e.g. 1000 Coins] |
| Coin Pack (Medium) | [e.g. 5000 Coins] |
| Coin Pack (Large) | [e.g. 12000 Coins] |

> 💡 Fill in exact Product IDs and amounts once finalized.

### 3.3 Tower System
- **How are towers placed?** [e.g. Drag and drop on the map grid]
- **How are towers bought?** [Spend Coins or Gems]
- **Tower types planned:**
  - [ ] [Tower 1 — e.g. Archer Tower]
  - [ ] [Tower 2 — e.g. Cannon Tower]
  - [ ] [Tower 3 — e.g. Magic Tower]

### 3.4 Wave System
- **How do waves work?** [e.g. Timed enemy spawns, increasing difficulty per wave]
- **What happens when the player loses?** [e.g. Restart from wave 1 / checkpoint]
- **What happens when the player wins a wave?** [e.g. Earn Coins, short break before next wave]

### 3.5 Save System
- **What is saved:** Currency (Coins, Gems), tower unlocks, highest wave reached, settings
- **Where it is saved:** `PlayerPrefs`
- **Key format:** `currency_Coins`, `currency_Gems`, etc.

---

## 4. UI Screens

- [ ] **Main Menu** — Play, Settings, Shop buttons
- [ ] **Gameplay HUD** — Health bar, Coins/Gems display, wave counter, tower buy panel
- [ ] **Shop Screen** — Gem Packs, Coin Packs (IAP)
- [ ] **Top Bar** — Always-visible Coins & Gems display
- [ ] **Win Popup** — Wave cleared, reward shown, continue button
- [ ] **Lose Popup** — Game over, replay button
- [ ] **Settings Screen** — Sound, music toggles
- [ ] **Purchase Failed Popup** — Shown when IAP fails
- [ ] **Buffering Screen** — Loading overlay during IAP processing

---

## 5. Monetization

| Type | Details |
|---|---|
| Gem Packs | Multiple size tiers — Small / Medium / Large |
| Coin Packs | Multiple size tiers — Small / Medium / Large |
| Ads | [Planned? e.g. Rewarded ads for bonus coins — Yes / No] |

---

## 6. Tech Stack

| Tool | Purpose |
|---|---|
| Unity [version] | Game Engine |
| Unity IAP | In-App Purchases (Gem & Coin packs) |
| Odin Inspector | Editor tooling, serialized dictionaries |
| Demigiant (DOTween) | UI animations and tweening |
| PlayerPrefs | Local save system |

---

## 7. TODO / Backlog

- [ ] Finalize all tower types and stats
- [ ] Set up IAP Product IDs in Google Play Console
- [ ] Design and implement wave progression difficulty curve
- [ ] Build all UI screens listed above
- [ ] Test IAP on real Android device
- [ ] Add rewarded ads (if planned)
- [ ] Polish main menu and gameplay HUD animations
- [ ] [Add more here]

---

## 8. Change Log

| Version | Date | What Changed |
|---|---|---|
| 1.0 | March 2026 | Initial design document created |

---

*This document is a living file — update it as your game evolves.*
