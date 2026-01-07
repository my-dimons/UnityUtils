SaveSystemManager
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveSystemManager** script is used in turn with other save systems to assist with save systems. This script is used to call the Save() and Load() functions in all scripts inheriting :doc:`ISaveableData`

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {
   	// Create a save slot with the example type "GameData", without encrpytion
   	SaveSlot saveFiles = new SaveSlot("save", SaveSystemManager.CreateSaveData<GameData>(SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json"), useEncryption)
   	
   	void Start()
   	{
   	   // Calls all the ISaveableData objects and their Save() function
   	   SaveSystemManager.SaveGame(saveFiles);
   	   
   	   // Calls all the ISaveableData objects and their Load() function
   	   SaveSystemManager.LoadGame(saveFiles);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveSystemManager
   :members: