[Model Validation in ASP.NET Web API](http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api)

这种默认的机制简单，但是对客户端不友好。

##Data Annotations


In ASP.NET Web API, you can use attributes from the System.ComponentModel.DataAnnotations namespace to set validation rules 
for properties on your model. 


``` C#
public HttpResponseMessage Post(Product product)
    {
        if (ModelState.IsValid)
        {
            // Do something with the product (not shown).

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
```


##Handling Validation Errors

还比较好友啊

``` C#
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        if (actionContext.ModelState.IsValid == false)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, actionContext.ModelState);
        }
    }
}
```



