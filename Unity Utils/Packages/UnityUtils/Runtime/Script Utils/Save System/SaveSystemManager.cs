using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;

public static class SaveSystemManager
{
    public static void SaveGame(Dictionary<string, string> saveFiles)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Save data for each save data type
        foreach (var dataID in saveFiles.Keys)
        {
            ISaveData data = SaveDataRegistry.GetInstance(dataID);

            // Put data from files to ISaveData's
            foreach (ISaveableData saveable in saveableData)
            {
                saveable.SaveData(data);
            }

            JsonSaveSystem.Save(data, saveFiles[dataID]);

            SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveFiles[dataID], SaveSystemUtils.JSON_SAVE_FILE_EXTENSION));
        }
    }

    public static void LoadGame(Dictionary<string, string> saveFiles)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Inject save data into saveable files
        foreach (var data in saveFiles)
        {
            ISaveData saveData = JsonSaveSystem.LoadSingleSaveFile(data.Value, SaveDataRegistry.GetClass(data.Key));

            foreach (ISaveableData saveable in saveableData)
            {
                saveable.LoadData(saveData);

                SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(data.Value, SaveSystemUtils.JSON_SAVE_FILE_EXTENSION));
            }
        }
    }

    public static List<ISaveableData> FindAllDataPersistanceObjects()
    {
        return Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ISaveableData>()
            .ToList();
    }
}
