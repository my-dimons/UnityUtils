Save System Example
================
        
Example of a Save System
-------------

(Holds data to be stored across sessions)

:doc:`ISaveData`

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
   
(Handles saving, loading, and creating the data)

:doc:`JsonSaveSystem`

:doc:`SaveDataID`

:doc:`SaveDataRegistry`

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
           string uniqueID = "GameData";
           string fileName = "game_save.json";
           bool useEncryption = true;
           
           // For the encryptionKey, look at the JsonSaveSystem docs for more information
           string encryptionKey = "EncryptionKeyHere";
           
           // If you're using encryption, make sure to set the key like so:
           JsonSaveSystem.SetEncryptionKey(encryptionKey);
           
           // Register a PlayerData into the registry
           SaveDataID playerData = SaveDataRegistry.Register<PlayerData>(uniqueID, fileName, useEncryption);

		// Add it into the saveFiles list
           saveFiles.Add(playerData);
       }
   }

(Actually loading and saving data into script)

:doc:`ISaveableData`

Player:

.. code:: csharp
  
   using TMPro;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class Player : MonoBehaviour, :doc:ISaveableData`
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