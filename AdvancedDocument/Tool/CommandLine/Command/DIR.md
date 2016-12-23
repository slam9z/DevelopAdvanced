Display a list of files and subfolders

## display folder all file

Syntax
      DIR [pathname(s)] [display_format] [file_attributes] [sorted] [time] [options]
Key
   [pathname] The drive, folder, and/or files to display, 
              this can include wildcards:
                 *   Match any characters
                 ?   Match any ONE character

   [display_format]
   /P   Pause after each screen of data.
   /W   Wide List format, sorted horizontally.
   /D   Wide List format, sorted by vertical column.

   [file_attributes] /A[:]attribute 

   /A:D  Folder         /A:-D  NOT Folder
   /A:R  Read-only      /A:-R  NOT Read-only 
   /A:H  Hidden         /A:-H  NOT Hidden
   /A:A  Archive        /A:-A  NOT Archive
   /A:S  System file    /A:-S  NOT System file
   /A:I  Not content indexed Files  /A:-I  NOT content indexed
   /A:L  Reparse Point  /A:-L  NOT Reparse Point (symbolic link)

   /A:X  No scrub file  /A:-X  Scrub file    (Windows 8+)
   /A:V  Integrity      /A:-V  NOT Integrity (Windows 8+)

   /A    Show all files
   Several attributes can be combined e.g. /A:HD-R

   [sorted]   Sorted by /O[:]sortorder

   /O:N   Name                  /O:-N   Name
   /O:S   file Size             /O:-S   file Size
   /O:E   file Extension        /O:-E   file Extension
   /O:D   Date & time           /O:-D   Date & time
   /O:G   Group folders first   /O:-G   Group folders last
   several attributes can be combined e.g. /O:GEN

   [time] /T:  the time field to display & use for sorting

   /T:C   Creation
   /T:A   Last Access
   /T:W   Last Written (default)

   [options]
   /S     include all subfolders.
   /R     Display alternate data streams. (Vista and above)
   /B     Bare format (no heading, file sizes or summary).
   /L     use Lowercase.
   /Q     Display the owner of the file.

   /N     long list format where filenames are on the far right.
   /X     As for /N but with the short filenames included.

   /C     Include thousand separator in file sizes. 
   /-C    Don’t include thousand separator in file sizes.

   /4     Display four-digit years
The switches above can be preset by adding them to an environment variable called DIRCMD. 
For example: SET DIRCMD=/O:N /S

Override any preset DIRCMD switches by prefixing the switch with - 
For example: DIR *.* /-S
Upper and Lower Case filenames: 
Filenames longer than 8 characters - will always display the filename with mixed case as entered.
Filenames shorter than 8 characters - can display the filename in upper or lower case - this can vary from one client to another (registry setting)

To obtain a bare DIR format (no heading or footer info) but retain all the details, pipe the output of DIR into FIND, this assumes that your date separator is /
DIR c:\temp\*.* | FIND "/"
Normally DIR /b will return just the filename, however when displaying subfolders with DIR /b /s the command will return a full pathname.
All file sizes are shown in bytes.
Examples
List the contents of c:\demo including ALL files:
dir /a c:\demo\
List the contents of c:\demo displaying only the filenames:
dir /b c:\demo\
List all the Reparse Points (symbolic links) in the current users profile:
dir %USERPROFILE% /a:i
List the contents of c:\demo with the full path of each file (source)
for %%A in ("c:\demo\*") do echo %%~fA
List the contents of c:\demo, without the header/footer details:
FOR /f "tokens=*" %%G IN ('dir c:\demo\*.* ^| find "/"') DO echo %%G
On Windows Vista and later, a list of alternate data streams can be obtained using DIR /R, on earlier operating systems, the SysInternals utility streams can be used instead.
DIR is an internal command.
“There it was, hidden in alphabetical order” ~ Rita Holt

Related

WHERE - Locate and display files in a directory tree.
XCOPY /L - List files without copying.
ROBOCOPY /L - List files with specific properties 
DIRUSE - show size of multiple subfolders. (Resource Kit)
Freedisk.exe - check free disk space. (Win 2K ResKit)
Powershell: Get-ChildItem - Get child items (contents of a folder or registry key) dir / ls / gci
You can also get File Sizes and Date/Time from Batch Parameters
Use DIR to display drive status - disk missing / ready / empty
Equivalent bash command (Linux): ls - List information about file(s)
Equivalent Powershell: Get-ChildItem