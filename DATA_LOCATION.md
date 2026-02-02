# Data File Location Guide

## Overview
All JSON data files for the Library Management System are stored in a centralized, predictable location: **`bin\Debug\Data\`**

This directory structure is automatically created and managed by the application on first run.

## File Locations

When you run the application from Visual Studio (Debug), all data files are stored at:

```
LibraryManagementSystem\bin\Debug\Data\
├── borrow.json              # Borrow requests and returns
├── reject.json              # Rejected borrow requests
├── books.json               # Book inventory
├── students.json            # Student records
├── users.json               # User accounts
├── category.json            # Book categories
└── notifications\           # Student notifications
    ├── Student Name 1\
    │   └── notifications.json
    ├── Student Name 2\
    │   └── notifications.json
    └── ...
```

## Why This Location?

### Application Architecture
- `AppDomain.CurrentDomain.BaseDirectory` always points to where the .exe is running
- In Visual Studio Debug mode, this is: `bin\Debug\`
- In Release builds, this is: `bin\Release\`
- The app automatically creates a `Data` subdirectory to keep files organized

### Centralized Configuration
All controllers use the **`DataPathHelper`** class to access data files:

```csharp
// Example: Getting the borrow.json path
string borrowPath = DataPathHelper.GetDataFilePath("borrow.json");

// Example: Getting the notifications directory
string notificationsDir = DataPathHelper.EnsureDataSubdirectory("notifications");
```

## Key Points

✅ **Single Source of Truth**: All data files are in one consistent location
✅ **Auto-Creation**: The `Data` directory is created automatically on first run
✅ **Rebuild Safe**: Files persist even when Visual Studio rebuilds the solution
✅ **Easy to Find**: Always look in `bin\Debug\Data\` when debugging
✅ **Portable**: The relative path structure works across machines

## Locating Your Data

If you need to:
- **Edit data manually**: Open `bin\Debug\Data\borrow.json` (not anywhere else)
- **Backup data**: Copy the entire `bin\Debug\Data\` folder
- **Debug file issues**: Check the actual location shown in console output
- **Reset data**: Delete files from `bin\Debug\Data\` (they'll be recreated on next run if needed)

## Controlled by DataPathHelper

The `DataPathHelper` class (located in `core\DataPathHelper.cs`) is the single entry point for all file path resolution:

```csharp
// In BorrowController
filePath = DataPathHelper.GetDataFilePath("borrow.json");

// In HomeController  
string borrowPath = GetBorrowPath();  // Which calls DataPathHelper internally

// In MonitoringController
string borrowPath = DataPathHelper.GetDataFilePath("borrow.json");
```

## Console Output

When you run the application, look for this message in the console:
```
Created data directory at: C:\...\LibraryManagementSystem\bin\Debug\Data
```

This confirms the data directory location and that it's ready to use.

## Important Notes

- ⚠️ Do NOT manually create JSON files elsewhere - the app won't find them
- ⚠️ Do NOT rely on files in the project root - the app only reads from `bin\Debug\Data\`
- ⚠️ If you're checking files and they're empty/missing, verify you're looking in `bin\Debug\Data\`
- ⚠️ When debugging, use the `Get Output Folder` command or check `bin\Debug\Data\` directly

## Moving to Production

When deploying to production:
1. Keep the same relative path structure (Data\ subdirectory)
2. The `DataPathHelper` will automatically resolve to the correct location
3. Optional: Modify `DataPathHelper.cs` to use a custom path if needed (e.g., `%AppData%\LibraryManagement\`)
