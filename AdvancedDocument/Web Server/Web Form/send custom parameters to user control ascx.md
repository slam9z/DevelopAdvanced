[send custom parameters to user control ascx](http://stackoverflow.com/questions/16205347/send-custom-parameters-to-user-control-ascx)

## answer

Create public properties of the user-control like:

```cs
public partial class SampleUC : UserControl
{
    public string CurrentPost {get;set;}
    public string RelationType {get;set;}

    //...

    //...
}
```

Assign those from the page using it either from markup like:

```aspx
<%@ Register TagPrefix="cc" TagName="SampleUC" Src="SampleUC.ascx" %>

<cc:SampleUC id="myUC" runat="server" CurrentPost="Sample Post Title" RelationType="Title" />
```

or from code-behind (of the page using it):

```cs
protected void Page_Load(object sender, EventArgs e)
{
    //...

    myUC.CurrentPost = "Sample Post Title";
    myUC.RelationType = "Title" ;

    //...
}
```