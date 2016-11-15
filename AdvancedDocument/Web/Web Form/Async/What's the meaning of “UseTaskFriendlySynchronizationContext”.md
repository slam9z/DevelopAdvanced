[What's the meaning of “UseTaskFriendlySynchronizationContext”?](http://stackoverflow.com/questions/9562836/whats-the-meaning-of-usetaskfriendlysynchronizationcontext)

Regarding `UseTaskFriendlySynchronizationContext`, from Microsoft Forums:

> That tells ASP.NET to use an entirely new asynchronous pipeline which follows CLR conventions for kicking off asynchronous operations, including returning threads to the ThreadPool when necessary. ASP.NET 4.0 and below followed its own conventions which went against CLR guidelines, and if the switch is not enabled it is very easy for asynchronous methods to run synchronously, deadlock the request, or otherwise not behave as expected.


Also, I think AsyncOperationManager is intended for desktop applications. For ASP.NET apps you should be using RegisterAsyncTask and setting <%@ Page Async="true", see here for more details.

So using the new c# keywords your example would be:

```cs
protected void Button1_Click(object sender, EventArgs e)
{
    RegisterAsyncTask(new PageAsyncTask(CallAysnc));
}

private async Task CallAysnc()
{
    var res = await new WebClient().DownloadStringTaskAsync("http://www.google.com");
    Response.Write(res);
}
```

The aim is to support the following by release but is not currently supported in the beta:

```cs
protected async void Button1_Click(object sender, EventArgs e)
{
    var res = await new WebClient().DownloadStringTaskAsync("http://www.google.com");
    Response.Write(res);
}
```