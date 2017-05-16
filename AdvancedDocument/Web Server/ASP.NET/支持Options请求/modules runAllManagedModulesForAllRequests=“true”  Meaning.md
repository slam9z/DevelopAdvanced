[<modules runAllManagedModulesForAllRequests=“true” /> Meaning](http://stackoverflow.com/questions/11048863/modules-runallmanagedmodulesforallrequests-true-meaning)


Modules Preconditions:

The IIS core engine uses preconditions to determine when to enable a particular module. Performance reasons, 
for example, might determine that you only want to execute managed modules for requests that also go to a 
managed handler. The precondition in the following example (precondition="managedHandler") only enables the 
forms authentication module for requests that are also handled by a managed handler, such as requests to 
.aspx or .asmx files:

```xml
<add name="FormsAuthentication" type="System.Web.Security.FormsAuthenticationModule" preCondition="managedHandler" />
```

If you remove the attribute precondition="managedHandler", Forms Authentication also applies to content that is
 not served by managed handlers, such as .html, .jpg, .doc, but also for classic ASP (.asp) or PHP (.php) 
 extensions. See "How to Take Advantage of IIS Integrated Pipeline" for an example of enabling ASP.NET modules
  to run for all content.

You can also use a shortcut to enable all managed (ASP.NET) modules to run for all requests in your application,
 regardless of the "managedHandler" precondition. 

To enable all managed modules to run for all requests without configuring each module entry to remove the 
"managedHandler" precondition, use the runAllManagedModulesForAllRequests property in the <modules> section:

```xml
<modules runAllManagedModulesForAllRequests="true" />    
```
When you use this property, the "managedHandler" precondition has no effect and all managed modules run for all 
requests.

Copied from [IIS Modules Overview: Preconditions](http://www.iis.net/learn/get-started/introduction-to-iis/iis-modules-overview#Precondition)