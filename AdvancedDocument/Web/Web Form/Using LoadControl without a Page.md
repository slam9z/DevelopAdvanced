[Using LoadControl without a Page](http://stackoverflow.com/questions/3313324/using-loadcontrol-without-a-page)

You can get your Page-Object from HttpContext in this way:

```cs
Page page = HttpContext.Current.Handler as Page;
if (page != null)
{
     // Use page instance to load your Usercontrol
}
```