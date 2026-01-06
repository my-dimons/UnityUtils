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

       /// A list with save file IDs and file names
       Dictionary<string, string> saveFiles = new();

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
           ISaveData gameData = SaveDataRegistry.CreateAndRegister<GameData>();
           string id = SaveDataRegistry.GetID(gameData);
           string fileName = "game_save"
           saveFiles.Add(id, fileName);
       }
   }
   
.. doxygenclass:: ISaveManager
   :namespace: UnityUtils::ScriptUtils::SaveSystem::ISaveManager
   :members: