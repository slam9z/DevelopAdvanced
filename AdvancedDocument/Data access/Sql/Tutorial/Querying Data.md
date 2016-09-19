[Querying Data](http://www.sqltutorial.org/)


##SQL SELECT

To query data in SQL, you use the SELECT statement. The SELECT statement contains the syntax for selecting columns, 
selecting rows, grouping data, joining tables, and performing simple calculations.


##SQL Alias

* Column alias

    When we design the tables, we often keep the column names short e.g., so_no for sales order number, 
    inv_no for invoice number, etc.


*Table alias

    We often assign a table a different name temporarily in a SELECT statement. 
    We call the new name of the table is the table alias. A table alias is also known as a *correlation name*.


##SQL DISTINCT

The primary key ensures that the table has no duplicate rows. 
However, when you use the SELECT statement to query a portion of the columns in a table, you may get duplicate rows.


###SQL DISTINCT and NULL values

NULL is special is SQL. It means that information is missing. NULL is not even equal to NULL.

The point is that if you have two or more NULL values in a column, does the RDBMS consider NULL values as the distinct values?

The answer is NO because the DISTINCT operator considers all NULL values are the same value.

As the result, the DISTINCT operator keeps only one NULL value and eliminates other NULL 
values when you apply the DISTINCT operator.



##SQL LIMIT

To retrieve a portion of rows returned by a query, you use the  LIMIT  and  OFFSET  clause. 
The following illustrates the syntax of the SQL LIMIT clause.

Not all RDBMSs support the SQL LIMIT syntax, therefore, 
the SQL LIMIT is available in some database systems only such as MySQL, PostgreSQL, SQLite, Sybase SQL Anywhere, HSQLDB, etc.