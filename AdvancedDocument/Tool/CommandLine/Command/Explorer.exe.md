Explorer.exe
Command-line switches that you can use to open the GUI Windows Explorer (Explorer.exe). 
Syntax
      Explorer.exe options

Options
    /n            Open a new single-pane window for the default selection. This is usually the root
                  of the drive Windows is installed on. If the window is already open, a duplicate opens.

    /e            Open Windows Explorer in its default view.

  (,)/root,object Open the specified object in a window view.

   /select,object Open a window view with the specified folder, file or application selected.

   /separate      Launch the explorer instance as a separate process.
                  (This is an undocumented feature)
Quotation marks are required if the File/Folder object contains spaces or symbols.
Explorer.exe is normally found in the Windows folder, typically C:\Windows\Explorer.exe 
Examples
Open an Explorer window with the 'C:\Demo' folder displayed:
Explorer.exe "C:\Demo"
Open an Explorer window with the 'examples' folder displayed and its parent hidden:
Explorer.exe /root,"C:\Demo\examples"
Open an Explorer window with SS64App selected:
Explorer.exe /select,"C:\Demo\SS64App.exe"
Open an Explorer window with C: expanded and SS64App selected:
Explorer.exe /e,/root,"C:\Demo\SS64App.exe"
Open an Explorer window with the share \\server64\FileShare1 :
Explorer.exe /root,"\\server64\FileShare1" 
Open an Explorer window with TestApp.exe selected in the share\\server64\FileShare1 :
Explorer.exe /root,\\server64\FileShare1,select,SS64App.exe 
Open an Explorer window at the root of the system drive C:\
Explorer \
Open an Explorer window at 'My Documents'
Explorer \\
or
Explorer /
Open an Explorer window at the 'Computer'
Explorer , 
“From the growth of the Internet through to the mapping of the human genome and our understanding of the human brain, the more we understand, the more there seems to be for us to explore” ~ Martin Rees

Related:
Shell: folder - Shortcuts to key folders.
CMD - Start a new CMD shell.
RUN Start | RUN commands.
START - Start a program, command or batch file.
ProfileFolders - Location of user profile folders.
Q152457 - Windows Explorer Command-Line Options.