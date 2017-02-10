[How to add a comment to an existing table column in SQL Server](http://stackoverflow.com/questions/9018518/how-to-add-a-comment-to-an-existing-table-column-in-sql-server)

Another way to do it programatically

```sql
EXEC sp_updateextendedproperty 
@name = N'MS_Description', @value = 'Your description',
@level0type = N'Schema', @level0name = dbo, 
@level1type = N'Table',  @level1name = Your Table Name, 
@level2type = N'Column', @level2name = Yuur Column Name;
```