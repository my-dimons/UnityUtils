using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // A list with save file IDs
    List<SaveDataID> saveFiles = new();

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
        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");

        SaveDataID gameData = SaveDataRegistry.Register<GameData>("game_save.json", true);
        saveFiles.Add(gameData);
    }
}
