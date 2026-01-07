JsonSaveSystem
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **JsonSaveSystem** script is used in turn with other save systems to assist with save systems. This script writes and loads data from json files

.. tip::

   To create a good encryption key, just spam your keyboard and make it decently long. (Ex. "]\fdS()fv.cx?Fjh*()S)(12fjdPGIO()GjvnFLKGj")
   
   
.. warning::

   Do not share your encryption key, otherwise it will be incredibly easy to decrypt and encrypt save files

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
   	   // Set encryption key for encrypting data (DO NOT SHARE THIS KEY ANYWHERE)
   	   JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
   	   
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