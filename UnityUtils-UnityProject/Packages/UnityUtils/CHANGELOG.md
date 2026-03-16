## v1.3.1 - General Cleanup Before v1.4.0

### Additions
- Contributing guide
- ObjectColorFlash can now use materials while flashing
- ObjectColorFlash has a static function to get either an unlit or lit material (prebuilt in)
- ObjectColorFlash IsFlashing() function which returns a bool
- ObjectColorFlash now only sets the changed material color and not both the material and the sprite renderers color
- ObjectDelay now has a DelayFrame() function that calls an action after a frame
- New Modifier Types
	- Root
	- Exponent
- Divide modifier value of 0 is now ignored instead of giving an error

### Fixes
- Fixed UIObjectToggleObjects Debug.Log()

### Changes
- Renamed the Unity Project from "Unity-Utils" to "UnityUtils-UnityProject"
- Renamed the GitHub repo from "Unity-Utils" to "UnityUtils"
- Formatted all scripts to have 2 tab spaces instead of 4
- Removed many ObjectDelay functions, now substituted for ObjectDelay.Delay()
- Renamed ObjectDelay's main delay function from "CallFunctionAfterTime" -> "Delay"

## v1.3.0 - Save System + Other

### Additions
- UIButtonQuitGame
- ObjectModifiers
- ObjectModifierData
- StopScreenshake(Transform camera) method to CameraShake.cs
- IsScreenshaking(Transform camera) method to CameraShake.cs
- Link to plans.rst on docs
- TODO GitHub Kanban (https://github.com/users/my-dimons/projects/2)
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

### Changes
- Changed Company name from "DefaultCompany" -> "mydimons"
- Changed Player Settings project name from "Examples" -> "UnityUtils"
- Changed docs theme

## v1.2.0 - UI Buttons + Other

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
