using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public interface ISaveableData
    {
        public void Save<T>(T data) where T : ISaveData;

        public void Load<T>(T data) where T : ISaveData;
    }
}
