[MySQL中的各种JOIN(CROSS JOIN, INNER JOIN, LEFT [OUTER] ](http://blog.csdn.net/zhuxinhua/article/details/5815910)

MySQL中的各种JOIN 

1. 笛卡尔积(交叉连接) 
在MySQL中可以为CROSS JOIN或者省略CROSS即JOIN，或者使用’,’ 
如 

```sql
SELECT * FROM table1 CROSS JOIN table2 
SELECT * FROM table1 JOIN table2 
SELECT * FROM table1,table2 
```

由于其返回的结果为被连接的两个数据表的乘积，因此当有WHERE, ON或USING条件的时候一般不建议使用，因为当数据表项目太多的时候，会非常慢。 
一般使用LEFT [OUTER] JOIN或者RIGHT [OUTER] JOIN 

2. 内连接INNER JOIN 
在MySQL中把INNER JOIN叫做等值连接，即需要指定等值连接条件 
在MySQL中CROSS和INNER JOIN被划分在一起，不明白。 
参看MySQL帮助手册 
http://dev.mysql.com/doc/refman/5.0/en/join.html 

```sql
join_table: 
    table_reference [INNER | CROSS] JOIN table_factor [join_condition] 
```

3. MySQL中的外连接，分为左外连接和右连接， 
即除了返回符合连接条件的结果之外，还要返回左表(左连接)或者右表(右连接)中不符合连接条件的结果，相对应的使用NULL对应。 

a. LEFT [OUTER] JOIN 

```sql
SELECT column_name FROM table1 LEFT [OUTER] JOIN table2 ON table1.column=table2.column 
```

除了返回符合连接条件的结果之外，还需要显示左表中不符合连接条件的数据列，相对应使用NULL对应 

b. RIGHT [OUTER] JOIN 

```sql
SELECT column_name FROM table1 RIGHT [OUTER] JOIN table2 ON table1.column=table2.column 
```

RIGHT与LEFT JOIN相似不同的仅仅是除了显示符合连接条件的结果之外，还需要显示右表中不符合连接条件的数据列，相应使用NULL对应 

——————————————– 
添加显示条件WHERE, ON, USING 
1. WHERE子句 
2. ON 
3. USING子句，如果连接的两个表连接条件的两个列具有相同的名字的话可以使用USING 
例如 
SELECT <column_name> FROM <table1> LEFT JOIN <table2> USING (<column_name>) 

连接多余两个表的情况 
举例： 

```
mysql> SELECT  artists.Artist, cds.title, genres.genre 
    -> FROM cds 
    -> LEFT JOIN genres 
    -> ON cds.genreID = genres.genreID 
    -> LEFT JOIN artists 
    -> ON cds.artistID = artists.artistID; 
或者 
mysql> SELECT artists.Artist, cds.title, genres.genre 
    -> FROM cds 
    -> LEFT JOIN genres 
    -> ON cds.genreID = genres.genreID 
    -> LEFT JOIN artists 
    -> ON cds.artistID = artists.artistID 
    -> WHERE (genres.genre = ‘Pop’); 
——————————————– 
```

另外需要注意的地方 

在MySQL中涉及到多表查询的时候，需要根据查询的情况，想好使用哪种连接方式效率更高。 
1. 交叉连接(笛卡尔积)或者内连接 
[INNER | CROSS] JOIN 
2. 左外连接LEFT [OUTER] JOIN或者右外连接RIGHT [OUTER] JOIN 

注意指定连接条件WHERE, ON，USING. 