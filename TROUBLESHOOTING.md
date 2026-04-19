# 🔧 Troubleshooting Guide

Common issues and their solutions.

---

## 🚨 Compilation Errors

### Issue: "The type or namespace name 'X' could not be found"

**Cause**: Missing namespace or incorrect using statement

**Solution**:
1. Check the using statements at top of file
2. Ensure all scripts are in correct folders
3. Wait for Unity to finish compiling (bottom-right corner)
4. Try: `Assets → Reimport All`

**Example**:
```csharp
// Add these at the top of your script
using RaahiFishing.Core;
using RaahiFishing.Data;
using RaahiFishing.Audio;
```

---

### Issue: "The name 'TextMeshProUGUI' does not exist"

**Cause**: TextMeshPro not imported

**Solution**:
1. `Window → TextMeshPro → Import TMP Essential Resources`
2. Add to script: `using TMPro;`
3. Use `TextMeshProUGUI` for UI text
4. Use `TMP_Dropdown` for dropdowns

---

### Issue: Scripts not appearing in "Add Component" menu

**Cause**: Compilation errors or Unity not finished compiling

**Solution**:
1. Check Console for errors (Ctrl+Shift+C)
2. Fix any red errors
3. Wait for spinning icon in bottom-right to stop
4. Try restarting Unity

---

## 🎮 Runtime Errors

### Issue: "NullReferenceException" when clicking buttons

**Cause**: Missing script references in Inspector

**Solution**:
1. Select the GameObject with the script
2. Check Inspector for missing references (shows "None" or "Missing")
3. Drag the correct GameObject/Component to the field
4. Common missing references:
   - MenuManager: Main Panel, Settings Panel, Loading Panel
   - FishingUI: State Text, Reel Mechanic Panel
   - AudioManager: Music Source, SFX Source

**Debug Tip**:
```csharp
// Add null checks in your code
if (stateText != null)
{
    stateText.text = "New Text";
}
else
{
    Debug.LogError("StateText is null!");
}
```

---

### Issue: "Object reference not set to an instance of an object" in AudioManager

**Cause**: AudioManager.Instance is null

**Solution**:
1. Ensure AudioManager GameObject exists in scene
2. Check AudioManager script is attached
3. AudioManager should be in MainMenu scene (persists to other scenes)
4. Check for duplicate AudioManagers (only one should exist)

**Verification**:
```csharp
if (AudioManager.Instance != null)
{
    AudioManager.Instance.PlayButtonClick();
}
else
{
    Debug.LogWarning("AudioManager not found!");
}
```

---

### Issue: Scene not loading when clicking Play button

**Cause**: Scene not added to Build Settings or wrong name

**Solution**:
1. `File → Build Settings`
2. Drag both scenes into "Scenes In Build":
   - MainMenu (index 0)
   - FishingGame (index 1)
3. Check scene name in MenuManager matches exactly: "FishingGame"
4. Scene names are case-sensitive!

---

### Issue: "Scene 'FishingGame' couldn't be loaded"

**Cause**: Scene name mismatch or not in build

**Solution**:
1. Check exact scene name in Project window
2. Update MenuManager → Fishing Scene Name field
3. Ensure scene is in Build Settings
4. Try: `File → Build Settings → Add Open Scenes`

---

## 🎨 UI Issues

### Issue: Buttons not responding to clicks

**Cause**: Missing EventSystem or UI blocking

**Solution**:
1. Check Hierarchy for "EventSystem" GameObject
2. If missing: `Right-click → UI → Event System`
3. Check button has "Button" component
4. Check button is not behind another UI element
5. Check Canvas has "Graphic Raycaster" component

---

### Issue: UI elements not visible

**Cause**: Incorrect anchoring or positioning

**Solution**:
1. Select UI element
2. Check RectTransform position
3. Try: Anchor Presets (top-left of RectTransform)
4. Set to appropriate anchor (center, top-left, etc.)
5. Check Canvas Scaler settings:
   - UI Scale Mode: "Scale With Screen Size"
   - Reference Resolution: 1920 x 1080

---

### Issue: Text is blurry or pixelated

**Cause**: Not using TextMeshPro or wrong settings

**Solution**:
1. Use TextMeshPro instead of legacy Text
2. Right-click → `UI → Text - TextMeshPro`
3. For pixel art: Set font size to multiples of 8 or 16
4. Enable "Auto Size" for responsive text

---

### Issue: Slider not working

**Cause**: Missing components or incorrect setup

**Solution**:
1. Slider needs these components:
   - Slider (script)
   - Background (Image)
   - Fill Area → Fill (Image)
   - Handle Slide Area → Handle (Image)
2. Check "Interactable" is enabled
3. Check Min/Max values are correct
4. Assign OnValueChanged event

---

## 🔊 Audio Issues

### Issue: No sound playing

**Cause**: Multiple possible causes

**Solution**:
1. **Check AudioManager**:
   - GameObject exists in scene
   - Script attached
   - Audio clips assigned in Inspector
   
2. **Check AudioSource components**:
   - MusicSource and SFXSource exist as children
   - AudioSource components attached
   - Not muted
   - Volume > 0

3. **Check audio files**:
   - Imported correctly
   - Not corrupted
   - Correct format (WAV, MP3, OGG)

4. **Check volume settings**:
   - Master volume slider not at 0
   - System volume not muted

**Debug**:
```csharp
void Start()
{
    if (AudioManager.Instance != null)
    {
        Debug.Log("AudioManager found!");
        AudioManager.Instance.PlayButtonClick();
    }
}
```

---

### Issue: Audio plays but very quiet

**Cause**: Volume settings too low

**Solution**:
1. Check AudioManager volume multipliers:
   - Music: `volume * 0.7f`
   - SFX: `volume * 1.0f`
2. Check AudioSource volume (should be 1.0)
3. Check audio clip import settings:
   - Select audio file
   - Check "Volume" slider (should be 1.0)
4. Increase master volume in settings

---

## 🎣 Gameplay Issues

### Issue: Fishing states not transitioning

**Cause**: State machine not initialized or input not detected

**Solution**:
1. Check FishingManager exists in scene
2. Check FishingManager script attached
3. Check Available Fish array has fish assigned
4. Add debug logs to states:
```csharp
public void Enter()
{
    Debug.Log("Entered IdleState");
    // rest of code
}
```
5. Check Input.GetKeyDown(KeyCode.Space) is working:
```csharp
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("Space pressed!");
    }
}
```

---

### Issue: "Waiting for a bite..." never ends

**Cause**: Timer not updating or state not transitioning

**Solution**:
1. Check FishingManager has Update() method
2. Check currentState.Execute() is called in Update()
3. Add debug log in WaitingState:
```csharp
public void Execute()
{
    waitTimer += Time.deltaTime;
    Debug.Log($"Wait timer: {waitTimer}/{targetWaitTime}");
    // rest of code
}
```

---

### Issue: Can't catch any fish (always fails)

**Cause**: Reel mechanic not configured correctly

**Solution**:
1. Check TensionBarMechanic script on ReelMechanicPanel
2. Verify all references assigned:
   - Tension Slider
   - Fill Image
   - Sweet Spot Indicator
3. Check settings are reasonable:
   - Tension Increase Rate: 0.3
   - Tension Decrease Rate: 0.15
   - Success Duration: 3
4. Test by making it easier:
   - Increase Sweet Spot Size to 0.5
   - Decrease Success Duration to 1

---

### Issue: No fish appear in catch log

**Cause**: Catch log not configured or prefab missing

**Solution**:
1. Check FishingUI script on Canvas
2. Verify references:
   - Catch Log Container (Content of ScrollView)
   - Catch Log Item Prefab
3. Check CatchLogItem prefab exists in Prefabs folder
4. Check CatchLogItem has CatchLogItem script
5. Add debug log:
```csharp
public void AddCatchToLog(FishData fish)
{
    Debug.Log($"Adding fish to log: {fish.fishName}");
    // rest of code
}
```

---

## 💾 Settings Issues

### Issue: Settings not persisting between sessions

**Cause**: PlayerPrefs not saving or testing in editor

**Solution**:
1. **Test in build, not editor!** PlayerPrefs behave differently
2. Ensure PlayerPrefs.Save() is called:
```csharp
PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, value);
PlayerPrefs.Save(); // Important!
```
3. Check settings are loaded on start:
```csharp
void Start()
{
    LoadSettings();
}
```
4. Clear PlayerPrefs if corrupted:
```csharp
// Add this temporarily to a script
void Start()
{
    PlayerPrefs.DeleteAll();
    Debug.Log("PlayerPrefs cleared!");
}
```

---

### Issue: Volume slider doesn't affect audio

**Cause**: Volume not applied to AudioManager

**Solution**:
1. Check SettingsManager.OnVolumeChanged() calls:
```csharp
public void OnVolumeChanged(float value)
{
    gameSettings.MasterVolume = value;
    AudioManager.Instance?.ApplyVolume(); // This line!
}
```
2. Check AudioManager.ApplyVolume() updates AudioSources:
```csharp
public void ApplyVolume()
{
    float volume = gameSettings.MasterVolume;
    musicSource.volume = volume * 0.7f;
    sfxSource.volume = volume;
}
```

---

## 🏗️ Build Issues

### Issue: Build fails with errors

**Cause**: Compilation errors or missing scenes

**Solution**:
1. Fix all Console errors first (must be 0 errors)
2. Check both scenes are in Build Settings
3. Try: `File → Build Settings → Player Settings`
4. Check Company Name and Product Name are set
5. Try: Clean build (delete Build folder, rebuild)

---

### Issue: Build runs but crashes immediately

**Cause**: Missing resources or incorrect settings

**Solution**:
1. Check all assets are included in build
2. Check scenes are in correct order in Build Settings
3. Try: `Edit → Project Settings → Player`
4. Check "API Compatibility Level" is set correctly
5. Check for platform-specific issues (Windows vs Mac)

---

### Issue: Build works but settings don't save

**Cause**: PlayerPrefs location different in build

**Solution**:
1. This is normal - PlayerPrefs work differently in build vs editor
2. Ensure PlayerPrefs.Save() is called after every change
3. Test by:
   - Change volume
   - Quit application
   - Restart application
   - Check if volume persisted

---

## 🐛 Debug Tips

### Enable Debug Logging

Add this to any script to see what's happening:

```csharp
void Start()
{
    Debug.Log($"[{GetType().Name}] Started");
}

void OnEnable()
{
    Debug.Log($"[{GetType().Name}] Enabled");
}

void OnDisable()
{
    Debug.Log($"[{GetType().Name}] Disabled");
}
```

### Check State Transitions

Add to FishingManager:

```csharp
public void ChangeState(IGameState newState)
{
    Debug.Log($"State change: {currentState?.GetType().Name} → {newState?.GetType().Name}");
    currentState?.Exit();
    currentState = newState;
    currentState?.Enter();
}
```

### Verify References

Add to Start() of any script:

```csharp
void Start()
{
    if (stateText == null) Debug.LogError("StateText is null!");
    if (reelMechanicPanel == null) Debug.LogError("ReelMechanicPanel is null!");
    // Check all references
}
```

### Test Input

```csharp
void Update()
{
    if (Input.anyKeyDown)
    {
        Debug.Log($"Key pressed: {Input.inputString}");
    }
}
```

---

## 🔍 Common Mistakes

### ❌ Mistake: Not assigning references in Inspector
**✅ Fix**: Always check Inspector after adding scripts

### ❌ Mistake: Wrong scene name in MenuManager
**✅ Fix**: Copy-paste scene name exactly

### ❌ Mistake: Forgetting to add scenes to Build Settings
**✅ Fix**: File → Build Settings → Add scenes

### ❌ Mistake: Testing settings persistence in editor
**✅ Fix**: Always test in build

### ❌ Mistake: Not creating Fish ScriptableObjects
**✅ Fix**: Create 3 fish before testing

### ❌ Mistake: AudioManager not in first scene
**✅ Fix**: AudioManager must be in MainMenu scene

### ❌ Mistake: Multiple managers in scene
**✅ Fix**: Only one AudioManager, one SceneLoader

### ❌ Mistake: Forgetting to import TextMeshPro
**✅ Fix**: Window → TextMeshPro → Import TMP Essential Resources

---

## 📋 Pre-Flight Checklist

Before testing, verify:

### Main Menu Scene
- [ ] Canvas exists with MenuManager
- [ ] 3 panels created (Main, Settings, Loading)
- [ ] All buttons have OnClick events
- [ ] SettingsManager on SettingsPanel
- [ ] SceneLoader GameObject exists
- [ ] AudioManager GameObject exists
- [ ] Audio clips assigned

### Fishing Game Scene
- [ ] Canvas exists with FishingUI
- [ ] FishingManager exists with 3 fish
- [ ] ReelMechanicPanel has TensionBarMechanic
- [ ] All UI references assigned
- [ ] BackButton has BackToMenuButton script

### Build Settings
- [ ] MainMenu scene (index 0)
- [ ] FishingGame scene (index 1)
- [ ] No compilation errors

### Assets
- [ ] 3 Fish ScriptableObjects created
- [ ] Fish sprites assigned
- [ ] 7 sound effects imported
- [ ] UI sprites imported

---

## 🆘 Still Stuck?

### Systematic Debugging Process

1. **Identify the problem**:
   - What were you trying to do?
   - What happened instead?
   - Any error messages?

2. **Check the Console**:
   - Red errors? Fix those first
   - Yellow warnings? Note them
   - Any stack traces? Read them

3. **Verify references**:
   - All Inspector fields assigned?
   - Any "None" or "Missing"?
   - Correct GameObjects dragged?

4. **Add debug logs**:
   - Log when methods are called
   - Log variable values
   - Log state transitions

5. **Test in isolation**:
   - Does the button work alone?
   - Does the state transition work?
   - Does the audio play?

6. **Compare with guide**:
   - Check UNITY_SETUP_GUIDE.md
   - Verify all steps completed
   - Check for typos

7. **Start fresh if needed**:
   - Create new scene
   - Test one feature at a time
   - Build up gradually

---

## 📞 Getting Help

### Information to Provide

When asking for help, include:
1. **What you're trying to do**
2. **What's happening instead**
3. **Error messages** (full text)
4. **What you've tried**
5. **Unity version**
6. **Screenshots** (if relevant)

### Useful Commands

```csharp
// Check if object exists
Debug.Log($"Object exists: {myObject != null}");

// Check object name
Debug.Log($"Object name: {gameObject.name}");

// Check component
Debug.Log($"Has component: {GetComponent<MyScript>() != null}");

// Check scene
Debug.Log($"Current scene: {SceneManager.GetActiveScene().name}");
```

---

## ✅ Success Indicators

You know it's working when:
- ✅ No errors in Console
- ✅ All buttons respond to clicks
- ✅ Scene loads with progress bar
- ✅ Settings persist after restart
- ✅ Complete fishing loop works
- ✅ Audio plays at correct times
- ✅ Fish appear in catch log
- ✅ Can return to menu

---

**Remember**: Most issues are simple reference assignments or typos. Check the basics first! 🔍
