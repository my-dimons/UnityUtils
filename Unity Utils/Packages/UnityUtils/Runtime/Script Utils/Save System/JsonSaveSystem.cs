using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class JsonSaveSystem
    {
        private static string encryptionKey = "Key";

        /// <summary>
        /// Serializes the inputted <paramref name="saveData"/> into <see cref="SaveDataID.fileName"/> in a json format
        /// </summary>
        /// <param name="saveData">saveData of the file to be saved</param>
        public static void Save(SaveData saveData, string saveSlotID)
        {
            string fullPath = SaveSystemUtils.GetSaveFilePath(Path.Combine(saveData.saveFileName));

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // Serialize data into json
                string dataToStore = JsonUtility.ToJson(saveData, !saveData.useEncryption);

                if (saveData.useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                    SaveSystemUtils.LogSaveFileEncrypted(SaveSystemUtils.GetSaveFilePath(saveData.saveFileName));
                }

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
        /// Deserializes a list of <see cref="SaveData"/> (Gotten via <see cref="SaveDataID.dataInstance"/>) from a json format file
        /// </summary>
        /// <param name="saveDatas">A list of saveDatas to load the <see cref="SaveData"/> of</param>
        /// <returns>List of all the loaded save datas</returns>
        public static List<SaveData> Load(List<SaveData> saveDatas, string saveSlotID)
        {
            List<SaveData> loadedData = new();

            // Load data into list
            foreach (SaveData saveData in saveDatas)
            {
                Type type = saveData.GetClassType();

                if (type != null)
                {
                    SaveData data = LoadSingleSaveFile(saveData, saveSlotID);

                    if (data != null)
                        loadedData.Add(data);
                }
            }

            return loadedData;
        }

        /// <summary>
        /// Deserializes a single <see cref="SaveData"/> (Gotten via <see cref="SaveDataID.dataInstance"/>) from a json format file
        /// </summary>
        /// <param name="saveData"><see cref="SaveDataID"/> to deserialize</param>
        /// <returns><see cref="SaveData"/> with the loaded data from the file</returns>
        public static SaveData LoadSingleSaveFile(SaveData saveData, string saveSlotID)
        {
            object loadedData = default;

            string fullPath = SaveSystemUtils.GetSaveFilePath(Path.Combine(saveData.saveFileName));

            // Get single file
            if (File.Exists(fullPath))
            {
                // Get provided json file
                try
                {
                    string json = File.ReadAllText(fullPath);

                    // Data decryption
                    if (saveData.useEncryption)
                    {
                        json = EncryptDecrypt(json);
                    }

                    // Deserialize Savedata temporarily to get the class type
                    SaveData tempJson = JsonUtility.FromJson<SaveData>(json);
                    
                    // Deserialize the data from json back into the object
                    loadedData = JsonUtility.FromJson(json, tempJson.GetClassType());
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
                }
            }

            return loadedData as SaveData;
        }

        public static SaveDataID DeserializeSaveDataID(SaveData data) => JsonConvert.DeserializeObject<SaveDataID>(data.saveDataID);
        public static SaveDataID DeserializeSaveDataID(string path)
        {
            string json = File.ReadAllText(path);
            JObject obj = JObject.Parse(json);

            string saveDataIDString = obj[nameof(SaveData.saveDataID)]?.ToString();

            SaveDataID data = JsonConvert.DeserializeObject<SaveDataID>(saveDataIDString);

            if (data != null)
            {
                return data;
            }

            Debug.Log("SaveDataID not found in: " + path);
            return null;
        }

        /// <summary>
        /// Sets the encryption key for file encryption
        /// </summary>
        /// <param name="key">Key to set to</param>
        public static void SetEncryptionKey(string key)
        {
            encryptionKey = key;
        }

        /// <summary>
        /// Encrypts data via an XOR shift of <paramref name="data"/> using the <see cref="encryptionKey"/>
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <returns>Encrypted data</returns>
        public static string EncryptDecrypt(string data)
        {
            string modifiedData = string.Empty;

            for (int character = 0; character < data.Length; character++)
            {
                modifiedData += (char)(data[character] ^ encryptionKey[character % encryptionKey.Length]);
            }

            return modifiedData;
        }
    }
}