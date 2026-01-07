using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public Dictionary<string, SaveSlot> saveSlots = new();

    public string activeSaveSlot;

    private readonly bool useEncryption = false;

    void Start()
    {
        InitializeData();

        saveSlots = GetAllSaveSlots();

        CreateSaveSlot("save_0");
        CreateSaveSlot("save_1");
        CreateSaveSlot("save_2");

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
        // Check if save slot already exists
        if (SaveSlotExists(saveSlot))
        {
            Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
            return;
        }

        List<SaveData> data = new();

        // add save data to save slot
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

    public bool SaveSlotExists(string saveSlot)
    {
        return saveSlots.ContainsKey(saveSlot);
    }
}
