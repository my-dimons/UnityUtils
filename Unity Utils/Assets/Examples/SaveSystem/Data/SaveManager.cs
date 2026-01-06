using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    Dictionary<ISaveData, string> saveData = new Dictionary<ISaveData, string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveData.Add(SaveDataRegistry.CreateAndRegister<GameData>(), "game_save");

        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void Save()
    {
        SaveSystemManager.Instance.SaveGame(saveData, SaveSystemManager.Instance.FindAllDataPersistanceObjects());
    }

    public void Load()
    {
        SaveSystemManager.Instance.LoadGame(saveData, SaveSystemManager.Instance.FindAllDataPersistanceObjects());
    }
}
