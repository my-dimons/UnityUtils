using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;

public static class SaveSystemManager
{
    /// <summary>
    /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script with an <see cref="ISaveableData"/> inherited.
    /// </summary>
    /// <param name="saveFiles">Dictionary with the saveFiles ID and name to save with</param>
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

    /// <summary>
    /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script with an <see cref="ISaveableData"/> inherited.
    /// </summary>
    /// <param name="saveFiles">Dictionary with the saveFiles ID and name to load with</param>
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
    
    /// <summary>
    /// Grab all <see cref="ISaveableData"/> to call functions on
    /// </summary>
    /// <returns>List of all objects with <see cref="ISaveableData"/> attached</returns>
    public static List<ISaveableData> FindAllDataPersistanceObjects() => 
        Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ISaveableData>()
            .ToList();
}
