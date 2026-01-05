BinarySaveSystem 
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **BinarySaveSystem** script is used in turn with scripts like :doc:`ISaveData`. This save system encrypts save files into binary files, when loaded, save's are decrypted.
   
Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class Player
   {
   	public float health;
   	public float level;
   	
   	string fileName = "player";
   	
   	public void Save()
   	{
   	   // Creates the save file
   	   BinarySaveSystem.Save<PlayerSaveData, Player>(this, fileName, input => new Player(input));
   	}
   	
   	public void Load()
   	{
   	   PlayerSaveData data = BinarySaveSystem.Load<PlayerSaveData>(fileName);
   	   
   	   health = data.health;
   	   level = data.level;
   	}
   }   
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::BinarySaveSystem 
   :members: