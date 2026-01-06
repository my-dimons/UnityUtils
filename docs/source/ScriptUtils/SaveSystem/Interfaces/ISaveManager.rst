ISaveManager
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **ISaveManager** is used in turn with other save systems. **ISaveManager** Is inherited from files that will manage the save systems
   
Example Usage
-------------
.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class SaveManager : MonoBehaviour, ISaveManager
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
           // Data string variables
           string uniqueID = "GameData";
           string fileName = "game_save.json";
           
           // Register a GameData (example class inheriting ISaveData)
           SaveDataID gameData = SaveDataRegistry.Register<GameData>(uniqueID, fileName);

		// Add it into the saveFiles list
           saveFiles.Add(gameData);
       }
   }