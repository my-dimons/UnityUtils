using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

public static class JsonSaveSystem
{
    /// <summary>
    /// Loads the inputted <paramref name="dataID"/> into its respective file
    /// </summary>
    /// <param name="dataID">dataID of the file to be saved</param>
    public static void Save(string dataID)
    {
        string fullPath = SaveSystemUtils.GetSaveFilePath(SaveDataRegistry.GetFileName(dataID), SaveSystemUtils.JSON_SAVE_FILE_EXTENSION);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize data into json
            string dataToStore = JsonUtility.ToJson(SaveDataRegistry.GetInstance(dataID), true);

            // Write data into files
            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);
        }
        catch (IOException e)
        {
            Debug.LogError($"Error saving file to {fullPath}. Exception: {e}");
        }
    }

    /// <summary>
    /// Loads json file data from the inputted files
    /// </summary>
    /// <param name="dataIDs">A list of dataIDs to load the <see cref="ISaveData"/> of</param>
    /// <returns>List of all the loaded save datas</returns>
    public static List<ISaveData> Load(List<string> dataIDs)
    {
        List<ISaveData> loadedData = new();

        // Load data into list
        foreach (string dataID in dataIDs)
        {
            Type type = SaveDataRegistry.GetClass(dataID);

            if (type != null)
            {
                ISaveData data = LoadSingleSaveFile(dataID);

                if (data != null)
                    loadedData.Add(data);
            }
        }

        return loadedData;
    }

    /// <summary>
    /// Loads a single save file from the files using a <paramref name="fileName"/> and <paramref name="type"/>
    /// </summary>
    /// <param name="fileName">Name of the file to grab</param>
    /// <param name="type"><see cref="Type"/> of file to grab</param>
    /// <returns><see cref="ISaveData"/> with the loaded data from the file</returns>
    public static ISaveData LoadSingleSaveFile(string dataID)
    {
        object loadedData = default;

        string fullPath = SaveSystemUtils.GetSaveFilePath(SaveDataRegistry.GetFileName(dataID), SaveSystemUtils.JSON_SAVE_FILE_EXTENSION);

        // Get single file
        if (File.Exists(fullPath))
        {
            // Get provided json file
            try
            {
                string json = File.ReadAllText(fullPath);

                // Deserialize the data from json back into the object
                loadedData = JsonUtility.FromJson(json, SaveDataRegistry.GetClass(dataID));
            }
            catch (IOException e)
            {
                Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
            }
        }

        return loadedData as ISaveData;
    }
}
