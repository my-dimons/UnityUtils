using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;
using System.IO;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveSystemManager
    {
        /// <summary>
        /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="dataIDs">Dictionary with the dataIDs ID and name to save with</param>
        public static void SaveGame(List<SaveData> dataIDs, string saveSlotID)
        {
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Save saveData for each save saveData classType
            foreach (SaveData saveData in dataIDs)
            {
                // Put saveData from files to SaveData's
                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.SaveData(saveData);
                }

                JsonSaveSystem.Save(saveData, saveSlotID);

                SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveData.saveFileName));
            }
        }

        /// <summary>
        /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="dataIDs">ID's to laod</param>
        public static void LoadGame(List<SaveData> dataIDs, string saveSlotID)
        {
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Inject save saveData into saveable files
            foreach (SaveData saveData in dataIDs)
            {
                SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData, saveSlotID);

                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.LoadData(data);

                    SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(data.saveFileName));
                }
            }
        }

        /// <summary>
        /// Gets all <see cref="ISaveableData"/> to call functions on
        /// </summary>
        /// <returns>List of all objects with <see cref="ISaveableData"/> attached</returns>
        public static List<ISaveableData> FindAllDataPersistanceObjects() =>
            Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveableData>()
                .ToList();

        public static Dictionary<string, List<SaveDataID>> LoadAllSaveSlots()
        {
            Dictionary<string, List<SaveDataID>> saveSlotDictionary = new();

            IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(Application.persistentDataPath).EnumerateDirectories();

            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                string saveSlotID = dirInfo.Name;
                List<SaveDataID> saveFiles = new();

                string partialPath = Path.Combine(Application.persistentDataPath, saveSlotID);

                // Loop through each file in directory
                foreach (FileInfo file in new DirectoryInfo(partialPath).GetFiles())
                {
                    string fullPath = Path.Combine(partialPath, file.Name);

                    if (!File.Exists(fullPath))
                    {
                        Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + saveSlotID);
                        continue;
                    }

                    SaveDataID saveData = JsonSaveSystem.DeserializeSaveDataID(fullPath);

                    if (saveData != null)
                        saveFiles.Add(saveData);
                }

                saveSlotDictionary.Add(saveSlotID, saveFiles);
            }

            return saveSlotDictionary;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueID">The unique ID of the new object</param>
        /// <param name="fileName">The file name of the save object</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveData CreateSaveData<T>(string uniqueID, string fileName, bool useEncryption) where T : SaveData, new()
        {
            T saveData = new T();
            saveData.SetData(uniqueID, fileName, useEncryption);

            return saveData;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The file name of the save object, gets used as the ID</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveData CreateSaveData<T>(string fileName, bool useEncryption) where T : SaveData, new() => CreateSaveData<T>(fileName, fileName, useEncryption);
    }
}