[Does Visual Studio Code have box select/multi-line edit?](http://stackoverflow.com/questions/30384442/does-visual-studio-code-have-box-select-multi-line-edit)

##Question

The title says it all. I've been using ATOM and I think they are doing great with their direction, 
problem I have is that it is terribly slow on bootup and still has some bugs. I heard Microsoft
released a new editor called Visual Studio Code and it looks pretty good, one key feature that
I need is multi-line edit and I can't seem to find anything about it having it.

##Answer


Press Ctrl+Alt+Down or Ctrl+Alt+Up to insert cursors below or above.
share

###Comment
  
Note: Your graphics card provider might overwrite these default shortcuts. – Pere Pages Apr 26 at 13:40 
2 
  
You may want to edit your shortcuts. Go to File > Preferences > Keyboard Shortcuts. As an example:[ { "key": "ctrl+alt+numpad2", "command": "editor.action.insertCursorBelow", "when": "editorTextFocus" },{ "key": "ctrl+alt+numpad8", "command": "editor.action.insertCursorAbove", "when": "editorTextFocus" } ] 
