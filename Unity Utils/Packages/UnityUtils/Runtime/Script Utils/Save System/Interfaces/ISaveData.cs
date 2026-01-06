using System;
using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    /// <summary>
    /// Implimenting this class means you must have "[System.Serializable]" above your class name
    /// </summary>
    public interface ISaveData
    {
        /// Data ID is used in the <see cref="SaveDataRegistry"/>. set string like so: public string DataID => nameof(CLASS_NAME);
        string DataID { get; }
    }
}
