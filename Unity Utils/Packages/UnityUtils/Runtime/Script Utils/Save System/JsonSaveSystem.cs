using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class JsonSaveSystem
    {
        /// If true will output Debug.Log()'s on Save/Load
        public static bool outputLogs = true;

        /// If true will use encryption
        public static bool useEncryption;

        private static string encryptionKey = "Key";

        /// <summary>
        /// Serializes the inputted <paramref name="saveData"/> into <see cref="SaveDataID.fileName"/> in a json format
        /// </summary>
        /// <param name="saveData">saveData of the file to be saved</param>
        public static void Save(SaveData saveData)
        {
            string fullPath = SaveSystemUtils.GetSaveFilePath(Path.Combine(saveData.saveFileName));

            saveData.Save();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // Serialize data into json
                string dataToStore = JsonConvert.SerializeObject(saveData, Formatting.Indented, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }); 

                if (useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);

                    if (outputLogs)
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
        public static List<SaveData> Load(List<SaveData> saveDatas)
        {
            List<SaveData> loadedData = new();

            // Load data into list
            foreach (SaveData saveData in saveDatas)
            {
                SaveData data = LoadSingleSaveFile(saveData);

                if (data != null)
                    loadedData.Add(data);
            }

            return loadedData;
        }

        /// <summary>
        /// Deserializes a single <see cref="SaveData"/> (Gotten via <see cref="SaveDataID.dataInstance"/>) from a json format file
        /// </summary>
        /// <param name="saveData"><see cref="SaveDataID"/> to deserialize</param>
        /// <returns><see cref="SaveData"/> with the loaded data from the file</returns>
        public static SaveData LoadSingleSaveFile(SaveData saveData)
        {
            SaveData loadedData = default;

            string fullPath = SaveSystemUtils.GetSaveFilePath(saveData.saveFileName);

            // Get single file
            if (File.Exists(fullPath))
            {
                // Get provided json file
                try
                {
                    string json = File.ReadAllText(fullPath);

                    // Data decryption
                    if (useEncryption)
                    {
                        json = EncryptDecrypt(json);
                    }   

                    // Deserialize the data from json back into the object
                    loadedData = JsonConvert.DeserializeObject<SaveData>(json,
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                    loadedData.Load();
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error loading file from {fullPath}. Exception: {e}");
                }
            }

            return loadedData;
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
        /// Sets the encryption bool
        /// </summary>
        /// <param name="encryption">Bool to set <see cref="useEncryption"/> to</param>
        public static void SetUseEncryption(bool encryption)
        {
            useEncryption = encryption;
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