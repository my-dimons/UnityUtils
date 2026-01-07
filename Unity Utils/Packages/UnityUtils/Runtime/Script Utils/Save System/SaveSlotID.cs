using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public class SaveSlotID
    {
        /// The slot id
        public readonly string id;

        /// Instances of the <see cref="ISaveData"/> linked to this object
        public readonly List<ISaveData> dataInstances;

        public SaveSlotID(string id, List<ISaveData> dataInstances)
        {
            this.id = id;
            this.dataInstances = dataInstances;
        }

        /// <summary>
        /// Adds a data instance to <see cref="dataInstances"/>
        /// </summary>
        /// <param name="dataInstance">instance to add</param>
        public void AddDataInstance(ISaveData dataInstance)
        {
            dataInstances.Add(dataInstance);
        }
    }
}