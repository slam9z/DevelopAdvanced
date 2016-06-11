##SQL GROUP BY clause 

group row into groups and apply an aggregate function to each group.

You often use the GROUP BY in conjunction with an aggregate function such as MIN, MAX, AVG, SUM, 
or COUNT to calculate a measure that provides the information for each group.

The following illustrates the syntax of the GROUP BY clause.

```sql
SELECT
	column1,
	column2,
	AGGREGATE_FUNCTION (column3)
FROM
	table1
GROUP BY
	column1,
	column2;
```



###SQL GROUP BY and DISTINCT

If you use the GROUP BY clause without an aggregate function, the GROUP BY clause behaves like the DISTINCT operator.


##SQL HAVING Clause 

filter groups summarized by the GROUP BY clause.

You often use the HAVING clause with the GROUP BY clause and also you can use it in the SELECT statement only. If you use a HAVING clause without a GROUP BY clause, the HAVING clause behaves like the WHERE clause.

The following illustrates the syntax of the HAVING clause:

```sql
SELECT
	column1,
	column2,
	AGGREGATE_FUNCTION (column3)
FROM
	table1
GROUP BY
	column1,
	column2
HAVING
	group_condition;
```


###HAVING vs. WHERE

The WHERE clause applies the condition to individual rows before the rows are summarized 
into groups by the GROUP BY clause. However, the HAVING clause applies the condition to the 
groups after the rows are grouped into groups.





 
