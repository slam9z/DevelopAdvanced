[cd](http://ss64.com/nt/cd.html)

>这才是专业完整的回答

##CD

Change Directory - Select a Folder (and drive)

Syntax

```
CD [/D] [drive:][path]
CD [..]
```

Key

/D : change the current DRIVE in addition to changing folder.

CHDIR is a synonym for CD.

##Errorlevels

Current directory was changed = 0
Directory does not exist or is not accessible = 1

##Tab Completion

This allows changing current folder by entering part of the path and pressing TAB

```
C:> CD Prog [PRESS TAB] 
Will go to C:\Program Files\
```

Tab Completion is disabled by default, it has been known to create difficulty when using a 
batch script to process text files that contain TAB characters.

Tab Completion is turned on by setting the registry value shown below

```
REGEDIT4
[HKEY_CURRENT_USER\Software\Microsoft\Command Processor]
"CompletionChar"=dword:00000009
```

##Examples

Change to the parent directory:
C:\Work> CD .. 

Change to the grant-parent directory:
C:\Work\backup\January> CD ..\..

Change to the ROOT directory:
C:\Work\backup\January> CD \ 

Display the current directory in the specified drive:
C:\> CD D: 

Display the current drive and directory:
C:\Work> CD

Display the current drive and directory:
C:\Work> ECHO "%CD%"

In a batch file to display the location of the batch script file (%0) 
C:\> ECHO "%~dp0"

In a batch file to CD to the location of the batch script file (%0) 
C:\> CD /d "%~dp0"

Move down the folder tree with a full path reference to the ROOT folder...
C:\windows> CD \windows\java
C:\windows\java> 

Move down the folder tree with a reference RELATIVE to the current folder...
C:\windows> CD java
C:\windows\java> 

Move up and down the folder tree in one command...
C:\windows\java> CD ..\system32
C:\windows\system32>
If Command Extensions are enabled the CD command is enhanced as follows: 

The current directory string is not CASE sensitive. 
So CD C:\wiNdoWs will set the current directory to C:\Windows

CD does not treat spaces as delimiters, so it is possible to CD into a subfolder name that 
contains a space without surrounding the name with quotes. 

For example: 
cd \My folder

is the same as: 
cd "\My folder" 
An asterisk can be used to complete a folder name
e.g. C:> CD pro* will move to C:\Program Files 

##Change the Current Drive

Enter the drive letter followed by a colon 
C:> E:
E:> 

To change drive and directory at the same time, use CD with the /D switch
C:> cd /D E:\utils
E:\utils\>

CHDIR is a synonym for CD

CD is an internal command.

>“Change is the law of life. And those who look only to the past or the present are certain to miss the 
>future” ~ John F. Kennedy

##Related:

pushd - Change Directory
Q156276 - Cmd does not support UNC names as the current directory 
Powershell: Set-Location - Set the current working location
Equivalent bash command (Linux): cd - Change Directory