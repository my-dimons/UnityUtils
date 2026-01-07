using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    // A list with save file IDs
    Dictionary<string, List<SaveDataID>> saveFiles = new();

    public string activeSaveSlot;

    void Start()
    {
        InitializeData();
        SaveSlot("save0");
        SaveSlot("save1");
        SaveSlot("save2");

        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void Save()
    {
        SaveSystemManager.SaveGame(saveFiles[activeSaveSlot]);
    }

    public void Load()
    {
        SaveSystemManager.LoadGame(saveFiles[activeSaveSlot]);
    }

    public void InitializeData()
    {
        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
    }

    public void SaveSlot(string saveSlot)
    {
        saveFiles.Add(saveSlot, new());
        activeSaveSlot = saveSlot;
        Debug.Log(saveSlot);
        SaveDataID gameData = SaveDataRegistry.Register<GameData>("saves/" + saveSlot + "/game_save.json", false);
        saveFiles[saveSlot].Add(gameData);
    }
}
