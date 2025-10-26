using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Folder_Synchronization_App
{
    public class Logger
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();
        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;


            string? logDirectory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }
        public void Log(string message)
        {
            string logEntry = $"[{DateTime.Now:G}] {message}";

            Console.WriteLine(logEntry);

            lock(_lockObject)
            {
                try
                {
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write to logs: {ex.Message}");
                }
            }
        }
    }
}

    