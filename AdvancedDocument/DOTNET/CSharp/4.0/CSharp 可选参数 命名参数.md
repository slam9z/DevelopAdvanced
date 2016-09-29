[C# 可选参数 命名参数 ](http://www.cnblogs.com/weiming/archive/2011/12/28/2304937.html)

##1.可选参数

可选参数是.NET4中新添加的功能，应用可选参数的方法在被调用的时可以选择性的添加需要的参数，而不需要的参数由参数默认值取代。


##2.命名参数

命名参数是把参数附上参数名称，这样在调用方法的时候不必按照原来的参数顺序填写参数，只需要对应好参数的名称也能完成方法。

```
class Program
    {
        /// <summary>
        /// 可选参数  命名参数
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine(ShowComputer("i3 370M","2G","320G"));
            Console.WriteLine(ShowComputer(disk: "320G", cpu: "i3 370M", ram: "2G"));
            Console.Read();
        }
 
        private static string ShowComputer(string cpu, string ram, string disk)
        {
            return "My computer ... \nCpu:" + cpu + "\nRam:" + ram + "\nDisk:" + disk + "\n";
        }
    }
```


*命名参数如果只是改变参数的顺序，这样的意义并不大，我们没有必要为了改变顺序而去用命名参数，他与可选参数结合才能显示出他真正的意义。*

```cs
class Program
    {
        /// <summary>
        /// 可选参数  命名参数
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine(ShowComputer(ram: "3G"));
            Console.Read();
        }
 
        private static string ShowComputer(string cpu = "i3 370M", string ram = "2G", string disk = "320G")
        {
            return "My computer ... \nCpu:" + cpu + "\nRam:" + ram + "\nDisk:" + disk + "\n";
        }
    }
```

程序只赋值了第二个参数ram，其他参数均为默认值，运行结果大家应该都知道了。这样命名参数和可选参数都发挥了他们独特的作用。