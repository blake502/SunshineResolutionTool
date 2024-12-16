# SunshineResolutionTool
 A tool to automatically change resolution for Sunshine game streaming.

# Prerequisite
 Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main).
 Run Powershell **as admin**.
 Exectute the following command:
 `Install-Module -Name WindowsDisplayManager`
 NOTE: You may need to set the [execution policy](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies) to unrestricted:
 `Set-ExecutionPolicy Unrestricted`

 # Setup
 1. Install [WindowsDisplayManager](https://github.com/patrick-theprogrammer/WindowsDisplayManager/tree/main) (instructions above).
 2. Download `SunshineResolutionTool.exe` from the [releases section](https://github.com/blake502/SunshineResolutionTool/releases).
 3. Run `SunshineResolutionTool.exe` to create the `settings.cfg` file.
 4. Modify the `settings.cfg` file.
 5. Copy both `SunshinResolutionTool.exe` and `settings.cfg` to a safe location (IE: `C:\Program Files\Sunshine`)
 6. Navigate to `%appdata%\Microsoft\Windows\Start Menu\Programs\Startup`
 7. Create a shortcut with this command: `powershell "start 'C:\Program Files\Sunshine\SunshineResolutionTool.exe' -WindowStyle Hidden"`
 8. Name the shortcut `SunshineResolutionTool`.
 9. Modify the shortcut to "Start in" the same directory as your executable.
 10. Run the shortcut.