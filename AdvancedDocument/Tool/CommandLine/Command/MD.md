[MD](http://ss64.com/nt/md.html)

MD
Make Directory - Creates a new folder. 
Syntax
      MD [drive:]path

Key
   The path can consist of any valid characters up to the maximum path length available
You should avoid using the following characters in folder names - they are known to cause problems

© ® " - & ' ^ ( ) and @

Many extended characters will not be recognised by older 16 bit windows applications.

The maximum length of a full pathname (folders and filename) under NTFS or FAT is 260 characters. 

Folder names are not case sensitive, but only folder names longer than 8 characters will always retain their case, as typed.
Errorlevels
If the Directory was successfully created %ERRORLEVEL% = 0
Directory could not be created = 1
Examples
C:\temp> MD MyFolder
Make several folders with one command
C:\temp> MD Alpha Beta Gamma
will create

C:\temp\Alpha\
C:\temp\Beta\
C:\temp\Gamma\ 

Make an entire path 
MD creates any intermediate directories in the path, if needed. 
For example, assuming \utils does not exist then: 
MD \utils\downloads\Editor 
   
   is the same as: 
   
   md \utils 
   cd \utils 
   md downloads 
   cd downloads 
   md Editor 

for long filenames include quotes

MD "\utils\downloads\Super New Editor" 
You cannot create a folder with the same name as any of the following devices:
CON, PRN, LPT1, LPT2 ..LPT9, COM1, COM2 ..COM9
This limitation ensures that redirection to these devices will always work.

If you plan to copy data onto CDROM avoid folder trees more than 8 folders deep.

MKDIR is a synonym for MD 

"We are American at puberty. We die French" - Evelyn Waugh 

Related:

RD - Delete folders or entire folder trees
MKLINK / Linkd - Link an NTFS directory to a target object.
powershell: New-Item -path c:\ -name "Demo Folder" -type directory
Equivalent bash command (Linux): mkdir - Create new folder(s)