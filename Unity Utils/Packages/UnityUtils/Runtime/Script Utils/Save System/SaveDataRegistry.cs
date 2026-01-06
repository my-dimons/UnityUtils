using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public static class SaveDataRegistry
{
    private static readonly Dictionary<string, Func<ISaveData>> saveDataTypes = new();
    private static readonly Dictionary<string, ISaveData> saveDataInstances = new();

    public static void Register<T>() where T : ISaveData, new()
    {
        string id = nameof(T);

        saveDataTypes[id] = () => new T();
        saveDataInstances[id] = new T();
    }

    public static ISaveData Create(string dataID) 
    {
        if (saveDataTypes.TryGetValue(dataID, out var save))
        {
            return save();
        }

        return null;
    }

    public static ISaveData Get(string dataID)
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
        ISaveData gameData = Create(nameof(T));
        
        return gameData;
    }



}
