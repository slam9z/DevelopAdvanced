[File Upload using jQuery AJAX in ASP.NET Web API](http://www.codeproject.com/Articles/806075/File-Upload-using-jQuery-AJAX-in-ASP-NET-Web-API)



##How to change the size limitation to upload large files?

If you want to upload large file(maximum 2 GB!) then you need to change the <httpRuntime> and <requestLimits> default settings
 in Web.config file like follows:

```xml
<system.web>
  <httpRuntime executionTimeout="240000" maxRequestLength="2147483647" />
</system.web>

<security>
  <requestFiltering>
    <requestLimits maxAllowedContentLength="4294967295"/>
  </requestFiltering>
</security>
```

###executionTimeout

It is the maximum number of seconds a request is allowed to execute before being automatically shut down by ASP.NET. The value
 is in seconds. 

Default Value: For ASP.NET 1.x it is 90 seconds and for ASP.NET 2.0 or higher it is 110 seconds.

Maximum Value: Theoretically its maximum value is the maximum value of TimeSpan i.e 10675199.02:48:05.4775807.
 The value of this setting is ignored in debug mode.

###maxRequestLength 

It is the maximum allowed request length. The value is in K.B(Kilo Bytes). 
Default Value: 4 MB
Maximum Value: Theoretically its maximum value is the maximum value of int i.e 2147483647.

###maxAllowedContentLength

It specifies the maximum length of content in a request. The value is in bytes.
Default Value: 30000000 bytes(~29 MB)
Maximum Value: Theoretically its maximum value is the maximum value of uint(unsigned interger) i.e 4294967295.