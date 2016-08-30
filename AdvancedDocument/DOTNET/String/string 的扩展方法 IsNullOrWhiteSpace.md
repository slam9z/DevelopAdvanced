[IsNullOrWhiteSpace extension method in .Net 4?](http://stackoverflow.com/questions/15928612/isnullorwhitespace-extension-method-in-net-4)


This is not an extension method but a static method of the String class.
Look here.
So you need to write:

```cs
string s = "123";
if(String.IsNullOrWhiteSpace(s))
{
}
```

You can always write you own extension:

```cs
public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string s)
    {
       return String.IsNullOrWhiteSpace(s);
    }
}

string s = "123";
if(s.IsNullOrWhiteSpace())
{
}
```