# Unity Game Utilities

A comprehensive collection of reusable Unity utilities and components designed to accelerate game development. This library provides common gameplay patterns, UI management, audio control, debugging tools, and helper functions to streamline your Unity projects.

## Overview

**Unity Game Utilities** (`Kalkatos.UnityGame`) is a modular toolkit that provides battle-tested solutions for common Unity game development challenges. Whether you're building a 2D platformer, 3D adventure, or UI-heavy application, these utilities help you focus on your game's unique features rather than reinventing the wheel.

## Key Features

### 🎵 Audio System
- **AudioController**: Centralized audio management with separate music and SFX channels
- Music playback with fade in/out support
- Sound effects with volume control, pitch variation, and clip variations
- Master volume controls for music and SFX independently
- Queue-based SFX channel management for optimal performance

### 🎨 UI Components
- **BaseScreen**: Animated screen transitions with DOTween integration
- **InfoBox**: Dynamic UI data binding system with type-safe component updates
- **SelectableImage** & **SelectableImageGrid**: Interactive image selection with customizable states
- **GradientImage**: Custom gradient rendering for UI elements
- **UiPanelPresenter**: Flexible panel presentation system

### 🎮 Event System
- **TimedEvent**: Flexible timer system with support for:
  - Single and sequential events
  - Looping with customizable loop counts
  - Update callbacks with normalized time values
  - Chained event sequences
- **UnityEventCaller**: Inspector-friendly event invocation
- **StartCaller**: Automatic event triggering on Start
- **AnimationEventCaller**: Bridge between Unity animations and UnityEvents
- **ObjectCluster**: Manage groups of objects with unified enable/disable

### 🔧 Helper Utilities
- **SingletonObject**: MonoBehaviour singleton pattern implementation
- **DontDestroyOnLoad**: Persist objects across scene transitions
- **MonoExtensions**: Rich set of extension methods including:
  - Coroutine-based Wait/WaitFrames helpers
  - Layer manipulation utilities
  - Animation curve tangent calculations
  - List shuffling
  - Vector2 to Vector3 conversions
  - Sprite and Image fading with DOTween
- **SerializableDictionary**: Inspector-friendly dictionary implementation
- **VersionText**: Automatic version display in UI
- **Quitter**: Simple application quit helper

### 📦 Data Management
- **ScriptableObjectContainer**: Organize and manage ScriptableObjects with editor utilities
- **SingletonScriptableObject**: ScriptableObject singleton pattern
- **TypedScriptableObjectContainer**: Type-safe ScriptableObject containers
- **SpriteListScriptable**: Manage sprite collections
- **Value Getters**: Flexible value retrieval system supporting:
  - Simple values
  - Random ranges
  - ScriptableObject references
  - Types: Float, Int, String, Bool

### 🐛 Debug Tools
- **DebugCommands**: In-game debug console with command registration
- **DebugOpener**: Toggle debug UI with keyboard shortcuts
- **DebugOnlyObject**: Objects that only exist in debug builds
- **CanvasLimiter**: Frame rate limiting for canvas rendering

### 🎨 Sprite Utilities
- **SpriteColorChanger**: Runtime sprite color manipulation with save/restore functionality

### 🛠️ Editor Tools
- **FindReferencesInProject**: Quickly locate all references to selected assets in your project

## Installation

### Unity Package Manager (Recommended)
1. Open the Unity Package Manager (Window > Package Manager)
2. Click the "+" button and select "Add package from git URL"
3. Enter: `https://github.com/kalkatos/unitygame.git`

### Manual Installation
1. Clone or download this repository
2. Copy the contents into your Unity project's Assets folder

## Dependencies

This library integrates with popular Unity packages:
- **DOTween**: Required for smooth animations and transitions in UI (BaseScreen) and sprite utilities (MonoExtensions fading)
- **Odin Inspector** (optional): Enhanced inspector experience with conditional fields
- **TextMeshPro**: For advanced text rendering in UI components (InfoBox, DebugCommands)

> **Note**: Code sections using optional dependencies are wrapped in conditional compilation directives (`#if ODIN_INSPECTOR`) and will gracefully degrade if not present. DOTween is required for animation features.

## Usage Examples

### Audio System
```csharp
using Kalkatos.UnityGame.Audio;

// Play background music with fade in
AudioController.PlayMusic(backgroundMusic, fadeInTime: 2f);

// Play sound effect
AudioController.PlaySfx(jumpSound);

// Stop music with fade out
AudioController.StopMusic(fadeOutTime: 1.5f);

// Adjust volumes
AudioController.SetMasterMusicVolume(0.7f);
AudioController.SetMasterSfxVolume(0.8f);
```

### Timed Events
```csharp
using Kalkatos.UnityGame;

// Create a simple timer
TimedEvent.Create(3f, onComplete);

// Use TimedEvent component for complex sequences
public class GameManager : MonoBehaviour
{
    [SerializeField] private TimedEvent countdownTimer;
    
    void StartCountdown()
    {
        countdownTimer.Rewind();
        countdownTimer.StartTimer();
    }
}
```

### UI Screens with Animations
```csharp
using Kalkatos.UnityGame;

public class MenuScreen : BaseScreen
{
    public override void Open()
    {
        base.Open(); // Handles fade in and scale animation
        // Add custom logic here
    }
    
    public override void Close()
    {
        base.Close(); // Handles fade out and scale animation
        // Add custom logic here
    }
}
```

### Value Getters
```csharp
using Kalkatos.UnityGame;

public class Enemy : MonoBehaviour
{
    [SerializeField] private FloatValueGetter health;
    [SerializeField] private FloatValueGetter damage;
    
    void Start()
    {
        // Values can be simple, random ranges, or ScriptableObject references
        float currentHealth = health.GetValue();
        float attackDamage = damage.GetValue(); // Random if configured as range
    }
}
```

### Info Box Data Binding
```csharp
using Kalkatos.UnityGame;

public class PlayerData : ScriptableObject, IInfoProvider
{
    public string playerName;
    public int score;
    public Sprite avatar;
    
    public Dictionary<string, object> GetInfo()
    {
        return new Dictionary<string, object>
        {
            { "name", playerName },
            { "score", score },
            { "avatar", avatar }
        };
    }
}

// InfoBox will automatically bind data to UI components based on keys
```

### Extension Methods
```csharp
using Kalkatos.UnityGame;

// Wait helper
this.Wait(2f, () => Debug.Log("2 seconds passed"));

// Wait frames
this.WaitFrames(5, () => Debug.Log("5 frames passed"));

// Set layer recursively
gameObject.SetLayer(LayerMask.NameToLayer("UI"));

// Shuffle a list
myList.Shuffle();

// Fade sprite
spriteRenderer.Fade(time: 1f, from: 0f, to: 1f);
```

## Project Structure

```
Kalkatos.UnityGame/
├── Audio/              # Audio management and playback
├── Data/               # ScriptableObject utilities and containers
├── Debug/              # Debug and development tools
├── Editor/             # Unity Editor extensions
├── Event/              # Event system and timed events
├── Helper/             # General utility scripts and extensions
├── Sprite/             # Sprite manipulation utilities
├── UI/                 # UI components and management
└── ValueGetter/        # Flexible value retrieval system
```

## Namespace

All scripts are organized under the `Kalkatos.UnityGame` namespace (with sub-namespaces like `Kalkatos.UnityGame.Audio` for audio components).

## Contributing

Contributions are welcome! Please feel free to submit issues, fork the repository, and create pull requests for bug fixes or new features.

## License

This project is licensed under the MIT License - see the [LICENSE.MD](LICENSE.MD) file for details.

Copyright (c) 2023 Alex Kalkatos

## Best Practices

- Use **AudioController** as a singleton for consistent audio management across scenes
- Leverage **ValueGetters** for designer-friendly value configuration in the inspector
- Utilize **TimedEvent** for any time-based gameplay mechanics
- Implement **IInfoProvider** and **IInfoReceiver** for clean UI data binding
- Use **BaseScreen** as a foundation for all your UI screens to maintain consistent animations
- Take advantage of **MonoExtensions** to write cleaner, more readable code

## Support

For questions, issues, or feature requests, please visit the [GitHub repository](https://github.com/kalkatos/unitygame).

---

**Happy Game Development! 🎮✨**
