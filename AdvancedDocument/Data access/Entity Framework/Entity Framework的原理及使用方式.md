[Entity Framework的原理及使用方式]()

ADO.NET Entity Framework操作数据库的过程对用户是透明的（当然我们可以通过一些工具或方法了解发送到数据库的SQL语句等）。
我们唯一能做的是操作EDM，EDM会将这个操作请求发往数据库。 

Entity Framework实现了一套类似于ADO.NET2.0中连接类（它们使用方式相同，均基于Provider模式）的被称作EntityClient的类用来操作EDM。
ADO.NET2.0的连接类是向数据库发送SQL命令操作表或视图，而EntityClient是向EDM发送EntitySQL操作Entity。
EntityClient在EntityFramework中的作用是相当重要的，所有发往EDM的操作都是经过EntityClient，包括使用LINQ to Entity进行的操作。 

![](http://images.cnblogs.com/cnblogs_com/lsxqw2004/060309_0406_EntityFram9.png)


这几种访问方式使用介绍如下：

1. EntityClient+EntitySQL 

    示例代码： 

    ```C#
    string city = "London";
    using (EntityConnection cn = new EntityConnection("Name=Entities"))
    {
      cn.Open();
      EntityCommand cmd = cn.CreateCommand();
      cmd.CommandText = @"SELECT VALUE c FROM Entities.Customers AS c WHERE 
                         c.Address.City = @city";
      cmd.Parameters.AddWithValue("city", city);
      DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
      while (rdr.Read())
        Console.WriteLine(rdr["CompanyName"].ToString());
      rdr.Close();
    }
    ```

2. ObjectService+EntitySQL 


    在有EntityClient+EntitySQL这种使用方式下，使用ObjectService+EntitySQL的方式是多此一举，不会得到任何编辑时或运行时的好处。
    在ObjectContext下使用EntitySQL的真正作用是将其与LINQ to Entity结合使用。具体可见下文所示。 

    示例代码： 

    ```C#
    string city = "London";
    using (Entities entities = new Entities()) 
    {
      ObjectQuery<Customers> query = entities.CreateQuery<Customers>(
        "SELECT VALUE c FROM Customers AS c WHERE c.Address.City = @city",
        new ObjectParameter("city", city)
      );

      foreach (Customers c in query)
        Console.WriteLine(c.CompanyName);
    }
    ```

 

3. ObjectContext+LINQ( to Entity) 

    ```C#
     string city = "London";
     using (Entities entities = new Entities()) 
     {
       var query = from c in entities.Customers
                   where c.Address.City == city
                   select c;
     
       foreach (Customers c in query)
         Console.WriteLine(c.CompanyName);
     }
    ```



    这两段示例代码中的entities.Customer的写法隐式调用了2中示例的ObjectQuery<Customers>来进行查询
    （关于此可以参见EDM的设计器文件-xxx.designer.cs）。在方式二中的Where方法传入的是一个Lambda表达式，
    你也可以传入一条EntitySQL语句做参数来将LINQ与EntitySQL结合使用。如下代码演示其使用： 

    ```C#
     string city = "London";
     using (Entities entities = new Entities()) 
     {
       var query = entities.Customers.Where("r.Address.City = '"+city+"'");
     
       foreach (Customers c in query)
         Console.WriteLine(c.CompanyName);
     }
    ```

 


## 使用技巧及需要注意的问题

这也是上文提到的在ObjectContext下使用EntitySQL的一个主要作用，上面的例子比较简单可能看不到这样使用的优势，
但是如下两种情况下使用EntitySQL可能是最好的选择。 

1. 动态构建查询条件
    当查询条件的个数固定时，我们也可以采用罗列多个Where扩展方法的形式，如下：  ObjectQuery.Where(LambdaExpression1).Where(LambdaExpression2)… 
    但是当这个条件的存在与否需要在运行时判断时，我们只能通过组合字符串来得到这个条件，我们可以将条件组合为EntitySQL并传递给Where()方法。 


2.数据库模糊查询 

    下面代码演示使用EntitySQL的like完成模糊查询：

    context.Customer.Where("it.CustomerID LIKE @CustomerID", new System.Data.Objects.ObjectParameter("CustomerID","%V%"));

    这个并不是只能使用EntitySQL来实现，LINQ to Entity也可以很容易完成。如下代码：  context.Customer.Where(r => r.CustomerID.Contains("V")); 
    同理，"V%"、"%V"可以分别使用StartsWith()与EndsWith()函数实现。 


    使用LINQ to Entity需要注意的一个方面是，在完成查询得到需要的结果后使用ToList或ToArray方法将结果转变为内存中的对象，
    然后使用LINQ to Objects来处理，否则处在Entity Framework的联机模式下对性能有很大的影响。 

 

## 几种方法的性能分析及使用选择 

首先用下图来说明一个执行过程。 

![](http://images.cnblogs.com/cnblogs_com/lsxqw2004/060309_0406_EntityFram11.png)

图中所示表达的意思已经非常清楚，稍加解释的是，无论是通过EntityClient直接提供给Entity Client Data Provider的
Entity SQL还是通过ObjectService传递的Entity SQL（或是LINQ to Entity），都在Entity Client Data Provider中被解释为相应的Command Tree，
并进一步解释为对应数据库的SQL。这样来看使用LINQ to Entity与Entity SQL的效率应该差不多，但是还有一个问题，
那就是EntitySQL所转换的最终SQL可能要比LINQ to Entity生成的SQL效率高，这在一定程度上使两者效率差增大，
但是LINQ to Entity有其它技术无法比拟的好处，那就是它的强类型特性，编辑时智能感知提醒，编译时发现错误，
这都是在一个大型项目中所需要的。虽然现在也有了调试EntitySQL的工具，但其与强类型的LINQ to Entity还是有很大差距。 


 另外在ObjectService与直接使用EntityClient问题的选择上。如果你想更灵活的控制查询过程，或者进行临时查询建议选择EntityCLient，
如果是操作数据那只能采用ObjectService。 

 

上文总结了各种操作EDM的方式，下面引用MSDN的一个对这几种技术进行比较的表格： 


| |EntityClient 和实体 SQL|对象服务和实体 SQL|对象服务和 LINQ|
|---|------|--------------------------------|--------------|

|定向到 EntityClient 提供程序|是|否|否|
 

|适合临时查询|是|是|否|
 

|可直接发出 DML|否|否|否|
 

|强类型化|否|否|是|
 

|可将实体作为结果返回|否|是|是|
 

通过这个表可以很好对某一场合下应该选择的技术进行判断。EntityClient 和实体 SQL可以进行最大的控制，
而使用LINQ to Entity可以获得最佳的编辑时支持。 

 

