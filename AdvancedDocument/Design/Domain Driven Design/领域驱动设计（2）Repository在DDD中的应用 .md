## 概述


## EF与Repository


## Unit Of Work 与 Repository

　　我们EfRepository的实现中，每一次Insert/Update/Delete操作被执行之后，变更就会立即同步到数据库中去。
第一，我们没有为多个操作添加一个事务的能力；第二，这会为我们带来性能上的损失。而Unit Of Work模式正好解决了我们的问题，下面是Martin Fowler 对于该模式的解释：


*“A Unit of Work keep track of everything you do during a business transaction that can affect the database. When you’re done, it figures out everything that need to be done to alter the database as a result of your work.”*

*<Unit of Work负责跟踪所有业务事务过程中数据库的变更。当事务完成之后，它找出需要处理的变更，并更新数据库。*


在EF中，DBContext它本身就已经是一个Unit Of Work的模式

``` C#
namespace RepositoryAndEf.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
```

## 洋葱架构(The Onion Architecture)与IRepository

洋葱架构很早就有，只不过08年的时候[Jeffery](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/)给它取了个名字，让它成为了一个模式。

各层依赖关系很强
![](http://images.cnitblog.com/blog/554526/201410/011354262222248.png)


现在一切都是以BLL为中心，BLL也不需要依懒于任何其它层了，作为独立的一块，我们可以更容易的进行单元测试，重构等。
![](http://images.cnitblog.com/blog/554526/201410/011426200348027.png)

*传统多层架构与现代（洋葱架构）多层架构的区别*
![](http://images.cnitblog.com/blog/554526/201410/011450428003845.png)


## 重新定义IRepository 

现在，我们再回过头去看Repository。它的两大职责：

1. 对领域实体的生命周期进行管理（从数据库重建，以及持久化到数据库）  ——被推迟到了应用层
2. 解除领域层对基础设施的依懒 


## 可有可无的Repository

我们把IRepository移出领域层之后，再加上我们对洋葱架构的理解。我们就可以知道Repository在应用层已经可以被替换成别的东西，IDAL也可以啊:)。
当然有人也许会建议直接拿EF来用多好，其实我不建议这样去做，考虑到以后把EF换掉的可能性。
并且我们加这样一个接口真的不会碍着我们什么事。如果有人觉得在读取数据的时候加一个Repository在中间，少掉了很多EF提供的功能，觉得很不爽，倒是可以试试像我们的IQuery接口一样直接对DbSet来查询。
我们甚至可以学习CQRS架构，将“读”的服务完全分离开，我们就可以单独针对“读”来独立设计。

但是Repository给我们带来的优点，这些优点也是我们不能轻易丢掉它的原因：

1. 提供一个简单的模型，来获取持久对象并管理期生命周期
2. 把应用和领域设计从持久技术、多种数据库策略解耦出来
3. 容易被替换成哑实现（Mock)以便我们在测试中使用



[初探领域驱动设计（2）Repository在DDD中的应用](http://www.cnblogs.com/jesse2013/p/ddd-repository.html)
