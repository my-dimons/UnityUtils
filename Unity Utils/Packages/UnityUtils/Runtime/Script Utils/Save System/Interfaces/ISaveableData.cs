using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public interface ISaveableData
    {
        void SaveData<T>(T data) where T : SaveData;

        void LoadData<T>(T data) where T : SaveData;
    }
}
