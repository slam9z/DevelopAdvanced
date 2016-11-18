##

vscodeOpenFile.reg才是正确的

不过每打开一个文件都要新建window这也很烦，


##Right click 

有多余的\"

[Right click on Windows folder and open with Visual Studio Code](http://thisdavej.com/right-click-on-windows-folder-and-open-with-visual-studio-code/)

Windows Registry Editor Version 5.00

; This will handle right clicking on a file

[HKEY_CLASSES_ROOT\*\shell\Open with VS Code]
@="Open with Code"
"Icon"="C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe,0"

[HKEY_CLASSES_ROOT\*\shell\Open with VS Code\command]
@="\"C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe\" \"%1\""

; This will handle right clicking on a folder and open that folder
; as a new project

[HKEY_CLASSES_ROOT\Directory\shell\vscode]
@="Open Folder as VS Code Project"
"Icon"="\"C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe\",0"

[HKEY_CLASSES_ROOT\Directory\shell\vscode\command]
@="\"C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe\" \"%1\""

; This handles the case of right clicking inside of a folder
; to open that folder as a new project

[HKEY_CLASSES_ROOT\Directory\Background\shell\vscode]
@="Open Folder as VS Code Project"
"Icon"="\"C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe\",0"

[HKEY_CLASSES_ROOT\Directory\Background\shell\vscode\command]
@="\"C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe\" \"%V\""