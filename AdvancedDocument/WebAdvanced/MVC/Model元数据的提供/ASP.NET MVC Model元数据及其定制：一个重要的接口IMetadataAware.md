[ASP.NET MVC Model元数据及其定制：一个重要的接口IMetadataAware ](http://www.cnblogs.com/artech/archive/2012/04/13/model-metadata-03.html)


``` C#
public interface IMetadataAware
{    
    void OnMetadataCreated(ModelMetadata metadata);
}
```

##实例演示：创建实现IMetadataAware接口的特性定制Model元数据