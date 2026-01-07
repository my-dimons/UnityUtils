SaveData
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveData** is used in turn with other save systems. **SaveData** Is inherited from files that hold save data for other scripts. 

.. warning ::

   Any script inheriting SaveData **must** include [System.Serializable] above the class declaration.
   
Example Usage
-------------
.. code:: csharp

   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   [System.Serializable]
   public class PlayerData : SaveData
   {
   	public float health;
   	public string name;
   }

Functions
--------

 .. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveData
   :members:
