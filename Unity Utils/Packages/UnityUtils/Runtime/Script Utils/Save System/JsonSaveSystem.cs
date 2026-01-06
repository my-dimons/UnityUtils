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
    /// Loads the inputted <paramref name="data"/> into the file with <paramref name="fileName"/>
    /// </summary>
    /// <param name="data">Data to load into the provided file</param>
    /// <param name="fileName">File to load the data into</param>
    public static void Save(ISaveData data, string fileName)
    {
        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemUtils.JSON_SAVE_FILE_EXTENSION);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize data into json
            string dataToStore = JsonUtility.ToJson(data, true);

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
    /// <param name="saveFiles">A list of <see cref="ISaveData"/> IDs and File Names</param>
    /// <returns>List of all the loaded save datas</returns>
    public static List<ISaveData> Load(Dictionary<string, string> saveFiles)
    {
        List<ISaveData> loadedData = new();

        // Load data into list
        foreach (var dataID in saveFiles.Keys)
        {
            // 
            if (saveFiles.TryGetValue(dataID, out var fileName))
            {
                // Get class for save file
                Type type = SaveDataRegistry.GetClass(dataID);

                if (type != null)
                {
                    ISaveData data = LoadSingleSaveFile(fileName, type);

                    if (data != null)
                        loadedData.Add(data);
                }
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
    public static ISaveData LoadSingleSaveFile(string fileName, Type type)
    {
        object loadedData = default;

        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemUtils.JSON_SAVE_FILE_EXTENSION);

        // Get single file
        if (File.Exists(fullPath))
        {
            // Get provided json file
            try
            {
                string json = File.ReadAllText(fullPath);

                // Deserialize the data from json back into the object
                loadedData = JsonUtility.FromJson(json, type);
            }
            catch (IOException e)
            {
                Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
            }
        }

        return loadedData as ISaveData;
    }
}
