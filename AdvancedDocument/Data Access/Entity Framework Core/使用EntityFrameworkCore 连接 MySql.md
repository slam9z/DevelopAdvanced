[使用EntityFrameworkCore 连接 MySql](http://www.cnblogs.com/uptothesky/p/6077502.html)


上篇文章介绍了如何在dotnetcore下使用Dapper连接MySql，这里再介绍使用使用EntityFrameworkCore 连接 MySql。

新建控制台项目，安装下面两个nuget包：

```
Install-Package Microsoft.EntityFrameworkCore
Install-Package MySql.Data.EntityFrameworkCore -Pre
```

定义两个类及Context：

```cs
public class BlogContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=10.255.19.111;database=mydb;uid=root;pwd=yourpassword;");
        }
        public DbSet<Blog> blog { get; set; }
        public DbSet<User> User { get; set; }
    }
    
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }

```

Main中测试：

```cs
using (var context = new BlogContext())
            {
                //User user = new User() { Name = "Herry", CreateTime = DateTime.Now };
                //context.User.Add(user);
                Blog blog = new Blog() { Url = "http://mysite.com" };
                context.blog.Add(blog);
                context.SaveChanges();
            }
```



这里需要注意了：

```cs
public DbSet<Blog> blog { get; set; }
```

如果mysql里的表名blog是小写的，这里就要定义成跟表名完全一样，否则会报错，在连接MsSqlServer的时候如果表名是Blogs，那么可以如下定义：

```cs
public DbSet<Blog> Blogs { get; set; }
```

如果表名是Blog，可以使用注解属性：
复制代码

```cs
[Table("Blog")]
public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }

```

这种注解属性在mysql里是不起作用的，所以User上的注解是没有作用的。