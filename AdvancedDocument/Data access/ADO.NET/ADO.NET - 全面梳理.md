[ADO.NET - 全面梳理 ](http://www.cnblogs.com/yangcaogui/archive/2012/06/09/2537086.html)


##1.简单的介绍下ADO.NET

 　　了解System.Data命名空间下我们常用的一些类：

```
 1 ①System.Data  → DataTable，DataSet，DataRow，DataColumn，DataRelation，Constraint，DataColumnMapping，DataTableMapping
 2 ②System.Data.Coummon     → 各种数据访问类的基类和接口
 3 ③System.Data.SqlClient   → 对Sql Server进行操作的数据访问类
 4   主要有：   a) SqlConnection            → 数据库连接器
 5             b) SqlCommand               → 数据库命名对象
 6             c) SqlCommandBuilder        → 生存SQL命令
 7             d) SqlDataReader            → 数据读取器
 8             e) SqlDataAdapter           → 数据适配器，填充DataSet
 9             f) SqlParameter             → 为存储过程定义参数
10             g) SqlTransaction           → 数据库事物

```

##2.SqlConnection(连接对象)


##3.SqlCommand(命令对象)


##4.SqlParameter(Sql参数)

用参数来避免sql注入。


##5.SqlDataReader(数据流读取器)

```
using (SqlConnection conn = new SqlConnection(""))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "";
                using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        //开始读取数据了，接下来你想怎么样就怎么样了
                        string str = dr.GetSqlString(0).ToString();
                    }
                }
            }
```


##6.SqlTransaction(事务)

```sql
BEGIN TRANSACTION

ROLLBACK

COMMIT


BEGIN TRANSACTION

    --你需要执行的更新，删除，插入的语句
    
IF(@@ERROR > 0) //这是系统变量，存储你在执行更新，删除，插入操作时发生错误的记录编号
    ROLLBACK
ELSE
    COMMIT
```