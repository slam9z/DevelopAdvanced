[How to run PowerShell scripts from C#](http://www.codeproject.com/Articles/18229/How-to-run-PowerShell-scripts-from-C)


##Using the Code

To add PowerShell scripting to your program, you first have to add a reference to the System.Management.Automation assembly. The SDK installs this assembly in the C:\Program Files\Reference Assemblies\Microsoft\WindowsPowerShell\v1.0 directory.
Then, you have to add the following 'using' statements to import the required types:

```cs
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
```

The following code block shows the RunScript method that does all the hard work. It takes the script text, executes it, and returns the result as a string.

```cs
private string RunScript(string scriptText)
{
    // create Powershell runspace

    Runspace runspace = RunspaceFactory.CreateRunspace();

    // open it

    runspace.Open();

    // create a pipeline and feed it the script text

    Pipeline pipeline = runspace.CreatePipeline();
    pipeline.Commands.AddScript(scriptText);

    // add an extra command to transform the script
    // output objects into nicely formatted strings

    // remove this line to get the actual objects
    // that the script returns. For example, the script

    // "Get-Process" returns a collection
    // of System.Diagnostics.Process instances.

    pipeline.Commands.Add("Out-String");

    // execute the script

    Collection<psobject /> results = pipeline.Invoke();

    // close the runspace

    runspace.Close();

    // convert the script result into a single string

    StringBuilder stringBuilder = new StringBuilder();
    foreach (PSObject obj in results)
    {
        stringBuilder.AppendLine(obj.ToString());
    }

    return stringBuilder.ToString();
}
```