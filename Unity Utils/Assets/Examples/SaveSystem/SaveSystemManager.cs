using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;

public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    private void Start()
    {

    }

    public void SaveGame(Dictionary<ISaveData, string> saveData, List<ISaveableData> saveableData)
    {
        List<ISaveData> saveDataList = saveData.Keys.ToList();

        // Save data for each save data type
        foreach (ISaveData data in saveDataList)
        {
            foreach (ISaveableData saveable in saveableData)
            {
                saveable.SaveData(data);
            }
        }

        foreach (var data in saveData)
        {
            JsonSaveSystem.Save(data.Key, data.Value);

            SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveData[data.Key], SaveSystemConfig.JSON_SAVE_FILE_EXTENSION));
        }
    }

    public void LoadGame(Dictionary<ISaveData, string> saveDataDictionary, List<ISaveableData> saveableData)
    {
        List<ISaveData> saveData = JsonSaveSystem.Load(saveDataDictionary.Values.ToArray<string>());

        // Load data for each save data type
        foreach (ISaveData data in saveData)
        {
            foreach (ISaveableData saveable in saveableData)
            {
                saveable.LoadData(data);

                SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(saveDataDictionary[data], SaveSystemConfig.JSON_SAVE_FILE_EXTENSION));
            }
        }
    }

    public List<ISaveableData> FindAllDataPersistanceObjects()
    {
        IEnumerable<ISaveableData> dataPersistanceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ISaveableData>();

        return dataPersistanceObjects.ToList();
    }
}
