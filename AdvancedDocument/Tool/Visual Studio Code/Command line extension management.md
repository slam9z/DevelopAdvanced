
[Support to install and uninstall extensions from cli](https://github.com/Microsoft/vscode/issues/691)

## [Command line extension management](https://code.visualstudio.com/docs/editor/extension-gallery#_command-line-extension-management)


To make it easier to automate and configure VS Code, it is possible to list, install, and uninstall extensions from the command line. When identifying an extension, provide the full name of the form publisher.extension, for example donjayamanne.python.

Example:

```
code --list-extensions
code --install-extension ms-vscode.cpptools
code --uninstall-extension ms-vscode.csharp
code --disable-extensions
```


```
code --list-extensions

code --install-extension  alefragnani.project-manager
code --install-extension  donjayamanne.githistory
code --install-extension  donjayamanne.jquerysnippets
code --install-extension  fknop.vscode-npm
code --install-extension  jmrog.vscode-nuget-package-manager
code --install-extension  McCarter.start-git-bash
code --install-extension  mohsen1.prettify-json
code --install-extension  ms-vscode.csharp
code --install-extension  ms-vscode.vs-keybindings
code --install-extension  tomoki1207.pdf

```

> 这个功能非常好，再也不怕重新安装，到处找插件了。