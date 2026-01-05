using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
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

    public static List<ISaveData> Load(string[] fileNames)
    {
        List<ISaveData> loadedData = new List<ISaveData>();

        foreach (string file in fileNames)
        {
            ISaveData data = LoadSingleSaveFile(file);
            loadedData.Add(data);
        }

        return loadedData;
    }

    public static ISaveData LoadSingleSaveFile(string fileName)
    {
        ISaveData loadedData = null;

        string fullPath = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.JSON_SAVE_FILE_EXTENSION);

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserialize the data from Json back into the object
                loadedData = JsonUtility.FromJson<ISaveData>(dataToLoad);
            }
            catch (IOException e)
            {
                Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
            }
        }

        return loadedData;
    }
}
