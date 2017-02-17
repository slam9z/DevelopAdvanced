[c# 扩展方法奇思妙用基础篇九：Expression 扩展](http://www.cnblogs.com/ldp615/archive/2011/09/15/expression-extension-methods.html)

```cs
Expression<Func<Person, bool>> exp =
    p => p.Name.Contains("ldp") && p.Birthday.Value.Year > 1990
```

但有些时候，要动态创建 Expression Trees，我们要用到 System.Linq.Expressions 命名空间中的 Expression 类。

使用 Expression 类 中的静态方法，前面的 Expression Trees 可如下创建：

```cs
var parameter = Expression.Parameter(typeof(Person), "p");
var left = Expression.Call(
    Expression.Property(parameter, "Name"),
    typeof(string).GetMethod("Contains"),
    Expression.Constant("ldp"));
var right = Expression.GreaterThan(
    Expression.Property(Expression.Property(Expression.Property(parameter, "Birthday"), "Value"), "Year"),
    Expression.Constant(1990));
var body = Expression.AndAlso(left, right);
var lambda = Expression.Lambda<Func<Person, bool>>(body, parameter);
```