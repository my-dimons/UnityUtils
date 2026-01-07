SaveSystemRegistry
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveDataRegistry** script is used in turn with other save systems to assist with save systems. This script is used for creating and registering :doc:`SaveDataID`'s, and keeps track of all of the current :doc:`SaveDataID`'s

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
   	   bool useEncryption = true;
   	   
   	   // Create a new GameData (example data class) and register it to the registry
   	   SaveDataID saveDataID = SaveDataRegistry.Register<GameData>(uniqueID, filename, useEncryption);
   	   
   	   // Get the ID
   	   string id = saveDataID.id;
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveSystemRegistry
   :members: