[Unit testing for Server.MapPath](http://stackoverflow.com/questions/19563106/unit-testing-for-server-mappath)



You can use dependancy injection and abstraction over Server.MapPath

```cs
public interface IPathProvider
{
   string MapPath(string path);
}
```

And production implementation would be:

```cs
public class ServerPathProvider : IPathProvider
{
     public string MapPath(string path)
     {
          return HttpContext.Current.Server.MapPath(path);
     }
}
```

While testing one:

```cs
public class TestPathProvider : IPathProvider
{
    public string MapPath(string path)
    {
        return Path.Combine(@"C:\project\",path);
    }
}
```
