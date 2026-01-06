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
    /// <param name="dataIDs">Dictionary with the dataIDs ID and name to save with</param>
    public static void SaveGame(List<string> dataIDs)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Save dataID for each save dataID type
        foreach (string dataID in dataIDs)
        {
            ISaveData data = SaveDataRegistry.GetInstance(dataID);

            // Put dataID from files to ISaveData's
            foreach (ISaveableData saveable in saveableData)
            {
                saveable.SaveData(data);
            }

            JsonSaveSystem.Save(dataID);

            SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(SaveDataRegistry.GetFileName(dataID), SaveSystemUtils.JSON_SAVE_FILE_EXTENSION));
        }
    }

    /// <summary>
    /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script with an <see cref="ISaveableData"/> inherited.
    /// </summary>
    /// <param name="dataIDs">ID's to laod</param>
    public static void LoadGame(List<string> dataIDs)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Inject save dataID into saveable files
        foreach (string dataID in dataIDs)
        {
            ISaveData saveData = JsonSaveSystem.LoadSingleSaveFile(dataID);

            foreach (ISaveableData saveable in saveableData)
            {
                saveable.LoadData(saveData);

                SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(SaveDataRegistry.GetFileName(dataID), SaveSystemUtils.JSON_SAVE_FILE_EXTENSION));
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
