SaveSystemRegistry
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveSystemRegistry** script is used in turn with other save systems to assist with save systems. This script is used to store :doc:`ISaveData`'s via an ID system

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {   	
   	void Start()
   	{
   	   // Create a new PlayerData (Player save data class) and register it to the registry
   	   ISaveData gameData = SaveDataRegistry.CreateAndRegister<GameData>();
   	   
   	   // Get the ID
   	   string id = SaveDataRegistry.GetID(gameData);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveSystemRegistry
   :members: