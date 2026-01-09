using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;

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
        public static void SaveGame(SaveSlot saveSlot)
        {
            long startTime = DateTime.Now.Ticks;

            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Save saveData for each save saveData classType
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
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
        public static void LoadGame(SaveSlot saveSlot)
        {
            long startTime = DateTime.Now.Ticks;
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Inject save saveData into saveable files
            foreach (SaveData saveData in saveSlot.GetSaveDatas())
            {
                SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData, saveSlot);

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
        /// <returns>Dictionary of the save slot name and the <see cref="SaveSlot"/></returns>
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
        /// <typeparam name="T"></typeparam>
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
    }
}