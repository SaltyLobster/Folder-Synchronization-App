using System;
using System.IO;

namespace Folder_Synchronization_App
{
    public class FolderSynchronizer
    {
        private readonly string _sourcePath;
        private readonly string _replicaPath;
        private readonly Logger _logger;

        public FolderSynchronizer(string sourcePath, string replicaPath, Logger logger)
        {
            _sourcePath = sourcePath;
            _replicaPath = replicaPath;
            _logger = logger;
        }
        public void Synchronize()
        {
            try
            {
                _logger.Log("Starting synchronization.");

                if (!Directory.Exists(_sourcePath))
                {
                    _logger.LogError($"Source folder does not exist: {_sourcePath}");
                    return;
                }

                if (!Directory.Exists(_replicaPath))
                {
                    Directory.CreateDirectory(_replicaPath);
                    _logger.Log($"Created replica folder: {_replicaPath}");
                }

                SyncDirectory(_sourcePath, _replicaPath);

                _logger.Log("Synchronization completed");
            }
            catch (Exception ex)
            {
                _logger.LogError("Synchronization failed", ex);
            }
        }
        private void SyncDirectory(string sourceDir, string replicaDir)
        {
            // Copy/Update files from source
            string[] sourceFiles = Directory.GetFiles(sourceDir);
            foreach (string sourceFile in sourceFiles)
            {
                string fileName = Path.GetFileName(sourceFile);
                string replicaFile = Path.Combine(replicaDir, fileName);

                if (!File.Exists(replicaFile))
                {
                    File.Copy(sourceFile, replicaFile);
                    _logger.Log($"Copied: {sourceFile} -> {replicaFile}");
                }
                else if (!FileComparer.AreFilesIdentical(sourceFile, replicaFile))
                {
                    File.Copy(sourceFile, replicaFile, true);
                    _logger.Log($"Updated: {replicaFile}");
                }    
            }

            // Copy/Update subdirectories
            string[] sourceDirectories = Directory.GetDirectories(sourceDir);
            foreach (string sourceSubDir in sourceDirectories)
            {
                string dirName = Path.GetFileName(sourceSubDir);
                string replicaSubDir = Path.Combine(replicaDir, dirName);

                if (!Directory.Exists(replicaSubDir))
                {
                    Directory.CreateDirectory(replicaSubDir);
                    _logger.Log($"Created directory: {replicaSubDir}");
                }

                SyncDirectory(sourceSubDir, replicaSubDir);
            }

            // Delete the files from replica that don't exist in source
            string[] replicaFiles = Directory.GetFiles(replicaDir);
            foreach (string replicaFile in replicaFiles)
            {
                string fileName = Path.GetFileName(replicaFile);
                string sourceFile = Path.Combine(sourceDir, fileName);

                if (!File.Exists(sourceFile))
                {
                    File.Delete(replicaFile);
                    _logger.Log($"Deleted: {replicaFile}");
                }
            }

            // Remove directories from replica that don't exist in source
            string[] replicaDirectories = Directory.GetDirectories(replicaDir);
            foreach (string replicaSubDir in replicaDirectories)
            {
                string dirName = Path.GetFileName(replicaSubDir);
                string sourceSubDir = Path.Combine(sourceDir, dirName);

                if (!Directory.Exists(sourceSubDir))
                {
                    Directory.Delete(replicaSubDir, true);
                    _logger.Log($"Deleted directory: {replicaSubDir}");
                }
            }
        }
    }
}
