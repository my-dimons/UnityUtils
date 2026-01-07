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
        public string saveDataID;
        public string saveFileName;
        public string classTypeName;
        public bool useEncryption;

        public void SetData(string saveDataID, string saveFileName, bool useEncryption)
        {
            this.saveDataID = saveDataID;
            this.saveFileName = saveFileName;
            this.useEncryption = useEncryption;
            this.classTypeName = this.GetType().AssemblyQualifiedName;
        }

        public Type GetClassType()
        {
            return Type.GetType(classTypeName);
        }
    }
}
