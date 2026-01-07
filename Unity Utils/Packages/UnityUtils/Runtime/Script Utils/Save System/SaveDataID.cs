using UnityEngine;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveDataID
    {
        /// The ID that this object is linked to
        public readonly string id;

        /// The file name that this object is linked to
        public readonly string fileName;

        /// Instance of the <see cref="SaveData"/> linked to this object
        public readonly SaveData dataInstance;
        
        /// The parent class classType
        public readonly Type classType;

        /// Wether or not this save data should be encrypted
        public readonly bool useEncryption;

        public SaveDataID(string id, string fileName, SaveData dataInstance, Type classType, bool useEncryption)
        {
            this.id = id;
            this.fileName = fileName;
            this.dataInstance = dataInstance;
            this.classType = classType;
            this.useEncryption = useEncryption;
        }
    }
}