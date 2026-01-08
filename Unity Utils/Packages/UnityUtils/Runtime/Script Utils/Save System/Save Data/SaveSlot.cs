using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveSlot
    {
        /// The save slot id
        public readonly string saveSlotName;

        /// The last time this save slot was saved
        public DateTime lastTimeSaved;

        /// Instances of the <see cref="SaveData"/> linked to this object
        private readonly List<SaveData> saveDatas = new();

        public SaveSlot(string saveSlotName)
        {
            this.saveSlotName = saveSlotName;

            // Create save slot save data
            string path = SaveSystemUtils.GetSaveSlotFilePath(saveSlotName, SaveSystemUtils.SAVE_SLOT_SAVE_FILE_NAME);
            SaveData saveSlotSaveData = SaveSystemManager.CreateSaveData<SaveSlotSaveData>(path);
            AddSaveData(saveSlotSaveData);
        }

        /// <summary>
        /// Adds a data instance to <see cref="saveDatas"/>
        /// </summary>
        /// <param name="dataInstance">instance to add</param>
        public void AddSaveData(SaveData dataInstance)
        {
            saveDatas.Add(dataInstance);
            dataInstance.SetSaveSlot(this);
        }

        public List<SaveData> GetSaveDatas()
        {
            return saveDatas;
        }

        public void SaveDataList(List<SaveData> saveDataList)
        {
            foreach (SaveData saveData in saveDataList)
            {
                AddSaveData(saveData);
            }
        }

        public void SetSaveDataSlotList(List<SaveData> saveDataList)
        {
            foreach (SaveData saveData in saveDataList)
            {
                saveData.SetSaveSlot(this);
            }
        }
    }
}