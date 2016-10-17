[Creating JSON on the fly with JObject](http://stackoverflow.com/questions/18246716/creating-json-on-the-fly-with-jobject)

##Question

For some of my unit tests I want the ability to build up particular JSON values (record albums in this case) that
 can be used as input for the system under test.

I have the following code:

```cs
var jsonObject = new JObject();
jsonObject.Add("Date", DateTime.Now);
jsonObject.Add("Album", "Me Against The World");
jsonObject.Add("Year", 1995);
jsonObject.Add("Artist", "2Pac");
```
This works fine, but I have never really like the "magic string" syntax and would prefer something closer to the 
expando-property syntax in JavaScript like this:

```cs
jsonObject.Date = DateTime.Now;
jsonObject.Album = "Me Against The World";
jsonObject.Year = 1995;
jsonObject.Artist = "2Pac";
```

##Ansewer

Well, how about:

```c#
dynamic jsonObject = new JObject();
jsonObject.Date = DateTime.Now;
jsonObject.Album = "Me Against the world";
jsonObject.Year = 1995;
jsonObject.Artist = "2Pac";
```

##MyAnsewer

主要是解决数组的问题，感觉这些方式才是最佳的，以前的那些写法有点恶心。

```cs
dynamic result = new JObject();

var list = new JArray();
// This illustrates how to get the file names for uploaded files.
foreach (var file in provider.FileData)
{
    FileInfo fileInfo = new FileInfo(file.LocalFileName);
    dynamic fileJson = new JObject();
    fileJson.name = fileInfo.Name;
    list.Add(fileJson);
}
result.files = list;

result.ToString();
```

##OtherAnswer

Or you have test data that is dynamic you can use JObject.FromObject operation and supply a inline object.

```cs
JObject o = JObject.FromObject(new
{
    channel = new
    {
        title = "James Newton-King",
        link = "http://james.newtonking.com",
        description = "James Newton-King's blog.",
        item =
            from p in posts
            orderby p.Title
            select new
            {
                title = p.Title,
                description = p.Description,
                link = p.Link,
                category = p.Categories
            }
    }
});
```

Json.net documentation for serialization