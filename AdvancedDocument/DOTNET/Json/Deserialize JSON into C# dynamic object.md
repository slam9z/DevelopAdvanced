[Deserialize JSON into C# dynamic object?](http://stackoverflow.com/questions/3142495/deserialize-json-into-c-sharp-dynamic-object)

##Question

Is there a way to deserialize JSON content into a C# 4 dynamic type? It would be nice to skip
 creating a bunch of classes in order to use the DataContractJsonSerializer. 

##Answer  Json.Decode

If you are happy to have a dependency upon the System.Web.Helpers assembly, then you can use the Json class:
dynamic data = Json.Decode(json);
It is included with the MVC framework as an additional download to the .NET 4 framework. Be sure to
give Vlad an upvote if that's helpful! However if you cannot assume the client environment includes this 
DLL, then read on.

##Answer Newtonsoft.Json

```cs
dynamic stuff = JObject.Parse("{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }");

string name = stuff.Name;
string address = stuff.Address.City;
```

##MyAnswer

Json.Decode方法找不到，好像需要MVC framework 。
JObject并没有生成动态对象。

```
string name = stuff.Name.Value;
string address = stuff.Address.City.Value;
```

才是真正的值，直接转换成string会带{}。


