[How can I compile and run c# program without using visual studio](http://stackoverflow.com/questions/18286855/how-can-i-compile-and-run-c-sharp-program-without-using-visual-studio)

##Question

I am very new to c#.I have just run c# 'Hello World' program using visual studio.
So,question comes into my mind.Can I run or compile c# program without using visual studio?
If it is possible then which compiler should i use?
Thanks.

##Answer

>原来msbuild是这样用的，很方便啊！就是要自己编辑csproj文件，也有点麻烦。

If you have the .NET v4 installed (so if you have a newer windows or if you apply the windows updates)
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe nomefile.cs
or
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe nomefile.sln
or
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe nomefile.csproj
```
It's highly probable that if you have .NET installed, the %FrameworkDir% variable is set, so:

```
%FrameworkDir%\v4.0.30319\csc.exe ...

%FrameworkDir%\v4.0.30319\msbuild.exe ...
```

