## 创建索引


## 映射包含继承关系的实体类

对于包含继承关系的实体类，在使用EF CodeFirst映射时可以采用TPH、TPT和TPC三种方式完成：

TPH：这是EF CodeFirst采用的默认方式，继承关系中的所有实体会被映射到同一张表。

TPT：所有类型映射到不同的表中，子类型所映射到的表只包含不存在于基类中的属性。子类映射的表的主键同时作为关联基类表的外键。

TPC：每个子类映射到不同的表，表中同时包含基类的属性。这种情况下查询非常复杂，真的完全不知道其存在的意义。后文也就不详细介绍了。


先介绍一下几演示所用的实体类，我们的产品类依然存在，这次多了几个孩子。


```C#

public class Product

{

    public int Id{ get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

}

 

public class PaperProduct:Product

{

    public int PageNum { get; set; }

}

 

public class ElectronicProduct : Product

{

    public double LifeTime { get; set; }

}

 

public class CD : ElectronicProduct

{

    public float Capacity { get; set; }

} 
```

 

它们的关系如图所示：

![](http://images0.cnblogs.com/blog2015/42044/201508/070851414247742.png)

### TPH(Table-Per-Hierarchy)

由于所有继承层次的类在一个表中，使用一个列区分这些类就是这种方式最重要的一点。默认情况下，
EF CodeFirst使用一个名为Discriminator的列并以类型名字符串作为值来区分不同的类。

我们可以使用如下配置来修改这个默认设置，另外由于TPH是EF CodeFirst的默认选择，无需附加其他配置。


```C#

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        Map<Product>(p => { p.Requires("ProductType").HasValue(0); }).ToTable("Product");

        HasKey(p => p.Id);

         

        Map<PaperProduct>(pp => { pp.Requires("ProductType").HasValue(1); });

        Map<ElectronicProduct>(ep => { ep.Requires("ProductType").HasValue(2); });

        Map<CD>(cd => { cd.Requires("ProductType").HasValue(3); });

    }

} 
```

Requires方法指定区分实体的列的名称，HasValue指定区分值。

特别注意，如果想要自定义表名的话，ToTable要和Map<Product>()在一行中调用，且ToTable()在后。。

添加点数据做测试：

```C#

var product = new Product() { Name = "投影仪", Description = "高分辨率" };

var paperproduct = new PaperProduct() { Name = "《天书》", PageNum = 5 };

var cd = new CD() { Name = "蓝光大碟", LifeTime = 50, Capacity = 50 };

 

context.Set<Product>().Add(product);

context.Set<Product>().Add(paperproduct);

context.Set<Product>().Add(cd);

context.SaveChanges(); 
```

看一下数据库中表结构和数据：


图2. TPH下的数据表

![](http://images0.cnblogs.com/blog2015/42044/201508/071959548152253.png)

EF按我们的配置添加了名为ProductType的列。当然我们也看到有很多为NULL的列。对于数据的查询不存在JOIN，就不再展示了。


### TPT(Table-Per-Type)

这种方式下，所有存在于基类的属性被存储于一张表，每个子类存储到一张表，表中只存子类独有的属性。子类表的主键作为基类表的主键的外键实现关联。
直接上配置代码：

```C#


public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        ToTable("Product");

        HasKey(p => p.Id);

 

        Map<PaperProduct>(pp => { pp.ToTable("PaperProduct"); });

        Map<ElectronicProduct>(ep => { ep.ToTable("ElectronicProduct"); });

        Map<CD>(cd => { cd.ToTable("CD"); });

    }

} 
```

如下是迁移代码，按我们所想针对基类和子类都生成了表：

```C#

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

    "dbo.PaperProduct",

    c => new

        {

            Id = c.Int(nullable: false),

            PageNum = c.Int(nullable: false),

        })

    .PrimaryKey(t => t.Id)

    .ForeignKey("dbo.Product", t => t.Id)

    .Index(t => t.Id);

 

CreateTable(

    "dbo.ElectronicProduct",

    c => new

        {

            Id = c.Int(nullable: false),

            LifeTime = c.Double(nullable: false),

        })

    .PrimaryKey(t => t.Id)

    .ForeignKey("dbo.Product", t => t.Id)

    .Index(t => t.Id);

 

CreateTable(

    "dbo.CD",

    c => new

        {

            Id = c.Int(nullable: false),

            Capacity = c.Single(nullable: false),

        })

    .PrimaryKey(t => t.Id)

    .ForeignKey("dbo.ElectronicProduct", t => t.Id)

    .Index(t => t.Id); 
```

我们使用TPH部分那段代码来插入测试数据，然后看一下查询生成的SQL。

先来查一下子类对象试试：

```C#

var productGet = context.Set<PaperProduct>().Where(r=>r.Id == 2).ToList(); 
```

生成的SQL看起来不错，就是一个INNER JOIN：

```C#

SELECT

    '0X0X' AS [C1], 

    [Extent1].[Id] AS [Id], 

    [Extent2].[Name] AS [Name], 

    [Extent2].[Description] AS [Description], 

    [Extent1].[PageNum] AS [PageNum]

    FROM  [dbo].[PaperProduct] AS [Extent1]

    INNER JOIN [dbo].[Product] AS [Extent2] ON [Extent1].[Id] = [Extent2].[Id]

    WHERE 2 = [Extent1].[Id] 
```

再来一个基类对象试试：


```C#

var productGet = context.Set<Product>().Where(r=>r.Id == 1).ToList(); 
```

这次悲剧了：


```C#

SELECT

    CASE WHEN (( NOT (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL))) AND ( NOT (([Project1].[C1] = 1) AND ([Project1].[C1] IS NOT NULL)))) THEN '0X' WHEN (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL) AND ( NOT (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)))) THEN '0X0X' WHEN (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)) THEN '0X0X0X' ELSE '0X1X' END AS [C1], 

    [Extent1].[Id] AS [Id], 

    [Extent1].[Name] AS [Name], 

    [Extent1].[Description] AS [Description], 

    CASE WHEN (( NOT (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL))) AND ( NOT (([Project1].[C1] = 1) AND ([Project1].[C1] IS NOT NULL)))) THEN CAST(NULL AS float) WHEN (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL) AND ( NOT (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)))) THEN [Project3].[LifeTime] WHEN (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)) THEN [Project3].[LifeTime] END AS [C2], 

    CASE WHEN (( NOT (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL))) AND ( NOT (([Project1].[C1] = 1) AND ([Project1].[C1] IS NOT NULL)))) THEN CAST(NULL AS real) WHEN (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL) AND ( NOT (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)))) THEN CAST(NULL AS real) WHEN (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)) THEN [Project3].[Capacity] END AS [C3], 

    CASE WHEN (( NOT (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL))) AND ( NOT (([Project1].[C1] = 1) AND ([Project1].[C1] IS NOT NULL)))) THEN CAST(NULL AS int) WHEN (([Project3].[C1] = 1) AND ([Project3].[C1] IS NOT NULL) AND ( NOT (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)))) THEN CAST(NULL AS int) WHEN (([Project3].[C2] = 1) AND ([Project3].[C2] IS NOT NULL)) THEN CAST(NULL AS int) ELSE [Project1].[PageNum] END AS [C4]

    FROM   [dbo].[Product] AS [Extent1]

    LEFT OUTER JOIN  (SELECT

        [Extent2].[Id] AS [Id], 

        [Extent2].[PageNum] AS [PageNum], 

        cast(1 as bit) AS [C1]

        FROM [dbo].[PaperProduct] AS [Extent2] ) AS [Project1] ON [Extent1].[Id] = [Project1].[Id]

    LEFT OUTER JOIN  (SELECT

        [Extent3].[Id] AS [Id], 

        [Extent3].[LifeTime] AS [LifeTime], 

        cast(1 as bit) AS [C1], 

        [Project2].[Capacity] AS [Capacity], 

        CASE WHEN (([Project2].[C1] = 1) AND ([Project2].[C1] IS NOT NULL)) THEN cast(1 as bit) WHEN ( NOT (([Project2].[C1] = 1) AND ([Project2].[C1] IS NOT NULL))) THEN cast(0 as bit) END AS [C2]

        FROM  [dbo].[ElectronicProduct] AS [Extent3]

        LEFT OUTER JOIN  (SELECT

            [Extent4].[Id] AS [Id], 

            [Extent4].[Capacity] AS [Capacity], 

            cast(1 as bit) AS [C1]

            FROM [dbo].[CD] AS [Extent4] ) AS [Project2] ON [Extent3].[Id] = [Project2].[Id] ) AS [Project3] ON [Extent1].[Id] = [Project3].[Id]

    WHERE 1 = [Extent1].[Id] 
```

试了几种写法，都不能改变把所有表都JOIN一遍的结果。看来是EF的问题。其实想想也对，Product类作为基类可以去引用子类的对象，
生成这样的SQL使我们有机会把得到Product对象转换成子类对象。但我认为应该提供一种方法明确只获取基类对象（不用做任何JOIN）。
是我不知道呢？还是EF就是没提供这样的方法呢？

对于TPH和TPT两种方式，前者会浪费一些存储空间，后者因为查询时JOIN损耗一些时间。个人认为对于子类和父类差别不太大的情况，可以选用TPH
，这样不会浪费太多空间同时也能有很好的查询速度。而对于子类和父类差别较大的情况，TPT就是一个更好的选择。


## 将一个实体映射到多个表


在数据库设计中这常被称作垂直分割。还是通过例子来看具体实现。我们给产品类增加2个新属性：


```C#

public class Product

{

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    //new property

    public float Price { get; set; }

    public float Weight { get; set; }

} 
```

我们希望将新属性存储在另一张数据表中，可以按如下方式配置：

```C#

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        Map(m =>

        {

            m.Properties(t => new { t.Id, t.Name, t.Description });

            m.ToTable("Product");

        })

        .Map(m =>

        {

            m.Properties(t => new { t.Id, t.Price, t.Weight });

            m.ToTable("ProductDetail");

        });

        HasKey(p => p.Id);

    }

} 
```

代码一目了然，分开指定属性和相应的表即可。生成的迁移代码如下：

```C#

CreateTable(

    "sample.Product",

    c => new

        {

            Id = c.Int(nullable: false, identity: true),

            Name = c.String(),

            Description = c.String(maxLength: 200),

        })

    .PrimaryKey(t => t.Id);

 

CreateTable(

    "sample.ProductDetail",

    c => new

        {

            Id = c.Int(nullable: false),

            Price = c.Single(nullable: false),

            Weight = c.Single(nullable: false),

        })

    .PrimaryKey(t => t.Id)

    .ForeignKey("sample.Product", t => t.Id)

    .Index(t => t.Id); 
```

是不是很眼熟，对！和之前配置1 - 1映射生成的迁移代码一模一样。当然生成的查询语句也是一样的。

## 将两个实体映射到一张表

我们把上一个例子中给Product增加的属性独立出来：
 
```C#

public class Product

{

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ProductDetail ProductDetail { get; set; }

}

 

public class ProductDetail

{

    public int Id { get; set; }

    public float Price { get; set; }

    public float Weight { get; set; }

    public virtual Product Product { get; set; }

} 
```

现在我们有2个实体类，接下来的配置将把它们映射到一张表：


```C#
public class ProductDetailMap : EntityTypeConfiguration<ProductDetail>

{

    public ProductDetailMap()

    {

        HasKey(pd=>pd.Id).HasRequired(pd => pd.Product).WithRequiredPrincipal(p=>p.ProductDetail);

        ToTable("Product");

    }

}

 

public class ProductMap : EntityTypeConfiguration<Product>

{

    public ProductMap()

    {

        HasKey(p => p.Id);

        ToTable("Product");        

    }

} 
```

生成的迁移代码可以看出，两个实体将被保存到一张表：

```C#
CreateTable(

    "dbo.Product",

    c => new

        {

            Id = c.Int(nullable: false, identity: true),

            Name = c.String(),

            Description = c.String(maxLength: 200),

            Price = c.Single(nullable: false),

            Weight = c.Single(nullable: false),

        })

    .PrimaryKey(t => t.Id); 
```

映射部分就到这里了。休息下吧。 
