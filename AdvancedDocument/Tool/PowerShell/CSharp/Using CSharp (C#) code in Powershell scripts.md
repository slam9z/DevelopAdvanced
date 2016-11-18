[Using CSharp (C#) code in Powershell scripts](https://blogs.technet.microsoft.com/stefan_gossner/2010/05/07/using-csharp-c-code-in-powershell-scripts/)



With Powershell we now got a new very powerful scripting language and many products like SharePoint provide their own extensions in form of CmdLets for administration purposes.

Customer like scripting languages as it allows them to write custom code without a need to run a compiler or to copy new executables to their production machines which usually requires a more complex approval process than deploying a script file or even to execute the commands within a command shell.

But on the other hand writing Powershell scripts requires to learn a new scripting language and to to use different tools than many of us are used to. As a developer I like the power of C# and IntelliSense in Visual Studio. In addition in the last couple of years I wrote many different tools in C# – and I don’t want to reinvent the wheel by porting them to Powershell.

So it would be great if the existing C# code could be reused inside Powershell without a need to implement it as Cmdlet.

And indeed the Powershell Version 2 provides a way to achieve this using the Add-Type Cmdlet which allows to generate a new .NET assembly using the provided C# source code in memory which can then be used by powershell scripts executed in the same session.

For demonstration purposes let’s assume we have the following simple C# code which allows to retrieve and set the RemoteTimeout value used in Content Deployment of SharePoint:

```cs
using Microsoft.SharePoint.Publishing.Administration; 
using System; 

namespace StefanG.Tools 
{ 
    public static class CDRemoteTimeout  
    { 
        public static void Get() 
        { 
            ContentDeploymentConfiguration cdconfig = ContentDeploymentConfiguration.GetInstance(); 
            Console.WriteLine(“Remote Timeout: “+cdconfig.RemoteTimeout); 
        } 


        public static void Set(int seconds) 
        { 
            ContentDeploymentConfiguration cdconfig = ContentDeploymentConfiguration.GetInstance(); 
            cdconfig.RemoteTimeout = seconds;
            cdconfig.Update();
        } 
    } 
}
```

Beside the standard .NET framework references this tool has references to two SharePoint DLLs (Microsoft.SharePoint.dll and Microsoft.SharePoint.Publishing.dll) which provide access to the SharePoint object model. To ensure that Powershell can correctly generate the assembly we need to provide this reference information to the Add-Type Cmdlet using the -ReferencedAssemblies parameter.

To specify the language of the source code (CSharp, CSharpVersion3, Visual Basic and JScript can be used) you need to provide the -Language parameter. CSharp is default. 

On my system I have a csharptemplate.ps1 file which I can quickly copy and adjust to run my C# code if required which looks like this:

```ps1
$Assem = ( 

…add referenced assemblies here…
    ) 

$Source = @” 
…add C# source code here…
“@ 

Add-Type -ReferencedAssemblies $Assem -TypeDefinition $Source -Language CSharp 
```

For the above listed C# example the final powershell script – would look like this:

```ps1
$Assem = ( 
    “Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c” , 
    “Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c” 
    ) 

$Source = @” 
using Microsoft.SharePoint.Publishing.Administration; 
using System; 

namespace StefanG.Tools 
{ 
    public static class CDRemoteTimeout  
    { 
        public static void Get() 
        { 
            ContentDeploymentConfiguration cdconfig = ContentDeploymentConfiguration.GetInstance(); 
            Console.WriteLine(“Remote Timeout: “+cdconfig.RemoteTimeout); 
        } 
         
        public static void Set(int seconds) 
        { 
            ContentDeploymentConfiguration cdconfig = ContentDeploymentConfiguration.GetInstance(); 
            cdconfig.RemoteTimeout = seconds;
            cdconfig.Update();
        } 
    } 
} 
“@ 

Add-Type -ReferencedAssemblies $Assem -TypeDefinition $Source -Language CSharp  

[StefanG.Tools.CDRemoteTimeout]::Get()

[StefanG.Tools.CDRemoteTimeout]::Set(600)
```

The last lines in the above listed sample demonstrates how to call the C# methods from Powershell.