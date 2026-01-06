using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour, ISaveManager
{
    public static SaveManager Instance { get; private set; }

    // A list with save file IDs
    List<string> saveFiles = new();

    void Start()
    {
        InitializeData();

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

    public void InitializeData()
    {
        ISaveData gameData = SaveDataRegistry.CreateAndRegister<GameData>("GameData", "game_save");
        saveFiles.Add(SaveDataRegistry.GetID(gameData));
    }
}
