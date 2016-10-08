[IDictionary<string, object> to ExpandoObject extension method](http://theburningmonk.com/2011/05/idictionarystring-object-to-expandoobject-extension-method/)

As you know, the ExpandoObject class implements the IDictionary<string, object> interface, so if you have an ExpandoObject
 you can easily cast it to an IDictionary<string, object> but there’s no built-in way to easily do the reverse.

Luckily, I came across a very useful extension method today which converts an IDictionary<string, object> into an ExpandoObject, 
which you can then use dynamically in your code, sweet! :-)

With some small modifications, here’s the code I ended up with, with some comments thrown in for good measures:

```cs
/// <summary>

/// Extension method that turns a dictionary of string and object to an ExpandoObject

/// </summary>

public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)

{

    var expando = new ExpandoObject();

    var expandoDic = (IDictionary<string, object>)expando;



    // go through the items in the dictionary and copy over the key value pairs)

    foreach (var kvp in dictionary)

    {

        // if the value can also be turned into an ExpandoObject, then do it!

        if (kvp.Value is IDictionary<string, object>)

        {

            var expandoValue = ((IDictionary<string, object>)kvp.Value).ToExpando();

            expandoDic.Add(kvp.Key, expandoValue);

        }

        else if (kvp.Value is ICollection)

        {

            // iterate through the collection and convert any strin-object dictionaries

            // along the way into expando objects

            var itemList = new List<object>();

            foreach (var item in (ICollection)kvp.Value)

            {

                if (item is IDictionary<string, object>)

                {

                    var expandoItem = ((IDictionary<string, object>) item).ToExpando();

                    itemList.Add(expandoItem);

                }

                else

                {

                    itemList.Add(item);

                }

            }



            expandoDic.Add(kvp.Key, itemList);

        }

        else

        {

            expandoDic.Add(kvp);

        }

    }



    return expando;

}
```

Enjoy!