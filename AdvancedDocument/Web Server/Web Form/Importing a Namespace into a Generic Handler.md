[Importing a Namespace into a Generic Handler](http://stackoverflow.com/questions/9368285/importing-a-namespace-into-a-generic-handler)


##answer

The ashx file itself should contain code like:
<%@ WebHandler Language="C#" Class="MyProject.MyHandlerClass" %>
And then your project will also contain a class file with that namespace and class:

```css
namespace MyProject
{
    public class MyHandlerClass : IHttpHandler
    {
         public void ProcessRequest(HttpContext context) { ... }
    }
}
```   
  
inside App_Code folder ? – Kubi Feb 21 '12 at 7:08 
   
  
off course it is. It was a silly question – Kubi Feb 21 '12 at 7:24 
