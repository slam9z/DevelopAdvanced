[Null Object pattern](https://en.wikipedia.org/wiki/Null_Object_pattern)
 	
Avoid null references by providing a default object.


In object-oriented computer programming, a Null Object is an object with no referenced value or with defined neutral ("null") behavior. The Null Object design pattern describes the uses of such objects and their behavior (or lack thereof). It was first published in the Pattern Languages of Program Design book series.[1]


## Alternatives

From C# 6.0 it is possible to use the "?." operator (Safe navigation (aka Elvis) operator) as shown below:

```cs
// compile as Console Application, requires C# 6.0 or higher
using System;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "test"; 
            Console.WriteLine(str?.Length);
            Console.ReadKey();
        }
    }
}
// The output will be:
// 4
```

### Extension methods and Null coalescing

In some Microsoft .NET languages, Extension methods can be used to perform what is called 'null coalescing'. This is because extension methods can be called on null values as if it concerns an 'instance method invocation' while in fact extension methods are static. Extension methods can be made to check for null values, thereby freeing code that uses them from ever having to do so. Note that the example below uses the C# Null coalescing operator to guarantee error free invocation, where it could also have used a more mundane if...then...else.

```cs
// compile as Console Application, requires C# 3.0 or higher
using System;
using System.Linq;
namespace MyExtensionWithExample {
    public static class StringExtensions { 
        public static int SafeGetLength(this string valueOrNull) { 
            return (valueOrNull ?? string.Empty).Length; 
        }
    }
    public static class Program {
        // define some strings
        static readonly string[] strings = new [] { "Mr X.", "Katrien Duck", null, "Q" };
        // write the total length of all the strings in the array
        public static void Main(string[] args) {
            var query = from text in strings select text.SafeGetLength(); // no need to do any checks here
            Console.WriteLine(query.Sum());
        }
    }
}
// The output will be:
// 18
```