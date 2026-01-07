using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // A list with save file IDs
    List<SaveDataID> saveFiles = new();

    public string activeSaveSlot;

    void Start()
    {
        InitializeData();
        SaveSlot("0");

        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void Save()
    {
        SaveSystemManager.SaveGame(saveFiles, activeSaveSlot);
    }

    public void Load()
    {
        SaveSystemManager.LoadGame(saveFiles, activeSaveSlot);
    }

    public void InitializeData()
    {
        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
    }

    public void SaveSlot(string saveSlot)
    {
        activeSaveSlot = saveSlot;

        Debug.Log(saveSlot);

        string path = Path.Combine("saves", saveSlot, "game_save.json");
        SaveDataID gameData = SaveSystemManager.CreateSaveDataID<GameData>(path, false);

        saveFiles.Add(gameData);
    }
}
