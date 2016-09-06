[Parameter Binding in ASP.NET Web API](http://www.asp.net/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api)

By default, Web API uses the following rules to bind parameters:

* If the parameter is a “simple” type, Web API tries to get the value from the URI. 
Simple types include the .NET primitive types (int, bool, double, and so forth), 
plus TimeSpan, DateTime, Guid, decimal, and string, plus any type with a type 
converter that can convert from a string. (More about type converters later.)

* For complex types, Web API tries to read the value from the message body, using a media-type formatter.

##Using [FromUri]

``` C#
public class GeoPoint
{
    public double Latitude { get; set; } 
    public double Longitude { get; set; }
}

public ValuesController : ApiController
{
    public HttpResponseMessage Get([FromUri] GeoPoint location) { ... }
}
```


##Using [FromBody]

At most one parameter is allowed to read from the message body. 


##Type Converters

``` C#
[TypeConverter(typeof(GeoPointConverter))]
public class GeoPoint
{
    public double Latitude { get; set; } 
    public double Longitude { get; set; }

    public static bool TryParse(string s, out GeoPoint result)
    {
        result = null;

        var parts = s.Split(',');
        if (parts.Length != 2)
        {
            return false;
        }

        double latitude, longitude;
        if (double.TryParse(parts[0], out latitude) &&
            double.TryParse(parts[1], out longitude))
        {
            result = new GeoPoint() { Longitude = longitude, Latitude = latitude };
            return true;
        }
        return false;
    }
}

class GeoPointConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, 
        CultureInfo culture, object value)
    {
        if (value is string)
        {
            GeoPoint point;
            if (GeoPoint.TryParse((string)value, out point))
            {
                return point;
            }
        }
        return base.ConvertFrom(context, culture, value);
    }
}
    

```


##Model Binders

A more flexible option than a type converter is to create a custom model binder. With a model binder, you have access to things like the HTTP request, the action description, and the raw values from the route data. 

To create a model binder, implement the IModelBinder interface. This interface defines a single method, BindModel:

``` C#
bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext);
```

Setting the Model Binder

There are several ways to set a model binder. First, you can add a [ModelBinder] attribute to the parameter.

``` C#
public HttpResponseMessage Get([ModelBinder(typeof(GeoPointModelBinder))] GeoPoint location)
```


##Value Providers


##HttpParameterBinding


##IActionValueBinder

    