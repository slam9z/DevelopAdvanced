[C# and Visual Basic - Use Roslyn to Write a Live Code Analyzer for Your API](https://msdn.microsoft.com/en-us/magazine/dn879356.aspx)


Figure 6 The Complete Code for DiagnosticAnalyzer.cs

```cs
using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
namespace RegexAnalyzer
{
  [DiagnosticAnalyzer(LanguageNames.CSharp)]
  public class RegexAnalyzerAnalyzer : DiagnosticAnalyzer
  {
    public const string DiagnosticId = "Regex";
    internal const string Title = "Regex error parsing string argument";
    internal const string MessageFormat = "Regex error {0}";
    internal const string Description = "Regex patterns should be syntactically valid.";
    internal const string Category = "Syntax";
    internal static DiagnosticDescriptor Rule =
      new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat,
      Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);
    public override ImmutableArray<DiagnosticDescriptor>
      SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }
    public override void Initialize(AnalysisContext context)
    {
      context.RegisterSyntaxNodeAction(
        AnalyzeNode, SyntaxKind.InvocationExpression);
    }
    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
      var invocationExpr = (InvocationExpressionSyntax)context.Node;
      var memberAccessExpr =
        invocationExpr.Expression as MemberAccessExpressionSyntax;
      if (memberAccessExpr?.Name.ToString() != "Match") return;
      var memberSymbol = context.SemanticModel.
        GetSymbolInfo(memberAccessExpr).Symbol as IMethodSymbol;
      if (!memberSymbol?.ToString().StartsWith(
        "System.Text.RegularExpressions.Regex.Match") ?? true) return;
      var argumentList = invocationExpr.ArgumentList as ArgumentListSyntax;
      if ((argumentList?.Arguments.Count ?? 0) < 2) return;
      var regexLiteral =
        argumentList.Arguments[1].Expression as LiteralExpressionSyntax;
      if (regexLiteral == null) return;
      var regexOpt = context.SemanticModel.GetConstantValue(regexLiteral);
      if (!regexOpt.HasValue) return;
      var regex = regexOpt.Value as string;
      if (regex == null) return;
      try
      {
        System.Text.RegularExpressions.Regex.Match("", regex);
      }
      catch (ArgumentException e)
      {
        var diagnostic =
          Diagnostic.Create(Rule, regexLiteral.GetLocation(), e.Message);
        context.ReportDiagnostic(diagnostic);
      }
    }
  }
}
```