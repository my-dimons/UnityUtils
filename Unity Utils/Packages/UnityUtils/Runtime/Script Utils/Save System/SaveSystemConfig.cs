using UnityEngine;

namespace UnityUtils.ScriptUtils.SaveSystem
{
    public static class SaveSystemConfig
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
    }
}
