[File Providers in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/file-providers)

## File Provider abstractions

File Providers are an abstraction over file systems. The main interface is IFileProvider. IFileProvider exposes methods to get file information (IFileInfo), directory information (IDirectoryContents), and to set up change notifications (using an IChangeToken).

IFileInfo provides methods and properties about individual files or directories. It has two boolean properties, Exists and IsDirectory, as well as properties describing the file's Name, Length (in bytes), and LastModified date. You can read from the file using its CreateReadStream method.


## File Provider implementations

Three implementations of IFileProvider are available: Physical, Embedded, and Composite. The physical provider is used to access the actual system's files. The embedded provider is used to access files embedded in assemblies. The composite provider is used to provide combined access to files and directories from one or more other providers.

### PhysicalFileProvider

The PhysicalFileProvider provides access to the physical file system. It wraps the System.IO.File type (for the physical provider), scoping all paths to a directory and its children. 


### EmbeddedFileProvider

The EmbeddedFileProvider is used to access files embedded in assemblies. In .NET Core, you embed files in an assembly with the <EmbeddedResource> element in the .csproj file:


## Watching for changes

## Globbing patterns

File system paths use wildcard patterns called globbing patterns. These simple patterns can be used to specify groups of files. The two wildcard characters are `*` and `**`.


## Recommendations for use in apps

> 感觉DI容易污染底层的dll?