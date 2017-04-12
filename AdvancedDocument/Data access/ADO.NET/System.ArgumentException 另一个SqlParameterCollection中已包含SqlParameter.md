[System.ArgumentException: 另一个SqlParameterCollection中已包含SqlParameter](http://www.cnblogs.com/OldYongs/archive/2011/03/12/1982021.html)

一般情况下，我们定义的一个SqlParameter参数数组，如：

```cs
SqlParameter[] parms = 
{
    new SqlParameter("@DateTime1", dtBegin),
    new SqlParameter("@DateTime2", dtEnd)
};
```
如果只给一个SqlCommand使用，这种情况的参数使用，不会出现异常，但如果该参数数组同时给两个Sqlcommand使用，就会出现如下异常：

　　System.ArgumentException: 另一个SqlParameterCollection中已包含SqlParameter。

       
## 原因如下：

声明的SqlParameter数组，而在循环的内部，每一次执行ExecuteNonQuery(或者其它命令方法)都由该方法内部的IDbCommand.Parameters.Add(IDbDataParameter)
将SqlParameter数组添加到IDbCommand的IDataParameterCollection中。而framework机制限制两个IDataParameterCollection指向同一个对象。
虽然ExecuteNonQuery方法内部声明了一个IDbCommand的临时对象，理论上讲，这个包含了IDataParameterCollection的IDbCommand对象会在
ExecuteNonQuery方法结束时从内存中释放。但是实际上可能是由于垃圾回收机制并没有将IDbCommand临时对象即时的回收，而且改对象绑定的Parameter集合也存在，
就像一个DropDownList添加Item一样。这样在下一个循环执行的时候，会导致两个IDataParameterCollection指向同一个对象，此时出现问题。

## 解决方案

### 解决方案一：

在每一次循环时，重新生成对象，但这样会产生大量的垃圾变量，不可取。

### 解决方案二：

将使用完之后的Command命令的Parameters集合清空。推荐使用，类似代码如下：

```C#
/// <summary>
/// 获取一个DataTable
/// </summary>
public static DataTable GetDataTable(
    string connDBStr, string sql, params SqlParameter[] cmdParms)
{
    SqlCommand cmd = new SqlCommand();
    using (SqlConnection conn = new SqlConnection(connDBStr))
    {
        PrepareSqlCommand(cmd, conn, null, sql, cmdParms);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable　　　　(SetSqlAsDataTableName(sql));
        da.Fill(dt);
        cmd.Parameters.Clear();//多了这一句，就解决了问题
        return dt;
    }
}
```

另外，如果不是数组，只是一个SqlParameter变量，如：

```cs
SqlParameter parm = 
    new SqlParameter("@Cust_Id", CustId.Trim());;
```

则多次被SqlCommand使用，不会出现问题，我已经做了试验！
参考自：http://www.cnblogs.com/yank/archive/2008/04/01/1132825.html 谢谢！


## 我的解决方法

是别人封装的我怎么办呢？基础库也不好添加。只能每次都创建了！
