using UnityEngine;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveDataID
    {
        public string id;
        public string fileName;
        public ISaveData dataInstance;
        public Type classType;
    }
}