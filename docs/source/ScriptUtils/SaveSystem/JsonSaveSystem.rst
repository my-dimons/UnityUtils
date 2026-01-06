JsonSaveSystem
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **JsonSaveSystem** script is used in turn with other save systems to assist with save systems. This script writes and loads data from json files

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {
   	List<SaveDataID> saveDataIDs = new();
   	
   	void Start()
   	{
   	   // Serializes all the save files
   	   for (SaveDataID dataID in saveDataID)
   	   {
   	      JsonSaveSystem.Save(dataID);
   	   }
   	   
   	   // Deserializes all the save files back into their ISaveData counterparts
   	   List<ISaveData> loadedData = JsonSaveSystem.Load(saveDataIDs);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::JsonSaveSystem
   :members: