

## Path root 的坑

### [Path.IsPathRooted](https://msdn.microsoft.com/en-us/library/system.io.path.ispathrooted(v=vs.110).aspx) 

以`\`开头的relative也会返回`true`。


### [Path.GetPathRoot](https://msdn.microsoft.com/en-us/library/system.io.path.getpathroot(v=vs.110).aspx)

也是这尿性


## check 

这样应该可以

```cs
public bool IsRelativePath(string path)
{

    var root = Path.GetPathRoot(path);
    if (string.IsNullOrWhiteSpace(root) || root[0] == Path.DirectorySeparatorChar)
    {
        return true;
    }
    return false;
}

```