using System.Collections;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveSystemUtils
    {
        /// The default file extension used for save files not needing a specific extension
        public const string SPECIAL_SAVE_FILE_EXTENSION = ".save";

        /// .json file extension
        public const string JSON_SAVE_FILE_EXTENSION = ".json";

        /// The default save slot ID, use when not using multiple save slots
        public const string DEFAULT_SAVE_SLOT_ID = "0";

        /// <summary>
        /// Generates the full file path for a save file using the specified file name and extension.
        /// </summary>
        /// <remarks>The returned path is located within the application's persistent data directory. Use
        /// this method to ensure save files are stored in a consistent and platform-appropriate location.</remarks>
        /// <param name="fileName">The base name of the save file</param>
        /// <param name="extension">The "." extension to add to the file (Ex. ".json")</param>
        /// <returns>A string containing the absolute path to the save file</returns>
        public static string GetSaveFilePath(string fileName, string extension) => Path.Combine(Application.persistentDataPath, fileName + extension);

        /// <summary>
        /// Generates the full file path for a save file using the specified file name.
        /// </summary>
        /// <remarks>The returned path is located within the application's persistent data directory. Use
        /// this method to ensure save files are stored in a consistent and platform-appropriate location.</remarks>
        /// <param name="fileName">The base name of the save file</param>
        /// <returns>A string containing the absolute path to the save file</returns>
        public static string GetSaveFilePath(string fileName) => Path.Combine(Application.persistentDataPath, fileName);

        /// <summary>
        /// Logs an error message indicating that a save file was not found at the specified path.
        /// </summary>
        /// <param name="path">The full file system path of the save file that could not be located</param>
        public static void LogSaveFileNotFound(string path)
        {
            Debug.Log("Save file not found in: " + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been created at the specified path.
        /// </summary>
        /// <param name="path">The full file system path where the save file was created</param>
        public static void LogSaveFileCreated(string path)
        {
            Debug.Log("Save file created in: " + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been loaded at the specified path.
        /// </summary>
        /// <param name="path">The file system path of the loaded save file</param>
        public static void LogSaveFileLoaded(string path)
        {
            Debug.Log("Save file loaded from: " + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been encrypted at the specified path.
        /// </summary>
        /// <param name="path">The file system path of the loaded save file</param>
        public static void LogSaveFileEncrypted(string path)
        {
            Debug.Log("Save file encryped in: " + path);
        }
    }
}