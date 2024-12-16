# SunshineResolutionTool
 A tool to automatically change display modes for Sunshine game streaming.

 Conveniently detects when a Sunshine client connects, and automatically adjusts resolution, refreshrate, and HDR enablement. Settings are restored to default state when client disconnects.

# Prerequisite
 Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main).
 
 Run Powershell **as admin**, and exectute the following command:
 
 `Install-Module -Name WindowsDisplayManager`
 
 **Note**: You may need to set the [execution policy](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies) to unrestricted:
 
 `Set-ExecutionPolicy Unrestricted`

 # Setup
 1. Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main) (instructions above).
 2. Download `SunshineResolutionTool.exe` from the [releases section](https://github.com/blake502/SunshineResolutionTool/releases).
 3. Run `SunshineResolutionTool.exe` to create the `settings.cfg` file.
 4. Modify the `settings.cfg` file.
 5. Copy both `SunshinResolutionTool.exe` and `settings.cfg` to a safe location (IE: `C:\Program Files\Sunshine`)
 6. Navigate to `%appdata%\Microsoft\Windows\Start Menu\Programs\Startup`
 7. Create a new shortcut in that location. The shortcut should point to this command:
 
 `powershell "start 'C:\Program Files\Sunshine\SunshineResolutionTool.exe' -WindowStyle Hidden"`

 **Note**: Update the location to wherever you placed `SunshineResolutionTool.exe`.
 
 8. Name the shortcut `SunshineResolutionTool`.
 9. Modify the shortcut to "Start in" the same directory as your executable.
 10. Run the shortcut.

# TODO
- Automatic install script
- Documentation
