[数据读取器与指定的Patient不兼容。某个类型“id”的成员在同名的数据读取器中没有对应列。](https://q.cnblogs.com/q/31525/)

这个问题太恶心了，Model有的属性，必须Select出来，如果Model更新，Sql必须也要更新。
而且有些列我并不需要。

DbFirst的原因吧。


[Database.SqlQuery](https://msdn.microsoft.com/en-us/library/gg679117(v=vs.113).aspx) 


使用join竟然也获取不到数据，垃圾的一比的方法