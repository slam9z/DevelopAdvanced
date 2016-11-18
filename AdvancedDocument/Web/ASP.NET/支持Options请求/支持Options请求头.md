  
看了这么多，唯一有效的是runAllManagedModulesForAllRequests="true"。

```xml
<system.webServer>
    <modules runAllManagedModulesForAllRequests="true">
        <remove name="WebDAVModule" />
    </modules>
   <handlers>
      <remove name="WebDAV" />
    </handlers>
</system.webServer>
```

设置了这么多都没啥用

Global.asax的Application_BeginRequest方法无法执行，
肯定被某个module处理了。先暂时这样吧

```xml
<system.webServer>

    <security>

      <requestFiltering>
        <requestLimits maxAllowedContentLength="4294967295" />
        <verbs>
          <add verb="OPTIONS" allowed="true" />
        </verbs>
      </requestFiltering>
    </security>

    <validation validateIntegratedModeConfiguration="false"/>
    <modules>
       <remove name="WebDAVModule" />
    </modules>

    <handlers>
      <remove name="WebDAV" />
      <remove name="ExtensionlessUrlHandler-Integrated-4.0"/>
      <remove name="OPTIONSVerbHandler"/>
      <remove name="TRACEVerbHandler"/>
      <add name="ExtensionlessUrlHandler-Integrated-4.0" path="*." verb="*" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0"/>
    </handlers>

  </system.webServer>
  ```