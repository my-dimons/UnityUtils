using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public static class JsonSaveSystem
{
    public static void Save(ISaveData data, string fileName)
    {
        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.JSON_SAVE_FILE_EXTENSION);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error saving file to {fullPath}. Exception: {e}");
        }
    }

    public static List<ISaveData> Load(Dictionary<ISaveData, string> saveDataDictionary)
    {
        List<ISaveData> loadedData = new List<ISaveData>();

        foreach (var file in saveDataDictionary)
        {
            ISaveData data = LoadSingleSaveFile<ISaveData>(file.Value);
            loadedData.Add(data);
        }

        return loadedData;
    }

    public static T LoadSingleSaveFile<T>(string fileName) where T : ISaveData
    {
        T loadedData = default;

        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.JSON_SAVE_FILE_EXTENSION);

        if (File.Exists(fullPath))
        {
            try
            {
                string json = File.ReadAllText(fullPath);

                // Deserialize the data from Json back into the object
                loadedData = JsonUtility.FromJson<T>(json);
            }
            catch (IOException e)
            {
                Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
            }
        }

        return loadedData;
    }
}
