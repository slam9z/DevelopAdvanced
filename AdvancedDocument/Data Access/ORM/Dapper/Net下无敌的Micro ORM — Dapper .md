[Net下无敌的Micro ORM — Dapper ](http://mp.weixin.qq.com/s?__biz=MzAwNTMxMzg1MA==&mid=204750104&idx=1&sn=bb22bbea03513a61db7f6cf462b174c7&scene=2&srcid=0905n7Qf4wxKqqXqpPBHzey0#rd)


假如你喜欢原生的Sql语句，又喜欢ORM的简单，那你一定会喜欢上Dapper这款ORM.https://github.com/StackExchange/dapper-dot-net
 
Dapper的优势：

1. Dapper是一个轻型的ORM类。代码就一个SqlMapper.cs文件，编译后就40K的一个很小的Dll.
1. Dapper很快。Dapper的速度接近与IDataReader，取列表的数据超过了DataTable。
1. Dapper支持什么数据库。Dapper支持Mysql,SqlLite,Mssql2000,Mssql2005,Oracle等一系列的数据库，当然如果你知道原理也可以让它支持Mongo db
1. Dapper的r支持多表并联的对象。支持一对多 多对多的关系。并且没侵入性，想用就用，不想用就不用。无XML无属性。代码以前怎么写现在还怎么写。
1. Dapper原理通过Emit反射IDataReader的序列队列，来快速的得到和产生对象。性能实在高高高。
1. Dapper支持net2.0,3.0,3.5,4.0,4.5。【如果想在Net2.0下使用，可以去网上找一下Net2.0下如何配置运行Net3.5即可。】
1. Dapper语法十分简单。并且无须迁就数据库的设计。


##Query()方法：

Query()是IDbConnection扩展方法并且重载了,从数据库里提取信息，并用来填充我们的业务对象模型。


##Execute方法：
 
正如Query方法是检索数据的，Execute方法不会检索数据，它与Query方法非常相似，但它总返回总数（受影响的行数）,而不是一个对象集合【如：insert update和delete】.