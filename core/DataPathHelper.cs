using System;
using System.IO;

namespace LibraryManagementSystem.core
{
    /// <summary>
    /// Centralized helper for managing all data file paths.
    /// All JSON data files are stored in: {ApplicationExecutableDirectory}/Data/
    /// When running from Visual Studio, this resolves to: bin/Debug/Data/
    /// </summary>
    internal static class DataPathHelper
    {
        private static readonly string _dataDirectory;

        static DataPathHelper()
        {
            // Get the directory where the application executable (.exe) is running
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            // Create a 'Data' subdirectory for all JSON files
            _dataDirectory = Path.Combine(baseDirectory, "Data");
            
            // Ensure the Data directory exists
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
                Console.WriteLine($"Created data directory at: {_dataDirectory}");
            }
        }

        /// <summary>
        /// Gets the full path for a data file by name (e.g., "borrow.json")
        /// </summary>
        public static string GetDataFilePath(string fileName)
        {
            return Path.Combine(_dataDirectory, fileName);
        }

        /// <summary>
        /// Gets the path for a subdirectory within Data (e.g., "notifications")
        /// </summary>
        public static string GetDataSubdirectoryPath(string subdirectoryName)
        {
            return Path.Combine(_dataDirectory, subdirectoryName);
        }

        /// <summary>
        /// Gets the root data directory path
        /// </summary>
        public static string GetDataDirectory()
        {
            return _dataDirectory;
        }

        /// <summary>
        /// Ensures a subdirectory exists within the Data folder
        /// </summary>
        public static string EnsureDataSubdirectory(string subdirectoryName)
        {
            string subdirectoryPath = GetDataSubdirectoryPath(subdirectoryName);
            if (!Directory.Exists(subdirectoryPath))
            {
                Directory.CreateDirectory(subdirectoryPath);
            }
            return subdirectoryPath;
        }
    }
}
