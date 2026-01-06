using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class JsonSaveSystem
    {
        /// <summary>
        /// Serializes the inputted <paramref name="saveDataID"/> into <see cref="SaveDataID.fileName"/> in a json format
        /// </summary>
        /// <param name="saveDataID">saveDataID of the file to be saved</param>
        public static void Save(SaveDataID saveDataID)
        {
            string fullPath = SaveSystemUtils.GetSaveFilePath(saveDataID.fileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // Serialize data into json
                string dataToStore = JsonUtility.ToJson(saveDataID.dataInstance, true);

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
        /// Deserializes a list of <see cref="ISaveData"/> (Gotten via <see cref="SaveDataID.dataInstance"/>) from a json format file
        /// </summary>
        /// <param name="saveDataIDs">A list of saveDataIDs to load the <see cref="ISaveData"/> of</param>
        /// <returns>List of all the loaded save datas</returns>
        public static List<ISaveData> Load(List<SaveDataID> saveDataIDs)
        {
            List<ISaveData> loadedData = new();

            // Load data into list
            foreach (SaveDataID dataID in saveDataIDs)
            {
                Type type = dataID.classType;

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
        /// Deserializes a single <see cref="ISaveData"/> (Gotten via <see cref="SaveDataID.dataInstance"/>) from a json format file
        /// </summary>
        /// <param name="saveDataID"><see cref="SaveDataID"/> to deserialize</param>
        /// <returns><see cref="ISaveData"/> with the loaded data from the file</returns>
        public static ISaveData LoadSingleSaveFile(SaveDataID saveDataID)
        {
            object loadedData = default;

            string fullPath = SaveSystemUtils.GetSaveFilePath(saveDataID.fileName);

            // Get single file
            if (File.Exists(fullPath))
            {
                // Get provided json file
                try
                {
                    string json = File.ReadAllText(fullPath);

                    // Deserialize the data from json back into the object
                    loadedData = JsonUtility.FromJson(json, saveDataID.classType);
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
                }
            }

            return loadedData as ISaveData;
        }
    }
}