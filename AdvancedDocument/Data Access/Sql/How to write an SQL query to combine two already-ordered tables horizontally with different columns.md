[How to write an SQL query to combine two already-ordered tables horizontally with different columns?](http://stackoverflow.com/questions/7983875/how-to-write-an-sql-query-to-combine-two-already-ordered-tables-horizontally-wit)


## question

I have two tables which I would like to place side-by-side exactly as they are. For example,

```
tableOne                              tableTwo
columnOne | columnTwo | columnThree   columnI | columnII | columnIII
```

The data in the two tables do not need to be related whatsoever -- the tables have the same row count -- and 

the data is already sorted in the two tables. Basically, I would like to do a full outer join on the two tables without an on operator.

How can I do this in a SQL query?


## answer

Well, you do want an ON operator - you just seem to want it to work automatically, which won't happen.

If you're saying Row 1 of tableOne maps to Row 1 of tableTwo, then you need to add a row column to each table 

and then join on it.

If you don't specify a join condition, you'll do a `cross join` that joins every row from tableOne to every row in tableTwo, which obviously isn't what you're looking for.
So do something like this:

```sql
select * from 
  (select *, row_number() over (order by 1) as RN from tableOne) a
  inner join (select *, row_number() over (order by 1) as RN from tableTwo) b
     on a.RN = b.RN
```     