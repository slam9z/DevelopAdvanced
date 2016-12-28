[How can I find the method that called the current method?](http://stackoverflow.com/questions/171970/how-can-i-find-the-method-that-called-the-current-method)


##　answer

A quick recap of the 2 approaches with speed comparison being the important part.

http://geekswithblogs.net/BlackRabbitCoder/archive/2013/07/25/c.net-little-wonders-getting-caller-information.aspx

Determining the caller at compile-time

```cs
static void Log(object message, 
[CallerMemberName] string memberName = "",
[CallerFilePath] string fileName = "",
[CallerLineNumber] int lineNumber = 0)
{
    // we'll just use a simple Console write for now    
    Console.WriteLine("{0}({1}):{2} - {3}", fileName, lineNumber, memberName, message);
}
```

Determining the caller using the stack

```css
static void Log(object message)
{
    // frame 1, true for source info
    StackFrame frame = new StackFrame(1, true);
    var method = frame.GetMethod();
    var fileName = frame.GetFileName();
    var lineNumber = frame.GetFileLineNumber();

    // we'll just use a simple Console write for now    
    Console.WriteLine("{0}({1}):{2} - {3}", fileName, lineNumber, method.Name, message);
}
```


Comparison of the 2 approaches
Time for 1,000,000 iterations with Attributes: 196 ms
Time for 1,000,000 iterations with StackTrace: 5096 ms

 