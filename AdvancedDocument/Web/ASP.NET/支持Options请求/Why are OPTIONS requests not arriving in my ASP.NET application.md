[Why are OPTIONS requests not arriving in my ASP.NET application?](http://stackoverflow.com/questions/6656354/why-are-options-requests-not-arriving-in-my-asp-net-application)


##question

I can't seem to receive HTTP OPTIONS requests in my IIS6 hosted ASP.NET application. I'm testing it using a debug
 breakpoint (and file-log) in my Global.asax's Application_BeginRequest method. The breakpoint is never hit and
  the client gets a HTTP 403 Forbidden, I'm assuming from IIS6 directly (GETs and POSTs work fine btw).

I've tried several things in the web.config, including adding the following line to either and both the 
<system.webServer><handlers> and <system.web><httpHandlers> sections.

```xml
<add name="OptionsHandler" verb="OPTIONS" path="*" type="System.Web.DefaultHttpHandler"/>
```

I've also played with the <system.webServer><security><requestFiltering><verbs> settings and allowUnlisted="true" 
and <add verb="OPTIONS" allowed="true"/>.

Also, I'm not using URLScan or any other tools that might intercept the calls. In case you're interested in IISLogs:

2011-07-11 20:26:05 W3SVC1215124377 127.0.0.1 OPTIONS /test.aspx - 80 - 127.0.0.1
 Mozilla/5.0+(Windows+NT+5.2;+rv:5.0)+Gecko/20100101+Firefox/5.0 403 1 0

##answer

For IIS6, you will have to enable the OPTIONS verb explicitly in the management console, and you will also
 need to map it to be handlded by ASP .NET. Only then, you will be able to register your handler in <system.web>
  and get the request processed by ASP .NET.
(Note, <system.webServer> settings only applies to IIS7).
   
  
How can I enable it explicitly in the management console? – Josef Pfleger Jul 11 '11 at 21:24 
3 
  
Found it! You have to explicitly allow the verb for each mapping in 
Web -> Properties -> Home Directory -> Configuration... -> Mappings. THX – Josef Pfleger Jul 12 '11 at 11:28 
   
  
For MVC you might need to do some more configurations