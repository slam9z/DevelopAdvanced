## config

### 节点位置

Web.config

```xaml
<configuration>
    <connectionStrings>
    </connectionStrings>
</configuration>
```

### 配置方法

不同的位置不一样。

* 使用configSource方式配置

    根节点是connectionStrings的单独配置文件

* 直接配置

### database first

```
 <add name="Entities" 

 connectionString="metadata=res://*/Entity.csdl|res://*/Entities.ssdl|res://*/Entities.msl;
 provider=System.Data.SqlClient;
 provider connection string=&quot;
 Data Source=192.168.0.1;
 Initial Catalog=Entities;
 Persist Security Info=True;
 User ID=sa;
 Password=sa;
 MultipleActiveResultSets=True&quot;" 

 providerName="System.Data.EntityClient"/>

```

