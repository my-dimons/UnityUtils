SaveSystemUtils
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **SaveSystemUtils** script is used in turn with other save systems to assist with save systems. This script is mostly only used in official save system scripts, but could find use in other scripts

Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class ExampleScript : MonoBehaviour
   {   	
   	void Start()
   	{
   	   // Gets the global path to the save file "filename.save" in the save slot "0"
   	   string path = SaveSystemUtils.GetSaveSlotFilePath("0", "filename.json");
   	   
   	   // Gets the save slot root folder
   	   string saveSlotRoot = SaveSystemUtils.GetSaveSlotRootPath();
   	}
   }

Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::SaveSystemUtils
   :members: