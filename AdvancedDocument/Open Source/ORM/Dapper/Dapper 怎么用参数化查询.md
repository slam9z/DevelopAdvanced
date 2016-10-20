[Dapper 怎么用参数化查询](https://q.cnblogs.com/q/46382/)

##Qustion

Dapper 怎么用参数化查询？
比如：conn.Query<Keyword>("select * from keywords where word=?word", new {  }).Count() != 0;
我要怎么给？word 赋值啊

##Answer

```cs
conn.Query<Keyword>("select * from keywords where word=?word", new { word = "xxx" }).Count() != 0;

//不同的数据库使用不同的符号。比如
 
conn.Query<Keyword>("select * from keywords where word = @word", new { word = "xxx" }).Count() != 0;
```

>还不是很习惯dapper的查询方式，一般使用动态类型或者使用匿名对象