using System;
using UnityEngine;
using UnityEngine.Rendering;

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

        /// Used to get the type of inherited class to serialize/deserialize
        public string classTypeName;

        /// If true, will encrypt/decrypt the save file when saving/loading
        public bool useEncryption;

        /// <summary>
        /// Set data variables for the save data
        /// </summary>
        /// <param name="saveFileName">File name to set to</param>
        /// <param name="useEncryption">If true, will use encryption</param>
        /// <param name="classType">Class inheriting this class</param>
        public void SetData(string saveFileName, bool useEncryption, Type classType)
        {
            this.saveFileName = saveFileName;
            this.useEncryption = useEncryption;
            this.classTypeName = classType.AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets the type of the inherited class
        /// </summary>
        public Type GetClassType()
        {
            return Type.GetType(classTypeName);
        }
    }
}
