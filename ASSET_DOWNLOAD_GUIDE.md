# 🎨 Asset Download & Setup Guide

This guide will help you download and import all the free assets needed for the Raahi Fishing Game.

---

## 📋 Quick Checklist

- [ ] UI Assets (Kenney UI Pack)
- [ ] Fish Sprites (CraftPix Fishing Pack)
- [ ] Sound Effects (Pixabay)
- [ ] Optional: Background Music
- [ ] Optional: Water/Ocean Textures

---

## 1. 🖼️ UI Assets - Kenney UI Pack

### Download
**Source**: https://kenney-assets.itch.io/ui-pack
**License**: CC0 (Public Domain) - Free for any use

### Steps:
1. Visit the link above
2. Click "Download Now"
3. You can download for free (or pay what you want)
4. Extract the ZIP file

### What to Import:
```
Assets/Art/Sprites/UI/
├── Buttons (various colors and states)
├── Panels (backgrounds for menus)
├── Sliders (for volume control)
├── Icons (settings, play, quit icons)
└── Progress bars (for loading screen)
```

### Recommended Sprites:
- **Buttons**: `button_square_flat.png`, `button_square_depth.png`
- **Panels**: `panel_brown.png`, `panel_blue.png`
- **Sliders**: `slider_horizontal.png`, `slider_handle.png`
- **Icons**: `icon_play.png`, `icon_settings.png`, `icon_cross.png`

### Unity Import:
1. In Unity: `Assets → Import New Asset → Multiple`
2. Select the UI sprites you want
3. In Inspector, set **Texture Type** to "Sprite (2D and UI)"
4. Set **Pixels Per Unit** to 100
5. Click Apply

---

## 2. 🐟 Fish & Fishing Assets - CraftPix

### Download
**Source**: https://craftpix.net/freebies/free-fishing-game-assets-pixel-art-pack/
**License**: Free for commercial use with attribution

### Steps:
1. Visit the link above
2. Click "Free Download"
3. Create a free account (required)
4. Download the ZIP file
5. Extract it

### What to Import:
```
Assets/Art/Sprites/
├── Fish/
│   ├── fish_1.png (use for Common fish)
│   ├── fish_2.png (use for Uncommon fish)
│   ├── fish_3.png (use for Rare fish)
│   └── ... (8 different fish included)
├── Water/
│   ├── water_tiles.png
│   └── water_background.png
└── Props/
    ├── fishing_rod.png
    └── boat.png (optional)
```

### Unity Import:
1. Import fish sprites to `Assets/Art/Sprites/Fish/`
2. Set **Texture Type** to "Sprite (2D and UI)"
3. Set **Pixels Per Unit** to 32 (pixel art)
4. Set **Filter Mode** to "Point (no filter)" for crisp pixels
5. Click Apply

---

## 3. 🔊 Sound Effects - Pixabay

### Download Links
**Source**: https://pixabay.com/sound-effects/
**License**: Pixabay License - Free for commercial use

### Required Sound Effects:

#### Cast Sound
- Search: "fishing cast" or "whoosh"
- Example: https://pixabay.com/sound-effects/search/whoosh/
- Save as: `cast.wav`

#### Splash Sound
- Search: "water splash"
- Example: https://pixabay.com/sound-effects/search/water%20splash/
- Save as: `splash.wav`

#### Bite Sound
- Search: "notification" or "alert"
- Example: https://pixabay.com/sound-effects/search/notification/
- Save as: `bite.wav`

#### Reel Sound
- Direct link: https://pixabay.com/sound-effects/fishing-reel-82246/
- Save as: `reel.wav`

#### Success Sound
- Search: "success" or "win"
- Example: https://pixabay.com/sound-effects/search/success/
- Save as: `success.wav`

#### Fail Sound
- Search: "fail" or "lose"
- Example: https://pixabay.com/sound-effects/search/fail/
- Save as: `fail.wav`

#### Button Click
- Search: "button click" or "UI click"
- Example: https://pixabay.com/sound-effects/search/button%20click/
- Save as: `button_click.wav`

### Unity Import:
1. Import all sounds to `Assets/Audio/SFX/`
2. Select all audio files in Unity
3. In Inspector:
   - **Load Type**: "Decompress On Load" (for short SFX)
   - **Preload Audio Data**: Checked
   - **Compression Format**: "Vorbis" (good quality, small size)
4. Click Apply

---

## 4. 🎵 Background Music (Optional)

### Download
**Source**: https://pixabay.com/music/
**License**: Pixabay License - Free for commercial use

### Recommended Searches:
- "calm ocean"
- "peaceful ambient"
- "relaxing nature"
- "coastal atmosphere"

### Unity Import:
1. Import to `Assets/Audio/Music/`
2. In Inspector:
   - **Load Type**: "Streaming" (for long music files)
   - **Compression Format**: "Vorbis"
3. Click Apply

---

## 5. 🌊 Water & Background Textures (Optional)

### Option A: Kenney Assets
**Source**: https://kenney.nl/ass ets/background-elements
**License**: CC0 (Public Domain)

### Option B: OpenGameArt
**Source**: https://opengameart.org/content/water-pack-tileset-for-platformer-pixel-art
**License**: Various (check individual assets)

### What to Get:
- Water texture (tiling)
- Sky/horizon background
- Dock or shore texture
- Optional: Clouds, sun, birds

### Unity Import:
1. Import to `Assets/Art/Textures/`
2. For tiling textures:
   - **Texture Type**: "Default"
   - **Wrap Mode**: "Repeat"
3. For backgrounds:
   - **Texture Type**: "Sprite (2D and UI)"

---

## 6. 🎨 Creating Fish ScriptableObjects

After importing fish sprites, create the fish data:

### Steps:
1. In Unity Project window, navigate to `Assets/ScriptableObjects/Fish/`
2. Right-click → `Create → Raahi Fishing → Fish Data`
3. Name it "GoldenMackerel"
4. In Inspector, set:
   ```
   Fish Name: Golden Mackerel
   Fish Icon: [Drag fish sprite here]
   Rarity: Common
   Spawn Probability: 0.5
   Difficulty: Easy
   Reel Speed Multiplier: 1.0
   Reaction Window: 2.0
   Hits Required: 3
   ```
5. Repeat for other fish:
   - **Majili Snapper** (Uncommon, Medium difficulty)
   - **Coastal Catfish** (Rare, Hard difficulty)

---

## 7. 🎮 Setting Up Audio Manager

### Steps:
1. In Hierarchy, create empty GameObject: "AudioManager"
2. Add component: `AudioManager` script
3. Create two child GameObjects:
   - "MusicSource" (add AudioSource component)
   - "SFXSource" (add AudioSource component)
4. In AudioManager Inspector:
   - Drag MusicSource to "Music Source" field
   - Drag SFXSource to "Sfx Source" field
   - Assign all sound effects to their respective fields:
     - Cast Sound → cast.wav
     - Splash Sound → splash.wav
     - Bite Sound → bite.wav
     - Reel Sound → reel.wav
     - Success Sound → success.wav
     - Fail Sound → fail.wav
     - Button Click Sound → button_click.wav

---

## 8. 🎯 Quick Import Checklist

### Minimum Required Assets:
- [ ] 3-5 UI button sprites
- [ ] 2-3 UI panel sprites
- [ ] 1 slider sprite
- [ ] 3 fish sprites (for 3 fish types)
- [ ] 7 sound effects (cast, splash, bite, reel, success, fail, click)

### Nice to Have:
- [ ] Background music
- [ ] Water textures
- [ ] Sky/horizon background
- [ ] More fish variety
- [ ] UI icons

---

## 9. 📦 Alternative: All-in-One Pack

If you want everything in one download:

**Kenney Game Assets All-in-1**
- **Source**: https://kenney.itch.io/kenney-game-assets
- **Price**: Pay what you want (suggested $9.99)
- **Includes**: 60,000+ assets including everything you need
- **License**: CC0 (Public Domain)

This single pack contains:
- All UI elements
- Fish and ocean sprites
- Sound effects
- Music
- Backgrounds
- And much more!

---

## 10. 🔍 Asset Organization Tips

### Recommended Folder Structure:
```
Assets/
├── Art/
│   ├── Sprites/
│   │   ├── UI/
│   │   │   ├── Buttons/
│   │   │   ├── Panels/
│   │   │   └── Icons/
│   │   ├── Fish/
│   │   │   ├── fish_common_1.png
│   │   │   ├── fish_uncommon_1.png
│   │   │   └── fish_rare_1.png
│   │   └── Environment/
│   │       ├── water.png
│   │       └── background.png
│   └── Textures/
│       └── water_tile.png
└── Audio/
    ├── SFX/
    │   ├── cast.wav
    │   ├── splash.wav
    │   ├── bite.wav
    │   ├── reel.wav
    │   ├── success.wav
    │   ├── fail.wav
    │   └── button_click.wav
    └── Music/
        └── ocean_ambient.mp3
```

---

## 11. ⚡ Quick Start (Minimal Setup)

If you're short on time, here's the absolute minimum:

### 1. UI (5 minutes)
- Download Kenney UI Pack
- Import: 1 button, 1 panel, 1 slider
- Use Unity's default UI for the rest

### 2. Fish (3 minutes)
- Use simple colored squares as placeholders:
  - Create 3 sprites in any image editor (64x64px)
  - White square = Common fish
  - Green square = Uncommon fish
  - Orange square = Rare fish

### 3. Audio (10 minutes)
- Download 7 sounds from Pixabay (links above)
- Import to Unity

### 4. Background (2 minutes)
- Use solid color background (blue for water, light blue for sky)
- Or download one free background from Kenney

**Total time: ~20 minutes for minimal viable assets**

---

## 12. 📝 Attribution Text

Add this to your README or in-game credits:

```
Assets Used:
- UI Assets: Kenney (kenney.nl) - CC0 License
- Fishing Sprites: CraftPix.net - Free License with Attribution
- Sound Effects: Pixabay - Pixabay License
- [Add any other assets you use]
```

---

## 🎉 You're Ready!

Once you've downloaded and imported these assets:
1. Assign sprites to UI elements in your scenes
2. Create Fish ScriptableObjects with fish sprites
3. Set up AudioManager with sound effects
4. Test the game!

**Need help?** Check the main README.md for architecture details and setup instructions.

---

## 🔗 Quick Links Summary

- **Kenney UI Pack**: https://kenney-assets.itch.io/ui-pack
- **CraftPix Fishing**: https://craftpix.net/freebies/free-fishing-game-assets-pixel-art-pack/
- **Pixabay Sounds**: https://pixabay.com/sound-effects/
- **Pixabay Music**: https://pixabay.com/music/
- **OpenGameArt**: https://opengameart.org/
- **Kenney All-in-1**: https://kenney.itch.io/kenney-game-assets

Happy fishing! 🎣
