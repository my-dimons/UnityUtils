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
    public class SaveData
    {
        /// Save file name to write files to
        public string saveFileName;

        /// The last time this script was saved
        public long lastTimeSavedTicks;

        /// The last time this script was loaded
        public long lastTimeLoadedTicks;

        [NonSerialized]
        /// The last <see cref="DateTime"/> the object was saved at
        public DateTime lastTimeSaved;
        
        [NonSerialized]
        /// The last <see cref="DateTime"/> the object was loaded at
        public DateTime lastTimeLoaded;

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

        /// <summary>
        /// Any actions to preform when saving
        /// </summary>
        public void Save()
        {
            lastTimeSaved = DateTime.Now;
        }

        /// <summary>
        /// Any actions to preform when loading
        /// </summary>
        public void Load()
        {
            lastTimeLoaded = DateTime.Now;
        }
    }
}
