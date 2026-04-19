# Settings Panel Setup Guide
## Reel-O-Fish - KALP Studio Assessment

Complete step-by-step guide to create the Settings panel in your Main Menu scene.

---

## 📋 Overview

The Settings panel includes:
- **Volume Slider** - Controls master audio volume (0-100%)
- **Quality Dropdown** - Changes graphics quality (Low, Medium, High)
- **Resolution Dropdown** - Changes screen resolution
- **Close Button** - Closes the settings panel

All settings persist across scenes using PlayerPrefs.

---

## 🎨 Step 1: Create the Settings Panel UI

### 1.1 Create Panel Container
1. Open the **MainMenu** scene
2. In Hierarchy, find your **Canvas**
3. Right-click Canvas → **UI → Panel**
4. Rename it to **SettingsPanel**
5. In Inspector:
   - Anchor: **Stretch** (all corners)
   - Color: Semi-transparent black (R:0, G:0, B:0, A:200)

### 1.2 Create Background Panel
1. Right-click **SettingsPanel** → **UI → Image**
2. Rename to **BackgroundPanel**
3. In Inspector:
   - Width: 600
   - Height: 500
   - Anchor: **Center**
   - Color: Dark gray (R:50, G:50, B:50, A:255)

---

## 🎯 Step 2: Add Title Text

1. Right-click **BackgroundPanel** → **UI → Text - TextMeshPro**
2. Rename to **TitleText**
3. In Inspector:
   - Text: "SETTINGS"
   - Font Size: 48
   - Alignment: Center
   - Color: White
   - Position: Top of panel
   - Anchor: Top Center
   - Pos Y: -50

---

## 🔊 Step 3: Create Volume Slider

### 3.1 Create Volume Container
1. Right-click **BackgroundPanel** → **Create Empty**
2. Rename to **VolumeContainer**
3. Position: Y = -120

### 3.2 Add Volume Label
1. Right-click **VolumeContainer** → **UI → Text - TextMeshPro**
2. Rename to **VolumeLabel**
3. In Inspector:
   - Text: "Master Volume"
   - Font Size: 24
   - Alignment: Left
   - Anchor: Left
   - Pos X: -200

### 3.3 Add Volume Slider
1. Right-click **VolumeContainer** → **UI → Slider**
2. Rename to **VolumeSlider**
3. In Inspector (Slider component):
   - Min Value: 0
   - Max Value: 1
   - Value: 1
   - Whole Numbers: OFF
4. Position:
   - Anchor: Center
   - Width: 300
   - Pos X: 50

### 3.4 Add Volume Value Text
1. Right-click **VolumeContainer** → **UI → Text - TextMeshPro**
2. Rename to **VolumeValueText**
3. In Inspector:
   - Text: "100%"
   - Font Size: 24
   - Alignment: Right
   - Anchor: Right
   - Pos X: 250

---

## 🎮 Step 4: Create Quality Dropdown

### 4.1 Create Quality Container
1. Right-click **BackgroundPanel** → **Create Empty**
2. Rename to **QualityContainer**
3. Position: Y = -200

### 4.2 Add Quality Label
1. Right-click **QualityContainer** → **UI → Text - TextMeshPro**
2. Rename to **QualityLabel**
3. In Inspector:
   - Text: "Graphics Quality"
   - Font Size: 24
   - Alignment: Left
   - Anchor: Left
   - Pos X: -200

### 4.3 Add Quality Dropdown
1. Right-click **QualityContainer** → **UI → Dropdown - TextMeshPro**
2. Rename to **QualityDropdown**
3. In Inspector (Dropdown component):
   - Options: (Click + to add)
     - Option 0: "Low"
     - Option 1: "Medium"
     - Option 2: "High"
   - Value: 2 (High by default)
4. Position:
   - Anchor: Center
   - Width: 300
   - Pos X: 50

---

## 📺 Step 5: Create Resolution Dropdown

### 5.1 Create Resolution Container
1. Right-click **BackgroundPanel** → **Create Empty**
2. Rename to **ResolutionContainer**
3. Position: Y = -280

### 5.2 Add Resolution Label
1. Right-click **ResolutionContainer** → **UI → Text - TextMeshPro**
2. Rename to **ResolutionLabel**
3. In Inspector:
   - Text: "Resolution"
   - Font Size: 24
   - Alignment: Left
   - Anchor: Left
   - Pos X: -200

### 5.3 Add Resolution Dropdown
1. Right-click **ResolutionContainer** → **UI → Dropdown - TextMeshPro**
2. Rename to **ResolutionDropdown**
3. In Inspector (Dropdown component):
   - Options: (Will be populated by script)
     - Option 0: "1920x1080"
     - Option 1: "1280x720"
     - Option 2: "800x600"
   - Value: 0
4. Position:
   - Anchor: Center
   - Width: 300
   - Pos X: 50

---

## ❌ Step 6: Create Close Button

1. Right-click **BackgroundPanel** → **UI → Button - TextMeshPro**
2. Rename to **CloseButton**
3. In Inspector:
   - Width: 200
   - Height: 60
   - Anchor: Bottom Center
   - Pos Y: 50
4. Find the **Text (TMP)** child:
   - Text: "CLOSE"
   - Font Size: 28
   - Alignment: Center

---

## 🔧 Step 7: Add SettingsManager Script

### 7.1 Create SettingsManager GameObject
1. Right-click in Hierarchy → **Create Empty**
2. Rename to **SettingsManager**
3. Click **Add Component**
4. Search for **SettingsManager** and add it

### 7.2 Assign References in Inspector
Select **SettingsManager** and assign:

**Volume Settings:**
- Volume Slider → Drag **VolumeSlider**
- Volume Value Text → Drag **VolumeValueText**

**Quality Settings:**
- Quality Dropdown → Drag **QualityDropdown**

**Resolution Settings:**
- Resolution Dropdown → Drag **ResolutionDropdown**

---

## 🎯 Step 8: Connect Settings Panel to Menu

### 8.1 Find MenuManager
1. Select the **MenuManager** GameObject in Hierarchy
2. In Inspector, find the **MenuManager** component

### 8.2 Assign Settings Panel
- Settings Panel → Drag **SettingsPanel**

### 8.3 Connect Settings Button
1. Find your **Settings Button** in the main menu
2. In Inspector, scroll to **Button** component
3. In **On Click ()** section:
   - Click **+** to add event
   - Drag **MenuManager** into the object field
   - Select **MenuManager → ShowSettings()**

### 8.4 Connect Close Button
1. Select **CloseButton** in SettingsPanel
2. In Inspector, scroll to **Button** component
3. In **On Click ()** section:
   - Click **+** to add event
   - Drag **MenuManager** into the object field
   - Select **MenuManager → HideSettings()**

---

## 🎨 Step 9: Style the UI (Optional)

### Slider Styling
1. Select **VolumeSlider**
2. Expand in Hierarchy to see:
   - Background
   - Fill Area → Fill
   - Handle Slide Area → Handle

**Customize:**
- Background: Light gray
- Fill: Green or blue
- Handle: White circle

### Dropdown Styling
1. Select **QualityDropdown** or **ResolutionDropdown**
2. Expand to see:
   - Label
   - Arrow
   - Template → Viewport → Content → Item

**Customize:**
- Background: Dark gray
- Label: White text
- Arrow: White
- Item Background: Highlight on hover

---

## 📱 Step 10: Set Initial State

1. Select **SettingsPanel** in Hierarchy
2. In Inspector, **uncheck** the checkbox at the top
   - This hides the panel by default
3. Save the scene (Ctrl+S / Cmd+S)

---

## ✅ Step 11: Test the Settings

### Test Checklist:
1. **Enter Play Mode**
2. **Click Settings button** → Panel should appear
3. **Move Volume Slider** → Value text should update (0-100%)
4. **Change Quality** → Should see quality level change
5. **Change Resolution** → Screen should resize
6. **Click Close** → Panel should disappear
7. **Exit Play Mode**
8. **Enter Play Mode again** → Settings should be remembered

---

## 🎯 Complete Hierarchy Structure

```
Canvas
├── MainMenu (Panel)
│   ├── Title
│   ├── PlayButton
│   ├── SettingsButton ← Connected to MenuManager.ShowSettings()
│   └── QuitButton
│
├── SettingsPanel ← Initially disabled
│   └── BackgroundPanel
│       ├── TitleText ("SETTINGS")
│       ├── VolumeContainer
│       │   ├── VolumeLabel ("Master Volume")
│       │   ├── VolumeSlider ← Assigned to SettingsManager
│       │   └── VolumeValueText ("100%") ← Assigned to SettingsManager
│       ├── QualityContainer
│       │   ├── QualityLabel ("Graphics Quality")
│       │   └── QualityDropdown ← Assigned to SettingsManager
│       ├── ResolutionContainer
│       │   ├── ResolutionLabel ("Resolution")
│       │   └── ResolutionDropdown ← Assigned to SettingsManager
│       └── CloseButton ← Connected to MenuManager.HideSettings()
│
└── MenuManager (GameObject)
    └── SettingsManager (GameObject)
```

---

## 🔍 Inspector Reference

### SettingsManager Component
```
┌─────────────────────────────────────┐
│ Settings Manager (Script)           │
├─────────────────────────────────────┤
│ Volume Settings                     │
│   Volume Slider      [Assigned] ✓   │
│   Volume Value Text  [Assigned] ✓   │
│                                     │
│ Quality Settings                    │
│   Quality Dropdown   [Assigned] ✓   │
│                                     │
│ Resolution Settings                 │
│   Resolution Dropdown [Assigned] ✓  │
└─────────────────────────────────────┘
```

### MenuManager Component
```
┌─────────────────────────────────────┐
│ Menu Manager (Script)               │
├─────────────────────────────────────┤
│ Settings Panel       [Assigned] ✓   │
└─────────────────────────────────────┘
```

---

## 🐛 Troubleshooting

### Settings Panel Doesn't Appear
- Check that SettingsPanel is assigned in MenuManager
- Verify Settings button has On Click event connected
- Make sure SettingsPanel is initially disabled

### Volume Slider Doesn't Work
- Check VolumeSlider is assigned in SettingsManager
- Verify AudioManager exists in the scene
- Check that AudioManager has an AudioSource component

### Quality Dropdown Doesn't Change Anything
- Check QualityDropdown is assigned in SettingsManager
- Verify dropdown has options: "Low", "Medium", "High"
- Check Unity Quality Settings (Edit → Project Settings → Quality)

### Resolution Dropdown Is Empty
- SettingsManager populates this automatically on Start()
- Check that SettingsManager script is enabled
- Verify the script has no errors in Console

### Settings Don't Persist
- Check that PlayerPrefs is being used (it is in the script)
- Verify settings are loaded in Start() method
- Test by changing settings, quitting, and restarting

---

## 💡 Tips

1. **Test in Build**: Some settings (like resolution) work differently in Editor vs Build
2. **Add More Settings**: You can add fullscreen toggle, vsync, etc.
3. **Visual Feedback**: Add sound effects when changing settings
4. **Validation**: Add min/max constraints to prevent invalid values
5. **Reset Button**: Consider adding a "Reset to Default" button

---

## 📝 Quick Setup Summary

1. Create SettingsPanel with BackgroundPanel
2. Add Title text
3. Create Volume section (Label + Slider + Value Text)
4. Create Quality section (Label + Dropdown)
5. Create Resolution section (Label + Dropdown)
6. Add Close button
7. Add SettingsManager GameObject with script
8. Assign all UI references to SettingsManager
9. Connect Settings button to MenuManager.ShowSettings()
10. Connect Close button to MenuManager.HideSettings()
11. Disable SettingsPanel by default
12. Test!

---

## 🎓 Understanding the Code

### How Volume Works:
- Slider value (0-1) is saved to PlayerPrefs
- On load, AudioManager.SetVolume() is called
- AudioManager adjusts AudioListener.volume

### How Quality Works:
- Dropdown index (0-2) is saved to PlayerPrefs
- QualitySettings.SetQualityLevel() is called
- Unity applies the quality preset

### How Resolution Works:
- Available resolutions are detected from Screen.resolutions
- Selected resolution is saved to PlayerPrefs
- Screen.SetResolution() is called with saved values

---

## ✅ You're Done When...

- Settings panel opens and closes smoothly
- Volume slider updates the percentage text
- Volume changes affect game audio
- Quality dropdown changes graphics quality
- Resolution dropdown changes screen size
- All settings persist after restarting the game
- Close button works correctly

Good luck! 🎮
