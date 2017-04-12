[关于SQL语句中SUM函数返回NULL的解决办法](http://www.cnblogs.com/wentiertong/archive/2010/11/30/1892413.html)

SUM 是SQL语句中的标准求和函数，如果没有符合条件的记录，那么SUM函数会返回NULL。
但多数情况下，我们希望如果没有符合条件记录的情况下，我们希望它返回0，而不是NULL，那么我们可以使用例如下面的方法来处理：

```sql
SELECT COALESCE(SUM(name),0) FROM person WHERE id > 0
```
行了，这下就不用费事去处理返回结果是否为NULL的情况了。
COALESCE 函数的意思是返回参数列表中第一个*不*为空的值，该方法允许传入多个参数，该函数也是SQL中的标准函数。
然后查了查关于对于NULL值的判断。地址：http://www.w3schools.com/sql/sql_isnull.asp

## SQL Server / MS Access

```sql
SELECT ProductName,UnitPrice*(UnitsInStock+ISNULL(UnitsOnOrder,0))
 FROM Products
```

## or we can use the COALESCE() function, like this:

```sql
SELECT ProductName,UnitPrice*(UnitsInStock+COALESCE(UnitsOnOrder,0))
 FROM Products
```