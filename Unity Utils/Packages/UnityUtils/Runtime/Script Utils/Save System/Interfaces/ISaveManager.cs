using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public interface ISaveManager
    {
        public void Save();

        public void Load();

        public void InitializeData();
    }
}

