using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;
using System;
using System.IO;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveSystemManager
    {
        /// If true will output Debug.Log()'s on Save/Load
        public static bool outputLogs = true;

        /// <summary>
        /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="saveDatas">Dictionary with the dataIDs ID and name to save with</param>
        public static void SaveGame(List<SaveData> saveDatas)
        {
            long startTime = DateTime.Now.Ticks;

            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Save saveData for each save saveData classType
            foreach (SaveData saveData in saveDatas)
            {
                // Put saveData from files to SaveData's
                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.SaveData(saveData);
                }

                JsonSaveSystem.Save(saveData);

                if (outputLogs)
                    SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveData.saveFileName));
            }

            long endTime = DateTime.Now.Ticks - startTime;

            if (outputLogs)
                Debug.Log($"Saved game data, took: {(endTime / TimeSpan.TicksPerMillisecond):N4}ms");
        }

        /// <summary>
        /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="saveDatas">ID's to laod</param>
        public static void LoadGame(List<SaveData> saveDatas)
        {
            long startTime = DateTime.Now.Ticks;
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Inject save saveData into saveable files
            foreach (SaveData saveData in saveDatas)
            {
                SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData);

                foreach (ISaveableData saveable in saveableData)
                {
                    saveable.LoadData(data);

                    if (outputLogs)
                        SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(data.saveFileName));
                }
            }

            long endTime = DateTime.Now.Ticks - startTime;

            if (outputLogs)
                Debug.Log($"Loaded game data, took: {(endTime / TimeSpan.TicksPerMillisecond):N4}ms");
        }

        /// <summary>
        /// Gets all <see cref="ISaveableData"/> to call functions on
        /// </summary>
        /// <returns>List of all objects with <see cref="ISaveableData"/> attached</returns>
        public static List<ISaveableData> FindAllDataPersistanceObjects() =>
            UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveableData>()
                .ToList();

        /// <summary>
        /// Loads all save slots from the save directory, if none exists one is created.
        /// </summary>
        /// <param name="useEncryption">If true, will treat all save files as though they are encryped (Make sure you consistently encrypt/decrypt files)</param>
        /// <returns>Dictionary of the save slot name and the <see cref="SaveSlot"/></returns>
        public static Dictionary<string, SaveSlot> LoadAllSaveSlots(bool useEncryption)
        {
            Dictionary<string, SaveSlot> saveSlotDictionary = new();

            // create save directory if it does not exist
            if (!Directory.Exists(SaveSystemUtils.GetSaveSlotRootPath()))
            {
                Directory.CreateDirectory(SaveSystemUtils.GetSaveSlotRootPath());
                return saveSlotDictionary;
            }

            IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(SaveSystemUtils.GetSaveSlotRootPath()).EnumerateDirectories();

            // Loop through each save directory
            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                string saveSlotName = dirInfo.Name;
                SaveSlot saveSlot;
                List<SaveData> saveDatas = new();

                string partialPath = SaveSystemUtils.GetSaveSlotPath(saveSlotName);

                // Loop through each file in directory
                foreach (FileInfo file in new DirectoryInfo(partialPath).GetFiles())
                {
                    string fullPath = Path.Combine(partialPath, file.Name);

                    // Skip if no data
                    if (!File.Exists(fullPath))
                    {
                        Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + saveSlotName);
                        continue;
                    }

                    // Get provided json file and decrypt if needed
                    string json = useEncryption ? JsonSaveSystem.EncryptDecrypt(File.ReadAllText(fullPath)) : File.ReadAllText(fullPath);

                    // Deserialize Savedata temporarily to get the class type
                    SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                    Type type = saveData.GetClassType();

                    // Skip if no type
                    if (type == null)
                    {
                        Debug.LogWarning("Skipping save file when loading all profiles because its type could not be determined: " + fullPath);
                        continue;
                    }

                    // Deserialize the data from json back into the object
                    object fullSaveData = JsonUtility.FromJson(json, saveData.GetClassType());

                    saveDatas.Add(fullSaveData as SaveData);
                }

                saveSlot = new SaveSlot(saveSlotName, saveDatas);

                saveSlotDictionary.Add(saveSlot.saveSlotName, saveSlot);
            }

            return saveSlotDictionary;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The file name of the save object</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveData CreateSaveData<T>(string fileName, bool useEncryption) where T : SaveData, new()
        {
            T saveData = new();
            saveData.SetData(fileName, useEncryption, typeof(T));

            return saveData;
        }
    }
}