using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveSlotID
    {
        /// The slot id
        public readonly string id;

        /// Instances of the <see cref="SaveData"/> linked to this object
        public readonly List<SaveData> dataInstances;

        public SaveSlotID(string id, List<SaveData> dataInstances)
        {
            this.id = id;
            this.dataInstances = dataInstances;
        }

        /// <summary>
        /// Adds a data instance to <see cref="dataInstances"/>
        /// </summary>
        /// <param name="dataInstance">instance to add</param>
        public void AddDataInstance(SaveData dataInstance)
        {
            dataInstances.Add(dataInstance);
        }
    }
}