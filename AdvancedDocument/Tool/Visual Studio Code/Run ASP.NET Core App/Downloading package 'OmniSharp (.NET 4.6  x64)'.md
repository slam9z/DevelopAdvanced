[omnisharp-vscode package](https://github.com/OmniSharp/omnisharp-vscode/blob/master/package.json)

[Always show "Downloading package 'OmniSharp (.NET 4.6 / x64)' (12310 KB) ](https://github.com/OmniSharp/omnisharp-vscode/issues/1028)


[The .NET Core Debugger is still being downloaded. See the C# Output Window for more information.](https://github.com/OmniSharp/omnisharp-vscode/issues/1430)


## Download

[omnisharp-vscode package](https://github.com/OmniSharp/omnisharp-vscode/blob/master/package.json)

> 打开里面的C# Extention依赖包的Url，使用百度云离线功能慢慢下才下载好的。


```json
{
      "description": "OmniSharp (.NET 4.6 / x64)",
      "url": "https://omnisharpdownload.blob.core.windows.net/ext/omnisharp-win-x64-1.17.0.zip",
      "installPath": "./bin/omnisharp",
      "platforms": [
        "win32"
      ],
      "architectures": [
        "x86_64"
      ],
      "installTestPath": "./bin/omnisharp/OmniSharp.exe"
},

{
      "description": ".NET Core Debugger (Windows / x64)",
      "url": "https://vsdebugger.azureedge.net/coreclr-debug-1-10-0/coreclr-debug-win7-x64.zip",
      "fallbackUrl": "https://vsdebugger.blob.core.windows.net/coreclr-debug-1-10-0/coreclr-debug-win7-x64.zip",
      "installPath": ".debugger",
      "runtimeIds": [
        "win7-x64"
      ],
      "installTestPath": "./.debugger/vsdbg-ui.exe"
}
```


##  install

[Always show "Downloading package 'OmniSharp (.NET 4.6 / x64)' (12310 KB) ](https://github.com/OmniSharp/omnisharp-vscode/issues/1028)


Sorry for the delay. I needed to get 1.6 out the door before taking some time to write up the steps below. Note that this isn't much fun, but you can use these steps to download the necessary packages and installing them manually. This really isn't supported but it should work.

    Download the relevant packages using the URLs from the "runtimeDependencies" node in the extension's package.json file. Make a note of which package entries in the "runtimeDependencies" node you're downloading from because you'll use additional information from those entries in the next steps.
        Windows:
            OmniSharp: Either "OmniSharp (.NET 4.6 / x86)" or "OmniSharp (.NET 4.6 / x64)" depending on whether you're running 32-bit or 64-bit Windows respectively.
            Debugger: ".NET Core Debugger (Windows / x64)" (Note: 32-bit Windows is not yet supported by the debugger. If you're running 32-bit Windows, you can skip the debugger.)
        OSX:
            Mono: "Mono Runtime (macOS)" and "Mono Framework Assemblies"
            OmniSharp: "OmniSharp (Mono 4.6)"
            Debugger: ".NET Core Debugger (macOS / x64)"
        Linux:
            Mono: Either "Mono Runtime (Linux / x86)" or "Mono Runtime (Linux / x64)" (depending on whether your're running on 32-bit or 64-bit Linux), and "Mono Framework Assemblies"
            OmniSharp: "OmniSharp (Mono 4.6)"
            Debugger: A version of the debugger that matches your distro (there are several).

    Unzip each package that you downloaded into a subdirectory within the extension install folder.
        Extension install folder: The extension install folder is located at %UserProfile%\.vscode\extensions\ms-vscode.csharp-<version> on Windows and ~/.vscode/extensions/ms-vscode.csharp-<version> on OSX/Linux.
        Subdirectory: The subdirectory is specified by the package's "installPath" from the "runtimeDependencies" node.

    For example, on 64-bit Windows, you'd unzip "OmniSharp (.NET 4.6 / x64)" to %UserProfile%\.vscode\extensions\ms-vscode.csharp-<version>\bin\omnisharp.

    (OSX/Linux-only) Mark files in the "binaries" section of a package's "runtimeDependencies" entry as executable with chmod 755 <file path>. Note that these file paths are relative to the package's "installPath".

    Create a 0-length file called "install.Lock" in the extension install folder. This is easily done with touch install.Lock in an OSX/Linux terminal window or copy NUL > install.Lock at a Windows command prompt.

And that should be it! If everything is right, you should be able to launch Visual Studio Code with a C# .NET Core project and get IntelliSense and debugging (if the debugger is supported on your platform).

