[只是想简单说下表达式树 - Expression Trees](http://www.cnblogs.com/liqingwen/p/5868688.html)

##目录

* 简介
* Lambda 表达式创建表达式树
* API 创建表达式树
* 解析表达式树
* 表达式树的永久性
* 编译表达式树
* 执行表达式树
* 修改表达式树
* 调试
 
##简介
　　
表达式树以树形数据结构表示代码，其中每一个节点都是一种表达式，比如方法调用和 x < y 这样的二元运算等。
你可以对表达式树中的代码进行编辑和运算。这样能够动态修改可执行代码、在不同数据库中执行 LINQ 查询以及创建动态查询。 
达式树还能用于动态语言运行时 (DLR) 以提供动态语言和 .NET Framework 之间的互操作性。 
 
##一、Lambda 表达式创建表达式树


若 lambda 表达式被分配给 Expression<TDelegate> 类型的变量，则编译器可以发射代码以创建表示该 lambda 表达式的表达式树。  
C# 编译器只能从表达式 lambda （或单行 lambda）生成表达式树。 
下列代码示例使用关键字 Expression创建表示 lambda 表达式：

```c#
Expression<Action<int>> actionExpression = n => Console.WriteLine(n);
Expression<Func<int, bool>> funcExpression1 = (n) => n < 0;
Expression<Func<int, int, bool>> funcExpression2 = (n, m) => n - m == 0;
```
 
##二、API 创建表达式树
　　

通过 API 创建表达式树需要使用 Expression 类
下列代码示例展示如何通过 API 创建表示 lambda 表达式：num => num == 0

```c#
//通过 Expression 类创建表达式树
//  lambda：num => num == 0
ParameterExpression pExpression = Expression.Parameter(typeof(int));    //参数：num
ConstantExpression cExpression = Expression.Constant(0);    //常量：0
BinaryExpression bExpression = Expression.MakeBinary(ExpressionType.Equal, pExpression, cExpression);   //表达式：num == 0
Expression<Func<int, bool>> lambda = Expression.Lambda<Func<int, bool>>(bExpression, pExpression);  //lambda 表达式：num => num == 0
```

代码使用 Expression 类的静态方法进行创建。
 

##三、解析表达式树 

```c#
下列代码示例展示如何分解表示 lambda 表达式 num => num == 0 的表达式树。

Expression<Func<int, bool>> funcExpression = num => num == 0;

//开始解析
ParameterExpression pExpression = funcExpression.Parameters[0]; //lambda 表达式参数
BinaryExpression body = (BinaryExpression)funcExpression.Body;  //lambda 表达式主体：num == 0

Console.WriteLine($"解析：{pExpression.Name} => {body.Left} {body.NodeType} {body.Right}");
```
 
 
##四、表达式树永久性

表达式树应具有永久性（类似字符串）。这意味着如果你想修改某个表达式树，则必须复制该表达式树然后替换其中的节点来创建
一个新的表达式树。  你可以使用表达式树访问者遍历现有表达式树。第七节介绍了如何修改表达式树。
 

##五、编译表达式树

Expression<TDelegate> 类型提供了 Compile 方法以将表达式树表示的代码编译成可执行委托。

```c#
//创建表达式树
Expression<Func<string, int>> funcExpression = msg => msg.Length;
//表达式树编译成委托
var lambda = funcExpression.Compile();
//调用委托
Console.WriteLine(lambda("Hello, World!"));

//语法简化
Console.WriteLine(funcExpression.Compile()("Hello, World!"));
```

##六、执行表达式树

执行表达式树可能会返回一个值，也可能仅执行一个操作（例如调用方法）。
只能执行表示 lambda 表达式的表达式树。表示 lambda 表达式的表达式树属于 LambdaExpression 或 Expression<TDelegate> 类型。若要执行这些表达式树，需要调用 Compile 方法来创建一个可执行委托，然后调用该委托。

```C#
const int n = 1;
const int m = 2;

//待执行的表达式树
BinaryExpression bExpression = Expression.Add(Expression.Constant(n), Expression.Constant(m));
//创建 lambda 表达式
Expression<Func<int>> funcExpression = Expression.Lambda<Func<int>>(bExpression);
//编译 lambda 表达式
Func<int> func = funcExpression.Compile();

//执行 lambda 表达式
Console.WriteLine($"{n} + {m} = {func()}");
```


##七、修改表达式树 
 　　该类继承 ExpressionVisitor 类，通过 Visit 方法间接调用 VisitBinary 方法将 != 替换成 ==。
基类方法构造类似于传入的表达式树的节点，但这些节点将其子目录树替换为访问器递归生成的表达式树。  

```c#
internal class Program
{
    private static void Main(string[] args)
    {
        Expression<Func<int, bool>> funcExpression = num => num == 0;
        Console.WriteLine($"Source: {funcExpression}");

        var visitor = new NotEqualExpressionVisitor();
        var expression = visitor.Visit(funcExpression);

        Console.WriteLine($"Modify: {expression}");

        Console.Read();
    }

    /// <summary>
    /// 不等表达式树访问器
    /// </summary>
    public class NotEqualExpressionVisitor : ExpressionVisitor
    {
        public Expression Visit(BinaryExpression node)
        {
            return VisitBinary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            return node.NodeType == ExpressionType.Equal
                ? Expression.MakeBinary(ExpressionType.NotEqual, node.Left, node.Right) //重新弄个表达式：用 != 代替 ==
                : base.VisitBinary(node);
        }
    }
}
```

##八、调试
