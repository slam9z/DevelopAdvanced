[SQL ALTER TABLE 语句](http://www.w3school.com.cn/sql/sql_alter.asp)


SQL ALTER TABLE 语法

如需在表中添加列，请使用下列语法:

> 需要特别注意

```sql
ALTER TABLE table_name
ADD  column_name datatype
```


要删除表中的列，请使用下列语法：

```sql
ALTER TABLE table_name 
DROP COLUMN column_name
```

注释：某些数据库系统不允许这种在数据库表中删除列的方式 (DROP COLUMN column_name)。

要改变表中列的数据类型，请使用下列语法：

```sql
ALTER TABLE table_name
ALTER COLUMN column_name datatype
```