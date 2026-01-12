## v1.3.0

### Additions
- UIButtonQuitGame
- ObjectModifiers
- ObjectModifierData
- StopScreenshake(Transform camera) method to CameraShake.cs
- IsScreenshaking(Transform camera) method to CameraShake.cs
- Link to plans.rst on docs
- Save System
    - ISaveableData
    - SaveData
    - SaveSlot
    - SaveSlotSaveData
    - SaveSystemManager
    - SaveSystemUtils
    - JsonSaveSystem

### Fixes
- Fixed ObjectDelays useRealtime (Wasn't waiting in realtime)
- Fixed camera shake from ending in an improper position
- Fixed package dependencies not properly getting recognized (I just forgot to make sure they were required)
- Changed docs theme

### Changes
- Changed Company name from "DefaultCompany" -> "mydimons"
- Changed Player Settings project name from "Examples" -> "UnityUtils"

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