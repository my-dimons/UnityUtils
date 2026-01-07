using System;
using System.Collections.Generic;
using System.IO;
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
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveDataID Register<T>(string uniqueID, string fileName, bool useEncryption) where T : SaveData, new()
        {
            // TODO: ADD CHECKS (if it already exists)
            SaveData saveDataInstance = new T();
            SaveDataID saveDataID = new SaveDataID(uniqueID, fileName, saveDataInstance, typeof(T), useEncryption);

            saveDataIDs.Add(saveDataID);

            return saveDataID;
        }

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">The file name of the save object, gets used as the ID</param>
        /// <param name="useEncryption">Wether or not to use encryption on this save data</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameters</returns>
        public static SaveDataID Register<T>(string fileName, bool useEncryption) where T : SaveData, new() => Register<T>(fileName, fileName, useEncryption);
    }
}