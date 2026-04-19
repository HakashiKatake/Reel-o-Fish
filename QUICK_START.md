# ⚡ Quick Start Guide

Get up and running in under 2 hours!

---

## 🎯 Goal
Build a complete fishing game with main menu for KALP Studio assessment.

---

## ✅ What's Already Done

All 20 C# scripts are written and ready:
- ✅ Main menu system
- ✅ Settings with persistence
- ✅ Async scene loading
- ✅ Complete fishing state machine
- ✅ Tension bar reel mechanic
- ✅ Audio management
- ✅ UI systems
- ✅ Fish data system

**You just need to set up the Unity scenes and import assets!**

---

## 🚀 3-Step Process

### Step 1: Download Assets (20 min)
**Minimum required:**
1. **UI Pack**: https://kenney-assets.itch.io/ui-pack
   - Download free
   - Import buttons, panels, sliders

2. **Fish Sprites**: https://craftpix.net/freebies/free-fishing-game-assets-pixel-art-pack/
   - Create free account
   - Download pack
   - Import 3 fish sprites

3. **Sound Effects**: https://pixabay.com/sound-effects/
   - Search and download:
     - "fishing cast" → cast.wav
     - "water splash" → splash.wav
     - "notification" → bite.wav
     - "fishing reel" → reel.wav
     - "success" → success.wav
     - "fail" → fail.wav
     - "button click" → button_click.wav

**OR use placeholders:**
- Colored squares for fish (white, green, orange)
- Unity default UI
- Skip sounds for now

### Step 2: Setup Scenes (45 min)

#### Main Menu Scene
1. Create new scene → Save as `MainMenu.unity`
2. Add Canvas (UI Scale Mode: Scale With Screen Size)
3. Add 3 panels:
   - **MainPanel**: Play, Settings, Quit buttons
   - **SettingsPanel**: Volume slider, Quality dropdown, Resolution dropdown, Back button
   - **LoadingPanel**: Progress bar, Loading text
4. Add scripts to Canvas:
   - `MenuManager` (assign panel references)
   - `SettingsManager` (assign UI references)
5. Create GameObjects:
   - **SceneLoader** (add SceneLoader script)
   - **AudioManager** (add AudioManager script + 2 AudioSource children)
6. Connect button OnClick events

#### Fishing Game Scene
1. Create new scene → Save as `FishingGame.unity`
2. Add environment:
   - Plane for water
   - Capsule for player
3. Add Canvas with:
   - **StateText** (top center)
   - **ReelMechanicPanel** (center, with Slider)
   - **CatchLogPanel** (right side, with ScrollView)
   - **BackButton** (top-left)
4. Add scripts:
   - Canvas: `FishingUI` (assign references)
   - ReelMechanicPanel: `TensionBarMechanic` (assign slider)
   - BackButton: `BackToMenuButton`
5. Create **FishingManager** GameObject (add FishingManager script)
6. Create 3 Fish ScriptableObjects:
   - Right-click → Create → Raahi Fishing → Fish Data
   - Assign sprites and properties
   - Add to FishingManager's Available Fish array

### Step 3: Build & Test (10 min)
1. File → Build Settings
2. Add scenes: MainMenu (0), FishingGame (1)
3. Build to `Build/` folder
4. Test the .exe

---

## 📋 Scene Setup Checklist

### Main Menu
- [ ] Canvas with MenuManager script
- [ ] 3 panels (Main, Settings, Loading)
- [ ] All buttons connected to MenuManager methods
- [ ] SettingsManager on SettingsPanel
- [ ] SceneLoader GameObject in scene
- [ ] AudioManager GameObject with audio clips

### Fishing Game
- [ ] Canvas with FishingUI script
- [ ] StateText assigned
- [ ] ReelMechanicPanel with TensionBarMechanic
- [ ] CatchLogPanel with ScrollView
- [ ] FishingManager with 3 Fish ScriptableObjects
- [ ] BackButton with BackToMenuButton script

### Build Settings
- [ ] MainMenu scene (index 0)
- [ ] FishingGame scene (index 1)

---

## 🎮 How to Play (Test This!)

1. **Main Menu**:
   - Click Play → See loading screen → Fishing game loads
   - Click Settings → Adjust volume → Back → Settings persist
   - Click Quit → Application closes

2. **Fishing Game**:
   - Press SPACE → Cast line
   - Wait 2-6 seconds → "FISH ON!" appears
   - Press SPACE quickly → Start reeling
   - Tap SPACE to keep tension in green zone for 3 seconds
   - See catch result → Auto return to idle
   - Press ESC → Return to menu

---

## 🔧 Inspector Assignments

### MenuManager (on Canvas in MainMenu)
```
Main Panel: [MainPanel GameObject]
Settings Panel: [SettingsPanel GameObject]
Loading Panel: [LoadingPanel GameObject]
Fishing Scene Name: "FishingGame"
```

### SettingsManager (on SettingsPanel)
```
Volume Slider: [VolumeSlider]
Quality Dropdown: [QualityDropdown]
Resolution Dropdown: [ResolutionDropdown]
Volume Value Text: [Text showing percentage]
```

### AudioManager (on AudioManager GameObject)
```
Music Source: [MusicSource child]
Sfx Source: [SFXSource child]
Cast Sound: [cast.wav]
Splash Sound: [splash.wav]
Bite Sound: [bite.wav]
Reel Sound: [reel.wav]
Success Sound: [success.wav]
Fail Sound: [fail.wav]
Button Click Sound: [button_click.wav]
```

### FishingManager (on FishingManager GameObject)
```
Available Fish: Size = 3
  Element 0: [GoldenMackerel ScriptableObject]
  Element 1: [MajiliSnapper ScriptableObject]
  Element 2: [CoastalCatfish ScriptableObject]
Min Wait Time: 2
Max Wait Time: 6
```

### FishingUI (on Canvas in FishingGame)
```
State Text: [StateText]
Reel Mechanic Panel: [ReelMechanicPanel]
Catch Log Container: [Content of ScrollView]
Catch Log Item Prefab: [CatchLogItem prefab]
Catch Count Text: [CatchCountText]
```

### TensionBarMechanic (on ReelMechanicPanel)
```
Tension Slider: [TensionSlider]
Fill Image: [Fill image inside slider]
Sweet Spot Indicator: [Green rectangle image]
Tension Increase Rate: 0.3
Tension Decrease Rate: 0.15
Sweet Spot Size: 0.2
Success Duration: 3
```

---

## 🐟 Fish ScriptableObject Examples

### Golden Mackerel (Common)
```
Fish Name: Golden Mackerel
Fish Icon: [fish sprite 1]
Rarity: Common
Spawn Probability: 0.5
Difficulty: Easy
Reel Speed Multiplier: 1.0
Reaction Window: 2.0
Hits Required: 3
```

### Majili Snapper (Uncommon)
```
Fish Name: Majili Snapper
Fish Icon: [fish sprite 2]
Rarity: Uncommon
Spawn Probability: 0.3
Difficulty: Medium
Reel Speed Multiplier: 1.5
Reaction Window: 1.5
Hits Required: 3
```

### Coastal Catfish (Rare)
```
Fish Name: Coastal Catfish
Fish Icon: [fish sprite 3]
Rarity: Rare
Spawn Probability: 0.2
Difficulty: Hard
Reel Speed Multiplier: 2.0
Reaction Window: 1.0
Hits Required: 3
```

---

## ⚠️ Common Issues

### Scripts not showing in Add Component
**Fix**: Wait for Unity to compile (check bottom-right corner)

### Button not responding
**Fix**: Check EventSystem exists in Hierarchy

### Scene not loading
**Fix**: Add both scenes to Build Settings (File → Build Settings)

### Audio not playing
**Fix**: 
1. Check AudioManager has audio clips assigned
2. Check volume slider is not at 0
3. Check AudioSource components exist

### Settings not saving
**Fix**: Test in build, not editor (PlayerPrefs work differently)

---

## 📚 Full Documentation

For detailed information, see:
- `README.md` - Complete overview and architecture
- `ASSET_DOWNLOAD_GUIDE.md` - Detailed asset instructions
- `UNITY_SETUP_GUIDE.md` - Step-by-step scene setup
- `PROJECT_SUMMARY.md` - Architecture and scoring

---

## 🎯 Success Criteria

Your game is ready when:
- ✅ Main menu loads with all buttons working
- ✅ Settings persist across scenes
- ✅ Play button shows loading screen
- ✅ Fishing game has complete gameplay loop
- ✅ Can cast, wait, react, reel, and catch fish
- ✅ Catch log shows caught fish
- ✅ Can return to menu
- ✅ Build runs without errors

---

## ⏱️ Time Estimate

- **Assets**: 20 minutes
- **Main Menu Scene**: 20 minutes
- **Fishing Scene**: 25 minutes
- **Testing**: 10 minutes
- **Build**: 5 minutes

**Total: ~80 minutes** (1 hour 20 minutes)

---

## 🏆 You've Got This!

The hard part (code architecture) is done. You just need to:
1. Import some sprites and sounds
2. Set up two scenes
3. Connect the dots in Inspector
4. Build and test

**Everything is modular and well-documented. You can do this!** 🎣

---

**Need help?** Check the detailed guides or the inline code comments.

**Ready?** Start with Step 1: Download Assets! 🚀
