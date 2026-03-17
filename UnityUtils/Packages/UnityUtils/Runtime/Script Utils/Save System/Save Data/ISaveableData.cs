namespace UnityUtils.ScriptUtils.SaveSystem {
  public interface ISaveableData {
    /// <summary>
    /// Called on <see cref="SaveSystemManager.SaveGame(SaveSlot)"/>
    /// </summary>
    /// <typeparam name="T">Type of data passed through</typeparam>
    /// <param name="data">data passed through</param>
    void SaveData<T>(T data) where T : SaveData;

    /// <summary>
    /// Called on <see cref="SaveSystemManager.LoadGame(SaveSlot)"/>
    /// </summary>
    /// <typeparam name="T">Type of data passed through</typeparam>
    /// <param name="data">data passed through</param>
    void LoadData<T>(T data) where T : SaveData;
  }
}
