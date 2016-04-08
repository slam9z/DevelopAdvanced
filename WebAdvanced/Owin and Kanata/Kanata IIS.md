[OWIN Middleware in the IIS integrated pipeline](http://www.asp.net/aspnet/overview/owin-and-katana/owin-middleware-in-the-iis-integrated-pipeline)

##How OWIN Middleware Executes in the IIS Integrated Pipeline

For OWIN console applications, the application pipeline built using the  startup configuration is set by the order the components are added using the IAppBuilder.
Use method. That is, the OWIN pipeline in the Katana runtime will process OMCs in the order they were registered using IAppBuilder.Use. 
In the IIS integrated pipeline the request pipeline consists of HttpModules subscribed to a pre-defined set of the pipeline events such as BeginRequest, 
AuthenticateRequest, AuthorizeRequest, etc. 

``` C#
public class MyModule : IHttpModule
{
    public void Dispose()
    {
        //clean-up code here.
    }
    public void Init(HttpApplication context)
    {
        // An example of how you can handle AuthenticateRequest events.
        context.AuthenticateRequest += ctx_AuthRequest;
    }
    void ctx_AuthRequest(object sender, EventArgs e)
    {
        // Handle event.
    }
}
```

##Stage Markers

```C#
app.UseStageMarker(PipelineStage.ResolveCache);
```

##Stage Marker Rules

```C#
public enum PipelineStage
{
    Authenticate = 0,
    PostAuthenticate = 1,
    Authorize = 2,
    PostAuthorize = 3,
    ResolveCache = 4,
    PostResolveCache = 5,
    MapHandler = 6,
    PostMapHandler = 7,
    AcquireState = 8,
    PostAcquireState = 9,
    PreHandlerExecute = 10,
}
```