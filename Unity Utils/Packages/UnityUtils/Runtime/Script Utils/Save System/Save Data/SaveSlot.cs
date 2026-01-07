using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveSlot
    {
        /// The save slot id
        public readonly string saveSlotName;

        /// Instances of the <see cref="SaveData"/> linked to this object
        public readonly List<SaveData> saveDatas;

        public SaveSlot(string saveSlotName, List<SaveData> saveDatas)
        {
            this.saveSlotName = saveSlotName;
            this.saveDatas = saveDatas;
        }

        /// <summary>
        /// Adds a data instance to <see cref="saveDatas"/>
        /// </summary>
        /// <param name="dataInstance">instance to add</param>
        public void AddSaveData(SaveData dataInstance)
        {
            saveDatas.Add(dataInstance);
        }
    }
}