using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public Dictionary<string, SaveSlot> saveSlots = new();

    public string activeSaveSlot;

    private readonly bool useEncryption = true;

    void Start()
    {
        InitializeData();

        saveSlots = GetAllSaveSlots();

        CreateSaveSlot("0");
        CreateSaveSlot("test_slot");
        CreateSaveSlot("1234");

        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    public void Save()
    {
        SaveSystemManager.SaveGame(saveSlots[activeSaveSlot].saveDatas);
    }

    public void Load()
    {
        SaveSystemManager.LoadGame(saveSlots[activeSaveSlot].saveDatas);
    }

    public void InitializeData()
    {
        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
    }

    public void CreateSaveSlot(string saveSlot)
    {
        if (saveSlots.ContainsKey(saveSlot))
        {
            Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
            return;
        }

        List<SaveData> data = new();

        string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json");
        data.Add(SaveSystemManager.CreateSaveData<GameData>(path, useEncryption));

        saveSlots.Add(saveSlot, new SaveSlot(saveSlot, data));
    }

    public void SetSaveSlot(string saveSlot)
    {
        if (saveSlots[saveSlot] != null)
            activeSaveSlot = saveSlot;
        else
            Debug.LogWarning("The save slot \"" + saveSlot + "\" is unavailable");
    }

    public Dictionary<string, SaveSlot> GetAllSaveSlots()
    {
        Dictionary<string, SaveSlot> loadedSaveSlots = SaveSystemManager.LoadAllSaveSlots(useEncryption);
        Debug.Log(loadedSaveSlots.Count + " save slots found.");

        foreach (var save in loadedSaveSlots)
        {
            Debug.Log($"SaveSlot: FileName: {save.Key}");
        }

        return loadedSaveSlots;
    }
}
