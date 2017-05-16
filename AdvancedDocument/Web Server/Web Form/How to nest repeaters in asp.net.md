[How to nest repeaters in asp.net](http://stackoverflow.com/questions/6171861/how-to-nest-repeaters-in-asp-net)



On the outer control, use the ItemDataBound event, like this:

```cs
<asp:Repeater ID="rptDiscipline" runat="server"
     OnItemDataBound="rptDiscipline_ItemDataBound">
```

Then, in the code-behind, handle the rptDiscipline_ItemDataBound event and manually bind the inner repeater. The repeater's ItemDataBound event fires once for each item that is repeated. So you'll do something like this:

```cs
protected void rptDiscipline_ItemDataBound(Object Sender, RepeaterItemEventArgs e) 
{
    // To get your data item, cast e.Item.DataItem to 
    // whatever you're using for the data object; for example a DataRow.

    // Get the inner repeater:
    Repeater rptPrograms = (Repeater) e.Item.FindControl("rptPrograms");

    // Set the inner repeater's datasource to whatever it needs to be.
    rptPrograms.DataSource = ...
    rptPrograms.DataMember = ...
    rptPrograms.DataBind();
}
```

EDIT: Updated to match your question's update.

You need to bind the outer repeater to a data source that has only one record per item you want the repeater to render. That means the data source needs to be a collection/list/datatable/etc that has only the disciplines in it. In your case, I would recommend getting a List<string> of disciplines from the DataTable for the inner collection, and bind the outer repeater to that. Then, the inner repeater binds to a subset of the data in the DataTable, using the ItemDataBound event. To get the subset, filter the DataTable through a DataView.

Here's code:

```cs
protected void Page_Load(object sender, EventItems e)
{
    // get your data table
    DataTable table = ...

    if ( !IsPostBack )
    {
        // get a distinct list of disciplines
        List<string> disciplines = new List<string>();
        foreach ( DataRow row in table )
        {
            string discipline = (string) row["Discipline"];
            if ( !disciplines.Contains( discipline ) )
                disciplines.Add( discipline );
        }
        disciplines.Sort();

        // Bind the outer repeater
        rptDiscipline.DataSource = disciplines;
        rptDiscipline.DataBind();
    }
}

protected void rptDiscipline_ItemDataBound(Object Sender, RepeaterItemEventArgs e) 
{
    // To get your data item, cast e.Item.DataItem to 
    // whatever you're using for the data object
    string discipline = (string) e.Item.DataItem;

    // Get the inner repeater:
    Repeater rptPrograms = (Repeater) e.Item.FindControl("rptPrograms");

    // Create a filtered view of the data that shows only 
    // the disciplines needed for this row
    // table is the datatable that was originally bound to the outer repeater
    DataView dv = new DataView( table );  
    dv.RowFilter = String.Format("Discipline = '{0}'", discipline);

    // Set the inner repeater's datasource to whatever it needs to be.
    rptPrograms.DataSource = dv;
    rptPrograms.DataBind();
}   
```

