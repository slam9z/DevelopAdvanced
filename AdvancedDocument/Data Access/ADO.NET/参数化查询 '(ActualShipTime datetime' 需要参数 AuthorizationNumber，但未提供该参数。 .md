[参数化查询 '(@ActualShipTime datetime' 需要参数 @AuthorizationNumber，但未提供该参数。 ](http://www.cnblogs.com/cxd4321/archive/2012/08/09/2629716.html)


在平时的C#项目开发中，当调用某个存储过程或函数的时候，我们可能经常会遇到这样的问题，
“过程或函数XXX需要XXX参数,但未提供该参数”,  这到底是怎么回事呢？是什么问题引起的？
出现这个错误一般会由以下几种情况引起：

1. 程序中传入参数与已定义的存储过程或函数的参数个数或名称不符；
2. 没有对传入的数据作空值的处理，如下，我们需要对可以为空的值作这样的处理，一旦其为空，就设置为DBNull.value.

```cs
foreach(SqlParameter p in parms)
{
  if(p.value == null)
  {
  p.value=DBNull.value;
  }
}
```
3. 传入了参数，却没有真正通过sqlCommand去操作。

所以当遇到这样的问题，首先需要做的就是检查一下写的代码是否属于这几种情况，如果是就做相应处理，
或者考虑定义的变量是否被初始化