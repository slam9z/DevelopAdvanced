[asp.net学习之Repeater控件](http://www.cnblogs.com/hsapphire/archive/2010/09/30/1839363.html)

##3. Repeater控件的事件处理

Repeater控件有以下事件： 
* DataBinding : Repeater控件绑定到数据源时触发 
* ItemCommand : Repeater控件中的子控件触发事件时触发 
* ItemCreated : 创建Repeater每个项目时触发  
* ItemDataBound : Repeater控件的每个项目绑定数据时触发 

例3：使用Repeater控件的事件支持编辑、更新、删除 

```cs
<script runat=”server”> 
// The name of the primary key column 
    string DataKeyName = "Id"; 
/// 把每个列的ID存储在ViewState["Keys"]对象中，ViewState["Keys"]是一个HashTable对象。 
    Hashtable Keys 
    { 
        get 
        { 
if (ViewState["Keys"] == null) 
                ViewState["Keys"] = new Hashtable(); 
return (Hashtable)ViewState["Keys"]; 
        } 
    } 
/// Repeater控件绑定到数据源时触发 
/// 每次更新，删除，增加后，都会触发这个事件，Keys中的值都会被清除。 
/// 在ItemDataBound事件发生时，被新的值填充 
    protected void rptMovies_DataBinding(object sender, EventArgs e) 
    { 
        Keys.Clear(); 
    } 
/// 每个项目绑定数据时触发 
    protected void rptMovies_ItemDataBound(object sender, RepeaterItemEventArgs e) 
    { 
// 如果是数据列，把ID列取出来，加入到ViewState["Keys"]中 
// DataBinder.Eval是在运行时计算数据绑定表达式。这样的用法要记住. 
if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType ==ListItemType.AlternatingItem) 
        { 
            Keys.Add(e.Item.ItemIndex, DataBinder.Eval(e.Item.DataItem, "Id")); 
        } 
    } 
/// 当点击Update,Insert,Delete按钮时触发 
    protected void rptMovies_ItemCommand(object source, RepeaterCommandEventArgs e) 
    { 
switch (e.CommandName) 
        { 
case "Update": 
                UpdateMovie(e); 
break; 
case "Insert": 
                InsertMovie(e); 
break; 
case "Delete": 
                DeleteMovie(e); 
break; 
        } 
    } 
/// Update a movie record 
    protected void UpdateMovie(RepeaterCommandEventArgs e) 
    { 
// 从一个数据项中获得相应的控件 
        TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle"); 
        TextBox txtDirector = (TextBox)e.Item.FindControl("txtDirector"); 
        CheckBox chkInTheaters = (CheckBox)e.Item.FindControl("chkInTheaters"); 
// 填充sqlDataSource的UpdateParameters值 
        srcMovies.UpdateParameters["Id"].DefaultValue = 
        Keys[e.Item.ItemIndex].ToString(); 
        srcMovies.UpdateParameters["Title"].DefaultValue = txtTitle.Text; 
        srcMovies.UpdateParameters["Director"].DefaultValue = txtDirector.Text; 
        srcMovies.UpdateParameters["InTheaters"].DefaultValue = 
        chkInTheaters.Checked.ToString(); 
// 进行Update 
        srcMovies.Update(); 
    } 
/// Insert a movie record 
    protected void InsertMovie(RepeaterCommandEventArgs e) 
    { 
// 从一个数据项中获得相应的控件 
        TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle"); 
        TextBox txtDirector = (TextBox)e.Item.FindControl("txtDirector"); 
        CheckBox chkInTheaters = (CheckBox)e.Item.FindControl("chkInTheaters"); 
// 填充sqlDataSource的InsertParameters值 
        srcMovies.InsertParameters["Title"].DefaultValue = txtTitle.Text; 
        srcMovies.InsertParameters["Director"].DefaultValue = txtDirector.Text; 
        srcMovies.InsertParameters["InTheaters"].DefaultValue = 
        chkInTheaters.Checked.ToString(); 
// Fire the InsertCommand 
        srcMovies.Insert(); 
    } 
/// Delete a movie record 
    protected void DeleteMovie(RepeaterCommandEventArgs e) 
    { 
// 设置sqlDataSource的DeleteParameters值 
        srcMovies.DeleteParameters["Id"].DefaultValue =  Keys[e.Item.ItemIndex].ToString(); 
// Fire the DeleteCommand 
        srcMovies.Delete(); 
    } 
</script>

```
=== 前台页面 ===

```html
<asp:Repeater id="rptMovies" DataSourceID="srcMovies" Runat="server" 
      OnItemCommand="rptMovies_ItemCommand" OnItemDataBound="rptMovies_ItemDataBound" OnDataBinding="rptMovies_DataBinding"> 
<HeaderTemplate> 
<table class="movies"> 
<tr> <th>Title</th><th>Director</th><th>In Theaters</th> </tr> 
</HeaderTemplate> 
<ItemTemplate> 
<tr> 
<td><asp:TextBox id="txtTitle" Text='<%#Eval("Title")%>' Runat="server" /></td> 
<td><asp:TextBox id="txtDirector" Text='<%#Eval("Director")%>' Runat="server" /></td> 
<td><asp:CheckBox id="chkInTheaters" Checked='<%#Eval("InTheaters")%>'Runat="server" /></td> 
<td><asp:LinkButton id="lnkUpdate" CommandName="Update" Text="Update" Runat="server" /> 
&nbsp;|&nbsp;<asp:LinkButton id="lnkDelete" CommandName="Delete" Text="Delete" 
                                  OnClientClick="return confirm(‘Are you sure?');" Runat="server" /></td> 
</tr> 
</ItemTemplate> 
<FooterTemplate> 
<tr> 
<td><asp:TextBox id="txtTitle" Runat="server" /></td> 
<td><asp:TextBox id="txtDirector" Runat="server" /></td> 
<td><asp:CheckBox id="chkInTheaters" Runat="server" /></td> 
<td><asp:LinkButton id="lnkInsert" CommandName="Insert" Text="Insert" Runat="server" /></td> 
</tr> 
</table> 
</FooterTemplate> 
</asp:Repeater> 
<asp:SqlDataSource id="srcMovies" ConnectionString="<%$ ConnectionStrings:Movies %>" 
    SelectCommand="SELECT Id,Title,Director,InTheaters FROM Movies" 
    UpdateCommand="UPDATE Movies SET Title=@Title,Director=@Director,InTheaters=@InTheaters WHERE Id=@Id" 
    InsertCommand="INSERT Movies(Title,Director,InTheaters) VALUES(@Title,@Director,@InTheaters)" 
    DeleteCommand="DELETE Movies WHERE Id=@Id" Runat="server"> 
<UpdateParameters> 
<asp:Parameter Name="Id" /> 
<asp:Parameter Name="Title" /> 
<asp:Parameter Name="Director" /> 
<asp:Parameter Name="InTheaters" /> 
</UpdateParameters> 
<InsertParameters> 
<asp:Parameter Name="Title" /> 
<asp:Parameter Name="Director" /> 
<asp:Parameter Name="InTheaters" /> 
</InsertParameters> 
<DeleteParameters> 
<asp:Parameter Name="Id" /> 
</DeleteParameters> 
</asp:SqlDataSource>
```