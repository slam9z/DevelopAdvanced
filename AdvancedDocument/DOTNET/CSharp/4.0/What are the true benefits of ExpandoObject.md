[What are the true benefits of ExpandoObject?](http://stackoverflow.com/questions/1653046/what-are-the-true-benefits-of-expandoobject)

##Question

The ExpandoObject class being added to .NET 4 allows you to arbitrarily set properties onto an object at runtime.
Are there any advantages to this over using a Dictionary<string,object>, or really even a Hashtable? As far as I can tell, 
this is nothing but a hash table that you can access with slightly more succinct syntax.

For example, why is this:

```cs
dynamic obj = new ExpandoObject();
obj.MyInt = 3;
obj.MyString = "Foo";
Console.WriteLine(obj.MyString);
```
Really better, or substantially different, than:

```cs
var obj = new Dictionary<string, object>();
obj["MyInt"] = 3;
obj["MyString"] = "Foo";
Console.WriteLine(obj["MyString"]);
```


What real advantages are gained by using ExpandoObject instead of just using an arbitrary dictionary type, other
 than not being obvious that you're using a type that's going to be determined at runtime.

##Answer

Since I wrote the MSDN article you are referring to, I guess I have to answer this one.

First, I anticipated this question and that's why I wrote a blog post that shows a more or less real use case for ExpandoObject:
 Dynamic in C# 4.0: Introducing the ExpandoObject. 

Shortly, ExpandoObject can help you create complex hierarchical objects. For example, imagine that you have a dictionary 
within a dictionary:

```cs
Dictionary<String, object> dict = new Dictionary<string, object>();
Dictionary<String, object> address = new Dictionary<string,object>();
dict["Address"] = address;
address["State"] = "WA";
Console.WriteLine(((Dictionary<string,object>)dict["Address"])["State"]);
```

The deeper is the hierarchy, the uglier is the code. With ExpandoObject it stays elegant and readable.

```cs
dynamic expando = new ExpandoObject();
expando.Address = new ExpandoObject();
expando.Address.State = "WA";
Console.WriteLine(expando.Address.State);
```

Second, as it was already pointed out, ExpandoObject implements INotifyPropertyChanged interface which gives you more
 control over properties than a dictionary.

Finally, you can add events to ExpandoObject like here:

```cs
class Program
{

   static void Main(string[] args)
   {
       dynamic d = new ExpandoObject();

       // Initialize the event to null (meaning no handlers)
       d.MyEvent = null;

       // Add some handlers
       d.MyEvent += new EventHandler(OnMyEvent);
       d.MyEvent += new EventHandler(OnMyEvent2);

       // Fire the event
       EventHandler e = d.MyEvent;

       if (e != null)
       {
           e(d, new EventArgs());
       }

       // We could also fire it with...
       //      d.MyEvent(d, new EventArgs());

       // ...if we knew for sure that the event is non-null.
   }

   static void OnMyEvent(object sender, EventArgs e)
   {
       Console.WriteLine("OnMyEvent fired by: {0}", sender);
   }

   static void OnMyEvent2(object sender, EventArgs e)
   {
       Console.WriteLine("OnMyEvent2 fired by: {0}", sender);
   }
}
```