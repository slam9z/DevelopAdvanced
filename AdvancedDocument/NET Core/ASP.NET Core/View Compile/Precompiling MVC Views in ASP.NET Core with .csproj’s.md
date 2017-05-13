[Precompiling MVC Views in ASP.NET Core with .csproj’s](https://scottsauber.com/2017/03/10/pre-compiling-razor-views-in-asp-net-core-with-csprojs/)


I didn’t see this documented on docs.asp.net yet for .csproj style projects, so I thought I’d blog about it, as it’s something I do on every project.
tldr;

Add this to your .csproj file:

2017-03-10_13-02-21.png

Alternatively, you can check out this GitHub commit that just adds pre-compiled views here.

Also – if you’re running on .NET Core and not Full Framework, then make sure your TargetFramework is netcoreapp1.1+ and not netcoreapp1.0.
What does precompiling views do?

Without precompiling your views, none of your views are compiled until they are requested for the first time.  This results in an extra few seconds of wait time for the end user.  Instead, most developers want to take a little bit of extra time in the publish step to precompile their views, so that users don’t have to eat that performance hit on the first request on every page.

Another benefit of precompiling your views is if there is a C# error on one of your .cshtml Razor pages (such as you changed the name of a property on your View Model and the rename didn’t update your .cshtml file), you will find out up front when you go to publish (via VS or a build server) rather than at run time when a user goes to request that page.

So now that we’ve seen what it does, let’s look at what it used to be in project.json.
What did it look like in project.json?

In project.json land, the pre-compiling of Views was a little verbose.  You had to do the following:

    Add a reference to “Microsoft.AspNetCore.Mvc.Razor.ViewCompilation.Design” under the “dependencies” section
    Add a reference to “Microsoft.AspNetCore.Mvc.Razor.ViewCompilation.Tools” under the tools section.
    Under buildOptions, set “preserveCompilationContext”to true
    And finally you had to add a fairly verbose post-publish script to actually use the pre-compile tool.All of that looked like this (removing the rest of the project.json for brevity):

This then generates a <ProjectName>.PrecompiledViews.dll in the root of your publish directory.  You will have no refs folder as well.
How does it look in .csproj?

In project.json, it was pretty verbose.  So what does it look like in .csproj?  It’s much cleaner.

    Add

```xml
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Razor.ViewCompilation" Version="1.1.0" />
```
    Add
```xml
    <MvcRazorCompileOnPublish>true</MvcRazorCompileOnPublish>
```
    Add
```xml
    <PreserveCompilationContext>true</PreserveCompilationContext>
```
So the final output of a .csproj might look a little something like this.  Note lines 5,6, and 32.

This will still generate a <ProjectName>.PrecompiledViews.dll in the root of your publish directory and you will still have no refs folder.

This simplicity in order to get precompiled views is yet another win for the .csproj style ASP.NET Core projects over project.json.