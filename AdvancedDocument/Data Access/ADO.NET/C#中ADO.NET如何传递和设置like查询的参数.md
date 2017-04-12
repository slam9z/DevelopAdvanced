[C#中ADO.NET如何传递和设置like查询的参数](http://shiyousan.com/post/635722142352339811)

## myanswer

```cs
string strSqlCommandText = "SELECT Title FROM Article WHERE Title LIKE '%'+@Title+'%'";
```

## article

在C#中如果通过ADO.NET进行SQL查询，一般会使用参数化查询，毕竟参数化查询可以防御SQL注入攻击。这里有个特殊的情况，就是LIKE操作符。因为LIKE操作符的语法是需要使用通配符进行匹配，所以如果参数是包含在通配符中，就会导致传參无效。
假设当前有张Article文章表，有个Title标题列，我们使用LIKE操作符 模糊查询有关ASP.NET MVC的标题，如果使用下面的SQL语句将无法查询出数据：

```cs
string strSqlCommandText = "SELECT Title FROM Article WHERE Title LIKE '%@Title%'";
SqlParameter parameter = new SqlParameter() { ParameterName = "@Title", Value = "ASP.NET MVC", SqlDbType = SqlDbType.NVarChar, Size = 50 };
```

造成此原因是由于在当前SQL语句中，我们将参数标识@Title包含在单引号中，这样就导致ADO.NET无法正确识别参数标识，所以上面的语句实际查询的内容是有包含"@Title"这个字符串的标题，而不是匹配"ASP.NET MVC"，相当于传参无效，我们声明的@Title参数等于没用到。

这里一定要注意，ADO.NET进行参数化时会自动将参数值包含在单引号中，除了特殊需求，最好不要自己手动添加单引号。ADO.NET中识别参数标识是使用符号@，如果在SQL语句中将参数标识放在单引号中，单引号中的参数标识只会被当成字符串！

所以要对LIKE语句进行参数化查询时，就要先对参数值进行格式化，在传参之前就设置好通配符，具体实现代码如下：

```cs
string strSqlCommandText = "SELECT Title FROM Article WHERE Title LIKE @Title";
SqlParameter parameter = new SqlParameter() { ParameterName = "@Title", Value = "%ASP.NET MVC%", SqlDbType = SqlDbType.NVarChar, Size = 50 };
```

从上面的代码中我们可以看到具体的变换有两点，第一点是SQL语句中取消了通配符%并且参数标识没有被单引号包含其中，第二点则是通配符直接放到了参数值中，这样ADO.NET在进行参数化后所生成的SQL就完全没问题了，最终也能正确的查询出结果。