using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public Dictionary<string, SaveSlot> saveSlots = new();

    public string activeSaveSlot;

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
        SaveSystemManager.SaveGame(saveSlots[activeSaveSlot]);
    }

    public void Load()
    {
        SaveSystemManager.LoadGame(saveSlots[activeSaveSlot]);

        List<SaveSlot> saves = new();
        foreach (var slot in saveSlots.Values)
            saves.Add(slot);

        Debug.Log("Most recently saved file: " + SaveSystemManager.GetMostRecentSave(saves).saveSlotName);
    }

    public void InitializeData()
    {
        JsonSaveSystem.SetEncryptionKey("YourEncryptionKey");
        JsonSaveSystem.UseEncryption(false);

        JsonSaveSystem.outputLogs = true;
        SaveSystemManager.outputLogs = true;
    }

    public void CreateSaveSlot(string saveSlot)
    {
        // Check if save slot already exists
        if (SaveSlotExists(saveSlot))
        {
            Debug.LogWarning("The save slot \"" + saveSlot + "\" already exists");
            return;
        }

        SaveSlot saveSlotObj = new(saveSlot);

        // add save data to save slot
        string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlot, "game_save.json");
        saveSlotObj.AddSaveData(SaveSystemManager.CreateSaveData<GameData>(path));

        saveSlots.Add(saveSlot, saveSlotObj);
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
        Dictionary<string, SaveSlot> loadedSaveSlots = SaveSystemManager.LoadAllSaveSlots();

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
