Save System Example
================
        
Example of a Save System
-------------

:doc:`Interfaces/ISaveData`

Create a Serializable script that inherits :doc:`Interfaces/ISaveData`, and will be used to hold data across sessions. 
We will create instances of this script and serialize it to .json files.

PlayerData:

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   using System;
   
   [Serializable]
   public class PlayerData : ISaveData
   {
   	public float health;
   	public float name;
   }
   

:doc:`JsonSaveSystem`

:doc:`SaveDataID`

:doc:`SaveDataRegistry`

Create a script that will save and load data, and will initialize our data's to save to.
If we're going to use encryption (Makes it harder to edit save data), make sure to set the encryption key.

Create an instance of PlayerData and give it a file name and a bool, with an optional ID (Will default to the file name if not provided).
Lastly, add the returned :doc:`SaveDataID`and save it to a List<:doc:`SaveDataID`> and pass that list into :doc:`SaveSystemManager`.SaveGame() and :doc:`SaveSystemManager`.LoadGame().

SaveManager:

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class SaveManager : MonoBehaviour
   {
       public static SaveManager Instance { get; private set; }

       List<SaveDataID> saveFiles = new();

       void Start()
       {
           InitializeData();

           if (Instance == null) Instance = this; else Destroy(gameObject);
       }

       public void Save()
       {
           SaveSystemManager.SaveGame(saveFiles);
       }
  
       public void Load()
       {
           SaveSystemManager.LoadGame(saveFiles);
       }
   
       public void InitializeData()
       {
           // Data variables
           string fileName = "game_save.json";
           bool useEncryption = true;
           
           // For the encryptionKey, look at the JsonSaveSystem docs for more information
           string encryptionKey = "EncryptionKeyHere";
           
           // If you're using encryption, make sure to set the key like so:
           JsonSaveSystem.SetEncryptionKey(encryptionKey);
           
           // Register a PlayerData into the registry
           SaveDataID playerData = SaveDataRegistry.Register<PlayerData>(fileName, useEncryption);

		// Add it into the saveFiles list
           saveFiles.Add(playerData);
       }
   }

:doc:`Interfaces/ISaveableData`

To load and save the PlayerData, make a script that inherits :doc:`Interfaces/ISaveableData`.
In this script create a SaveData<T>(T) and LoadData<T>(T) function, these functions will be called by :doc:`SaveSystemManager` when loading and saving data.

In the Save/Load function make sure to check if the given data is the proper data you need to receive, then set all the needed variables.

Player:

.. code:: csharp
  
   using TMPro;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class Player : MonoBehaviour, ISaveableData
   {
       public int health = 2;
       public string name = "John";

       public void SaveData<T>(T data) where T : ISaveData
       {
           // Check if the data is the correct type
           if (data is PlayerData save)
           {
               save.health = health;
               save.name = name;
           }
       }

       public void LoadData<T>(T data) where T : ISaveData
       {
           // Check if the data is the correct type
           if (data is PlayerData save)
           {
               health = save.health;
               name = save.name;
           }
       }
   }
   
   
SaveManager with Save Slots
--------------------------

[WIP]