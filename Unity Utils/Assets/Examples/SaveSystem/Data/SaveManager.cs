using System.Collections.Generic;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveManager : MonoBehaviour, ISaveManager
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
        JsonSaveSystem.SetEncryptionKey(";fds'SRp[2r-~fdogjf)W$Kv-fjvDNJF)W4-vxFJfjds0fJ$)(-rjI*SJ09$K_VUSi980rth09");

        SaveDataID gameData = SaveDataRegistry.Register<GameData>("GameData", "game_save.json", true);
        saveFiles.Add(gameData);
    }
}
