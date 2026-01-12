using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    /// <summary>
    /// Test description
    /// </summary>
    public static class SaveSystemManager
    {
        /// If true will output Debug.Log()'s on Save/Load
        public static bool outputLogs = true;

        /// <summary>
        /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="saveSlot">Save slot to save data from <see cref="ISaveableData"/>'s</param>
        public static void SaveGame(SaveSlot saveSlot)
        {
            long startTime = DateTime.Now.Ticks;

            // Save saveData for each save saveData classType
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
            {
                // Put saveData from files to SaveData's
                SaveAllSaveableData(saveData);
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
        /// <param name="saveSlot">Save slot to load data from all the <see cref="ISaveableData"/>'s</param>
        public static void LoadGame(SaveSlot saveSlot)
        {
            long startTime = DateTime.Now.Ticks;

            // Inject save saveData into saveable files
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
            {
                SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData, saveSlot);

                LoadAllSaveableData(data);
            }

            long endTime = DateTime.Now.Ticks - startTime;

            if (outputLogs)
                Debug.Log($"Loaded game data, took: {(endTime / TimeSpan.TicksPerMillisecond):N4}ms");
        }

        /// <summary>
        /// Delete a save slot
        /// </summary>
        /// <param name="saveSlot">Save slot to delete</param>
        public static void DeleteSaveSlot(SaveSlot saveSlot)
        {
            JsonSaveSystem.Delete(saveSlot);
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
        /// Loads all save slots from the <see cref="SaveSystemUtils.SAVE_FILES_NAME"/> directory, if none exist one is created.
        /// </summary>
        /// <returns>Dictionary of the save slot name and <see cref="SaveSlot"/></returns>
        public static Dictionary<string, SaveSlot> LoadAllSaveSlots()
        {
            Dictionary<string, SaveSlot> saveSlotDictionary = new();

            // create save directory if it does not exist
            JsonSaveSystem.CreateRootSaveDataIfNotExisting();

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

                    saveDatas.Add(JsonSaveSystem.GetSaveData(JsonSaveSystem.GetJsonStringData(fullPath)));
                }

                saveSlot = new SaveSlot(saveSlotName);
                saveSlot.SetSaveDataSlot(saveDatas);
                saveSlot.AddSaveData(saveDatas);
                saveSlot.LoadAllSaveDatas();

                saveSlotDictionary.Add(saveSlot.saveSlotName, saveSlot);
            }

            return saveSlotDictionary;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T">data type to encode, must inherit <see cref="SaveData"/></typeparam>
        /// <param name="fileName">The file name of the save object</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveData CreateSaveData<T>(string fileName) where T : SaveData, new()
        {
            T saveData = new();
            saveData.SetData(fileName);

            return saveData;
        }

        /// <summary>
        /// Grabs the most recent save in a list of <see cref="SaveSlot"/>
        /// </summary>
        /// <param name="saveSlots">Save slots to sort through</param>
        /// <returns>Most recently saved slot</returns>
        public static SaveSlot GetMostRecentSave(List<SaveSlot> saveSlots)
        {
            return saveSlots
                .OrderByDescending(d => d.lastTimeSaved)
                .First();
        }

        public static void SaveAllSaveableData(SaveData dataToSave, List<ISaveableData> saveTo)
        {
            foreach (ISaveableData saveable in saveTo)
            {
                saveable.SaveData(dataToSave);
            }
        }

        public static void SaveAllSaveableData(SaveData dataToSave)
        {
            SaveAllSaveableData(dataToSave, FindAllDataPersistanceObjects());
        }

        public static void LoadAllSaveableData(SaveData dataToSave, List<ISaveableData> saveTo)
        {
            foreach (ISaveableData saveable in saveTo)
            {
                saveable.LoadData(dataToSave);

                if (outputLogs)
                    SaveSystemUtils.LogSaveFileLoaded(SaveSystemUtils.GetSaveFilePath(dataToSave.saveFileName));
            }
        }

        public static void LoadAllSaveableData(SaveData dataToSave)
        {
            LoadAllSaveableData(dataToSave, FindAllDataPersistanceObjects());
        }

    }
}