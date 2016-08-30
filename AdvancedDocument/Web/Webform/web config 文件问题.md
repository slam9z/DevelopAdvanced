##modules配置

IIS10

HTTP Error 500.19 - Internal Server Error

The requested page cannot be accessed because the related configuration data for the page is invalid.


```xml
<system.webServer>
	<modules runAllManagedModulesForAllRequests="true" />
		<defaultDocument>
			<files>
			<add value="index.aspx" />
			</files>
		</defaultDocument>
	<directoryBrowse enabled="true" />
</system.webServer>
```
