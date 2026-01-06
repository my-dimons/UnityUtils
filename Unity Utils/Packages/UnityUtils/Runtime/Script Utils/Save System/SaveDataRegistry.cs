using System;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveDataRegistry
    {
        private static readonly List<SaveDataID> saveDataIDs = new();

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueID">The unique ID of the new object</param>
        /// <param name="fileName">The file name of the save object</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameteres</returns>
        public static SaveDataID Register<T>(string uniqueID, string fileName, bool useEncryption) where T : ISaveData, new()
        {
            // TODO: ADD CHECKS (if id already exists)
            ISaveData saveDataInstance = new T();
            SaveDataID saveDataID = new SaveDataID(uniqueID, fileName, saveDataInstance, typeof(T), useEncryption);

            saveDataIDs.Add(saveDataID);

            return saveDataID;
        }

        /// <summary>
        /// Searches <see cref="saveDataIDs"/> for <see cref="ISaveData"/>, if found it will return its matching ID 
        /// </summary>
        /// <param name="saveData">Save data to get ID from</param>
        /// <returns>ID string</returns>
        public static string GetID(ISaveData saveData)
        {
            SaveDataID data = GetIDFromISaveData(saveData);

            if (data != null)
                return data.id;

            return null;
        }

        /// <summary>
        /// Gets a <see cref="SaveDataID"/> from the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">ID of the SaveDataID</param>
        /// <returns><see cref="SaveDataID"/> of the found ID, returns null if nothing is found</returns>
        public static SaveDataID GetSaveDataIDFromID(string id)
        {
            foreach (SaveDataID dataID in saveDataIDs)
                if (dataID.id == id)
                    return dataID;

            return null;
        }

        /// <summary>
        /// Gets a <see cref="SaveDataID"/> from the provided <see cref="ISaveData"/>
        /// </summary>
        /// <param name="saveData"><see cref="ISaveData"/> of the SaveDataID</param>
        /// <returns><see cref="SaveDataID"/> of the found <see cref="ISaveData"/>, returns null if nothing is found</returns>
        public static SaveDataID GetIDFromISaveData(ISaveData saveData)
        {
            foreach (SaveDataID dataID in saveDataIDs)
                if (dataID.dataInstance == saveData)
                    return dataID;

            return null;
        }
    }
}