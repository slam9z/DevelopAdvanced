*映射* 

## Fluent API

一千道一万，EF还是一个ORM工具，映射永远是最核心的部分。所以接下来详细介绍Code First模式下EF的映射配置。 

通过Code First来实现映射模型有两种方式*Data Annotation*和*Fluent API*。 

Data Annotation需要在实体类（我通常的称呼，一般就是一个Plain Object）的属性上以Attribute的方式表示主键、外键等映射信息。
这种方式不符合解耦合的要求所以一般不建议使用。 

第二种方式就是要重点介绍的Fluent API。Fluent API的配置方式将实体类与映射配置进行解耦合，有利于项目的扩展和维护。 

Fluent API方式中的核心对象是DbModelBuilder。 

在重写的DbContext的OnModelCreating方法中，我们可以这样配置一个实体的映射： 

```cs
protected override void OnModelCreating(DbModelBuilder modelBuilder)

{

    modelBuilder.Entity<Product>().HasKey(t => t.Id);

     

    base.OnModelCreating(modelBuilder);

} 
```

使用上面这种方式的一个问题是OnModelCreating方法会随着映射配置的增多越来越大。
一种更好的方式是继承EntityTypeConfiguration<EntityType>并在这个类中添加映射代码，如：


```cs

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        this.ToTable("Product");

        this.HasKey(p => p.Id);

        this.Property(p => p.Name).IsRequired(); 

    }

} 
```

然后将这个类的实例添加到modelBuilder的Configurations就可以了。

```cs
modelBuilder.Configurations.Add(new ProductMap()); 
```

如果不想手动一个个添加自定的映射配置类对象，还可以使用反射将程序集中所有的EntityTypeConfiguration<>一次性添加到modelBuilder.Configurations集合中
，下面的代码展示了这个操作（代码来自nopCommerce项目）：

```cs

var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()

.Where(type => !String.IsNullOrEmpty(type.Namespace))

.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

foreach (var type in typesToRegister)

{

    dynamic configurationInstance = Activator.CreateInstance(type);

    modelBuilder.Configurations.Add(configurationInstance);

} 
```

这样，OnModelCreating就大大简化，并且一劳永逸的是，以后添加新的实体映射只需要添加新的继承自EntityTypeConfiguration<>的XXXMap类而不需要修改OnModelCreating方法。

这种方式给实体和映射提供最佳的解耦合，强烈推荐。


>EF CodeFirst的自动发现

>例如我们的程序中有一个名为Employee的实体类，我们没有为其定义映射配置(EntityTypeConfiguration<Employee>)，但如果我们使用类似下面这样的代码去进行调用，EF会自动为Employee创建默认映射并进行迁移等一系列操作。
>
>
>
>var employeeList = context.Set<Employee>().ToList(); 
>
>
>
>当然为了能更灵活的配置映射，还是建议手动创建EntityTypeConfiguration<Employee>。
>
>另外2种情况下，EF也会自动创建映射。
>
>1.类A的对象作为类B的一个导航属性存在，如果类B被包含在EF映射中，则EF也会为类A创建默认映射。


>2.类A继承自类B，如果类A或类B中的一个被包含在EF映射中，则EF也会为另一个创建默认映射（且使用TPH方式进行，详见下文映射高级话题）。


 
通过上面的介绍可以看到EntityTypeConfiguration类正事Fluent API的核心，下面我们以EntityTypeConfiguration的方法为线，
依次了解如何进行Fluent API配置。

## EntityTypeConfiguration 基本方法

ToTable：指定映射到的数据库表的名称。

HasKey：配置主键（也用于配置关联主键）
 

Property：这个方法返回PrimitivePropertyConfiguration的对象，根据属性不同可能是子类StringPropertyConfiguration的对象。通过这个对象可以详细配置属性的信息如IsRequired()或HasMaxLength(400)。 

Ignore：指定忽略哪个属性（不映射到数据表） 

对于基本映射这几个方法几乎包括了一切，下面是个综合示例： 


```cs
ToTable("Product");

ToTable("Product","newdbo");//指定schema，不使用默认的dbo

HasKey(p => p.Id);//普通主键

HasKey(p => new {p.Id, p.Name});//关联主键

Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);//不让主键作为Identity自动生成

Property(p => p.Name).IsRequired().HasMaxLength(20).HasColumnName("ProductName").IsUnicode(false);//非空，最大长度20，自定义列名，列类型为varchar而非nvarchar

Ignore(p => p.Description); 
``` 


> * 使用modelBuilder.HasDefaultSchema("newdbo");可以给所有映射实体指定schema。

> * PrimitivePropertyConfiguration还有许多可配置的选项，如HasColumnOrder指定列在表中次序，IsOptional指定列是否可空，HasPrecision指定浮点数的精度等等，不再列举。 


## EntityTypeConfiguration 关联

下面一系列示例的主角是产品，为了配合演示还请了产品小伙伴们，它们将在演示过程中逐一登场。 


基本上，下面展示的关联的配置都可以从关联类的任意一方的EntityTypeConfiguration<T>开始配置。无论从哪一方起开始配置不同的写法最终都能实现相同的效果。
下面的示例将只展示其中之一配置的方式，等价的另一种配置不再展示。
 

产品类的基本结构如下，后面演示过程中将根据需要为其添加新的属性。 


```cs
public class Product

{

    public int Id{ get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

} 
```


### 1 - 1关联

（虽然看起来最简单，但这个好像是理解起来最麻烦的一种配置）

这种关联从实际关系上来看是两个类共享相同的值作为主键，比如有User表和UserPhoto表，他们都应该使用UserId作为主键，并且通过相同的UserId值进行关联。
但这种关系反映在数据库中必须通过外键的概念来实现，这时候就需要一个表的主键既作为主键又作为关联表的外键。
EF中各种配置方式无非就是告诉EF CodeFirst让那个表的主键作为另一个表的外键而已，现在不理解的，看一下下面的例子就明白了。
（其实，如果用Data Annotation配置反而很简单，[Key],[ForeignKey]标一标就可以了）

这节使用到的是保修卡这个角色，我们知道一个产品对应一个保修卡，产品和保修卡使用相同的产品编号。这正是我们说的1对1的好例子。

```cs
public class WarrantyCard

{

    public int ProductId { get; set; }

    public DateTime ExpiredDate { get; set; }

    public virtual Product Product { get; set; }

} 
```

我们给Product也增加保修卡属性：

```cs
public virtual WarrantyCard WarrantyCard { get; set; } 
```

下面来看看怎么把Product和WarrantyCard关联起来。经过&ldquo;千百&rdquo;次的尝试，终于找到了下面这些结果看起来很正确的组合，先列于下方，
后面慢慢分析：

```cs
public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

 

        //第一组（两条效果完全相同）

        HasRequired(p => p.WarrantyCard).WithRequiredDependent(i => i.Product);

        HasRequired(p => p.WarrantyCard).WithOptional(i => i.Product);

 

        //第二组（两条效果完全相同）

        HasRequired(p => p.WarrantyCard).WithRequiredPrincipal(i => i.Product);

        HasOptional(p => p.WarrantyCard).WithRequired(i => i.Product);

    }

}

 

public class WarrantyCardMap : EntityTypeConfiguration<WarrantyCard>

{

    public WarrantyCardMap()

    {

        ToTable("WarrantyCard");

        HasKey(i => i.ProductId);

    }

} 
```

除了以上这些组合，其它组合都没法达到效果（都会生成多余的外键）。

第一组Fluent API生成的迁移代码：

```cs
CreateTable(

    "dbo.Product",

    c => new

        {

            Id = c.Int(nullable: false),

            Name = c.String(),

            Description = c.String(maxLength: 200),

        })

    .PrimaryKey(t => t.Id)

    .ForeignKey("dbo.WarrantyCard", t => t.Id)

    .Index(t => t.Id);

 

CreateTable(

    "dbo.WarrantyCard",

    c => new

        {

            ProductId = c.Int(nullable: false, identity: true),

            ExpiredDate = c.DateTime(nullable: false),

        })

    .PrimaryKey(t => t.ProductId); 

```

值得注意的是，外键指定在Product表的Id列上，Product的主键Id不作为标识列。

再来看看第二组Fluent API生成的迁移代码：

```cs
CreateTable(

    "dbo.Product",

    c => new

        {

            Id = c.Int(nullable: false, identity: true),

            Name = c.String(),

            Description = c.String(maxLength: 200),

        })

    .PrimaryKey(t => t.Id);

 

CreateTable(

    "dbo.WarrantyCard",

    c => new

        {

            ProductId = c.Int(nullable: false),

            ExpiredDate = c.DateTime(nullable: false),

        })

    .PrimaryKey(t => t.ProductId)

    .ForeignKey("dbo.Product", t => t.ProductId)

    .Index(t => t.ProductId); 
```

变化就在于外键添加到WarrantyCard表的主键ProductId上，而且这个键也不做标识列使用了。

对于当前场景这两组配置应该选择那一组呢。对于产品和保修卡，肯定是先有产品后有保修卡，保修卡应该依赖于产品而存在。
所以第二组配置把外键设置到WarrantyCard的主键更为合适，让WarrantyCard依赖Product符合当前场景。
即Product作为Principal而WarrantyCard作为Dependent，其实这么多代码也无非就是明确两个关联对象Principal和Dependent的地位而已。

使用第二组配置创建表后，我们可以添加数据：

可以一次性添加保修卡和合格证：
 
```cs

var product = new Product()

{

    Name = "空调",

    Description = "冰冰凉",

    WarrantyCard = new WarrantyCard()

    {

        ExpiredDate = DateTime.Now.AddYears(3)

    }

};

context.Set<Product>().Add(product);

context.SaveChanges(); 

```
也可以分开进行：


```cs

var product = new Product()

{

    Name = "投影仪",

    Description = "高分辨率"

};

context.Set<Product>().Add(product);

context.SaveChanges();

 

WarrantyCard card = new WarrantyCard()

{

    ProductId = product.Id,

    ExpiredDate = DateTime.Now.AddYears(3)

};

context.Set<WarrantyCard>().Add(card);

context.SaveChanges(); 
```

对于查询来说，第一组和第二组配置生成的SQL相同。都是INNER JOIN，这里就不再列出了。


### 单向1 - *关联(可为空)

这里新登场角色是和发票，发票有自己的编号，有些产品有发票，有些产品没有发票。我们希望通过产品找到发票而又不需要由发票关联到产品。

```cs
public class Invoice

{

    public int Id { get; set; }

    public string InvoiceNo { get; set; }   

    public DateTime CreateDate { get; set; }

} 
```

产品类新增的属性如下：

```cs
public virtual Invoice Invoice { get; set; }

public int? InvoiceId { get; set; } 
```

可以使用如下代码创建Product到Invoice的关联

```cs

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

        HasOptional(p => p.Invoice).WithMany().HasForeignKey(p => p.InvoiceId);

    }

}

 

public class InvoiceMap : EntityTypeConfiguration<Invoice>

{

    public InvoiceMap()

    {

        ToTable("Invoice");

        HasKey(i => i.Id);

    }

} 
```

HasOptional表示一个产品可能会有发票，WithMany的参数为空表示我们不需要由发票关联到产品，HasForeignKey用来指定Product表中的外键列。

还可以通过WillCascadeOnDelete()配置是否级联删除，这个大家都知道，就不多说了。

运行迁移后，数据库生成的Product表外键可为空（注意实体类中表示外键的属性一定要为Nullable类型，不然迁移代码不能生成）。

下面写段代码来测试下这个映射配置，先是创建一个测试对象


```cs

var product = new Product()

{

    Name = "书",

    Description = "码农书籍",

    Invoice = new Invoice()//这里不创建Invoice也可以，因为其可以为null

    {

        InvoiceNo = "12345",

        CreateDate = DateTime.Now

    }

};

context.Set<Product>().Add(product);

context.SaveChanges(); 
```

然后查询，注意，创建和查询要分2次执行，不然不会走数据库，直接由EF Context返回结果了。


```cs

var productGet = context.Set<Product>().Include(p=>p.Invoice).FirstOrDefault(); 
```

通过SS Profiler可以看到生成的SQL如下：


```sql
SELECT TOP (1) 

    [Extent1].[Id] AS [Id], 

    [Extent1].[Name] AS [Name], 

    [Extent1].[Description] AS [Description], 

    [Extent1].[InvoiceId] AS [InvoiceId], 

    [Extent2].[Id] AS [Id1], 

    [Extent2].[InvoiceNo] AS [InvoiceNo], 

    [Extent2].[CreateDate] AS [CreateDate]

    FROM  [dbo].[Products] AS [Extent1]

    LEFT OUTER JOIN [dbo].[Invoices] AS [Extent2] ON [Extent1].[InvoiceId] = [Extent2].[Id] 
```

可以看到对于外键可空的情况，EF生成的SQL使用了LEFT OUTER JOIN，基本上复合我们的期待。

 

### 单向1 - *关联（不可为空）

为了演示这个关联，请出一个新对象合格证，合格证有自己的编号，而且一个产品是必须有合格证。


```cs

public class Certification

{

    public int Id { get; set; }

    public string Inspector { get; set; }

} 
```

我们给Product添加关联合格证的属性：


```cs

public virtual Certification Certification { get; set; }

public int CertificationId { get; set; } 

```

配置Product到Certification映射的代码与之前的类似，就是把HasOptional换成了HasRequired：
 
```cs

HasRequired(p => p.Certification).WithMany().HasForeignKey(p=>p.CertificationId); 
```

生成的迁移代码，外键列不能为空。创建对象时Product必须和Certification一起创建。生成的查询语句除了把LEFT OUTER JOIN换成INNER JOIN外其他都一样，
不再赘述。


### 双向1 - *关联

这是比较常见的场景，如一个产品可以对应多张照片，每张照片关联一个产品。先来看看新增的照片类：

```cs
public class ProductPhoto

{

    public int Id { get; set; }

    public string FileName { get; set; }

    public float FileSize { get; set; }

    public virtual Product Product { get; set; }

    public int ProductId { get; set; }

} 
```

给Product增加ProductPhoto集合：

```cs
public virtual ICollection<ProductPhoto> Photos { get; set; } 
```

然后是映射配置：

```cs
public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

        HasMany(p => p.Photos).WithRequired(pp => pp.Product).HasForeignKey(pp => pp.ProductId);

    }

}

 

public class ProductPhotoMap : EntityTypeConfiguration<ProductPhoto>

{

    public ProductPhotoMap()

    {

        ToTable("ProductPhoto");

        HasKey(pp => pp.Id);

    }

} 
```

代码很容易理解，HasMany表示Product中有多个ProductPhoto，WithRequired表示ProductPhoto一定会关联到一个Product。

我们来看另一种等价的写法（在ProductPhoto中配置关联）：

```cs
public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

    }

}

 

public class ProductPhotoMap : EntityTypeConfiguration<ProductPhoto>

{

    public ProductPhotoMap()

    {

        ToTable("ProductPhoto");

        HasKey(pp => pp.Id);

        HasRequired(pp => pp.Product).WithMany(p => p.Photos).HasForeignKey(pp => pp.ProductId);

    }

} 
```

有没有感觉和之前单向1 - *的配置很像？其实就是WithMany多了参数而已。随着例子越来越多，大家应该对这几个配置理解的越来越深了。
 

迁移到数据库后，我们添加些数据测试下： 



```cs

var product = new Product()

{

    Name = "投影仪",

    Description = "高分辨率"

};

context.Set<Product>().Add(product);

context.SaveChanges();

 

ProductPhoto pp1 = new ProductPhoto()

{

    FileName = "正面图",

    FileSize = 3,

    ProductId = product.Id

};

 

ProductPhoto pp2 = new ProductPhoto()

{

    FileName = "侧面图",

    FileSize = 5,

    ProductId = product.Id

};

 

context.Set<ProductPhoto>().Add(pp1);

context.Set<ProductPhoto>().Add(pp2);

context.SaveChanges(); 
```

试一试一次读取Product及ProductPhoto：

```cs
var productGet = context.Set<Product>().Include(p=>p.Photos).ToList(); 
```

生成的SQL如下：

```sql
SELECT

        [Limit1].[Id] AS [Id], 

        [Limit1].[Name] AS [Name], 

        [Limit1].[Description] AS [Description], 

        [Extent2].[Id] AS [Id1], 

        [Extent2].[FileName] AS [FileName], 

        [Extent2].[FileSize] AS [FileSize], 

        [Extent2].[ProductId] AS [ProductId], 

        CASE WHEN ([Extent2].[Id] IS NULL) THEN CAST(NULL AS int) ELSE 1 END AS [C1]

        FROM   (SELECT TOP (1) [c].[Id] AS [Id], [c].[Name] AS [Name], [c].[Description] AS [Description]

            FROM [dbo].[Product] AS [c] ) AS [Limit1]

        LEFT OUTER JOIN [dbo].[ProductPhoto] AS [Extent2] ON [Limit1].[Id] = [Extent2].[ProductId] 
```

有点小复杂，用LEFT OUTER JOIN的原因是，可能有的Product没有ProductPhoto。


### * - *关联 

这次轮到产品标签登场了。一个产品可以有多个标签，一个标签也可对应多个产品：
 

```cs


public class Tag

{

    public int Id { get; set; }

    public string Text { get; set; }

    public virtual ICollection<Product> Products { get; set; }

} 
```

给Product增加标签集合：


```cs

public virtual ICollection<Tag> Tags { get; set; } 
```

映射代码：

```cs

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

        HasMany(p => p.Tags).WithMany(t => t.Products).Map(m => m.ToTable("Product_Tag_Mapping"));

    }

}

 

public class TagMap : EntityTypeConfiguration<Tag>

{

    public TagMap()

    {

        ToTable("Tag");

        HasKey(t => t.Id);

    }

} 

```


比较特殊的就是需要指定一个关联表保存多对多的映射关系。


```cs


CreateTable(

    "dbo.Product_Tag_Mapping",

    c => new

        {

            Product_Id = c.Int(nullable: false),

            Tag_Id = c.Int(nullable: false),

        })

    .PrimaryKey(t => new { t.Product_Id, t.Tag_Id })

    .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)

    .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)

    .Index(t => t.Product_Id)

    .Index(t => t.Tag_Id); 
```

一般情况下使用自动生成的外键就好，也可以自己定义外键名称。


```cs
HasMany(p => p.Tags).WithMany(t => t.Products).Map(m =>

{

    m.ToTable("Product_Tag_Mapping");

    m.MapLeftKey("Pid");

    m.MapRightKey("Tid");

}); 
```

迁移代码变成如下：


```cs

CreateTable(

    "dbo.Product_Tag_Mapping",

    c => new

        {

            Pid = c.Int(nullable: false),

            Tid = c.Int(nullable: false),

        })

    .PrimaryKey(t => new { t.Pid, t.Tid })

    .ForeignKey("dbo.Product", t => t.Pid, cascadeDelete: true)

    .ForeignKey("dbo.Tag", t => t.Tid, cascadeDelete: true)

    .Index(t => t.Pid)

    .Index(t => t.Tid); 
```


把映射代码中的WithMany参数去掉，就是一种单向* - *的映射效果。如我们需要通过Product找到所有Tag，但不需要通过Tag找到有这个标签的Product。有点类似与单向1 - *。

但这里不管WithMany是否有参数，生成的迁移代码都是一样的。

我们也写点数据进去，测试下：

```cs
var product = new Product()

{

    Name = "投影仪",

    Description = "高分辨率",

    Tags = new List<Tag>

    {

        new Tag(){Text = "性价比高"}

    }

     

};

context.Set<Product>().Add(product);

context.SaveChanges(); 
```

使用预加载(Include(p=>p.Tags))时的SQL：

```sql

SELECT

    [Project1].[Id] AS [Id], 

    [Project1].[Name] AS [Name], 

    [Project1].[Description] AS [Description], 

    [Project1].[C1] AS [C1], 

    [Project1].[Id1] AS [Id1], 

    [Project1].[Text] AS [Text]

    FROM ( SELECT

        [Limit1].[Id] AS [Id], 

        [Limit1].[Name] AS [Name], 

        [Limit1].[Description] AS [Description], 

        [Join1].[Id] AS [Id1], 

        [Join1].[Text] AS [Text], 

        CASE WHEN ([Join1].[Product_Id] IS NULL) THEN CAST(NULL AS int) ELSE 1 END AS [C1]

        FROM   (SELECT TOP (1) [c].[Id] AS [Id], [c].[Name] AS [Name], [c].[Description] AS [Description]

            FROM [dbo].[Product] AS [c] ) AS [Limit1]

        LEFT OUTER JOIN  (SELECT [Extent2].[Product_Id] AS [Product_Id], [Extent3].[Id] AS [Id], [Extent3].[Text] AS [Text]

            FROM  [dbo].[Product_Tag_Mapping] AS [Extent2]

            INNER JOIN [dbo].[Tag] AS [Extent3] ON [Extent3].[Id] = [Extent2].[Tag_Id] ) AS [Join1] ON [Limit1].[Id] = [Join1].[Product_Id]

    )  AS [Project1]

    ORDER BY [Project1].[Id] ASC, [Project1].[C1] ASC 
```

如你所料，因为现在存在3个表，所以使用了2次JOIN。

 

### 一点补充

之前的示例中用到多次HasForeignKey()方法来指定外键，如果实体类中不存在表示外键的属性，我们可以用下面的方式指定外键列
，这样这个外键列只存在于数据库，不存在于实体中：

```cs
HasOptional(p => p.Invoice).WithMany().Map(m => m.MapKey("DbOnlyInvoiceId")); 

```
 

对于关联的映射EF提供了很多方法，可谓让人眼花缭乱，上面只写了我了解的一部分，如有没有覆盖到的场景，欢迎大家在评论中讨论。

dudu老大也曾写了很多关于EF映射的文章，这应该是EF中最令人迷惑的一点，不知道未来某个版本能否简化一下呢？
