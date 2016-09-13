[Completely remove ViewState for specific pages](http://stackoverflow.com/questions/2432972/completely-remove-viewstate-for-specific-pages)



You could override Render and strip it out with a Regex.
Sample as requested. (NB: Overhead of doing this would almost certainly be greater than any possible benefit though!)

[edit: this function was also useful for stripping all hidden input boxes for using the HTML output as a word doc by changing the MIMEType and file extension]

```cs
protected override void Render(HtmlTextWriter output)
{
    StringWriter stringWriter = new StringWriter();

    HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);
    base.Render(textWriter);

    textWriter.Close();

    string strOutput = stringWriter.GetStringBuilder().ToString();

    strOutput = Regex.Replace(strOutput, "<input[^>]*id=\"__VIEWSTATE\"[^>]*>", "", RegexOptions.Singleline);

    output.Write(strOutput);
}
```