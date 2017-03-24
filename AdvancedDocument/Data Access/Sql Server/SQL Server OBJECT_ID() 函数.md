[SQL Server OBJECT_ID() 函数](http://www.cnblogs.com/AngelLee2009/p/3342730.html)

返回架构范围内对象的数据库对象标识号。

重要提示

　　使用 OBJECT_ID 不能查询非架构范围内的对象（如 DDL 触发器）。对于在 sys.objects 目录视图中找不到的对象，需要通过查询适当的目录视图来获取该对象的标识号。例如，若要返回 DDL 触发器的对象标识号，请使用 SELECT OBJECT_ID FROM sys.triggers WHERE name = 'DatabaseTriggerLog'语法：

```
OBJECT_ID ( '[ database_name . [ schema_name ] . | schema_name . ] 
    object_name' [ ,'object_type' ] )
```

参数：

　　 ' object_name '要使用的对象。object_name 的数据类型为 varchar 或 nvarchar。如果 object_name 的数据类型为 varchar，则它将隐式转换为 nvarchar。可以选择是否指定数据库和架构名称。
　　' object_type '架构范围的对象类型。object_type 的数据类型为 varchar 或 nvarchar。如果 object_type 的数据类型为 varchar，则它将隐式转换为 nvarchar。有关对象类型的列表，请参阅 sys.objects (Transact-SQL) 中的 type 列。
返回类型：int

示例：

A. 返回指定对象的对象 ID

```sql
USE master;
GO
SELECT OBJECT_ID(N'AdventureWorks.Production.WorkOrder') AS 'Object ID';
GO
```

B. 验证对象是否存在

```sql
USE AdventureWorks;
GO
IF OBJECT_ID (N'dbo.AWBuildVersion', N'U') IS NOT NULL
DROP TABLE dbo.AWBuildVersion;
GO
```
