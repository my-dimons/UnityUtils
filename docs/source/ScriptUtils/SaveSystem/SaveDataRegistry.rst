SaveDataRegistry
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveDataRegistry** script is used in turn with other save systems to assist with save systems. This script is used for creating and registering :doc:`SaveDataID`'s, and keeps track of all of the current :doc:`SaveDataID`'s

.. tip::

   You can create subdirectories like this: Path.Combine("saves", "game_data.json"), just make sure you're **using System;**. This comes in handy for save slots.

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {   	
   	void Start()
   	{
   	   string fileName = "game_data.json";
   	   bool useEncryption = true;
   	   
   	   // Create a new GameData (example data class) and register it to the registry
   	   SaveDataID saveDataID = SaveDataRegistry.Register<GameData>(filename, useEncryption);
   	   
   	   // Get the ID
   	   string id = saveDataID.id;
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveDataRegistry
   :members: