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
        /// <summary>
        /// Generates the full file path for a save file using the specified file name and sequence number (For multi-save possibilities).
        /// </summary>
        /// <remarks>The returned path is located within the application's persistent data directory. Use
        /// this method to ensure save files are stored in a consistent and platform-appropriate location.</remarks>
        /// <param name="fileName">The base name of the save file</param>
        /// <param name="sequence">The sequence number to append to the file name. Defaults to 0 if not specified</param>
        /// <returns>A string containing the absolute path to the save file</returns>
        public static string GetSaveFilePath(string fileName, string extension) => Application.persistentDataPath + "/" + fileName + extension;

        /// <summary>
        /// Logs an error message indicating that a save file was not found at the specified path.
        /// </summary>
        /// <param name="path">The full file system path of the save file that could not be located</param>
        public static void LogSaveFileNotFound(string path)
        {
            Debug.Log(SaveSystemConfig.SAVE_FILE_NOT_FOUND_ERROR + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been created at the specified path.
        /// </summary>
        /// <param name="path">The full file system path where the save file was created</param>
        public static void LogSaveFileCreated(string path)
        {
            Debug.Log(SaveSystemConfig.SAVE_FILE_CREATED_MESSAGE + path);
        }

        /// <summary>
        /// Logs a message indicating that a save file has been loaded at the specified path.
        /// </summary>
        /// <param name="path">The file system path of the loaded save file</param>
        public static void LogSaveFileLoaded(string path)
        {
            Debug.Log(SaveSystemConfig.SAVE_FILE_LOADED_MESSAGE + path);
        }
    }
}