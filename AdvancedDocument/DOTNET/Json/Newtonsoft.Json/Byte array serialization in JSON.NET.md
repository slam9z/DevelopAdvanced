[Byte array serialization in JSON.NET](http://stackoverflow.com/questions/2644853/byte-array-serialization-in-json-net)

## question

Given this simple class:

```cs
class HasBytes
{
    public byte[] Bytes { get; set; }
}
```

I can put it through JSON.NET such that the byte array is base-64 encoded:

```cs
var bytes = new HasBytes { Bytes = new byte[] { 1, 2, 3, 4 } };
var json = JsonConvert.SerializeObject(bytes);
```

Then I can read it back again in this slightly over-complicated way:

```cs
TextReader textReader = new StringReader(json);
JsonReader jsonReader = new JsonTextReader(textReader);
var result = (HasBytes)JsonSerializer.Create(null)
                 .Deserialize(jsonReader, typeof(HasBytes));
```

All good. But if I first turn the contents of jsonReader into a JToken:

```cs
var jToken = JToken.ReadFrom(jsonReader);
```

And then turn that back into a JsonReader by wrapping it in a JTokenReader:

```cs
jsonReader = new JTokenReader(jToken);
```

Then the deserialization throws an exception: "Expected bytes but got string".

Shouldn't the new JsonReader be logically equivalent to the original one? Why does the "raw" JsonTextReader have the ability to treat a string as a base-64 byte array whereas the JTokenReader version does not?
