##JSON Media-Type Formatter

JSON formatting is provided by the JsonMediaTypeFormatter class. 
By default, JsonMediaTypeFormatter uses the Json.NET library to perform serialization. Json.NET is a third-party open source project.

If you prefer, you can configure the JsonMediaTypeFormatter class to use the DataContractJsonSerializer instead of Json.NET.
 To do so, set the UseDataContractJsonSerializer property to true:

```
var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
json.UseDataContractJsonSerializer = true;
```


###JSON Serialization

```
 [JsonIgnore]
```

###Dates

If your web API receives loosely structured JSON objects from clients, you can deserialize the request body to a Newtonsoft.Json.Linq.JObject type.

``` C#
public void Post(JObject person)
{
    string name = person["Name"].ToString();
    int age = person["Age"].ToObject<int>();
}
```



##XML Media-Type Formatter

XML formatting is provided by the XmlMediaTypeFormatter class. By default, XmlMediaTypeFormatter
 uses the DataContractSerializer class to perform serialization. 

If you prefer, you can configure the XmlMediaTypeFormatter to use the XmlSerializer instead of the  
DataContractSerializer. To do so, set the UseXmlSerializer property to true:

``` C#
var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
xml.UseXmlSerializer = true;
```


##Handling Circular Object References

