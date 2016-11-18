[how can we load usercontrol using generic handler?](http://stackoverflow.com/questions/5499753/how-can-we-load-usercontrol-using-generic-handler)

It seems page.Form is null here, that's why you've got a null reference exception. You c
ould add your user control to the page's control collection instead:

```cs
page.Controls.Add(ctrl);
```

You could also use HttpServerUtility.Execute method for page rendering:

```cs
StringWriter output = new StringWriter();
HttpContext.Current.Server.Execute(page, output, false);
```

And finally take a look onto Tip/Trick: Cool UI Templating Technique to use with ASP.NET AJAX for non-UpdatePanel 
scenarios article by Scott Guthrie which covers your problem.