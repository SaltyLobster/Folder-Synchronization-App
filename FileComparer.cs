using System;
using System.IO;
using System.Security.Cryptography;

namespace FolderSynchronizationApp
{
    class FileComparer
    {
        public static string CalculateFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }
        public static bool AreFilesIdentical(string file1Path, string file2Path)
        {
            if (!File.Exists(file1Path) || !File.Exists(file2Path))
            {
                return false;
            }

            FileInfo file1Info = new FileInfo(file1Path);
            FileInfo file2Info = new FileInfo(file2Path);

            if (file1Info.Length != file2Info.Length)
            {
                return false;
            }

            string hash1 = CalculateFileHash(file1Path);
            string hash2 = CalculateFileHash(file2Path);

            return hash1 == hash2;

        }
    }
}
