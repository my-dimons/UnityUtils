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

    // Dictionary mapping string ID -> Type of the object
    private static readonly Dictionary<string, Type> saveDataClasses = new();

    public static void Register<T>() where T : ISaveData, new()
    {
        string id = nameof(T);

        ISaveData instance = new T();

        saveDataTypes[id] = () => new T();

        saveDataInstances[id] = instance;

        saveDataClasses[id] = typeof(T);

        saveDataIDs[instance] = id;
    }

    public static ISaveData Create(string dataID) 
    {
        if (saveDataTypes.TryGetValue(dataID, out var save))
        {
            return save();
        }

        return null;
    }

    public static Type GetClass(string dataID)
    {
        if (saveDataClasses.TryGetValue(dataID, out var type))
        {
            return type; 
        }

        return null;
    }

    public static ISaveData GetInstance(string dataID)
    {
        if (saveDataInstances.TryGetValue(dataID, out var save))
        {
            return save;
        }
        return null;
    }

    public static ISaveData CreateAndRegister<T>() where T : ISaveData, new()
    {
        Register<T>();
        
        return GetInstance(nameof(T));
    }

    public static string GetID(ISaveData saveData)
    {
        if (saveDataIDs.TryGetValue(saveData, out var id))
        {
            return id;
        }

        return null;
    }
}
