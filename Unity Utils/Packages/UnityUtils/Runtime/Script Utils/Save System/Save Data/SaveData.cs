using System;
using UnityEngine;
using UnityEngine.Rendering;
using Newtonsoft.Json;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    /// <summary>
    /// Implimenting this class means you must have "[System.Serializable]" above your class name
    /// </summary>
    [Serializable]
    public abstract class SaveData
    {
        /// Save file name to write files to
        public string saveFileName;

        /// The save slot this data belongs to
        [JsonIgnore]
        public SaveSlot saveSlot;

        /// <summary>
        /// Set data variables for the save data
        /// </summary>
        /// <param name="saveFileName">File name to set to</param>
        /// <param name="useEncryption">If true, will use encryption</param>
        /// <param name="classType">Class inheriting this class</param>
        public void SetData(string saveFileName)
        {
            this.saveFileName = saveFileName;
        }

        public void SetSaveSlot(SaveSlot saveSlot)
        {
            this.saveSlot = saveSlot;
        }

        /// <summary>
        /// Any actions to preform when saving
        /// </summary>
        public virtual void Save()
        {

        }

        /// <summary>
        /// Any actions to preform when loading
        /// </summary>
        /// <remarks>Unless data is written, this will not end up saving the data</remarks>
        public virtual void Load()
        {
            
        }
    }
}
