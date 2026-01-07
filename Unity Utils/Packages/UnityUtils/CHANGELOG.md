## v1.3.0

### Additions
- UIButtonQuitGame
- ObjectModifiers
- ObjectModifierData
- StopScreenshake(Transform camera) method to CameraShake.cs
- IsScreenshaking(Transform camera) method to CameraShake.cs
- Save System
    - ISaveData
    - ISaveableData
    - SaveDataID
    - SaveDataRegistry
    - SaveSystemManager
    - SaveSystemUtils
    - JsonSaveSystem

### Changes
- Fixed camera shake from ending in an improper position

## v1.2.0

### Additions
- ObjectDelays.cs
- "Objects" folder for ObjectAnimations and ObjectDelays
- UI Button animations have a variable for applied transform (Defaults to itself)
- More Debug.Logs() in button scripts
- ObjectColorFlash.cs
- Object Animation for UI Canvas group alpha
- CameraShake.cs
- CameraBillboard.cs
- UI Buttons
    - UIButtonHoverPosition.cs
    - UIButtonHoverDebugLogs.cs
    - UIButtonSpawnParticles.cs
    - UIButtonSceneSwitcher.cs
    - UIButtonToggleObjects.cs


### Changes
- CoroutineStarter renamed to "CoroutineHelper"
- Renamed docs files to be more like the C# scripts
- Many msc code and doc improvements
- Seperated ParticleManager into "ParticleSpawner" and "ParticleModifier"