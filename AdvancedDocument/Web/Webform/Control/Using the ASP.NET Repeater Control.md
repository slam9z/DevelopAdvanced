[Using the ASP.NET Repeater Control](https://www.sitepoint.com/asp-net-repeater-control/)


##Step 1 — Create the Page and Insert the Repeater Control

The Repeater control allows you to create templates to define the layout of its content. The templates are:

* ItemTemplate — Use this template for elements that are rendered once per row of data.
* AlternatingItemTemplate — Use this template for elements that are rendered every other row of data. This allows you to alternate background colors, for example.
* HeaderTemplate — Use this template for elements that you want to render once before your ItemTemplate section.
* FooterTemplate — Use this template for elements that you want to render once after your ItemTemplate section.
* SeperatorTemplate — Use this template for elements to render between each row, such as line breaks.

```html
<asp:Repeater ID="catlist" runat="server">
<HeaderTemplate>
<tr>
<td class="imgspace">
<img src="Images/areas.jpg" width="91" height="28" class="bigtext">
</td>
</tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
<td>
<div align=center>
<asp:HyperLink class="text"
NavigateUrl="<%# "mainframeset.aspx?CatType=" +
DataBinder.Eval(Container.DataItem,"Sub_Category_ID")%>"
Text="<%#DataBinder.Eval(Container.DataItem, "Sub_Category_Text")%>"
runat="server" target="mainFrame" ID="Hyperlink1" NAME="Hyperlink1"/>
<br></div>
</td>
</tr>
</ItemTemplate>
<FooterTemplate>
<tr>
<td>
</td>
</tr>
</FooterTemplate>
</asp:Repeater>
```

##Step 2 — Get the Data

Now let’s look at the data retrieval. Here is the Page_Load event in the Code Behind file.

```c#
private void Page_Load(object sender, System.EventArgs e)
{
SqlConnection conDotNet = new SqlConnection
"Server=xxxxxxx;UID=xxxx;PWD=xxxxx;Database=DotNetGenius");
string sSQL = "Select sub_category_id, sub_category_text
from Sub_Category";
SqlCommand cmd = new SqlCommand(sSQL, conDotNet);
conDotNet.Open();
SqlDataReader dtrCat = cmd.ExecuteReader();
catlist.DataSource = dtrCat;
catlist.DataBind();
}
```

