[如何在SQL Server 中添加注释](https://zhidao.baidu.com/question/568441959.html)

首先，要明确一点的是注释存在sysproperties表中而不是跟创建的表捆绑到一起的
具体使如方法如下：


* 使用SQL Server窗口创建表是会有注释窗口；

* 使用SQL语句的comment语句，该语句放置在create table()后面，如：

    ```sql
    comment on table table_name is 'table_mark'
    comment on column table_name."Column" is 'column_mark'
    ```

* 调用系统存储过程sp_addextendedproperty来添加注释，如：

   ```sql
    EXECUTE sp_addextendedproperty N'MS_Description',N'雇员信息',N'user',N'dbo',N'table',N'Employee',NULL,NULL
    EXECUTE sp_addextendedproperty N'MS_Description',N'主键ID,自动增加',N'user',N'dbo',N'table',N'Employee',N'column',N'EmployeeID'
   ```
    或者

   ```sql
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CharData', @level2type=N'COLUMN',@level2name=N'charid'
    GO
    ```