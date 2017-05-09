
## devlopment

### install current .NET Core

[Download .NET Core](https://www.microsoft.com/net/download/core#/sdk)

```
dotnet restore
Welcome to .NET Core!
---------------------
Learn more about .NET Core @ https://aka.ms/dotnet-docs. Use dotnet --help to see available commands or go to  https://aka.ms/dotnet-cli-docs.
Telemetry
--------------
The .NET Core tools collect usage data in order to improve your experience. The data is anonymous and does not include command-line arguments. The data is collected by Microsoft and shared with the community.
You can opt out of telemetry by setting a DOTNET_CLI_TELEMETRY_OPTOUT environment variable to 1 using your favorite shell.
You can read more about .NET Core tools telemetry @ https://aka.ms/dotnet-cli-telemetry.
Configuring...
-------------------
A command is running to initially populate your local package cache, to improve restore speed and enable offline access.This   command will take up to a minute to complete and will only happen once.
Decompressing 100% 7222 ms
Expanding 37%
```

### VS Code C# plugin

`F1` view restore 
  
## run

```
Open  Debug panel

Debug  .NET Core Launch (web)
```

### debug

```
Run 'Debug: Download .NET Core Debugger' in the Command Palette or open a .NET project directory to download the .NET Core Debugger
```

```
Updating C# dependencies...
Platform: win32, x86_64

Downloading package 'OmniSharp (.NET 4.6 / x64)' (20702 KB) 

command 'csharp.downloadDebugger' not found

The .NET Core Debugger is still being downloaded. See the C# Output Window for more information.

```

> 无法debug,直接用dotnet run运行


### test

mstest


```
dotnet test
```

> 会出现中文乱码



### ohter

launchsetting













