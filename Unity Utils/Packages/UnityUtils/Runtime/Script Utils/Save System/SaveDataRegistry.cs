using System;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveDataRegistry
    {
        // Dictionary mapping string ID -> function to create a new saveDataInstance
        private static readonly Dictionary<string, Func<ISaveData>> saveDataTypes = new();

        // Dictionary mapping string stored saveDataInstance -> ID
        private static readonly Dictionary<ISaveData, string> saveDataIDss = new();

        // Dictionary mapping string ID -> stored saveDataInstance
        private static readonly Dictionary<string, ISaveData> saveDataInstances = new();

        /// Dictionary mapping string ID -> Type of the object
        private static readonly Dictionary<string, Type> saveDataClasses = new();

        /// Dictionary mapping string ID -> string file name of the object
        private static readonly Dictionary<string, string> saveDataFileNames = new();

        private static readonly List<SaveDataID> saveDataIDs = new();

        /// <summary>
        /// Registers a new <see cref="SaveDataID"/> to the registry to be referenced later
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueID">The unique ID of the new object</param>
        /// <param name="fileName">The file name of the save object</param>
        /// <returns>The <see cref="SaveDataID"/> with its filled in parameteres</returns>
        public static SaveDataID Register<T>(string uniqueID, string fileName) where T : ISaveData, new()
        {
            // TODO: ADD CHECKS (if id already exists)
            ISaveData saveDataInstance = new T();
            SaveDataID saveDataID = new SaveDataID(uniqueID, fileName, saveDataInstance, typeof(T));

            saveDataIDs.Add(saveDataID);

            return saveDataID;
        }

        /// <summary>
        /// Searches <see cref="saveDataClasses"/> for the matching <paramref name="dataID"/>, if found it will return the <see cref="Type"/> of class
        /// </summary>
        /// <param name="dataID">ID to search for</param>
        /// <returns>Type of class</returns>
        public static Type GetClass(string dataID)
        {
            SaveDataID data = GetSaveDataIDFromID(dataID);

            if (data != null)
                return data.classType;

            return null;
        }

        /// <summary>
        /// Searches <see cref="saveDataInstances"/> for the provided <paramref name="dataID"/>, if found it will return its matching ID 
        /// </summary>
        /// <param name="dataID">ID to search for</param>
        /// <returns>ISaveData from <paramref name="dataID"/></returns>
        public static ISaveData GetInstance(string dataID)
        {
            SaveDataID data = GetSaveDataIDFromID(dataID);

            if (data != null)
                return data.dataInstance;

            return null;
        }

        /// <summary>
        /// Searches <see cref="saveDataFileNames"/> for the provided <paramref name="dataID"/>, if found it will return its matching file name
        /// </summary>
        /// <param name="dataID">ID to search for</param>
        /// <returns>file name from <paramref name="dataID"/></returns>
        public static string GetFileName(string dataID)
        {
            SaveDataID data = GetSaveDataIDFromID(dataID);

            if (data != null)
                return data.fileName;

            return null;
        }

        /// <summary>
        /// Searches <see cref="saveDataFileNames"/> for the provided <paramref name="saveData"/>, if found it will return its matching file name
        /// </summary>
        /// <param name="saveData">ID to search for</param>
        /// <returns>file name from <paramref name="saveData"/></returns>
        public static string GetFileName(ISaveData saveData)
        {
            SaveDataID data = GetIDFromISaveData(saveData);

            if (data != null)
                return data.fileName;

            return null;
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