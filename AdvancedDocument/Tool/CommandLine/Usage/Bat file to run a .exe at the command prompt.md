[Bat file to run a .exe at the command prompt](http://stackoverflow.com/questions/221730/bat-file-to-run-a-exe-at-the-command-prompt)

##answer

To start a program and then close command prompt without waiting for program to exit:

```
start /d "path" file.exe
```

>可以使用绝对路径和相对路径