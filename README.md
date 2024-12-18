# SunshineResolutionTool
 A tool to automatically change display modes for Sunshine game streaming.

 Conveniently detects when a Sunshine client connects, and automatically adjusts resolution, refreshrate, and HDR enablement. Settings are restored to default state when client disconnects.

# Prerequisite
 Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main).
 
 Run Powershell **as admin**, and exectute the following command:
 
 `Install-Module -Name WindowsDisplayManager`

 # Setup
 1. Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main) (instructions above).
 2. Download `SunshineResolutionTool.exe` from the [releases section](https://github.com/blake502/SunshineResolutionTool/releases).
 3. Run `SunshineResolutionTool.exe` to create the `settings.cfg` file.
 4. Modify the `settings.cfg` file to specify your desired resolutions, refresh rates, and HDR enablements.

**Note**: If you only want to use SunshineResolutionTool for this session, simply run the tool. Otherwise, follow the rest of the instructions to have SunshineResolutionTool launch in the background automatically at login.
   
 5. Copy both `SunshinResolutionTool.exe` and `settings.cfg` to a safe location (IE: `C:\Program Files\Sunshine`)
 6. Navigate to `%appdata%\Microsoft\Windows\Start Menu\Programs\Startup`
 7. Create a new shortcut in that location. The shortcut should point to this command:
 
 `"C:\Program Files\Sunshine\SunshineResolutionTool.exe" -hide`

 **Note**: Update the location to wherever you placed `SunshineResolutionTool.exe`. Be sure to include the `-hide` argument.
 
 8. Run the shortcut.

# TODO
- Automatic install script
- Documentation
