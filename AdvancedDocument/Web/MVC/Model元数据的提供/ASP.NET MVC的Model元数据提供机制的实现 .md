[ASP.NET MVC的Model元数据提供机制的实现 ](http://www.cnblogs.com/artech/archive/2012/05/09/model-metadata-provision.html)

##一、 ModelMetadataProvider

``` C#
public abstract class ModelMetadataProvider
{    
    public abstract IEnumerable<ModelMetadata> GetMetadataForProperties( object container, Type containerType);
    public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);
    public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
}
```

##二、DataAnnotationsModelMetadataProvider


##三、对Model元数据提供系统的扩展

