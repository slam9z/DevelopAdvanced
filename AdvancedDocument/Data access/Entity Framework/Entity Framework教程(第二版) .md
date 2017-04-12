[Entity Framework教程(第二版) ](http://www.cnblogs.com/lsxqw2004/p/4701979.html)

## EF的发展历程


## 核心

随着Code First一起出现的DbContext和DbSet类绝对可以称得上EF的功能核心，其取代了之前的ObjectContext和ObjectSet类，
提供了与数据库通信，管理内存中实体的重要功能。

### DbContext类

主要是负责与数据库进行通信，管理实体到数据库的映射模型，跟踪实体的更改
（正如这个类名字Context所示，其维护了一个EF内存中容器，保存所有被加载的实体并跟踪其状态）。
关于模型映射和更改跟踪下面都有专门的小节来讨论。Dbcontext中最常用的几个方法如：

* SaveChanges(和6.0开始增加的异步方法SaveChangesAsync)：用于将实体的修改保存到数据库。


* Set<T>：获取实体相应的DbSet对象，我们对实体的增删改查操作都是通过这个对象来进行的。


    还有几个次常用但很重要的属性方法：

* Database属性：一个数据库对象的表示，通过其SqlQuery、ExecuteSqlCommand等方法可以直接执行一些Sql语句或SqlCommand；
   EF6起可以通过Database对象控制事务。


* Entry：获取EF Context中的实体的状态，在更改跟踪一节会讨论其作用。


* ChangeTracker：返回一个DbChangeTracker对象，通过这个对象的Entries属性，我们可以查询EF Context中所有缓存的实体的状态。


### DbSet类

这个类的对象正是通过刚刚提到的Set<T>方法获取的对象。其中的方法都与操作实体有关，如：

* Find/FindAsync：按主键获取一个实体，首先在EF Context中查找是否有被缓存过的实体，如果查找不到再去数据库查找，
如果数据库中存在则缓存到EF Context并返回，否则返回null。

* Attach：将一个已存在于数据库中的对象添加到EF Context中，实体状态被标记为Unchanged。
对于已有相同key的对象存在于EF Context的情况，如果这个已存在对象状态为Unchanged则不进行任何操作，否则将其状态更改为Unchanged。


* Add：将一个已存在于数据库中的对象添加到EF Context中，实体状态被标记为Added。
    对于已有相同key的对象存在于EF Context且状态为Added则不进行任何操作。


* Remove：将一个已存在于EF Context中的对象标记为Deleted，当SaveChanges时，这个对象对应的数据库条目被删除。
    注意，调用此方法需要对象已经存在于EF Context。

* Include：详见下面预加载一节。
 

* AsNoTracking：相见变更跟踪一节。 


* Local属性：用来跟踪所有EF Context中状态为Added，Modified、Unchanged的实体。作用好像不是太大。没怎么用过。 

* Create：这个方法至今好像没有用到过，不知道干啥的。有了解的评论中给解释下吧。 


## 映射

## 变更追踪

## 数据加载 

## 并发

## 异步

C#5.0开始增加了async和await关键字，配合.NET Framework 4.5大大简化了异步方法的实现和调用。EF也顺应趋势在6.0起开始支持异步操作。

EF中异步操作分为2部分异步获取数据及异步提交数据。

异步提交数据只有一种途径，就是DbContext中的SaveChangesAsync方法。关于异步方法怎么调用本文不细说了，那是另一个大主题。园子也有很多相关文章。


关于异步推荐一本书《C#并发编程经典实例》。这本书还没有翻译版的时候我就找英文电子版读过一遍，受益匪浅。

关于异步获取数据根据场景不同有很多种选择，列举几个方法在下面：

```C#
* FindAsync

* LoadAsync

* FirstAsync

* FirstOrDefaultAsync

* ToListAsync
```

可能还有其他不一一列举了。

一般现在项目都使用各种结构大致类似的IRepository/ConcreteRepository接口/类包装EF，
我们只需要根据同步方法添加异步方法并调用上面这些EF中提供的异步方法，就可以很轻松的让我们存储层支持异步。


>异步方法一个很大的特点就是传播性，基本上我们存储层的代码改成异步，上面所有调用代码也都要以异步实现。
>所以让项目支持异步还是一个需要从开始就规划的工作，后期改的话成本有点高。


## 迁移    


## 未涉及

本文未涉及的内容包括

* 映射存储过程/表值函数


* 执行原生SQL查询


* 枚举/空间数据类型


* 自定义实体验证


* CodeFirst映射约定/配置映射约定


* EF6版本DbContext.Database中的事务支持


* EF6的日志支持


* EF6基于代码的配置


不涉及这些的原因是是博主我几乎没用过写出来也误导人。
