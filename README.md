# Raahi - Fishing Mini Game
## KALP Studio Unity Developer Assessment

A slice-of-life fishing adventure game featuring a polished main menu and complete fishing minigame with state machine architecture.

---

## 🎮 How to Play

### Main Menu
- **Play**: Loads the fishing game with async loading indicator
- **Settings**: Adjust master volume, quality, and resolution (persists across sessions)
- **Quit**: Exits the application

### Fishing Game
1. **Press SPACE** to cast your line
2. **Wait** for a fish to bite (2-6 seconds)
3. **React quickly** when you see "FISH ON!" and press SPACE
4. **Reel it in** by keeping the tension bar in the green sweet spot
   - Tension rises automatically
   - Tap SPACE to reduce tension
   - Stay in the sweet spot for 3 seconds to catch the fish
5. **View your catch** in the log on the right side
6. **Press ESC** or click "Back to Menu" to return

---

## 🏗️ Architecture Overview

This project follows **SOLID principles** and uses clean, modular architecture:

### Design Patterns Used

#### 1. **State Pattern** (Fishing Game)
- `IGameState` interface defines state contract
- Each fishing state (`IdleState`, `WaitingState`, `BiteState`, `ReelingState`, `ResultState`) is a separate class
- `FishingManager` coordinates state transitions
- **Benefits**: Easy to debug, extend, and test individual states

#### 2. **Strategy Pattern** (Reel Mechanics)
- `IReelMechanic` interface allows different reel implementations
- `TensionBarMechanic` is one implementation
- Easy to add new mechanics (mash-to-reel, timing rings) without changing core code
- **Benefits**: Open/Closed Principle - open for extension, closed for modification

#### 3. **Singleton Pattern** (Managers)
- `SceneLoader`, `AudioManager`, `FishingManager`, `FishingUI` use singleton pattern
- Ensures single instance and global access
- Properly handles scene transitions with `DontDestroyOnLoad`

#### 4. **Data-Driven Design** (ScriptableObjects)
- `FishData` ScriptableObject defines all fish properties
- Adding new fish requires zero code changes
- Designers can create fish without programmer intervention

### SOLID Principles Applied

#### Single Responsibility Principle (SRP)
- `SceneLoader`: Only handles scene loading
- `AudioManager`: Only manages audio
- `GameSettings`: Only handles settings persistence
- `MenuManager`: Only controls menu flow
- `SettingsManager`: Only manages settings UI
- Each fishing state: Only handles one state's logic

#### Open/Closed Principle (OCP)
- `IReelMechanic` interface allows new mechanics without modifying existing code
- `IGameState` interface allows new states without changing state machine
- `FishData` ScriptableObjects allow new fish without code changes

#### Liskov Substitution Principle (LSP)
- Any `IGameState` implementation can replace another
- Any `IReelMechanic` implementation can replace another
- Polymorphism used correctly throughout

#### Interface Segregation Principle (ISP)
- `IGameState` has only essential methods (Enter, Execute, Exit)
- `IReelMechanic` has only what's needed (Initialize, UpdateMechanic)
- No client forced to depend on unused methods

#### Dependency Inversion Principle (DIP)
- High-level modules depend on abstractions (`IGameState`, `IReelMechanic`)
- `ReelingState` depends on `IReelMechanic` interface, not concrete implementation
- Easy to swap implementations

---

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── IGameState.cs          # State pattern interface
│   │   └── SceneLoader.cs         # Async scene loading
│   ├── Data/
│   │   ├── FishData.cs            # Fish ScriptableObject definition
│   │   └── GameSettings.cs        # Settings persistence
│   ├── Audio/
│   │   └── AudioManager.cs        # Audio playback and volume
│   ├── Menu/
│   │   ├── MenuManager.cs         # Main menu flow
│   │   └── SettingsManager.cs     # Settings UI control
│   ├── Fishing/
│   │   ├── FishingManager.cs      # Main fishing controller
│   │   ├── States/
│   │   │   ├── IdleState.cs       # Waiting to cast
│   │   │   ├── WaitingState.cs    # Line cast, waiting for bite
│   │   │   ├── BiteState.cs       # Reaction window
│   │   │   ├── ReelingState.cs    # Reel mechanic active
│   │   │   └── ResultState.cs     # Show catch result
│   │   └── Mechanics/
│   │       ├── IReelMechanic.cs   # Reel mechanic interface
│   │       └── TensionBarMechanic.cs # Tension bar implementation
│   └── UI/
│       ├── FishingUI.cs           # Fishing game UI manager
│       ├── LoadingUI.cs           # Loading screen
│       ├── CatchLogItem.cs        # Individual catch entry
│       └── BackToMenuButton.cs    # Return to menu
├── ScriptableObjects/
│   └── Fish/                      # Fish data assets
│       ├── GoldenMackerel.asset
│       ├── MajiliSnapper.asset
│       └── CoastalCatfish.asset
├── Scenes/
│   ├── MainMenu.unity
│   └── FishingGame.unity
├── Prefabs/                       # UI and game prefabs
├── Art/
│   ├── Sprites/                   # 2D sprites and icons
│   └── Textures/                  # Textures and backgrounds
└── Audio/
    ├── SFX/                       # Sound effects
    └── Music/                     # Background music
```

---

## 🎨 Assets Used

All assets are **free and properly licensed** for commercial use:

### UI Assets
- **Kenney UI Pack**: https://kenney-assets.itch.io/ui-pack
  - 400+ UI sprites in multiple colors
  - Buttons, panels, sliders, icons
  - License: CC0 (Public Domain)

### Fish & Ocean Sprites
- **CraftPix Free Fishing Game Assets**: https://craftpix.net/freebies/free-fishing-game-assets-pixel-art-pack/
  - Fish sprites, water tiles, fishing animations
  - License: Free for commercial use with attribution
  
- **OpenGameArt Fish Pack**: https://opengameart.org/content/fish-pack-0
  - Various fish and sea creatures
  - License: CC0 or OGA-BY 3.0

### Sound Effects
- **Pixabay Sound Effects**: https://pixabay.com/sound-effects/
  - Fishing reel: https://pixabay.com/sound-effects/fishing-reel-82246/
  - Water splash sounds
  - Success/fail jingles
  - License: Pixabay License (Free for commercial use)

### Fonts
- **Google Fonts** (if used): https://fonts.google.com/
  - License: Open Font License

---

## ✅ Requirements Checklist

### Main Menu (20 points)
- ✅ Play button with async scene loading
- ✅ Loading indicator (progress bar with percentage)
- ✅ Settings panel with volume slider
- ✅ Settings panel with quality dropdown
- ✅ Settings persist using PlayerPrefs
- ✅ Quit button (works in editor and build)
- ✅ Visual polish (background, title, hover effects)
- ✅ Clean Canvas hierarchy with proper anchoring

### Fishing Game - Mechanics (25 points)
- ✅ Complete gameplay loop (Idle → Waiting → Bite → Reeling → Result)
- ✅ Clear state machine implementation
- ✅ Random wait time (2-6 seconds)
- ✅ Reaction window with visual/audio cues
- ✅ Tension bar reel mechanic
- ✅ Success and failure conditions
- ✅ Return to idle after result

### Code Architecture (25 points)
- ✅ ScriptableObjects for fish data
- ✅ State Pattern for fishing states
- ✅ Strategy Pattern for reel mechanics
- ✅ Separation of concerns (managers, UI, data, states)
- ✅ SOLID principles throughout
- ✅ Clean folder structure
- ✅ Proper naming conventions

### UI and Feedback (15 points)
- ✅ Clear state communication (text updates)
- ✅ Color changes for different states
- ✅ Catch log with fish display
- ✅ Catch counter
- ✅ Responsive buttons
- ✅ Back to menu button

### Polish and Extras (10 points)
- ✅ Sound effects (cast, splash, bite, reel, success, fail)
- ✅ Audio manager with volume control
- ✅ State-based color feedback
- ✅ Smooth state transitions
- ⚠️ Particle effects (optional - can be added)
- ⚠️ Camera shake (optional - can be added)

### README and Submission (5 points)
- ✅ Clear README with architecture explanation
- ✅ Asset credits with links
- ✅ SOLID principles documented
- ✅ Known issues and improvements listed

---

## 🔧 Known Issues & Future Improvements

### Known Issues
1. **No particle effects yet**: Water splash and celebration particles not implemented
2. **No camera shake**: Would add juice to bite and catch moments
3. **Limited fish variety**: Only 3 fish types created (easy to add more)
4. **No background music**: Only SFX implemented
5. **No animations**: Character and bobber are static

### With More Time, I Would Add:
1. **Visual Polish**
   - Particle effects for water splash, bubbles, and catch celebration
   - Camera shake on bite and catch
   - Animated bobber floating on water
   - Character casting animation
   - Day/night cycle affecting fish spawns

2. **Gameplay Enhancements**
   - Multiple reel mechanics (mash-to-reel, timing rings)
   - Fish escape chance during reeling
   - Combo system for consecutive catches
   - Rare fish popup with special effects
   - Fishing spot selection
   - Bait system affecting spawn rates

3. **UI Improvements**
   - Fish encyclopedia/collection screen
   - Statistics (biggest catch, total caught, etc.)
   - Achievement system
   - Better catch log with scrolling
   - Tutorial overlay for first-time players

4. **Technical Improvements**
   - Object pooling for catch log items
   - Save/load system for catch history
   - Difficulty progression over time
   - More robust input system (new Input System)
   - Unit tests for state machine
   - Event system for decoupling

5. **Audio**
   - Background music with dynamic layers
   - Ambient ocean sounds
   - Rarity-specific catch jingles
   - Voice lines or text-to-speech feedback

---

## 🚀 Build Instructions

### Playing the Build
1. Navigate to the `Build/` folder
2. Run `RaahiFishing.exe` (Windows)
3. Enjoy!

### Building from Unity
1. Open project in Unity 2021.3+ (URP)
2. File → Build Settings
3. Add scenes in order:
   - MainMenu
   - FishingGame
4. Select Windows/Mac/Linux
5. Click "Build"

---

## 🎯 Design Decisions

### Why State Machine?
- **Clarity**: Each state is isolated and easy to understand
- **Debuggability**: Can log state transitions and inspect current state
- **Extensibility**: Adding new states doesn't affect existing ones
- **Testability**: Each state can be unit tested independently

### Why ScriptableObjects for Fish?
- **Data-driven**: Designers can create fish without code
- **Memory efficient**: Single instance shared across references
- **Inspector-friendly**: Easy to tweak values and see changes
- **Modular**: Fish data separate from game logic

### Why Dependency Injection?
- **Testability**: Easy to mock dependencies
- **Flexibility**: Can swap implementations
- **Decoupling**: Components don't know about concrete types

### Why Singleton for Managers?
- **Global access**: Needed across scenes
- **Single source of truth**: One audio manager, one scene loader
- **Persistence**: DontDestroyOnLoad for cross-scene functionality
- **Trade-off**: Acknowledged that singletons can make testing harder, but appropriate for managers in this scope

---

## 📝 Code Quality Notes

### What I'm Proud Of
- **Clean state machine**: Easy to read and extend
- **SOLID adherence**: Each class has one job
- **No magic numbers**: Constants and serialized fields
- **Proper namespaces**: Organized and collision-free
- **XML comments**: Every public class and interface documented
- **Consistent naming**: PascalCase for public, camelCase for private

### What Could Be Better
- **More interfaces**: Could abstract AudioManager and SceneLoader
- **Event system**: Reduce coupling between managers
- **Object pooling**: For catch log items
- **Unit tests**: Would add for state machine logic
- **More error handling**: Edge cases and null checks

---

## 🎓 Learning Outcomes

This project demonstrates:
- ✅ State Pattern implementation
- ✅ Strategy Pattern for swappable mechanics
- ✅ ScriptableObject data-driven design
- ✅ Async scene loading with progress
- ✅ PlayerPrefs persistence
- ✅ Clean architecture and SOLID principles
- ✅ Unity UI best practices
- ✅ Audio management
- ✅ Modular, extensible codebase

---

## 📧 Contact

**Developer**: [Your Name]
**Email**: [Your Email]
**GitHub**: [Your GitHub]
**Submission Date**: [Date]

---

## 📄 License

This project uses various free assets with their respective licenses (see Assets Used section).
Code is provided for assessment purposes for KALP Studio.

---

**Thank you for reviewing my submission! I'm excited to discuss the architecture and any improvements during the follow-up call.** 🎣
