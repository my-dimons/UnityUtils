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
    public static void SaveGame(List<SaveDataID> dataIDs)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Save saveID for each save saveID type
        foreach (SaveDataID saveID in dataIDs)
        {
            // Put saveID from files to ISaveData's
            foreach (ISaveableData saveable in saveableData)
            {
                saveable.SaveData(saveID.dataInstance);
            }

            JsonSaveSystem.Save(saveID);

            SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveID.fileName));
        }
    }

    /// <summary>
    /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script with an <see cref="ISaveableData"/> inherited.
    /// </summary>
    /// <param name="dataIDs">ID's to laod</param>
    public static void LoadGame(List<SaveDataID> dataIDs)
    {
        List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

        // Inject save saveID into saveable files
        foreach (SaveDataID dataID in dataIDs)
        {
            ISaveData saveData = JsonSaveSystem.LoadSingleSaveFile(dataID);

            foreach (ISaveableData saveable in saveableData)
            {
                saveable.LoadData(saveData);

                SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(dataID.fileName));
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
