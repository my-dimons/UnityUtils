Save System Example
================
        
Example of a Save System
-------------

PlayerData
~~~~~~~~~~
:doc:`SaveData/SaveData`

.. note::

	Some data types cannot be stored. Instances of scripts CAN be saved, but may run into recursion errors depending on what the script is referencing. If you want to be safe, only use primitive types.
	
	
Create a Serializable script that inherits :doc:`SaveData/SaveData`. This will be used to hold data across sessions. 
We will create instances of this script and serialize it to .json files later.

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   using System;
   
   [Serializable]
   public class PlayerData : SaveData
   {
   	public float health;
   	public float name;
   	public float[3] playerPosition = new float[3]; // Vector3, using a normal "Vector3" will run into recursion errors
   }
   
SaveManager (Simple)
~~~~~~~~~~~
:doc:`JsonSaveSystem`

:doc:`SaveSystemManager`

:doc:`SaveData/SaveSlot`

:doc:`SaveData/SaveData`

Create a script that will save and load data, and will initialize our :doc:`SaveData/SaveSlot`'s to save to (Which hold a list of :doc:`SaveData/SaveData`'s).
If we're going to use encryption (Makes it harder to edit save data), make sure to set the encryption key on the JsonSaveSystem.

We will make a dictionary with a string for the save slot name, and a :doc:`SaveData/SaveSlot` which will hold our :doc:`SaveData`.
The script below is an example (with some helper functions) of a solid save system with save slots. 

.. note::

	You do not need to use a dictionary to save the slots, but in this example it makes it easier to get the save slots.
	
If you don't want to use save slots, just use a singular slot name

This is a simple example of a SaveManager, to see a more complicated version view the bottom of this script

.. code:: csharp
   
   using System.Collections.Generic;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class SaveManager : MonoBehaviour
   {
       public static SaveManager Instance { get; private set; }

       public Dictionary<string, SaveSlot> saveSlots = new();

       public string activeSaveSlot;

       void Start()
       {
           InitializeData();

           saveSlots = SaveSystemManager.LoadAllSaveSlots();

           CreateSaveSlot("save_0");
           CreateSaveSlot("save_1");
           CreateSaveSlot("save_2");

           if (Instance == null) Instance = this; else Destroy(gameObject);
       }

       public void Save()
       {
           SaveSystemManager.SaveGame(saveSlots[activeSaveSlot]);
       }

       public void Load()
       {
           SaveSystemManager.LoadGame(saveSlots[activeSaveSlot]);
       }
       
       public void Delete()
       {
       	SaveSystemManager.DeleteSaveSlot(saveSlots[activeSaveSlot]);
       	saveSlots.Remove(activeSaveSlot);
       }

       public void InitializeData()
       {
           // Encryption
           JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
           JsonSaveSystem.UseEncryption(false);
           
           // Debug.Logs()
           JsonSaveSystem.outputLogs = false;
           SaveSystemManager.outputLogs = true;
       }

       public void CreateSaveSlot(string saveSlot)
       {
           // Check if save slot already exists
           if (saveSlots.ContainsKey(saveSlot))
           {
               Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
               return;
           }

           SaveSlot saveSlotObj = new(saveSlot);

           // add save data to save slot
           string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json");
           saveSlotObj.AddSaveData(SaveSystemManager.CreateSaveData<GameData>(path));

           saveSlots.Add(saveSlot, saveSlotObj);
       }
   }
   
Player
~~~~~~

:doc:`SaveData/ISaveableData`

To load and save the PlayerData script we made, make a script that inherits :doc:`SaveData/ISaveableData`.
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
               
               // (Will have a better way to do this soon, check the future plans on the main page)
               save.playerPosition[0] = transform.position.x;
               save.playerPosition[1] = transform.position.y;
               save.playerPosition[2] = transform.position.z;
           }
       }

       public void LoadData<T>(T data) where T : SaveData
       {
           // Check if the data is the correct type
           if (data is PlayerData save)
           {
               health = save.health;
               name = save.name;
               transform.position = new Vector3(save.playerPosition[0], save.playerPosition[1], save.playerPosition[2]);
           }
       }
   }
   
   
   
SaveManager (Complex)
~~~~~~~~~~~~~~~~~~~~~

A more complex version of the simple SaveManager from above. This SaveManager has many more functions that allow for better use

.. code:: csharp
     
	using System.Collections.Generic;
	using UnityEngine;
	using UnityUtils.ScriptUtils.SaveSystem;
	
	public class SaveManager : MonoBehaviour
	{
	    public static SaveManager Instance { get; private set; }
	
	    public Dictionary<string, SaveSlot> saveSlots = new();
	
	    public string activeSaveSlot;
	
	    void Start()
	    {
	        InitializeData();
	
	        saveSlots = GetAllSaveSlots();
	
	        CreateSaveSlot("save_0");
	        CreateSaveSlot("save_1");
	        CreateSaveSlot("save_2");
	
	        if (Instance == null) Instance = this; else Destroy(gameObject);
	    }
	
	    public void Save()
	    {
	        SaveSystemManager.SaveGame(saveSlots[activeSaveSlot]);
	    }
	
	    public void Load()
	    {
	        SaveSystemManager.LoadGame(saveSlots[activeSaveSlot]);
	        
	        // Make a list of save slots to get the most recent save
	        List<SaveSlot> saves = new();
	        foreach (var slot in saveSlots.Values)
	            saves.Add(slot);
	
	        Debug.Log("Most recently saved file: " + SaveSystemManager.GetMostRecentSave(saves).saveSlotName);
	    }
	    
	    public void Delete()
	    {
 		   if (SaveSlotExists(activeSaveSlot)
 		   {
 		       SaveSystemManager.DeleteSaveSlot(saveSlots[activeSaveSlot]);
 		       saveSlots.Remove(activeSaveSlot)
 		   }	  
	    }
	
	    public void InitializeData()
	    {
	        // Encryption
	        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
	        JsonSaveSystem.UseEncryption(false);
			
	        // Debug.Logs()
	        JsonSaveSystem.outputLogs = true;
	        SaveSystemManager.outputLogs = true;
	    }
	
	    public void CreateSaveSlot(string saveSlot)
	    {
	        // Check if save slot already exists
	        if (SaveSlotExists(saveSlot))
	        {
	            Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
	            return;
	        }
	
	        SaveSlot saveSlotObj = new(saveSlot);
	
	        // add save data to save slot
	        string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json");
	        saveSlotObj.AddSaveData(SaveSystemManager.CreateSaveData<GameData>(path));
	
	        saveSlots.Add(saveSlot, saveSlotObj);
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
	        Dictionary<string, SaveSlot> loadedSaveSlots = SaveSystemManager.LoadAllSaveSlots();
	
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
