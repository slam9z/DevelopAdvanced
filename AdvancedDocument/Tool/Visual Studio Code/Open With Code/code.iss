Root: HKCU; Subkey: "SOFTWARE\Classes\*\shell\{#RegValueName}"; ValueType: expandsz; ValueName: ""; ValueData: "Open with {#NameShort}"; Tasks: addcontextmenufiles; Flags: uninsdeletekey

Root: HKCU; Subkey: "SOFTWARE\Classes\*\shell\{#RegValueName}"; ValueType: expandsz; ValueName: "Icon"; ValueData: "{app}\{#ExeBasename}.exe"; Tasks: addcontextmenufiles

Root: HKCU; Subkey: "SOFTWARE\Classes\*\shell\{#RegValueName}\command"; ValueType: expandsz; ValueName: ""; ValueData: """{app}\{#ExeBasename}.exe"" ""%1"""; Tasks: addcontextmenufiles

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\shell\{#RegValueName}"; ValueType: expandsz; ValueName: ""; ValueData: "Open with {#NameShort}"; Tasks: addcontextmenufolders; Flags: uninsdeletekey

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\shell\{#RegValueName}"; ValueType: expandsz; ValueName: "Icon"; ValueData: "{app}\{#ExeBasename}.exe"; Tasks: addcontextmenufolders

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\shell\{#RegValueName}\command"; ValueType: expandsz; ValueName: ""; ValueData: """{app}\{#ExeBasename}.exe"" ""%V"""; Tasks: addcontextmenufolders

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\background\shell\{#RegValueName}"; ValueType: expandsz; ValueName: ""; ValueData: "Open with {#NameShort}"; Tasks: addcontextmenufolders; Flags: uninsdeletekey

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\background\shell\{#RegValueName}"; ValueType: expandsz; ValueName: "Icon"; ValueData: "{app}\{#ExeBasename}.exe"; Tasks: addcontextmenufolders

Root: HKCU; Subkey: "SOFTWARE\Classes\directory\background\shell\{#RegValueName}\command"; ValueType: expandsz; ValueName: ""; ValueData: """{app}\{#ExeBasename}.exe"" ""%V"""; Tasks: addcontextmenufolders

Root: HKCU; Subkey: "SOFTWARE\Classes\Drive\shell\{#RegValueName}"; ValueType: expandsz; ValueName: ""; ValueData: "Open with {#NameShort}"; Tasks: addcontextmenufolders; Flags: uninsdeletekey

Root: HKCU; Subkey: "SOFTWARE\Classes\Drive\shell\{#RegValueName}"; ValueType: expandsz; ValueName: "Icon"; ValueData: "{app}\{#ExeBasename}.exe"; Tasks: addcontextmenufolders

Root: HKCU; Subkey: "SOFTWARE\Classes\Drive\shell\{#RegValueName}\command"; ValueType: expandsz; ValueName: ""; ValueData: """{app}\{#ExeBasename}.exe"" ""%V"""; Tasks: addcontextmenufolders