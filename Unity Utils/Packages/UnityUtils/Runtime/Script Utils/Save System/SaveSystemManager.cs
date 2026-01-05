using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveSystemManager : MonoBehaviour
    {
        private List<ISaveableData> saveableDatas;
        public List<ISaveData> saveDatas;

        public void SaveAllData()
        {
            foreach (ISaveableData data in saveableDatas)
                foreach(ISaveData saveData in saveDatas)
                    data.Save<ISaveData>(saveData);
        }

        public void LoadAllData()
        {
            foreach (ISaveableData data in saveableDatas)
                foreach (ISaveData saveData in saveDatas)
                    data.Load<ISaveData>(saveData);
        }

        public List<ISaveableData> FindAllSaveableDataObjects()
        {
            IEnumerable<ISaveableData> saveableDataObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<ISaveableData>();

            return new List<ISaveableData>(saveableDataObjects);
        }
    }
}