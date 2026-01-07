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
        /// <summary>
        /// Calls <see cref="ISaveableData.SaveData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="saveDatas">Dictionary with the dataIDs ID and name to save with</param>
        public static void SaveGame(List<SaveData> saveDatas)
        {
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

                SaveSystemUtils.LogSaveFileCreated(SaveSystemUtils.GetSaveFilePath(saveData.saveFileName));
            }
        }

        /// <summary>
        /// Calls <see cref="ISaveableData.LoadData{T}(T)"/> on every script inheriting <see cref="ISaveableData"/>
        /// </summary>
        /// <param name="saveDatas">ID's to laod</param>
        public static void LoadGame(List<SaveData> saveDatas)
        {
            List<ISaveableData> saveableData = FindAllDataPersistanceObjects();

            // Inject save saveData into saveable files
            foreach (SaveData saveData in saveDatas)
            {
                SaveData data = JsonSaveSystem.LoadSingleSaveFile(saveData);

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
            UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveableData>()
                .ToList();

        public static Dictionary<string, SaveSlot> LoadAllSaveSlots()
        {
            Dictionary<string, SaveSlot> saveSlotDictionary = new();
            IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(SaveSystemUtils.GetSaveSlotRootPath()).EnumerateDirectories();

            // Loop through each directory
            foreach (DirectoryInfo dirInfo in dirInfos)
            {
                string saveSlotName = dirInfo.Name;
                SaveSlot saveFiles;
                List<SaveData> saveDatas = new();

                string partialPath = SaveSystemUtils.GetSaveSlotPath(saveSlotName);

                // Loop through each file in directory
                foreach (FileInfo file in new DirectoryInfo(partialPath).GetFiles())
                {
                    string fullPath = Path.Combine(partialPath, file.Name);

                    if (!File.Exists(fullPath))
                    {
                        Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: " + saveSlotName);
                        continue;
                    }

                    // manually load data here since we don't have the SaveData instances to pass in
                    SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(fullPath));

                    Type type = saveData.GetClassType();

                    if (type == null)
                    {
                        Debug.LogWarning("Skipping save file when loading all profiles because its type could not be determined: " + fullPath);
                        continue;
                    }

                    object fullSaveData = JsonUtility.FromJson(File.ReadAllText(fullPath), saveData.GetClassType());

                    saveDatas.Add(fullSaveData as SaveData);
                    //
                    //if (saveData != null)
                    //    saveFiles.Add(saveData);
                }

                saveFiles = new SaveSlot(saveSlotName, saveDatas);

                saveSlotDictionary.Add(saveSlotName, saveFiles);
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