ISaveData
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **ISaveData** is used in turn with other save systems. **ISaveData** Is inherited from files that hold save data for other scripts. 

.. warning ::

   Any script inheriting ISaveData **must** include [System.Serializable] above the class declaration.
   
Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   [System.Serializable]
   public class PlayerSaveData : ISaveData
   {
   	public float health;
   	public float level;
   	
   	// Constructor for saving data (Player is an example script)
   	public PlayerSaveData(Player player)
   	{
   	   health = player.health;
   	   level = player.level;
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::ISaveData
   :members: