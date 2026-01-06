using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public interface ISaveableData
    {
        void SaveData<T>(T data) where T : ISaveData;

        void LoadData<T>(T data) where T : ISaveData;
    }
}
