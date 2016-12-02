[Working with Common/Utility Libraries](http://stackoverflow.com/questions/41405/working-with-common-utility-libraries)

## question

At the company I work for we have a "Utility" project that is referenced by pretty much ever application we build. It's got lots of things like NullHelpers, ConfigSettingHelpers, Common ExtensionMethods etc.
The way we work is that when we want to make a new project, we get the latest version of the project from source control add it to the solution and then reference the project from any new projects that get added to the solution.

This has worked ok, however there have been a couple of instances where people have made "breaking changes" to the common project, which works for them, but doesn't work for others.

I've been thinking that rather than adding the common library as a project reference perhaps we should start developing the common library as a standalone dll and publish different versions and target a particular version for a particular project so that changes can be made without any risk to other projects using the common library.

Having said all that I'm interested to see how others reference or use their common libraries.

## answer   

That's exactly what we're doing. We have a Utility project which has some non project specific useful functions. We increase the version manually (minor), build the project in Release version, sign it and put it to a shared location.

People then use the specific version of the library.

If some useful methods are implemented in some specific projects which could find their way into main Utility project, we put the to a special helper class in the project, and mark them as a possible Utility candidate (simple //TODO). At the end of the project, we review the candidates and if they stick, we move them to the main library.

Breaking changes are a no-no and we mark methods and classes as [Obsolete] if needed. 
But, it doesn't really matter because we increase the version on every publish.
Hope this helps.