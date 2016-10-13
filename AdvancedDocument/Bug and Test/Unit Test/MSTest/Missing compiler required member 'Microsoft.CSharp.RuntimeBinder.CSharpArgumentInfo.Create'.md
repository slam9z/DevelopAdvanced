[Using System.Dynamic in Roslyn](http://stackoverflow.com/questions/22864790/using-system-dynamic-in-roslyn)

##Question

I modified the example that comes with the new version of Roslyn that was released yesterday to use dynamic and ExpandoObject 
but I am getting a compiler error which I am not sure how to fix. The error is:

```
(7,21): error CS0656: Missing compiler required member 'Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create'
```

Can you not use dynamics in the new compiler yet? How can I fix this? Here is the example that I updated:

```cs
[TestMethod]
public void EndToEndCompileAndRun()
{
    var text = @"using System.Dynamic;
    public class Calculator
    {
        public static object Evaluate()
        {
            dynamic x = new ExpandoObject();
            x.Result = 42;
            return x.Result;
        } 
    }";

    var tree = SyntaxFactory.ParseSyntaxTree(text);
    var compilation = CSharpCompilation.Create(
        "calc.dll",
        options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
        syntaxTrees: new[] {tree},
        references: new[] {new MetadataFileReference(typeof (object).Assembly.Location), new MetadataFileReference(typeof (ExpandoObject).Assembly.Location)});

    Assembly compiledAssembly;
    using (var stream = new MemoryStream())
    {
        var compileResult = compilation.Emit(stream);
        compiledAssembly = Assembly.Load(stream.GetBuffer());
    }

    Type calculator = compiledAssembly.GetType("Calculator");
    MethodInfo evaluate = calculator.GetMethod("Evaluate");
    string answer = evaluate.Invoke(null, null).ToString();

    Assert.AreEqual("42", answer);
}
```

##Answer

I think that you should reference the Microsoft.CSharp.dll assembly