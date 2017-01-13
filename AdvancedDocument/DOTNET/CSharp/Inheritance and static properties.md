[Inheritance and static properties](http://stackoverflow.com/questions/3776305/inheritance-and-static-properties)


## question

I don't understand the following phenomenon, could someone explain me please what I got wrong?

```cs
public class BaseClass
{
    public BaseClass()
    {
        BaseClass.Instance = this;
    }

    public static BaseClass Instance
    {
        get;
        private set;
    }
}

public class SubClassA : BaseClass
{
    public SubClassA() 
        : base()
    { }
}

public class SubClassB : BaseClass
{
    public SubClassB()
        : base()
    { }
}

class Program
{
    static void Main(string[] args)
    {
        SubClassA a = new SubClassA();
        SubClassB b = new SubClassB();

        Console.WriteLine(SubClassA.Instance.GetType());
        Console.WriteLine(SubClassB.Instance.GetType());

        Console.Read();
    }
}
```


As I understood, the compiler should generate a new Type through inheritance, that SubClassA and SubClassB are really own types with own static variables. But it seems that the static part of the class is not inherited but referenced - what do i get wrong?
c# .net oop inheritance

## answer
	

Inheritance in .NET works only on instance base. Static methods are defined on the type level not on the instance level. That is why overriding doesn't work with static methods/properties/events...

Static methods are only held once in memory. There is no virtual table etc. that is created for them.

If you invoke an instance method in .NET, you always give it the current instance. This is hidden by the .NET runtime, but it happens. Each instance method has as first argument a pointer (reference) to the object that the method is run on. This doesn't happen with static methods (as they are defined on type level). How should the compiler decide to select the method to invoke?
