
```
System.InvalidOperationException occurred
  HResult=0x80131509
  Message=The DbContextOptions passed to the GlobalContext constructor must be a DbContextOptions<GlobalContext>. When registering multiple DbContext types make sure that the constructor for each context type has a DbContextOptions<TContext> parameter rather than a non-generic DbContextOptions parameter.
  Source=<Cannot evaluate the exception source>
  StackTrace:
   at Microsoft.EntityFrameworkCore.DbContext..ctor(DbContextOptions options)
   at Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext`1..ctor(DbContextOptions options)
   at Rwby.Global.Service.GlobalContext..ctor(DbContextOptions options) in D:\Source\MyGithub\Rwby\src\Rwby.Global\Rwby.Global.Service\Repository\GlobalContext.cs:line 16
```

##

DbContextOptions 使用泛型

```cs
public GlobalContext(DbContextOptions<GlobalContext> options) : base(options)
    {
    }
```