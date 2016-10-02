编译器错误消息: CS0104: “EnumType”是“CEProjectCenter.Model.EnumType”和“CTBExchange.Model.EnumType”之间的不明确的引用

CTBExchange.Model在web.config

```xml
<system.web>
    <pages>
        <namespaces>
            <add namaspace="CTBExchange.Model">
```

全局引用就是一个杯具!

这些人内部类用疯狂了！