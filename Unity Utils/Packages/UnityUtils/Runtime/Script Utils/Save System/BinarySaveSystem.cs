using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class BinarySaveSystem
    {
        /// <summary>
        /// Serializes the specified input data to a save file to be loaded later
        /// </summary>
        /// <remarks>Uses binary serialization to write the transformed data to disk. If a
        /// file with the specified name already exists, it will be overwritten</remarks>
        /// <typeparam name="TData">The type of the data to be serialized. Must implement <see cref="ISaveData"/></typeparam>
        /// <typeparam name="TInput">The type of the input data to be transformed and saved.</typeparam>
        /// <param name="inputData">The input data to be transformed and saved to the file.</param>
        /// <param name="fileName">The name of the file to which the data will be saved. Do not include any file extensions, file extension is determined by <see cref="SaveSystemConfig"/></param>
        /// <param name="createSaveData">A function that transforms the input data into a serializable object of type <typeparamref name="TData"/></param>
        public static void Save<TData, TInput>(TInput inputData, string fileName, Func<TInput, TData> createSaveData) where TData : ISaveData
        {
            BinaryFormatter formatter = new();
            string path = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.SPECIAL_SAVE_FILE_EXTENSION);

            FileStream stream = new(path, FileMode.Create);
            SaveSystemUtils.LogSaveFileCreated(path);

            TData data = createSaveData(inputData);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        /// <summary>
        /// Loads and deserializes save data of the specified type from a file with the given name
        /// </summary>
        /// <remarks>Ensure that <typeparamref name="T"/> is compatible with the serialized data format</remarks>
        /// <typeparam name="T">The type of save data to load. Must implement <see cref="ISaveData"/></typeparam>
        /// <param name="fileName">The name of the file from which to load the save data.</param>
        public static T Load<T>(string fileName) where T : ISaveData
        {
            string path = SaveSystemUtils.GetSaveFilePath(fileName, SaveSystemConfig.SPECIAL_SAVE_FILE_EXTENSION);

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Open);

                T data = (T) formatter.Deserialize(stream);

                stream.Close();

                return data;
            } 
            else
            {
                SaveSystemUtils.LogSaveFileNotFound(path);
                return default;
            }
        }
    }
}
