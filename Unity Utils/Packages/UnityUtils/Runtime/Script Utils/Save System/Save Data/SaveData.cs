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
        public string saveFileName;
        public string classTypeName;
        public bool useEncryption;

        public void SetData(string saveFileName, bool useEncryption, Type classType)
        {
            this.saveFileName = saveFileName;
            this.useEncryption = useEncryption;
            this.classTypeName = classType.AssemblyQualifiedName;
        }

        public Type GetClassType()
        {
            return Type.GetType(classTypeName);
        }
    }
}
