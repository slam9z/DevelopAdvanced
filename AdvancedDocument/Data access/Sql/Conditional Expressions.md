[Conditional Expressions]()


##SQL CASE

Summary: in this tutorial, you will learn how to use the SQL CASE expression to add the logic to the SQL statements.


The SQL CASE expression allows you to evaluate a list of conditions and returns one of the possible results. 
The CASE expression has two formats: simple CASE and searched CASE.


###Simple CASE expression

The following illustrates the simple CASE expression:

```sql
CASE expression
WHEN when_expression_1 THEN
	result_1
WHEN when_expression_2 THEN
	result_2
WHEN when_expression_3 THEN
	result_3
...
ELSE
	else_result
END
```


###Searched CASE expression

The following shows the searched CASE expression.

```sql
CASE
WHEN boolean_expression_1 THEN
	result_1
WHEN boolean_expression_2 THEN
	result_2
WHEN boolean_expression_3 THEN
	result_3
ELSE
	else_result
END;
```




 




