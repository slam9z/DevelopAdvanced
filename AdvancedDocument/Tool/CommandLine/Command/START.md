[START](http://ss64.com/nt/start.html)


Start a program, command or batch script (opens in a new window.)
Syntax
      START "title" [/D path] [options] "command" [parameters]

Key:
   title       Text for the CMD window title bar (required.)
   path        Starting directory.
   command     The command, batch file or executable program to run.
   parameters  The parameters passed to the command.

Options:
   /MIN         Start window Minimized.
   /MAX         Start window Maximized.
   /W or /WAIT  Start application and wait for it to terminate.
                (for an internal cmd command or a batch file this runs CMD /K)

   /LOW         Use IDLE priority class.
   /NORMAL      Use NORMAL priority class.
   /ABOVENORMAL Use ABOVENORMAL priority class.
   /BELOWNORMAL Use BELOWNORMAL priority class.
   /HIGH        Use HIGH priority class.
   /REALTIME    Use REALTIME priority class.

   /B         Start application without creating a new window. In this case
              ^C will be ignored - leaving ^Break as the only way to 
              interrupt the application.
   /I         Ignore any changes to the current environment.
              Use the original environment passed to cmd.exe

   /NODE      The preferred Non-Uniform Memory Architecture (NUMA)
              node as a decimal integer.

   /AFFINITY  The processor affinity mask as a hexadecimal number.
              The process will be restricted to running on these processors.

   Options for 16-bit WINDOWS programs only

   /SEPARATE  Start in separate memory space. (more robust) 32 bit only.
   /SHARED    Start in shared memory space. (default) 32 bit only.
Always include a TITLE this can be a simple string like "My Script" or just a pair of empty quotes ""
According to the Microsoft documentation, the title is optional, but depending on the other options chosen you can have problems if it is omitted.
If command is an internal cmd command or a batch file then the command processor is run with the /K switch to cmd.exe. This means that the window will remain after the command has been run.
Document files can be invoked through their file association just by typing the name of the file as a command. 
e.g. START "" MarchReport.DOC will launch the application associated with the .DOC file extension and load the document. 
To minimise any chance of the wrong exectuable being run, specify the full path to command and/or (at a minimum) include the file extension: START notepad.exe
If you START an application without a file extension (for example WinWord instead of WinWord.exe)then the PATHEXT environment variable will be read to determine which file extensions to search for and in what order. The default value for the PATHEXT variable is: .COM;.EXE;.BAT;.CMD
Multiprocessor systems
Processor affinity is assigned as a hex number but calculated from the binary positions (similar to NODRIVES) 
Hex Binary        Processors
 1 00000001 Proc 1 
 3 00000011 Proc 1+2
 7 00000111 Proc 1+2+3
 C 00001100 Proc 3+4 etc
Specifying /NODE allows processes to be created in a way that leverages memory locality on NUMA systems. For example, two processes that communicate with each other heavily through shared memory can be created to share the same preferred NUMA node in order to minimize memory latencies. They allocate memory from the same NUMA node when possible, and they are free to run on processors outside the specified node. 
start /NODE 1 app1.exe
start /NODE 1 app2.exe 
These two processes can be further constrained to run on specific processors within the same NUMA node. 
In the following example, app1 runs on the low-order two processors of the node, while app2 runs on the next two processors of the node. This example assumes the specified node has at least four logical processors. Note that the node number can be changed to any valid node number for that computer without having to change the affinity mask.
start /NODE 1 /AFFINITY 0x3 app1.exe 
start /NODE 1 /AFFINITY 0xc app2.exe
Running executable (.EXE) files
When a file that contains a .exe header, is invoked from a CMD prompt or batch file (with or without START), it will be opened as an executable file. The filename extension does not have to be .EXE. The file header of executable files start with the 'magic sequence' of ASCII characters 'MZ' (0x4D, 0x5A) The 'MZ' being the initials of Mark Zibowski, a Microsoft employee at the time the file format was designed.
Command Extensions
If Command Extensions are enabled, external command invocation through the command line or the START command changes as follows: 
Non-executable files can be invoked through their file association just by typing the name of the file as a command. (e.g. WORD.DOC would launch the application associated with the .DOC file extension). This is based on the setting in HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ext\OpenWithList, or if that is not specified, then the file associations - see ASSOC and FTYPE. 
When executing a 32-bit GUI application, CMD.EXE does not wait for the application to terminate before returning to the command prompt. This new behavior does NOT occur if executing within a command script.
In Windows XP this requires the use of start /wait:
start /wait /b First.exe
start /wait /b Second.exe
When executing a command line whose first token is the string CMD without an extension or path qualifier, then CMD is replaced with the value of the COMSPEC variable. This prevents picking up CMD.EXE from the current directory. 
When executing a command line whose first token does NOT contain an extension, then CMD.EXE uses the value of the COMSPEC environment variable. This prevents picking up CMD.EXE from the current directory.
When executing a command line whose first token does NOT contain an extension, then CMD.EXE uses the value of the PATHEXT environment variable to determine which extensions to look for and in what order. The default value for the PATHEXT variable is: .COM;.EXE;.BAT;.CMD Notice the syntax is the same as the PATH variable, with semicolons separating the different elements.
When searching for an executable, if there is no match on any extension, then looks to see if the name matches a directory name. If it does, the START command launches the Explorer on that path. If done from the command line, it is the equivalent to doing a CD /D to that path.
Errorlevels
If the command is successfully started %ERRORLEVEL% = unchanged (possibly a bug)
If the command fails to start then ERRORLEVEL = 9059
START /WAIT batch_file - will return the ERRORLEVEL specified by EXIT
Examples
Run a minimised Login script:
START "My Login Script" /Min Login.cmd
Start a program and wait for it to complete before continuing:
START "" /wait autocad.exe
Open a file with a particular program:
START "" "C:\Program Files\Microsoft Office\Winword.exe" "D:\Docs\demo.txt"
Open Windows Explorer and list the files in the current folder (.) :
C:\any\old\directory> START .
Connect to a new printer: (this will setup the print connection/driver )
START \\print_server\printer_name

Start an application and specify where files will be saved (Working Directory):
START /D C:\Documents\ /MAX "Maximised Notes" notepad.exe
START is an internal command.
“Do not run; scorn running with thy heels” ~ Shakespeare, The Merchant of Venice

Related:
WMIC process call create "c:\some.exe","c:\exec_dir" - This method returns the PID of the started process.
CALL - Call one batch program from another 
CMD - can be used to call a subsequent batch and ALWAYS return even if errors occur.
Powershell: Invoke-Item - Invoke an executable or open a file (ii)
Q162059 - Opening Office documents
Equivalent bash command (Linux) : open - Open a file in its default application.