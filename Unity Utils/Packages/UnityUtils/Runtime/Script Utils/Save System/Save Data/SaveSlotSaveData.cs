using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    [Serializable]
    public class SaveSlotSaveData : SaveData
    {
        /// The last <see cref="DateTime"/> the object was saved at
        public DateTime lastTimeSaved;

        public override void Save()
        {
            lastTimeSaved = DateTime.Now;
            saveSlot.lastTimeSaved = lastTimeSaved;
        }

        public override void Load()
        {
            Debug.Log("Loaded SaveSlotSaveData: " + lastTimeSaved);
            saveSlot.lastTimeSaved = lastTimeSaved;
        }
    }
}