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

        /// The error message used when a save file cannot be found at the specified location
        public const string SAVE_FILE_NOT_FOUND_ERROR = "Save file not found in: ";

        /// The message used to when a save file has been loaded
        public const string SAVE_FILE_LOADED_MESSAGE = "Save file loaded from: ";

        /// The message displayed when a save file is created
        public const string SAVE_FILE_CREATED_MESSAGE = "Save file created in: ";

        /// The message displayed when a save file is encryped
        public const string SAVE_FILE_ENCRYPTED_MESSAGE = "Save file encryped in: ";

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
            Debug.Log(SAVE_FILE_NOT_FOUND_ERROR + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been created at the specified path.
        /// </summary>
        /// <param name="path">The full file system path where the save file was created</param>
        public static void LogSaveFileCreated(string path)
        {
            Debug.Log(SAVE_FILE_CREATED_MESSAGE + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been loaded at the specified path.
        /// </summary>
        /// <param name="path">The file system path of the loaded save file</param>
        public static void LogSaveFileLoaded(string path)
        {
            Debug.Log(SAVE_FILE_LOADED_MESSAGE + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been encrypted at the specified path.
        /// </summary>
        /// <param name="path">The file system path of the loaded save file</param>
        public static void LogSaveFileEncrypted(string path)
        {
            Debug.Log(SAVE_FILE_ENCRYPTED_MESSAGE + path);
        }
    }
}