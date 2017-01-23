[Force Visual Studio to fail compile for syntax errors in aspx/ascx pages for ASP.Net](http://stackoverflow.com/questions/21799963/force-visual-studio-to-fail-compile-for-syntax-errors-in-aspx-ascx-pages-for-asp)

使vs强制编译aspx和ascx。


这个是必须的，连编译都通不过的代码，注定是垃圾！


[How can I compile Asp.Net Aspx Pages before loading them with a webserver?](http://stackoverflow.com/questions/108405/how-can-i-compile-asp-net-aspx-pages-before-loading-them-with-a-webserver)



## answer

In the Post-build event command line: text area, write this (for .NET 4.0):

```
%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_compiler.exe -v / -p "$(SolutionDir)$(ProjectName)"
```