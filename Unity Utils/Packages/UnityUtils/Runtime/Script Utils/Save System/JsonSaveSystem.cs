using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

public static class JsonSaveSystem
{
    public static void Save(ISaveData data, string fileName)
    {
        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.JSON_SAVE_FILE_EXTENSION);

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

    public static ISaveData LoadSingleSaveFile(string fileName, Type type)
    {
        object loadedData = default;

        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.JSON_SAVE_FILE_EXTENSION);

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
