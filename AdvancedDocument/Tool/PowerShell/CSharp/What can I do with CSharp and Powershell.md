[What can I do with C# and Powershell?](http://stackoverflow.com/questions/742262/what-can-i-do-with-c-sharp-and-powershell)


##ansewr

I think the most interesting thing you can do with C# and PowerShell is to build CmdLet's. These are essentially plugins to PowerShell that are written in managed code and act like normal functions. They have a verb-noun pair and many of the functions you already use are actually cmdlets under the hood.
http://msdn.microsoft.com/en-us/magazine/cc163293.aspx


##answer



At the highest level you have two different options You can from a C# program host PowerShell and execute PowerShell commands via RunSpaces and pipelines.

Or you can from within PowerShell run C# code. This can be done two ways. With a PowerShell snapin, a compiled dll which provides PowerShell cmdlets and navigation providers, or via the new cmdlet Add-Type, which lets you dynamically import C#, VB, F# code. From the help

```
$source = @"
public class BasicTest
{
    public static int Add(int a, int b)
    {
        return (a + b);
    }

    public int Multiply(int a, int b)
    {
        return (a * b);
    }
}
"@

Add-Type -TypeDefinition $source
[BasicTest]::Add(4, 3)
$basicTestObject = New-Object BasicTest 
$basicTestObject.Multiply(5, 2)
```