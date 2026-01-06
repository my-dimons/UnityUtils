using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public interface ISaveManager
    {
        /// Save data function
        public void Save();

        /// Load data function
        public void Load();

        /// 
        public void InitializeData();
    }
}

