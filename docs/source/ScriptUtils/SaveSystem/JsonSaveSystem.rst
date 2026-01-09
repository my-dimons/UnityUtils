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
   	List<SaveSlot> saveDatas = new();
   	
   	void Start()
   	{
   	   // Set encryption key for encrypting data (DO NOT SHARE THIS KEY ANYWHERE)
   	   JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
   	   
   	   // Use the encryption key
   	   JsonSaveSystem.UseEncryption(true);
   	   
   	   // Serializes all the save slots, but note: do not use this, call SaveSystemManager instead to properly save and load data to ISaveable's
   	   for (SaveSlot data in saveDatas)
   	   {
   	      JsonSaveSystem.Save(data);
   	   }
   	   
   	   // Deserializes all the save files back into their SaveSlot counterparts
   	   List<SaveSlot> loadedData = JsonSaveSystem.Load(saveDatas);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::SaveSystem::JsonSaveSystem
   :members: