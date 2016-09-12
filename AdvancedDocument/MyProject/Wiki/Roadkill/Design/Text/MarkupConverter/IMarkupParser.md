```c#
    
public interface IMarkupParser
{
	/// <summary>
	/// Transforms the provided specific markup text to HTML
	/// </summary>
	string Transform(string transform);

	/// <summary>
	/// Occurs when an image tag is parsed.
	/// </summary>
	event EventHandler<ImageEventArgs> ImageParsed;

	/// <summary>
	/// Occurs when a hyperlink is parsed.
	/// </summary>
	event EventHandler<LinkEventArgs> LinkParsed;

	/// <summary>
	/// Help/documentation for the parser's tokens.
	/// </summary>
	MarkupParserHelp MarkupParserHelp { get; }
}

```