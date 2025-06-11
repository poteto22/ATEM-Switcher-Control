#define MyAppName "ATEM Switcher Control"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Your Name"
#define MyAppExeName "WinformApp.exe"

[Setup]
AppId={{YOUR-UNIQUE-APP-ID-HERE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=installer
OutputBaseFilename=ATEM-Switcher-Control-Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "thai"; MessagesFile: "compiler:Languages\Thai.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Main application files
Source: "bin\Release\net6.0-windows\win-x64\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net6.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

; BMDSwitcherAPI file
Source: "lib\BMDSwitcherAPI64.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\ATEM Switcher Control"; Filename: "{app}\WinformApp.exe"
Name: "{commondesktop}\ATEM Switcher Control"; Filename: "{app}\WinformApp.exe"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent 