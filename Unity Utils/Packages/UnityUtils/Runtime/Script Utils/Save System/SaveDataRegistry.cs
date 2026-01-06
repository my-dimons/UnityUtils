using System;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public static class SaveDataRegistry
{
    // Dictionary mapping string ID -> function to create a new instance
    private static readonly Dictionary<string, Func<ISaveData>> saveDataTypes = new();

    // Dictionary mapping string stored instance -> ID
    private static readonly Dictionary<ISaveData, string> saveDataIDs = new();

    // Dictionary mapping string ID -> stored instance
    private static readonly Dictionary<string, ISaveData> saveDataInstances = new();

    /// Dictionary mapping string ID -> Type of the object
    private static readonly Dictionary<string, Type> saveDataClasses = new();

    /// <summary>
    /// Registers a new class to all the registry dictionaries to be referenced later
    /// </summary>
    /// <remarks>Call <see cref="Create(string)"/> after this function, or call <see cref="CreateAndRegister{T}"/> for more simplicity</remarks>
    /// <typeparam name="T">Class to register</typeparam>
    public static void Register<T>() where T : ISaveData, new()
    {
        string id = nameof(T);

        ISaveData instance = new T();

        saveDataTypes[id] = () => new T();

        saveDataInstances[id] = instance;

        saveDataClasses[id] = typeof(T);

        saveDataIDs[instance] = id;
    }

    /// <summary>
    /// Creates a new <see cref="ISaveData"/>
    /// </summary>
    /// <remarks>Call <see cref="Register{T}"/> then this function, or call <see cref="CreateAndRegister{T}"/> for more simplicity</remarks>
    /// <param name="dataID">ID of <see cref="ISaveData"/> to create</param>
    /// <returns>Created <see cref="ISaveData"/></returns>
    public static ISaveData Create(string dataID) 
    {
        if (saveDataTypes.TryGetValue(dataID, out var save))
            return save();

        return null;
    }

    /// <summary>
    /// Creates and registers a new <see cref="ISaveData"/> into the registry
    /// </summary>
    /// <typeparam name="T">Class to add into the registry</typeparam>
    /// <returns>Registered <see cref="ISaveData"/></returns>
    public static ISaveData CreateAndRegister<T>() where T : ISaveData, new()
    {
        Register<T>();

        return GetInstance(nameof(T));
    }

    /// <summary>
    /// Searches <see cref="saveDataClasses"/> for the matching <paramref name="dataID"/>, if found it will return the <see cref="Type"/> of class
    /// </summary>
    /// <param name="dataID">ID to search for</param>
    /// <returns>Type of class</returns>
    public static Type GetClass(string dataID)
    {
        if (saveDataClasses.TryGetValue(dataID, out var type))
            return type; 

        return null;
    }

    /// <summary>
    /// Searches <see cref="saveDataInstances"/> for the provided <paramref name="dataID"/>, if found it will return its matching ID 
    /// </summary>
    /// <param name="dataID">ID to search for</param>
    /// <returns>ISaveData from dataID</returns>
    public static ISaveData GetInstance(string dataID)
    {
        if (saveDataInstances.TryGetValue(dataID, out var save))
            return save;

        return null;
    }

    /// <summary>
    /// Searches <see cref="saveDataIDs"/> for <see cref="ISaveData"/>, if found it will return its matching ID 
    /// </summary>
    /// <param name="saveData">Save data to get ID from</param>
    /// <returns>ID string</returns>
    public static string GetID(ISaveData saveData)
    {
        if (saveDataIDs.TryGetValue(saveData, out var id))
            return id;

        return null;
    }
}
