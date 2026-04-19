# 🏗️ Architecture Documentation

Detailed breakdown of the system architecture and design decisions.

---

## 📊 System Overview

```
┌─────────────────────────────────────────────────────────────┐
│                        RAAHI FISHING GAME                    │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  ┌──────────────┐              ┌──────────────┐            │
│  │  Main Menu   │─────────────▶│ Fishing Game │            │
│  │    Scene     │   Async Load │    Scene     │            │
│  └──────────────┘              └──────────────┘            │
│         │                              │                     │
│         │                              │                     │
│         ▼                              ▼                     │
│  ┌──────────────────────────────────────────────┐          │
│  │         Persistent Managers (DontDestroyOnLoad)         │
│  ├──────────────────────────────────────────────┤          │
│  │  • SceneLoader                                │          │
│  │  • AudioManager                               │          │
│  │  • GameSettings (Data)                        │          │
│  └──────────────────────────────────────────────┘          │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎯 Core Architecture Principles

### 1. Separation of Concerns
Each system has a single, well-defined responsibility:

```
┌─────────────┐
│   SYSTEMS   │
├─────────────┤
│ Core        │ → Scene loading, state interfaces
│ Data        │ → Settings, fish definitions
│ Audio       │ → Sound playback, volume control
│ Menu        │ → Menu flow, settings UI
│ Fishing     │ → Game logic, state machine
│ UI          │ → Display, user feedback
└─────────────┘
```

### 2. Dependency Flow

```
High Level (Abstract)
        ↑
        │
   ┌────┴────┐
   │Interface│
   └────┬────┘
        │
        ↓
Low Level (Concrete)
```

**Example:**
```
ReelingState (High Level)
     ↓ depends on
IReelMechanic (Abstraction)
     ↑ implemented by
TensionBarMechanic (Low Level)
```

---

## 🎮 Main Menu Architecture

### Component Hierarchy

```
MainMenu Scene
│
├── SceneLoader (Persistent)
│   └── SceneLoader.cs
│
├── AudioManager (Persistent)
│   ├── AudioManager.cs
│   ├── MusicSource (AudioSource)
│   └── SFXSource (AudioSource)
│
└── Canvas
    ├── MenuManager.cs
    │
    ├── MainPanel
    │   ├── PlayButton → MenuManager.OnPlayButtonClicked()
    │   ├── SettingsButton → MenuManager.OnSettingsButtonClicked()
    │   └── QuitButton → MenuManager.OnQuitButtonClicked()
    │
    ├── SettingsPanel
    │   ├── SettingsManager.cs
    │   ├── VolumeSlider → SettingsManager.OnVolumeChanged()
    │   ├── QualityDropdown → SettingsManager.OnQualityChanged()
    │   ├── ResolutionDropdown → SettingsManager.OnResolutionChanged()
    │   └── BackButton → MenuManager.OnBackButtonClicked()
    │
    └── LoadingPanel
        ├── LoadingUI.cs
        ├── ProgressBar (Slider)
        └── ProgressText (TextMeshPro)
```

### Data Flow

```
User Input
    ↓
MenuManager (Orchestrator)
    ↓
SceneLoader (Scene Management)
    ↓
LoadingUI (Visual Feedback)
    ↓
Scene Loaded
```

### Settings Persistence

```
UI Input
    ↓
SettingsManager
    ↓
GameSettings (Data Layer)
    ↓
PlayerPrefs (Unity API)
    ↓
Disk Storage
```

---

## 🎣 Fishing Game Architecture

### State Machine Design

```
┌─────────────────────────────────────────────────────────┐
│                   FISHING STATE MACHINE                  │
├─────────────────────────────────────────────────────────┤
│                                                          │
│     ┌──────┐                                            │
│     │ IDLE │ ◀──────────────────────┐                  │
│     └───┬──┘                         │                  │
│         │ Press SPACE                │                  │
│         ▼                             │                  │
│    ┌─────────┐                       │                  │
│    │ WAITING │                       │                  │
│    └────┬────┘                       │                  │
│         │ Random Timer (2-6s)        │                  │
│         ▼                             │                  │
│     ┌──────┐                         │                  │
│     │ BITE │                         │                  │
│     └───┬──┘                         │                  │
│         │ Press SPACE (in time)      │                  │
│         ├─────────────────┐          │                  │
│         │                 │ Missed   │                  │
│         ▼                 ▼          │                  │
│    ┌─────────┐       ┌────────┐     │                  │
│    │ REELING │       │ RESULT │─────┘                  │
│    └────┬────┘       │(Failed)│                        │
│         │            └────────┘                        │
│         │ Success/Fail                                  │
│         ▼                                               │
│    ┌────────┐                                          │
│    │ RESULT │──────────────────────┘                  │
│    │(Success)│                                          │
│    └────────┘                                          │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

### Component Hierarchy

```
FishingGame Scene
│
├── FishingManager
│   ├── FishingManager.cs (State Machine Controller)
│   ├── Available Fish[] (ScriptableObject references)
│   │
│   └── States (Instances)
│       ├── IdleState
│       ├── WaitingState
│       ├── BiteState
│       ├── ReelingState
│       └── ResultState
│
├── Canvas
│   ├── FishingUI.cs (UI Manager)
│   │
│   ├── StateText (TextMeshPro)
│   │
│   ├── ReelMechanicPanel
│   │   ├── TensionBarMechanic.cs (IReelMechanic)
│   │   ├── TensionSlider
│   │   └── SweetSpotIndicator
│   │
│   ├── CatchLogPanel
│   │   ├── ScrollView
│   │   │   └── Content (Container)
│   │   │       └── CatchLogItem (Prefab instances)
│   │   └── CatchCountText
│   │
│   └── BackButton
│       └── BackToMenuButton.cs
│
└── Environment
    ├── Water (Plane)
    ├── Player (Capsule)
    └── Camera
```

### State Pattern Implementation

```
┌──────────────┐
│  IGameState  │ (Interface)
├──────────────┤
│ + Enter()    │
│ + Execute()  │
│ + Exit()     │
└──────┬───────┘
       │
       │ Implemented by
       │
       ├─────────────┬─────────────┬─────────────┬─────────────┐
       ▼             ▼             ▼             ▼             ▼
  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐
  │  Idle   │  │ Waiting │  │  Bite   │  │ Reeling │  │ Result  │
  │  State  │  │  State  │  │  State  │  │  State  │  │  State  │
  └─────────┘  └─────────┘  └─────────┘  └─────────┘  └─────────┘
```

### Strategy Pattern Implementation

```
┌────────────────┐
│ IReelMechanic  │ (Interface)
├────────────────┤
│ + Initialize() │
│ + Update()     │
└────────┬───────┘
         │
         │ Implemented by
         │
         ├──────────────────┬──────────────────┬──────────────────┐
         ▼                  ▼                  ▼                  ▼
  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐
  │  Tension Bar │  │ Mash to Reel │  │ Timing Rings │  │   Custom     │
  │  Mechanic    │  │  (Future)    │  │  (Future)    │  │  (Future)    │
  └──────────────┘  └──────────────┘  └──────────────┘  └──────────────┘
       ✅                 ⚠️                 ⚠️                 ⚠️
   Implemented         Not Yet            Not Yet            Not Yet
```

---

## 📦 Data Architecture

### ScriptableObject Design

```
┌─────────────────────────────────────────┐
│          FishData (ScriptableObject)     │
├─────────────────────────────────────────┤
│ • fishName: string                       │
│ • fishIcon: Sprite                       │
│ • rarity: FishRarity (enum)             │
│ • spawnProbability: float               │
│ • difficulty: FishDifficulty (enum)     │
│ • reelSpeedMultiplier: float            │
│ • reactionWindow: float                 │
│ • hitsRequired: int                     │
└─────────────────────────────────────────┘
         │
         │ Instances
         │
         ├─────────────────┬─────────────────┬─────────────────┐
         ▼                 ▼                 ▼                 ▼
  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐
  │   Golden     │  │    Majili    │  │   Coastal    │  │     More     │
  │  Mackerel    │  │   Snapper    │  │   Catfish    │  │   (Future)   │
  │  (Common)    │  │ (Uncommon)   │  │    (Rare)    │  │              │
  └──────────────┘  └──────────────┘  └──────────────┘  └──────────────┘
```

### Settings Data Flow

```
┌──────────────────────────────────────────────────────────┐
│                    GameSettings Class                     │
├──────────────────────────────────────────────────────────┤
│                                                           │
│  Properties:                    Storage:                  │
│  • MasterVolume ◀──────────────▶ PlayerPrefs             │
│  • QualityLevel ◀──────────────▶ PlayerPrefs             │
│  • ResolutionIndex ◀───────────▶ PlayerPrefs             │
│                                                           │
│  Methods:                                                 │
│  • ApplySettings() → Apply to Unity systems              │
│                                                           │
└──────────────────────────────────────────────────────────┘
         ▲                                    ▲
         │                                    │
         │ Used by                            │ Used by
         │                                    │
  ┌──────┴────────┐                  ┌───────┴────────┐
  │ SettingsManager│                  │ GameInitializer│
  │  (Main Menu)   │                  │ (Fishing Game) │
  └────────────────┘                  └────────────────┘
```

---

## 🔊 Audio Architecture

### Audio Manager Design

```
┌─────────────────────────────────────────────────────────┐
│                    AudioManager                          │
│                    (Singleton, Persistent)               │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  Components:                                             │
│  ┌──────────────┐              ┌──────────────┐        │
│  │ MusicSource  │              │  SFXSource   │        │
│  │ (AudioSource)│              │ (AudioSource)│        │
│  └──────────────┘              └──────────────┘        │
│                                                          │
│  Sound Effects:                                          │
│  • castSound                                            │
│  • splashSound                                          │
│  • biteSound                                            │
│  • reelSound                                            │
│  • successSound                                         │
│  • failSound                                            │
│  • buttonClickSound                                     │
│                                                          │
│  Methods:                                                │
│  • PlayCastSound()                                      │
│  • PlaySplashSound()                                    │
│  • PlayBiteSound()                                      │
│  • PlayReelSound()                                      │
│  • PlaySuccessSound()                                   │
│  • PlayFailSound()                                      │
│  • PlayButtonClick()                                    │
│  • SetMasterVolume(float)                               │
│  • ApplyVolume()                                        │
│                                                          │
└─────────────────────────────────────────────────────────┘
         ▲                    ▲                    ▲
         │                    │                    │
         │ Called by          │ Called by          │ Called by
         │                    │                    │
  ┌──────┴──────┐      ┌─────┴──────┐      ┌─────┴──────┐
  │ MenuManager │      │   States   │      │SettingsMan │
  └─────────────┘      └────────────┘      └────────────┘
```

---

## 🎨 UI Architecture

### UI Responsibility Separation

```
┌────────────────────────────────────────────────────────┐
│                    UI LAYER                             │
├────────────────────────────────────────────────────────┤
│                                                         │
│  FishingUI (Manager)                                    │
│  ├─ UpdateStateText()      ← Called by States          │
│  ├─ SetStateTextColor()    ← Called by States          │
│  ├─ ShowReelMechanic()     ← Called by ReelingState    │
│  ├─ HideReelMechanic()     ← Called by States          │
│  └─ AddCatchToLog()        ← Called by ResultState     │
│                                                         │
│  LoadingUI (Display)                                    │
│  └─ UpdateProgress()       ← Called by SceneLoader     │
│                                                         │
│  CatchLogItem (Component)                               │
│  └─ Setup(FishData)        ← Called by FishingUI       │
│                                                         │
└────────────────────────────────────────────────────────┘
```

---

## 🔄 Scene Loading Flow

```
User Clicks Play
       ↓
MenuManager.OnPlayButtonClicked()
       ↓
SceneLoader.LoadSceneAsync("FishingGame")
       ↓
Start Coroutine
       ↓
AsyncOperation (Unity)
       ├─ Progress: 0% → 90%
       │  └─ LoadingUI.UpdateProgress()
       │
       ├─ Wait for 90%
       │
       ├─ Show 100%
       │
       └─ Activate Scene
              ↓
       FishingGame Loaded
              ↓
       GameInitializer.Start()
              ↓
       Apply Saved Settings
```

---

## 🎯 SOLID Principles in Action

### Single Responsibility Principle (SRP)

```
✅ SceneLoader      → Only loads scenes
✅ AudioManager     → Only manages audio
✅ GameSettings     → Only handles settings data
✅ MenuManager      → Only controls menu flow
✅ SettingsManager  → Only manages settings UI
✅ FishingManager   → Only coordinates fishing states
✅ Each State       → Only handles one state's logic
```

### Open/Closed Principle (OCP)

```
Adding New Fish:
  Create FishData ScriptableObject
  ✅ No code changes needed

Adding New Reel Mechanic:
  Implement IReelMechanic
  ✅ No changes to existing mechanics

Adding New State:
  Implement IGameState
  ✅ No changes to existing states
```

### Liskov Substitution Principle (LSP)

```
Any IGameState can replace another:
  IdleState ↔ WaitingState ↔ BiteState ↔ ReelingState ↔ ResultState
  ✅ All work with FishingManager

Any IReelMechanic can replace another:
  TensionBarMechanic ↔ MashToReelMechanic ↔ TimingRingsMechanic
  ✅ All work with ReelingState
```

### Interface Segregation Principle (ISP)

```
IGameState:
  ✅ Only 3 methods (Enter, Execute, Exit)
  ✅ No unused methods

IReelMechanic:
  ✅ Only 2 methods (Initialize, UpdateMechanic)
  ✅ No unused methods
```

### Dependency Inversion Principle (DIP)

```
High-Level Modules depend on Abstractions:

ReelingState ──depends on──▶ IReelMechanic (abstraction)
                                    ▲
                                    │ implements
                                    │
                            TensionBarMechanic (concrete)

✅ ReelingState doesn't know about TensionBarMechanic
✅ Easy to swap implementations
```

---

## 🧪 Testability

### Unit Testable Components

```
✅ GameSettings
   - Test: Save/Load values
   - Test: Apply settings

✅ FishData
   - Test: Probability calculations
   - Test: Difficulty scaling

✅ Individual States
   - Test: Enter/Execute/Exit logic
   - Test: State transitions

✅ TensionBarMechanic
   - Test: Tension calculations
   - Test: Success/Failure conditions
```

### Integration Test Points

```
✅ Menu → Fishing Scene transition
✅ Settings persistence across scenes
✅ Complete fishing gameplay loop
✅ Audio playback at correct times
```

---

## 📈 Scalability

### Easy to Extend

```
Add New Fish:
  1. Create FishData ScriptableObject
  2. Assign to FishingManager
  ✅ Done! (0 code changes)

Add New Reel Mechanic:
  1. Create class implementing IReelMechanic
  2. Add to ReelMechanicPanel
  3. Swap in ReelingState
  ✅ Done! (1 new file, 1 line change)

Add New State:
  1. Create class implementing IGameState
  2. Add to FishingManager
  3. Add transition logic
  ✅ Done! (1 new file, minimal changes)

Add New Sound:
  1. Import audio file
  2. Add field to AudioManager
  3. Add PlayX() method
  ✅ Done! (minimal changes)
```

---

## 🔒 Error Handling

### Defensive Programming

```
Null Checks:
  ✅ All UI references checked before use
  ✅ AudioManager instance checked
  ✅ SceneLoader instance checked

Fallbacks:
  ✅ Default fish if none selected
  ✅ Default settings if not saved
  ✅ Editor quit handling

Validation:
  ✅ Clamped values (volume 0-1)
  ✅ Array bounds checking
  ✅ State transition validation
```

---

## 📊 Performance Considerations

### Optimizations

```
✅ Singleton pattern for managers (no repeated instantiation)
✅ ScriptableObjects for data (shared instances)
✅ Coroutines for async operations (non-blocking)
✅ Object pooling ready (CatchLogItem prefab system)
✅ Minimal Update() calls (only active states)
```

### Memory Management

```
✅ DontDestroyOnLoad only for managers
✅ Scene unloading cleans up UI
✅ No memory leaks in state transitions
✅ Audio clips loaded on demand
```

---

## 🎓 Design Patterns Summary

| Pattern | Where Used | Benefit |
|---------|-----------|---------|
| **State** | Fishing states | Clean state management |
| **Strategy** | Reel mechanics | Swappable algorithms |
| **Singleton** | Managers | Global access, persistence |
| **Factory** | Fish selection | Probability-based creation |
| **Observer** | UI updates | Decoupled communication |
| **Command** | Button actions | Encapsulated actions |

---

## 🏆 Architecture Quality Metrics

```
✅ Cohesion: High (each class has one job)
✅ Coupling: Low (depend on abstractions)
✅ Complexity: Low (simple, readable code)
✅ Maintainability: High (easy to modify)
✅ Testability: High (isolated components)
✅ Extensibility: High (easy to add features)
✅ Readability: High (clear naming, comments)
```

---

## 📝 Conclusion

This architecture demonstrates:
- ✅ Professional-grade code organization
- ✅ Industry-standard design patterns
- ✅ SOLID principles throughout
- ✅ Clean, maintainable codebase
- ✅ Easy to extend and test
- ✅ Production-ready structure

**Perfect for a technical interview discussion!** 🎯
