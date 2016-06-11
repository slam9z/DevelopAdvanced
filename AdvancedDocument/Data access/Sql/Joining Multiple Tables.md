##SQL INNER JOIN 

introduce you to the join concept and show you how to use the INNER JOIN clause to query data from multiple tables.


Suppose the column name of the A & B tables is n, the following statement illustrates the inner join clause:

```sql
SELECT
  A.n
FROM A
INNER JOIN B ON B.n = A.n;
```

![](http://www.sqltutorial.org/wp-content/uploads/2016/03/SQL-INNER-JOIN.png)

For each row in table A, the inner join clause finds the matching rows in the table B. 
If a row is matched, it is included in the final result set.




##LEFT OUTER JOIN 

provide you with another kind of joins that allows you to combine data from multiple tables.

![](http://www.sqltutorial.org/wp-content/uploads/2016/03/SQL-LEFT-JOIN.png)

When we join table A with table B, all the rows in table A (the left table) are included in the result 
set whether there is a matching row in the table B or not.


##RIGHT OUTER JOIN


##FULL OUTER JOIN


##CROSS JOIN (Cartesian Product)


##SQL SELF JOIN 


how to join a table to itself and when do you use it? welcome to the self-join tutorial.

```sql
SELECT
 column1,
 column2,
 column3,
        ...
FROM
 table1 A
INNER JOIN table1 B ON B.column1 = A.column2;
```
