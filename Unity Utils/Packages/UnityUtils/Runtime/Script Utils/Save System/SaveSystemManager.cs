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
        public static void SaveGame(List<SaveDataID> dataIDs, string saveSlotID)
        {
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Save saveID for each save saveID type
            foreach (SaveDataID saveID in dataIDs)
            {
                // Put saveID from files to ISaveData's
                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.SaveData(saveID.dataInstance);
                }

                JsonSaveSystem.Save(saveID, saveSlotID);

                SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveID.fileName));
            }
        }

        /// <summary>
        /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="dataIDs">ID's to laod</param>
        public static void LoadGame(List<SaveDataID> dataIDs, string saveSlotID)
        {
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Inject save saveID into saveable files
            foreach (SaveDataID dataID in dataIDs)
            {
                ISaveData saveData = JsonSaveSystem.LoadSingleSaveFile(dataID, saveSlotID);

                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.LoadData(saveData);

                    SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(dataID.fileName));
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
        public static SaveDataID CreateSaveDataID<T>(string uniqueID, string fileName, bool useEncryption) where T : ISaveData, new()
        {
            ISaveData saveDataInstance = new T();
            SaveDataID saveDataID = new SaveDataID(uniqueID, fileName, saveDataInstance, typeof(T), useEncryption);

            return saveDataID;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The file name of the save object, gets used as the ID</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveDataID CreateSaveDataID<T>(string fileName, bool useEncryption) where T : ISaveData, new() => CreateSaveDataID<T>(fileName, fileName, useEncryption);
    }
}