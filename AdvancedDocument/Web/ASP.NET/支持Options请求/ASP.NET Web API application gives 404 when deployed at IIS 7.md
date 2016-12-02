[ASP.NET Web API application gives 404 when deployed at IIS 7](http://stackoverflow.com/questions/15389855/asp-net-web-api-application-gives-404-when-deployed-at-iis-7)

> 部署到测试环境会出现问题！

## answer


I don't understand why but like said in this answer, adding runAllManagedModulesForAllRequests="true" did the trick.

```xml
<system.webServer>
    <validation validateIntegratedModeConfiguration="false"/> 
    <modules runAllManagedModulesForAllRequests="true"/> 
</system.webServer>
```

Anybody who can explain why?

## answer

For me, this issue was slightly different than other answers, as I was only receiving 404s on OPTIONS, yet I already had OPTIONS specifically stated in my Integrated Extensionless URL Handler options. Very confusing.

* As others have stated, runAllManagedModulesForAllRequests="true" in the modules node is an easy way to blanket-fix most Web API 404 issues - although I prefer @DavidAndroidDev 's answer which is much less intrusive. But there was something additional in my case.
* Unfortunately, I had this set in IIS under Request Filtering in the site:

By adding the following security node to the web.config was necessary to knock that out - full system.webserver included for context:

```xml
<system.webServer>
<modules runAllManagedModulesForAllRequests="true">
    <remove name="WebDAVModule" />
</modules>
<handlers>
    <remove name="ExtensionlessUrlHandler-Integrated-4.0" />
    <remove name="OPTIONSVerbHandler" />
    <remove name="TRACEVerbHandler" />
    <remove name="WebDAV" />
    <add name="ExtensionlessUrlHandler-Integrated-4.0" path="*." verb="*" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
</handlers>
<security>
    <requestFiltering>
    <verbs>
        <remove verb="OPTIONS" />
    </verbs>
    </requestFiltering>
</security>
</system.webServer>
```

Although it's not the perfect answer for this question, it is the first result for "IIS OPTIONS 404" on Google, so I hope this helps someone out; cost me an hour today. 