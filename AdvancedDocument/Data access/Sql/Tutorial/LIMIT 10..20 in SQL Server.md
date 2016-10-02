[LIMIT 10..20 in SQL Server](http://stackoverflow.com/questions/971964/limit-10-20-in-sql-server)


I'm trying to do something like :

```sql
SELECT * FROM table LIMIT 10,20
```

or

```sql
SELECT * FROM table LIMIT 10 OFFSET 10
```

but using SQL Server

The only solution I found looks like overkill:

```sql
SELECT * FROM ( 
  SELECT *, ROW_NUMBER() OVER (ORDER BY name) as row FROM sys.databases 
 ) a WHERE row > 5 and row <= 10
```

I also found:

```sql
SELECT TOP 10 * FROM stuff; 
```

... but it's not what I want to do since I can't specify the starting limit.


##bester

The LIMIT clause is not part of standard SQL. It's supported as a vendor extension to SQL by MySQL, PostgreSQL, and SQLite. 

Other brands of database may have similar features (e.g. TOP in Microsoft SQL Server), but these don't always work identically.

It's hard to use TOP in Microsoft SQL Server to mimic the LIMIT clause. There are cases where it just doesn't work.

The solution you showed, using ROW_NUMBER() is available in Microsoft SQL Server 2005 and later. 
This is the best solution (for now) that works solely as part of the query.

Another solution is to use TOP to fetch the first count + offset rows, and then use the API to seek past the first offset rows.


See also:

* "Emulate MySQL LIMIT clause in Microsoft SQL Server 2000"
* "Paging of Large Resultsets in ASP.NET"
