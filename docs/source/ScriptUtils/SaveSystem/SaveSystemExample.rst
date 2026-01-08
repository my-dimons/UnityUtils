Save System Example
================
        
Example of a Save System
-------------

PlayerData
~~~~~~~~~~
:doc:`SaveData/SaveData`

Create a Serializable script that inherits :doc:`SaveData/SaveData`, and will be used to hold data across sessions. 
We will create instances of this script and serialize it to .json files.

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   using System;
   
   [Serializable]
   public class PlayerData : SaveData
   {
   	public float health;
   	public float name;
   }
   
SaveManager (SaveSlots)
~~~~~~~~~~~
:doc:`JsonSaveSystem`

:doc:`SaveSystemManager`

:doc:`SaveData/SaveSlot`

:doc:`SaveData/SaveData`

Create a script that will save and load data, and will initialize our data's to save to.
If we're going to use encryption (Makes it harder to edit save data), make sure to set the encryption key on the JsonSaveSystem.

We need to make a Dictionary with a string for the save slot name, and a :doc:`SaveData/SaveSlot` which will hold our :doc`SaveData`.
The script below is an example (with some helper functions) of a solid save system with save slots. 

If you don't want to use save slots, see the other SaveManager example

.. code:: csharp
   
   using System.Collections.Generic;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class SaveManager : MonoBehaviour
   {
       public static SaveManager Instance { get; private set; }

       public Dictionary<string, SaveSlot> saveSlots = new();

       public string activeSaveSlot;

       private readonly bool useEncryption = false;

       void Start()
       {
           InitializeData();

           saveSlots = GetAllSaveSlots();

           if (Instance == null) Instance = this; else Destroy(gameObject);
       }

       public void Save()
       {
            SaveSystemManager.SaveGame(saveSlots[activeSaveSlot].saveDatas);
       }

       public void Load()
       {
           SaveSystemManager.LoadGame(saveSlots[activeSaveSlot].saveDatas);
       }

       public void InitializeData()
       {   
           JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
       }

       public void CreateSaveSlot(string saveSlot)
       {
           // Check if save slot already exists
           if (SaveSlotExists(saveSlot))
           {
               Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
               return;
           }

           List<SaveData> data = new();

           // add save data to save slot
           string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json");
           data.Add(SaveSystemManager.CreateSaveData<GameData>(path, useEncryption));
           saveSlots.Add(saveSlot, new SaveSlot(saveSlot, data));
       }

       public void SetSaveSlot(string saveSlot)
       {
           if (saveSlots[saveSlot] != null)
               activeSaveSlot = saveSlot;
           else
               Debug.LogWarning("The save slot \"" + saveSlot + "\" is unavailable");
       }

       public Dictionary<string, SaveSlot> GetAllSaveSlots()
       {
           Dictionary<string, SaveSlot> loadedSaveSlots = SaveSystemManager.LoadAllSaveSlots(useEncryption);

           Debug.Log(loadedSaveSlots.Count + " save slots found.");

           foreach (var save in loadedSaveSlots)
           {
               Debug.Log($"SaveSlot: FileName: {save.Key}");
           }

           return loadedSaveSlots;
       }

       public bool SaveSlotExists(string saveSlot)
       {
           return saveSlots.ContainsKey(saveSlot);
       }
   }
   
SaveManager (No Save Slots)
~~~~~~~~~~~~~~~~~~~~~

.. code:: csharp
   
   using System.Collections.Generic;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class SaveManager : MonoBehaviour
   {
       public static SaveManager Instance { get; private set; }

       SaveSlot saveSlot;

       public string saveSlotName = "save"; 

       private readonly bool useEncryption = false;

       void Start()
       {
           InitializeData();

           saveSlot = GetSaveSlot();
           
           if (Instance == null) Instance = this; else Destroy(gameObject);
       }

       public void Save()
       {
            SaveSystemManager.SaveGame(saveSlot.saveDatas);
       }

       public void Load()
       {
           SaveSystemManager.LoadGame(saveSlot.saveDatas);
       }

       public void InitializeData()
       {   
           JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");

	List<SaveData> saveData = new();
	
        // add save data to save slot
           string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlotName, "game_save.json");
           saveData.Add(SaveSystemManager.CreateSaveData<GameData>(path, useEncryption));
           
           saveSlot = new(saveSlotName, saveData);
           
       }

       public SaveSlot GetSaveSlot()
       {
           Dictionary<string, SaveSlot> loadedSaveSlots = SaveSystemManager.LoadAllSaveSlots(useEncryption);

           if (loadedSaveSlots.ContainsKey(saveSlotName))
           {
              return loadedSaveSlots[saveSlotName];
           }

           return null;
       }
   }
   
Player
~~~~~~
:doc:`Interfaces/ISaveableData`

To load and save the PlayerData, make a script that inherits :doc:`Interfaces/ISaveableData`.
In this script create a SaveData<T>(T) and LoadData<T>(T) function, these functions will be called by :doc:`SaveSystemManager` when loading and saving data.

In the Save/Load function make sure to check if the given data is the proper data you need to receive, then set all the needed variables.

.. code:: csharp
  
   using TMPro;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class Player : MonoBehaviour, ISaveableData
   {
       public int health = 2;
       public string name = "John";

       public void SaveData<T>(T data) where T : SaveData
       {
           // Check if the data is the correct type
           if (data is PlayerData save)
           {
               save.health = health;
               save.name = name;
           }
       }

       public void LoadData<T>(T data) where T : SaveData
       {
           // Check if the data is the correct type
           if (data is PlayerData save)
           {
               health = save.health;
               name = save.name;
           }
       }
   }
   
