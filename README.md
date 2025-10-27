# Folder Synchronization Application

A simple C# console application that performs one-way synchronization between two folders, maintaining an identical copy of a source folder in a replica folder.

## Quick Start for Testing

1. **Clone the repository**
```bash
git clone https://github.com/SaltyLobster/Folder-Synchronization-App.git
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
Folder-Synchronization-App/
├── .git/                                    # Git repository data
├── .vs/                                     # Visual Studio cache (ignored)
├── bin/                                     # Compiled binaries (ignored)
├── obj/                                     # Build objects (ignored)
├── TestFolders/                             # Test data folders (ignored)
├── .gitignore                               # Git ignore rules
├── FileComparer.cs                          # File comparison logic
├── Folder Synchronization App.csproj        # C# project file
├── Folder Synchronization App.sln           # Visual Studio solution file
├── FolderSynchronizer.cs                    # Main synchronization logic
├── Logger.cs                                # Logging functionality
├── Program.cs                               # Application entry point
└── README.md                                # Project documentation
```

## File Descriptions

- **Program.cs** - Entry point of the application
- **FolderSynchronizer.cs** - Core synchronization engine
- **FileComparer.cs** - Compares files for differences
- **Logger.cs** - Manages logging to file and console
- **Folder Synchronization App.csproj** - Project configuration and dependencies

## License

This project is created as a test task demonstration.

## Author

Created by Yevhenii Buriak as a test task for Junior QA in Dev at Veeam.