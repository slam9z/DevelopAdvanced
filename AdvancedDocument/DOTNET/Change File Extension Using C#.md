[Change File Extension Using C#](http://stackoverflow.com/questions/5259961/change-file-extension-using-c-sharp)



There is: Path.ChangeExtension method. E.g.:

```cs
var result = Path.ChangeExtension(myffile, ".jpg");
```

In the case if you also want to physically change the extension, you could use File.Move method:

```cs
File.Move(myffile, Path.ChangeExtension(myffile, ".jpg"));
```
