## v1.3.4 - Finally Fixing ObjectColorFlash (I hope)

### Additions
- ObjectColorFlash SetOriginalMaterial(Material) function

### Fixes
- ObjectColorFlash's Flash() function now ignores sprite renderers color (properly overriding color)
- ObjectColorFlash now gets its original material on Start() instead of when flashing. This fixes an error where when flashing a ObjectColorFlash that is already flashing it would stay stuck in the flash material
- Updated ObjectColorFlash's documentation
- Update documentation input folder from "Packages/UnityUtils" -> "Packages/com.mydimons.unityutils" to fix documentation generation

## v1.3.3 - BUG FIX

### Fixes
- Fixed ObjectColorFlash's material making sprites become 'low poly', now uses a shader graph that properly overrides the sprites color. More functionality is coming in v1.4.0 for damage flashing!

### Changes
- ObjectColorFlash's Flash() function now returns a coroutine, so it's possible to stop it if needed.

## v1.3.2 - Msc Fixes

### Additions
- ObjectColorFLash.FlashWhite() overload that only takes in duration

### Fixes
- Fixed Installation guide's git url
- Add new package with new name to Unity project
- Fixed v1.3.1 additions changelog (Forgot to add ObjectColorFlash overloads, but is now all removed soooo...)
- Cleaned up ObjectColorFlash's documentation
- Removed unnecessary Material duplications in ObjectColorFlash 

### Changes
- ObjectColorFlash renamed all FlashColor() and FlashWhite() functions to Flash()
- ObjectColorFlash default parameters can now be edited in the applied script's inspector
- Removed ObjectColorFlash ColorFlashMaterial Enum (now only uses unlit material)
- ObjectColorFlash removed all overloads and just made the parameters have a default value
- Renamed Unity project from "UnityUtils-UnityProject" to "UnityUtils"

## v1.3.1 - General Cleanup Before v1.4.0

### Additions
- Contributing guide
- ObjectColorFlash can now use materials while flashing
- ObjectColorFlash has a static function to get either an unlit or lit material (prebuilt in)
- ObjectColorFlash now has many overloaded functions for more usage
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
