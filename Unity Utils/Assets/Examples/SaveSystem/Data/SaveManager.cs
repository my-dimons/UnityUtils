using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    /// 
    Dictionary<string, string> saveFiles = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ISaveData gameData = SaveDataRegistry.CreateAndRegister<GameData>();
        saveFiles.Add(SaveDataRegistry.GetID(gameData), "game_save");

        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void Save()
    {
        SaveSystemManager.SaveGame(saveFiles);
    }

    public void Load()
    {
        SaveSystemManager.LoadGame(saveFiles);
    }
}
