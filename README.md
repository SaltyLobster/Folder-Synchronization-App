# Folder Synchronization Application

A simple C# console application that performs one-way synchronization between two folders, maintaining an identical copy of a source folder in a replica folder.

## Features

- **One-way synchronization**: Replica folder becomes an exact copy of source folder
- **Periodic execution**: Automatically syncs at specified intervals
- **Comprehensive logging**: All operations logged to both console and file
- **MD5 hash comparison**: Efficiently detects file changes
- **Recursive directory support**: Handles nested folder structures
- **Command-line interface**: Easy to use and automate

## Requirements

- .NET 6.0 or higher
- Windows, Linux, or macOS

## Quick Start for Testing

1. **Clone the repository**
```bash
git clone <your-repository-url>
cd Folder_Synchronization_App
```

2. **Build the project**
```bash
dotnet build
```

3. **Run with provided test folders**
```bash
dotnet run TestFolders/Source TestFolders/Replica 10 test.log
```

4. **Test the synchronization**
   - Open another terminal/file explorer
   - Add, modify, or delete files in `TestFolders/Source`
   - Watch the console output and check `test.log` to see synchronization in action
   - Check `TestFolders/Replica` to verify it matches the source

5. **Stop the application**
   - Press `ENTER` in the console to stop

## How It Works

The application performs the following operations during each synchronization cycle:

1. **Copy new files** from source to replica
2. **Update modified files** in replica (detected via MD5 hash comparison)
3. **Create missing directories** in replica
4. **Remove extra files** from replica that don't exist in source
5. **Remove extra directories** from replica that don't exist in source

All operations are logged with timestamps to both the console and a log file.

## Usage

```bash
dotnet run <source_path> <replica_path> <interval_seconds> <log_file_path>
```

### Parameters

- `source_path` - Path to the source folder
- `replica_path` - Path to the replica folder (created if doesn't exist)
- `interval_seconds` - Synchronization interval in seconds (must be positive)
- `log_file_path` - Path to the log file (directory created if doesn't exist)

### Example

```bash
dotnet run "C:\MyDocuments" "D:\Backup\MyDocuments" 60 "sync.log"
```

This command will:
- Synchronize `C:\MyDocuments` to `D:\Backup\MyDocuments`
- Run synchronization every 60 seconds
- Log all operations to `sync.log`

The application will run continuously until you press ENTER to stop it.

## Example Output

```
[27.10.2025 7:33:21 PM] Application started.
[27.10.2025 7:33:21 PM] Synchronization interval is 10 seconds
[27.10.2025 7:33:21 PM] Starting synchronization...
[27.10.2025 7:33:21 PM] Created replica folder: TestFolders\Replica
[27.10.2025 7:33:21 PM] Copied: TestFolders\Source\document.txt -> TestFolders\Replica\document.txt
[27.10.2025 7:33:21 PM] Created directory: TestFolders\Replica\subfolder
[27.10.2025 7:33:21 PM] Copied: TestFolders\Source\subfolder\notes.txt -> TestFolders\Replica\subfolder\notes.txt
[27.10.2025 7:33:21 PM] Synchronization completed successfully.
Press ENTER to stop...
```

## Project Structure

```
Folder_Synchronization_App/
├── Program.cs              # Main entry point and Timer setup
├── Logger.cs               # Logging functionality
├── FileComparer.cs         # MD5 hash calculation and file comparison
├── FolderSynchronizer.cs   # Core synchronization logic
├── README.md
├── .gitignore
└── TestFolders/
    ├── Source/             # Sample source folder with test files
    │   ├── document.txt
    │   ├── image.jpg
    │   └── subfolder/
    │       └── notes.txt
    └── Replica/            # Auto-generated during testing (gitignored)
```

## Error Handling

The application includes comprehensive error handling:

- **Invalid arguments**: Shows usage instructions
- **Missing source folder**: Logs error and skips synchronization
- **File access errors**: Logged but doesn't crash the application
- **Log file errors**: Falls back to console-only logging

## Technical Details

- **File comparison**: Uses MD5 hashing for efficient change detection
- **Size optimization**: Compares file sizes before calculating hashes
- **Thread safety**: Logger uses locks to prevent concurrent write issues
- **Recursive sync**: Automatically handles nested directory structures
- **Timer-based**: Uses `System.Threading.Timer` for periodic execution

## Notes

- This is a one-way synchronization tool - changes in replica are overwritten
- Large files are handled efficiently through stream-based hashing
- The application creates necessary directories automatically
- Synchronization runs immediately on startup, then at specified intervals

## License

This project is created as a test task demonstration.

## Author

Created by Yevhenii Buriak as a test task for Junior QA in Dev at Veeam.
