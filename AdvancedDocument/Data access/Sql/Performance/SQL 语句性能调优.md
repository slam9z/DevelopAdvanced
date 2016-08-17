[SQL 语句性能调优](http://www.ibm.com/developerworks/cn/data/library/techarticles/dm-1002limh/index.html)

##检查执行计划



##根据权值来优化查询条件

1. Sargability

理想的 SQL 表达式应该采用下面这种通用的格式：

```sql
 <column> <comparison operator> <literal>
```

早些时候，IBM 研究人员将这种查询条件语名命名为”sargable predicates”，因为 SARG 是 Search ARGument 的组合。


##针对专门操作符的调优

前面，讲的是关于查询条件的一般规则，在这一节中，将讨论如何使用专门的操作符来改进 SQL 代码的性能。

###与 (AND) 

数据库系统按着从左到右的顺序来解析一个系列由 AND 连接的表达式，但是 Oracle 却是个例外，它是从右向左地解析表达式。
可以利用数据库系统的这一特性，来将概率小的表达示放在前面，或者是如果两个表达式可能性相同，那么可将相对不复杂的表达式放在前面。
这样做的话，如果第一个表达式为假的话，那么数据库系统就不必再费力去解析第二个表达式了。

###或 (OR)

和与 (AND) 操作符相反，在用或 (OR) 操作符写 SQL 语句时，就应该将概率大的表达示放在左面，因为如果第一个表达示为假的话，
OR 操作符意味着需要进行下一个表达示的解析。

###与 + 或

按照集合的展开法则，


###非 (NOT)

让非 (NOT) 表达示转换成更易读的形式。简单的条件能通过将比较操作符进行反转来达到转换的目的，


###IN

很多人认为如下的两个查询条件没有什么差别，因为它们返回的结果集是相同的：

条件 1：

```
 ... WHERE column1 = 5 

 OR column1 = 6
```

条件 2：

```
 ... WHERE column1 IN (5, 6)
```

这样的想法并不完全正确，对于大多数的数据库操作系统来说，IN 要比 OR 执行的快


###UNION

在 SQL 中，两个表的 UNION 就是两个表中不重复的值的集合，即 UNION 操作符返返回的两个或多个查询结果中不重复行的集合。
这是一个很好的合并数据的方法，但是这并不是最好的方法。


查询 1：

```sql
 SELECT * FROM Table1 

 WHERE column1 = 5 

 UNION 

 SELECT * FROM Table1 

 WHERE column2 = 5
```

查询 2：

```sql
 SELECT DISTINCT * FROM Table1 

 WHERE column1 = 5 

 OR column2 = 5
```


