[SQL Server 存储过程](http://www.cnblogs.com/hoojo/archive/2011/07/19/2110862.html)

##存储过程的概念 

存储过程Procedure是一组为了完成特定功能的SQL语句集合，经编译后存储在数据库中，用户通过指定存储过程的名称并给出参数来执行。 

存储过程中可以包含逻辑控制语句和数据操纵语句，它可以接受参数、输出参数、返回单个或多个结果集以及返回值。


###存储过程的优点 

A、 存储过程允许标准组件式编程 

存储过程创建后可以在程序中被多次调用执行，而不必重新编写该存储过程的SQL语句。而且数据库专业人员可以随时对存储过程进行修改，但对应用程序源代码却毫无影响，从而极大的提高了程序的可移植性。 

B、 存储过程能够实现较快的执行速度 

如果某一操作包含大量的T-SQL语句代码，分别被多次执行，那么存储过程要比批处理的执行速度快得多。因为存储过程是预编译的，在首次运行一个存储过程时，查询优化器对其进行分析、优化，并给出最终被存在系统表中的存储计划。而批处理的T-SQL语句每次运行都需要预编译和优化，所以速度就要慢一些。 

C、 存储过程减轻网络流量 

对于同一个针对数据库对象的操作，如果这一操作所涉及到的T-SQL语句被组织成一存储过程，那么当在客户机上调用该存储过程时，网络中传递的只是该调用语句，否则将会是多条SQL语句。从而减轻了网络流量，降低了网络负载。 

D、 存储过程可被作为一种安全机制来充分利用 

系统管理员可以对执行的某一个存储过程进行权限限制，从而能够实现对某些数据访问的限制，避免非授权用户对数据的访问，保证数据的安全。 
 

##系统存储过程 

系统存储过程是系统创建的存储过程，目的在于能够方便的从系统表中查询信息或完成与更新数据库表相关的管理任务或其他的系统管理任务。系统存储过程主要存储在master数据库中，以“sp”下划线开头的存储过程。尽管这些系统存储过程在master数据库中，但我们在其他数据库还是可以调用系统存储过程。有一些系统存储过程会在创建新的数据库的时候被自动创建在当前数据库中。 

常用系统存储过程有： 

```sql
exec sp_databases; --查看数据库
exec sp_tables;        --查看表
exec sp_columns student;--查看列
exec sp_helpIndex student;--查看索引
exec sp_helpConstraint student;--约束
exec sp_stored_procedures;
exec sp_helptext 'sp_stored_procedures';--查看存储过程创建、定义语句
exec sp_rename student, stuInfo;--修改表、索引、列的名称
exec sp_renamedb myTempDB, myDB;--更改数据库名称
exec sp_defaultdb 'master', 'myDB';--更改登录名的默认数据库
exec sp_helpdb;--数据库帮助，查询数据库信息
exec sp_helpdb master;
```


##用户自定义存储过程 

###创建语法 

```sql
create proc | procedure pro_name
    [{@参数数据类型} [=默认值] [output],
     {@参数数据类型} [=默认值] [output],
     ....
    ]
as
    SQL_statements
```
  
###创建不带参数存储过程 

```sql
--创建存储过程
if (exists (select * from sys.objects where name = 'proc_get_student'))
    drop proc proc_get_student
go
create proc proc_get_student
as
    select * from student;

--调用、执行存储过程
exec proc_get_student;
```

###修改存储过程 

```sql
--修改存储过程
alter proc proc_get_student
as
select * from student;
```


##Raiserror 

Raiserror返回用户定义的错误信息，可以指定严重级别，设置系统变量记录所发生的错误。 

   语法如下： 

```sql
Raiserror({msg_id | msg_str | @local_variable}
  {, severity, state}
  [,argument[,…n]]
  [with option[,…n]]
)
```