[How do I replace the *first instance* of a string in .NET?](http://stackoverflow.com/questions/141045/how-do-i-replace-the-first-instance-of-a-string-in-net)

## answer1

```cs
string ReplaceFirst(string text, string search, string replace)
{
  int pos = text.IndexOf(search);
  if (pos < 0)
  {
    return text;
  }
  return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
}

```


## answer2



As itsmatt said Regex.Replace is a good choice for this however to make his answer more complete I will fill it in with a code sample:

```cs
using System.Text.RegularExpressions;
...
Regex regex = new Regex("foo");
string result = regex.Replace("foo1 foo2 foo3 foo4", "bar", 1);             
// result = "bar1 foo2 foo3 foo4"
```

The third parameter, set to 1 in this case, is the number of occurrences of the regex pattern that you want to replace in the input string from the beginning of the string.

I was hoping this could be done with a static Regex.Replace overload but unfortunately it appears you need a Regex instance to accomplish it.
