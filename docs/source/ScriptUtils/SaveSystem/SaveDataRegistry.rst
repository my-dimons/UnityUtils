SaveSystemRegistry
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveSystemRegistry** script is used in turn with other save systems to assist with save systems. This script is used to store :doc:`ISaveData`'s via an ID system, data is stored in :doc:`SaveDataID`.

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {   	
   	void Start()
   	{
   	   string uniqueID = "GameData";
   	   string fileName = "game_data.json";
   	   
   	   // Create a new GameData (example data class) and register it to the registry
   	   SaveDataID saveDataID = SaveDataRegistry.Register<GameData>(uniqueID, fileName);
   	   
   	   // Get the ID
   	   string id = saveDataID.id;
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveSystemRegistry
   :members: