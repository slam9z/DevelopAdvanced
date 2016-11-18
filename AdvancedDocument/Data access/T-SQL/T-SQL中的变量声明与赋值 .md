[T-SQL中的变量声明与赋值 ](http://blog.csdn.net/chennengtao/article/details/8547336)

> 这个基本没写过有点陌生，写sql脚本的时候，条件使用变量有很多好处。
> * 修改值只需要修改变量的赋值
> * sql转到程序里面不要改


## T-SQL变量声明

在T-SQL中使用DECLARE语句声明变量以及游标。语法格式如下：

```
DECLARE @<variable name> <variable type> [= <value>][,@<variable name> <variable type> [= <value>]]  
```

变量名必须以 at 符 (@) 开头，必须符合标识符规则。变量不能是 text、ntext 或image 数据类型。可以一次声明一个变量，也可以一次声明几个变量，如果不初始化变量，则变量被置为NULL，直到显式的将其设置为其他的值。
 
## 设置变量的值

在T-SQL中使用SELECT或者SET语句设置变量的值，他们的作用是相同的，但是依然有区别：
SELECT语句可以一次设置多个变量，而SET语句只能一次声明一个变量，例：

```sql
DECLARE @i INT,@j INT  
SET @i = 0,@j = 0  
```

代码将不能通过，显示如下错误消息：

```
服务器: 消息 170，级别 15，状态 1，行 2  
第 2 行: ',' 附近有语法错误。  
```

将SET替换为SELECT语句则正常通过。如下：

```sql
DECLARE @i INT,@j INT  
SELECT @i = 0,@j = 0  
```

命令已成功完成。  
SELECT语句允许源数据来自SELECT查询中的列，例：

```sql
DECLARE @i INT  
SELECT @i = COUNT(*) FROM [sysobjects]  
```

代码正常通过，但是使用SET语句需要做出相应调整：

```sql
DECLARE @i INT  
SET @i = (SELECT COUNT(*) FROM [sysobjects])  
PRINT @i  
```

语法不及使用SELECT易读，特别是有多个列需要赋值给变量的时候，使用SELECT语句的情况下不能为列指定别名，使用SET语句时可以指定别名。

* 使用SET语句可以为游标变量赋值或者定义游标，而SELECT语句不可以。

为游标变量赋值：

```sql
--定义游标  
DECLARE db_cursor CURSOR FOR SELECT * FROM [sysobjects]  
--声明游标变量  
DECLARE @my_variable CURSOR   
--为游标变量赋值，如果游标变量先前引用了一个不同的游标，则删除先前的引用。  
SET @my_variable = db_cursor    
--释放游标  
DEALLOCATE db_cursor   
```

定义游标：

```sql
--声明游标变量  
DECLARE @db_cursorvar CURSOR  
--普通变量  
DECLARE @object_name nvarchar(80)  
  
--设置游标  
SET @db_cursorvar = CURSOR FOR  
SELECT [name]  
FROM [sysobjects]  
  
--打开游标并循环读取游标  
OPEN @db_cursorvar  
  
FETCH NEXT FROM @db_cursorvar  
INTO @object_name  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    PRINT @object_name  
  
    FETCH NEXT FROM @db_cursorvar  
    INTO @object_name  
END  
  
--关闭并释放游标  
CLOSE @db_cursorvar  
DEALLOCATE @db_cursorvar  

```

需要注意的是SET语句时ANSI/ISO标准的一部分，而SELECT语句，我就不得而知，如果你知道，也欢迎告诉我。