[like参数化查询](http://www.cnblogs.com/lzrabbit/archive/2012/04/22/2465313.html#like)


like查询根据个人习惯将通配符写到参数值中或在SQL拼接都可，两种方法执行效果一样，在此不在详述


```cs
using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    SqlCommand comm = new SqlCommand();
    comm.Connection = conn;
    //将 % 写到参数值中
    comm.CommandText = "select * from Users(nolock) where UserName like @UserName";
    comm.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 200) { Value = "rabbit%" });
    comm.ExecuteNonQuery();
}

using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    SqlCommand comm = new SqlCommand();
    comm.Connection = conn;
    //SQL中拼接 %
    comm.CommandText = "select * from Users(nolock) where UserName like @UserName+'%'";
    comm.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 200) { Value = "rabbit%" });
    comm.ExecuteNonQuery();
}
```


> 如果在客户端输入一个%传入会导致被当成一个通配符

[SQL 'LIKE' query using '%' where the search criteria contains '%'](http://stackoverflow.com/questions/10803489/sql-like-query-using-where-the-search-criteria-contains)

MS-SQL; If you want a % symbol in search_criteria in to be treated as a literal character rather than as a wildcard, escape it to [%];

```sql
.... where name like '%' + replace(search_criteria, '%', '[%]') + '%'

```