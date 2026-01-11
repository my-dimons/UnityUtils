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
        private static bool useEncryption;

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
                string dataToStore = SaveSaveData(saveData); 

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
        /// Loads all save files in a save slot
        /// </summary>
        /// <param name="saveSlot">Save slot to load</param>
        /// <returns></returns>
        public static SaveSlot Load(SaveSlot saveSlot)
        {
            SaveSlot tempSaveSlot = new(saveSlot.saveSlotName);

            // Load data into list
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
            {
                tempSaveSlot.AddSaveData(LoadSingleSaveFile(saveData, tempSaveSlot));
            }

            return tempSaveSlot;
        }

        /// <summary>
        /// Loads a single save slot file
        /// </summary>
        /// <param name="saveData">Save data to load</param>
        /// <param name="saveSlot">Save slot to load it to</param>
        /// <returns></returns>
        public static SaveData LoadSingleSaveFile(SaveData saveData, SaveSlot saveSlot)
        {
            SaveData loadedData = default;

            string fullPath = SaveSystemUtils.GetSaveFilePath(saveData.saveFileName);

            // Get single file
            if (File.Exists(fullPath))
            {
                try
                {
                    loadedData = GetSaveData(GetJsonStringData(fullPath));

                    loadedData.SetSaveSlot(saveSlot);
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
        /// Deletes the inputted save slot's save files
        /// </summary>
        /// <param name="saveSlot"></param>
        public static void Delete(SaveSlot saveSlot)
        {
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
            {
                string fullPath = SaveSystemUtils.GetSaveFilePath(saveData.saveFileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    if (outputLogs)
                        SaveSystemUtils.LogSaveFileDeleted(fullPath);
                }
            }

            Directory.Delete(SaveSystemUtils.GetSaveSlotPath(saveSlot.saveSlotName), true);
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
        public static void UseEncryption(bool encryption)
        {
            useEncryption = encryption;
        }

        /// <summary>
        /// Encrypts data via an XOR shift of <paramref name="data"/> using the <see cref="encryptionKey"/>
        /// </summary>
        /// <remarks>Make sure you <see cref="SetEncryptionKey(string)"/></remarks>
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

        /// <summary>
        /// Grabs the string of data inside a json file via a path
        /// </summary>
        /// <param name="path">Full path to the save file</param>
        /// <returns>A string of all the data in the json</returns>
        public static string GetJsonStringData(string path)
        {
            return useEncryption ? EncryptDecrypt(File.ReadAllText(path)) : File.ReadAllText(path);
        }

        /// <summary>
        /// Deserializes a json string file
        /// </summary>
        /// <remarks>Use <see cref="GetJsonStringData(string)"/> to get a json string from a path</remarks>
        /// <param name="json">Json to deserialize</param>
        /// <returns>Deserialized save data</returns>
        public static SaveData GetSaveData(string json)
        {
            return JsonConvert.DeserializeObject<SaveData>(json,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        /// <summary>
        /// Serializes a <see cref="SaveData"/> and return the string of serialized data
        /// </summary>
        /// <param name="saveData">Data to serialize</param>
        /// <returns>String of serialized data</returns>
        public static string SaveSaveData(SaveData saveData)
        {
            return JsonConvert.SerializeObject(saveData, Formatting.Indented,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        }

        /// <summary>
        /// Will create <see cref="SaveSystemUtils.GetSaveSlotRootPath()"/> if it does not exist
        /// </summary>
        public static void CreateRootSaveDataIfNotExisting()
        {
            if (!Directory.Exists(SaveSystemUtils.GetSaveSlotRootPath()))
            {
                Directory.CreateDirectory(SaveSystemUtils.GetSaveSlotRootPath());
            }
        }
    }
}