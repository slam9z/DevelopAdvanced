[Check if Key Exists in NameValueCollection](Check if Key Exists in NameValueCollection)


## answer1

From MSDN:

>   This property returns null in the following cases:
>
>    1) if the specified key is not found;

So you can just:

```cs
NameValueCollection collection = ...
string value = collection[key];
if (value == null) // key doesn't exist
```
>    2) if the specified key is found and its associated value is null.


## answer2

Use this method:

```cs
private static bool ContainsKey(NameValueCollection collection, string key)
{
    if (collection.Get(key) == null)
    {
        return collection.AllKeys.Contains(key);
    }

    return true;
}
```