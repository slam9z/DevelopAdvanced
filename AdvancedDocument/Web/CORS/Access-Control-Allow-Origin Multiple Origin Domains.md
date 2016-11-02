[Access-Control-Allow-Origin Multiple Origin Domains?](http://stackoverflow.com/questions/1653308/access-control-allow-origin-multiple-origin-domains)

```cs
public static string GetAllowOrigin(HttpContext context)
{
    var res = context.Response;
    var req = context.Request;

    string origin = null;

    if (req.UrlReferrer != null)
    {
        origin = req.UrlReferrer?.Host;
        origin = string.Format("{1}://{0}", origin, req.UrlReferrer.Scheme);
    }
    if (origin == null)
    {
        origin = req.Headers["Origin"];
    }

    var domain = "baseurl";

    if (!string.IsNullOrWhiteSpace(domain) && origin != null && origin.Contains(domain))
    {
        return origin;
    }

    return null;
}
```