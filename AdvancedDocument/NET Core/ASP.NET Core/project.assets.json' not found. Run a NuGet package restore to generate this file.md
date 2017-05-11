[project.assets.json' not found. Run a NuGet package restore to generate this file]()

然后一堆错误，手工生成这个文件可以才可以`build`，可以`run`。

```
E:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.MvcClient>dotnet restore
  Restoring packages for E:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.MvcClient\Rwby.Global.MvcClient.csproj...
  Restoring packages for E:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.MvcClient\Rwby.Global.MvcClient.csproj...
  Restoring packages for E:\Source\MyGithub\Rwby\src\Rwby.Global\IdentityConstants\IdentityConstants.csproj...
  Lock file has not changed. Skipping lock file write. Path: E:\Source\MyGithub\Rwby\src\Rwby.Global\IdentityConstants\obj\project.assets.json
  Restore completed in 462.41 ms for E:\Source\MyGithub\Rwby\src\Rwby.Global\IdentityConstants\IdentityConstants.csproj.
  Restore completed in 981.78 ms for E:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.MvcClient\Rwby.Global.MvcClient.csproj.
```

restore 两个Rwby.Global.MvcClient.csproj

## solution

```
dotnet restore --no-cache
```

> 不知道与1.1.2版本的包有关系吗