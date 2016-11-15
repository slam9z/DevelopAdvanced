[Using Roslyn and PowerShell 3 to Convert C# to PowerShell](http://csharpening.net/?p=1311)


Right after seeing Jason Bock’s Roslyn session at That Conference I wanted to combine the two abstract syntax tree systems to do something like this. I haven’t finished a full working copy but anticipate making something that is usable via the command line or other projects in DLL form. With Roslyn, one of the samples creates a Visual Studio extension that can copy VB code and paste C# code and vice versa. I thought this would be awesome to do with PowerShell. Here’s how it works.

In the Roslyn.Compilers.CSharp namespace there is a StatementVistor class. This class works much that same as the ICustomAstVisitor we find in the System.Management.Automation namespace for PowerShell. It uses the same Visitor pattern which makes it very easy to use them together.

First, we need to extend from StatementVisitor and use the Ast class as the type parameter. We make it the type parameter because the Ast class is a base class for all PowerShell syntax nodes. We need to create a basic Convert method that accepts some C# text, converts it to a syntax tree and then visits the tree. I did a little fudging here and grabbed just the VariableDeclarationSyntax nodes from the syntax tree.
My target C# code to convert is:  System.DateTime time = System.DateTime.Now;


Next we need to break down the statement into the nodes that we are concerned about converting. These nodes break down like this. For each of the different nodes we need to override a particular visiting method.

Here is an example of how one of the nodes is visited. As you can see the visitor node accepts a C#\Roslyn specific node and returns a PowerShell Ast node.

After implementing all the necessary visitor methods, we can then go ahead and create a little test app to visit some C# code. Here is the code that I used to test this.  The source variable contains the C# code and the target variable contains what the output should look like. The converted script block contents are passed to the PowerShell object for execution.

And here’s the final result:

Wicked! It should be possible to do a lot of conversion between C# and PowerShell. There will be some tricks involved in converting C# to PowerShell. For instance, PowerShell doesn’t really have classes that can be defined in purely PowerShell. There are some methods to do this that may be appropriate but we’ll see what we can do. 