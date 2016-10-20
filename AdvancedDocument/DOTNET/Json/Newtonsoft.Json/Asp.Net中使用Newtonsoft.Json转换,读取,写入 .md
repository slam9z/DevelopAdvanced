[Asp.Net中使用Newtonsoft.Json转换,读取,写入 ](http://blog.163.com/dreamman_yx/blog/static/265268942011217114528843/)

##JsonConvert

*JsonConvert.DeserializeObject*与 *JsonConvert.SerializeObject*挺有用的。

```cs
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

//把Json字符串反序列化为对象
目标对象 = JsonConvert.DeserializeObject(JSON字符串, typeof(目标对象));
//把目标对象序列化为Json字符串
string Json字符串 = JsonConvert.SerializeObject(目标对象);
```

1. 引用Newtonsoft.Json.dll
2. 在项目中添加引用..
序列化和反序列在.net项目中:

```cs
Product product = new Product();
 
product.Name = "Apple";
product.Expiry = new DateTime(2008, 12, 28);
product.Price = 3.99M;
product.Sizes = new string[] { "Small", "Medium", "Large" };
 
string output = JsonConvert.SerializeObject(product);
//{
//  "Name": "Apple",
//  "Expiry": new Date(1230422400000),
//  "Price": 3.99,
//  "Sizes": [
//     "Small",
//     "Medium",
//     "Large"
//  ]
//}
 
Product deserializedProduct = (Product)JsonConvert.DeserializeObject(output, typeof(Product));
```

##JsonReader and JsonWriter

尽量不使用这种方法

###读取JSON

```cs
string jsonText = "['JSON!',1,true,{property:'value'}]";
 
JsonReader reader = new JsonReader(new StringReader(jsonText));
 
Console.WriteLine("TokenType\t\tValueType\t\tValue");
 
while (reader.Read())
{
     Console.WriteLine(reader.TokenType + "\t\t" + WriteValue(reader.ValueType) + "\t\t" + WriteValue(reader.Value))
}
```

###JSON写入

```cs
StringWriter sw = new StringWriter();
JsonWriter writer = new JsonTextWriter(sw);
 
writer.WriteStartArray();
writer.WriteValue("JSON!");
writer.WriteValue(1);
writer.WriteValue(true);
writer.WriteStartObject();
writer.WritePropertyName("property");
writer.WriteValue("value");
writer.WriteEndObject();
writer.WriteEndArray();
 
writer.Flush();
 
string jsonText = sw.GetStringBuilder().ToString();
 
Console.WriteLine(jsonText);
// ['JSON!',1,true,{property:'value'}]
 
//把datatable转换成json格式 
public string GetAllCategory()   
{        
    string result = "";   
    DataTable dt= catDAO.GetAllCategory();   
    result=JsonConvert.SerializeObject(dt, new DataTableConverter());   
    return result;   
}  
```

##JObject,JArray对象

###FromObject方法

```
Could not determine JSON object type for type FileInfoEntity
```