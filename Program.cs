using System;
using System.Threading;

namespace FolderSynchronizationApp
{
    class Program
    {
        private static Timer? _timer;
        private static FolderSynchronizer? _synchronizer;
        private static Logger? _logger;
        private static int _intervalSeconds;

        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: FolderSync <source_path> <replica_path> interval_seconds> <log_file_path>");
                Console.WriteLine("Example: FolderSync C:\\Source C:\\Replica 60 C:\\Logs\\sync.log");
                return;
            }

            string sourcePath = args[0];
            string replicaPath = args[1];
            string intervalInput = args[2];
            string logFilePath = args[3];

            if (!int.TryParse(intervalInput, out int intervalSeconds) || intervalSeconds <= 0)
            {
                Console.WriteLine("Error: Interval must be a positive number");
                return;
            }

            _intervalSeconds = intervalSeconds;

            try
            {
                _logger = new Logger(logFilePath);
                _logger.Log("Application started.");

                if (Path.GetFullPath(sourcePath).Equals(Path.GetFullPath(replicaPath), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Source and Replica folders cannot have the same path");
                    _logger.Log("Application stopped: Source and Replica paths are identical");
                    Console.WriteLine("Press ENTER to close the Application");
                    Console.ReadLine();
                    return;
                }

                if (Path.GetFullPath(replicaPath).StartsWith(Path.GetFullPath(sourcePath) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                    {
                    Console.WriteLine("Replica folder cannot be inside the Source folder");
                    _logger.Log("Application stopped: Replica is subdirectory of Source folder");
                    Console.WriteLine("Press ENTER to close the application");
                    Console.ReadLine();
                    return;
                    }

                _synchronizer = new FolderSynchronizer(sourcePath, replicaPath, _logger);

                _timer = new Timer(SyncCallback, null, 0, _intervalSeconds * 1000);
                _logger.Log($"Synchronization interval is {_intervalSeconds} seconds");
                Console.WriteLine("Press ENTER to stop...");

                Console.ReadLine();

                _timer?.Dispose();
                _logger.Log("Application stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                _logger?.LogError("Fatal application error", ex);
            }
        }

        private static void SyncCallback (object? state)
        {
            if (_synchronizer == null || _logger == null)
                return;

            try
            {
                _synchronizer.Synchronize();
            }
            catch (Exception ex)
            {
                _logger?.LogError("Error during synchronization callback", ex);
            }
        }
    }
}
