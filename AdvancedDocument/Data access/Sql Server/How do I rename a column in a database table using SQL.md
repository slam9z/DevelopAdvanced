[How do I rename a column in a database table using SQL?](http://stackoverflow.com/questions/174582/how-do-i-rename-a-column-in-a-database-table-using-sql)


In sql server you can use

```sql
exec sp_rename '<TableName.OldColumnName>','<NewColumnName>','COLUMN'
```
or

```sql
sp_rename '<TableName.OldColumnName>','<NewColumnName>','COLUMN'
```
