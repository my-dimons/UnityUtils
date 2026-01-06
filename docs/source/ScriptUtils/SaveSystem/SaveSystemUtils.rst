SaveDataRegistry
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
   	   // Registers a GameData (example script) into the registry 
   	   SaveDataID gameData = SaveDataRegistry.Register<GameData>("GameData", "game_save.json")
      }
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveDataRegistry
   :members: