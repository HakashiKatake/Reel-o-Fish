# 🎮 Unity Scene Setup Guide

This guide walks you through setting up both scenes in Unity after importing all scripts and assets.

---

## 📋 Prerequisites

- ✅ All scripts created in `Assets/Scripts/`
- ✅ Assets downloaded and imported (see ASSET_DOWNLOAD_GUIDE.md)
- ✅ Unity 2021.3+ with URP installed

---

## 🎬 Scene 1: Main Menu

### Step 1: Create the Scene
1. In Unity, go to `File → New Scene`
2. Choose "URP" template
3. Save as `Assets/Scenes/MainMenu.unity`

### Step 2: Create Canvas
1. Right-click in Hierarchy → `UI → Canvas`
2. Rename to "MainMenuCanvas"
3. In Inspector:
   - **Canvas Scaler → UI Scale Mode**: "Scale With Screen Size"
   - **Reference Resolution**: 1920 x 1080
   - **Match**: 0.5 (balance between width and height)

### Step 3: Create Background
1. Right-click Canvas → `UI → Image`
2. Rename to "Background"
3. Stretch to full screen (Anchor Presets → stretch/stretch)
4. Assign a background sprite or color

### Step 4: Create Title Text
1. Right-click Canvas → `UI → Text - TextMeshPro`
2. Rename to "GameTitle"
3. Set text: "RAAHI - Fishing Adventure"
4. Position at top center
5. Font size: 72
6. Alignment: Center

### Step 5: Create Main Panel
1. Right-click Canvas → `UI → Panel`
2. Rename to "MainPanel"
3. Position in center
4. Add 3 buttons as children:

#### Play Button
- Right-click MainPanel → `UI → Button - TextMeshPro`
- Rename to "PlayButton"
- Text: "PLAY"
- Position: Top of panel
- Add `MenuManager` script to Canvas
- In Button Inspector → OnClick():
  - Drag Canvas to object field
  - Select `MenuManager.OnPlayButtonClicked`

#### Settings Button
- Create similar to Play Button
- Text: "SETTINGS"
- Position: Middle
- OnClick: `MenuManager.OnSettingsButtonClicked`

#### Quit Button
- Create similar to Play Button
- Text: "QUIT"
- Position: Bottom
- OnClick: `MenuManager.OnQuitButtonClicked`

### Step 6: Create Settings Panel
1. Right-click Canvas → `UI → Panel`
2. Rename to "SettingsPanel"
3. Set active to FALSE (will be shown when needed)
4. Add children:

#### Volume Slider
- Right-click SettingsPanel → `UI → Slider`
- Rename to "VolumeSlider"
- Min Value: 0, Max Value: 1, Value: 1
- Add Text label: "Master Volume"
- Add Text for value display (e.g., "100%")

#### Quality Dropdown
- Right-click SettingsPanel → `UI → Dropdown - TextMeshPro`
- Rename to "QualityDropdown"
- Add Text label: "Quality"
- Options will be populated by script

#### Resolution Dropdown
- Create similar to Quality Dropdown
- Rename to "ResolutionDropdown"
- Add Text label: "Resolution"

#### Back Button
- Right-click SettingsPanel → `UI → Button - TextMeshPro`
- Text: "BACK"
- OnClick: `MenuManager.OnBackButtonClicked`

### Step 7: Create Loading Panel
1. Right-click Canvas → `UI → Panel`
2. Rename to "LoadingPanel"
3. Set active to FALSE
4. Add children:

#### Loading Text
- Add Text: "Loading..."
- Center position

#### Progress Bar
- Right-click LoadingPanel → `UI → Slider`
- Rename to "ProgressBar"
- Disable Interactable
- Min: 0, Max: 1, Value: 0

#### Progress Text
- Add Text: "0%"
- Position below progress bar

#### Spinner (Optional)
- Add Image with rotating icon
- Will rotate via script

### Step 8: Setup MenuManager
1. Select Canvas
2. Add Component → `MenuManager` script
3. In Inspector, assign:
   - Main Panel → MainPanel
   - Settings Panel → SettingsPanel
   - Loading Panel → LoadingPanel
   - Fishing Scene Name: "FishingGame"

### Step 9: Setup SettingsManager
1. Select SettingsPanel
2. Add Component → `SettingsManager` script
3. In Inspector, assign:
   - Volume Slider → VolumeSlider
   - Quality Dropdown → QualityDropdown
   - Resolution Dropdown → ResolutionDropdown
   - Volume Value Text → (the text showing percentage)

### Step 10: Setup LoadingUI
1. Select LoadingPanel
2. Add Component → `LoadingUI` script
3. In Inspector, assign:
   - Progress Bar → ProgressBar
   - Progress Text → ProgressText
   - Spinner → (spinner image if you have one)

### Step 11: Create SceneLoader GameObject
1. Right-click in Hierarchy → `Create Empty`
2. Rename to "SceneLoader"
3. Add Component → `SceneLoader` script
4. This will persist across scenes

### Step 12: Create AudioManager GameObject
1. Right-click in Hierarchy → `Create Empty`
2. Rename to "AudioManager"
3. Add Component → `AudioManager` script
4. Create two child GameObjects:
   - "MusicSource" (Add Component → Audio Source)
   - "SFXSource" (Add Component → Audio Source)
5. In AudioManager Inspector:
   - Assign Music Source and SFX Source
   - Assign all sound effect clips

### Step 13: Save and Test
1. Save scene
2. Press Play
3. Test all buttons
4. Verify settings persist

---

## 🎣 Scene 2: Fishing Game

### Step 1: Create the Scene
1. `File → New Scene` (URP template)
2. Save as `Assets/Scenes/FishingGame.unity`

### Step 2: Create Environment

#### Camera Setup
1. Select Main Camera
2. Position: (0, 2, -10)
3. Background: Solid Color (light blue for sky)

#### Water Plane
1. Right-click Hierarchy → `3D Object → Plane`
2. Rename to "Water"
3. Position: (0, 0, 0)
4. Scale: (5, 1, 5)
5. Assign water material/texture

#### Dock/Shore (Optional)
1. Create Cube or import dock model
2. Position at edge of water
3. This is where player "stands"

#### Player Placeholder
1. Right-click → `3D Object → Capsule`
2. Rename to "Player"
3. Position on dock
4. This can be replaced with character model later

### Step 3: Create UI Canvas
1. Right-click → `UI → Canvas`
2. Rename to "FishingCanvas"
3. Canvas Scaler settings (same as Main Menu)

### Step 4: Create State Text
1. Right-click Canvas → `UI → Text - TextMeshPro`
2. Rename to "StateText"
3. Position: Top center
4. Font size: 48
5. Alignment: Center
6. Text: "Press [SPACE] to Cast"

### Step 5: Create Reel Mechanic Panel
1. Right-click Canvas → `UI → Panel`
2. Rename to "ReelMechanicPanel"
3. Position: Center
4. Set active to FALSE (shown during reeling)

#### Tension Slider
- Right-click ReelMechanicPanel → `UI → Slider`
- Rename to "TensionSlider"
- Disable Interactable
- Min: 0, Max: 1, Value: 0.5
- Make it vertical or horizontal (your choice)

#### Sweet Spot Indicator
- Right-click TensionSlider → `UI → Image`
- Rename to "SweetSpotIndicator"
- Small colored rectangle (green)
- Position will be set by script

#### Instructions Text
- Add Text: "Keep tension in the GREEN zone!"
- Position above slider

### Step 6: Create Catch Log Panel
1. Right-click Canvas → `UI → Panel`
2. Rename to "CatchLogPanel"
3. Position: Right side of screen
4. Size: Narrow vertical panel

#### Catch Count Text
- Add Text: "Caught: 0"
- Position at top of panel

#### Scroll View
- Right-click CatchLogPanel → `UI → Scroll View`
- This will contain catch log items
- Rename Content to "CatchLogContainer"

#### Catch Log Item Prefab
1. Create a prefab for individual catches:
   - Right-click CatchLogContainer → `UI → Panel`
   - Rename to "CatchLogItem"
   - Add Image for fish icon
   - Add Text for fish name
   - Add Text for rarity
2. Add Component → `CatchLogItem` script
3. Assign references in Inspector
4. Drag to `Assets/Prefabs/` to create prefab
5. Delete from scene

### Step 7: Create Back to Menu Button
1. Right-click Canvas → `UI → Button - TextMeshPro`
2. Rename to "BackButton"
3. Position: Top-left corner
4. Text: "← Menu" or "ESC"
5. Add Component → `BackToMenuButton` script
6. OnClick: `BackToMenuButton.OnBackToMenuClicked`

### Step 8: Setup FishingManager
1. Right-click Hierarchy → `Create Empty`
2. Rename to "FishingManager"
3. Add Component → `FishingManager` script
4. In Inspector:
   - **Available Fish**: Set size to 3
   - Drag your 3 Fish ScriptableObjects here
   - Min Wait Time: 2
   - Max Wait Time: 6

### Step 9: Setup FishingUI
1. Select Canvas
2. Add Component → `FishingUI` script
3. In Inspector, assign:
   - State Text → StateText
   - Reel Mechanic Panel → ReelMechanicPanel
   - Catch Log Container → CatchLogContainer (the Content of ScrollView)
   - Catch Log Item Prefab → (drag from Prefabs folder)
   - Catch Count Text → CatchCountText

### Step 10: Setup Tension Bar Mechanic
1. Select ReelMechanicPanel
2. Add Component → `TensionBarMechanic` script
3. In Inspector, assign:
   - Tension Slider → TensionSlider
   - Fill Image → (the Fill image inside slider)
   - Sweet Spot Indicator → SweetSpotIndicator
4. Adjust settings:
   - Tension Increase Rate: 0.3
   - Tension Decrease Rate: 0.15
   - Sweet Spot Size: 0.2
   - Success Duration: 3

### Step 11: Lighting (Optional)
1. Select Directional Light
2. Adjust color for time of day (warm for sunset, cool for day)
3. Add skybox or gradient background

### Step 12: Add Settings Application
1. Create empty GameObject: "GameInitializer"
2. Create new script: `GameInitializer.cs`
```csharp
using UnityEngine;
using RaahiFishing.Data;
using RaahiFishing.Audio;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        // Apply saved settings
        GameSettings settings = new GameSettings();
        settings.ApplySettings();
        
        // Apply audio volume
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplyVolume();
        }
    }
}
```
3. Add this script to GameInitializer GameObject

### Step 13: Save and Test
1. Save scene
2. Add both scenes to Build Settings:
   - `File → Build Settings`
   - Drag MainMenu (index 0)
   - Drag FishingGame (index 1)
3. Press Play from MainMenu
4. Test full flow

---

## 🐟 Creating Fish ScriptableObjects

### Step 1: Create Fish Data Assets
1. Navigate to `Assets/ScriptableObjects/Fish/`
2. Right-click → `Create → Raahi Fishing → Fish Data`

### Fish 1: Golden Mackerel (Common)
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

### Fish 2: Majili Snapper (Uncommon)
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

### Fish 3: Coastal Catfish (Rare)
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

## 🎨 UI Styling Tips

### Button Hover Effects
1. Select button
2. In Button component:
   - **Normal Color**: White
   - **Highlighted Color**: Light yellow
   - **Pressed Color**: Gray
   - **Selected Color**: Light blue
3. Set **Transition**: Color Tint
4. **Fade Duration**: 0.1

### Panel Styling
- Add slight transparency (Alpha: 200-230)
- Add border using Outline component
- Use consistent color scheme

### Text Styling
- Use TextMeshPro for better quality
- Enable **Auto Size** for responsive text
- Add **Shadow** or **Outline** for readability

---

## 🔧 Common Issues & Fixes

### Issue: Scripts not appearing in Add Component
**Fix**: Wait for Unity to compile, or restart Unity

### Issue: TextMeshPro not available
**Fix**: Window → TextMeshPro → Import TMP Essential Resources

### Issue: Buttons not responding
**Fix**: Ensure EventSystem exists in scene (created automatically with first UI element)

### Issue: Scene not loading
**Fix**: Add scenes to Build Settings (File → Build Settings)

### Issue: Audio not playing
**Fix**: 
- Check AudioManager is in scene
- Verify audio clips are assigned
- Check volume is not 0

### Issue: Settings not persisting
**Fix**: PlayerPrefs are saved automatically, but test in build (not just editor)

---

## ✅ Final Checklist

### Main Menu Scene
- [ ] Canvas with proper scaling
- [ ] Main panel with Play, Settings, Quit buttons
- [ ] Settings panel with volume, quality, resolution
- [ ] Loading panel with progress bar
- [ ] MenuManager script configured
- [ ] SettingsManager script configured
- [ ] SceneLoader GameObject
- [ ] AudioManager GameObject
- [ ] All buttons have OnClick events

### Fishing Game Scene
- [ ] Environment (water, dock, player)
- [ ] Canvas with state text
- [ ] Reel mechanic panel with tension slider
- [ ] Catch log panel with scroll view
- [ ] Back to menu button
- [ ] FishingManager with fish array
- [ ] FishingUI script configured
- [ ] TensionBarMechanic script configured
- [ ] 3 Fish ScriptableObjects created and assigned

### Build Settings
- [ ] MainMenu scene (index 0)
- [ ] FishingGame scene (index 1)
- [ ] Platform selected (Windows/Mac/Linux)

---

## 🚀 Ready to Build!

Once everything is set up:
1. `File → Build Settings`
2. Click "Build"
3. Choose output folder: `Build/`
4. Wait for build to complete
5. Test the .exe file

---

## 📝 Next Steps

1. **Polish**: Add particle effects, animations, camera shake
2. **Test**: Play through multiple times, catch different fish
3. **Optimize**: Check performance, optimize UI
4. **Build**: Create final build for submission
5. **Document**: Update README with any changes

---

**Need help?** Check the main README.md for architecture details and troubleshooting.

Happy building! 🎮
