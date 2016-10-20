Insert需要参数话吗?现在还没弄明白

```cs
var insertSql= @"
INSERT INTO User
(Id,name)
VALUES
(@Id,@Name)
"
insertSql
, new SqlParameter("@Id", entity.Id)
, new SqlParameter("@Name", entity.Name)

```

这种形式会保存,说找不到参数。

##AddWithValue

[How to insert SQL parameter's value in C#](http://www.codeproject.com/Questions/640647/How-to-inject-foreign-key-ih-the-Csharp)

Add parameters to your Command Parameters using:
command.Parameters.AddWithValue(...)

Example Given with your query string:
Hide   Copy Code

```cs
string equery = "Insert into leave(UserId,leaveType,fromDate,toDate,numdays,seasonLeave,Reason)values(@UserId,'" + leavetype.Text.ToString().Trim() + "',@seaonleave,'" + reason.Text.ToString().Trim() + "')";
 
SqlConnection connection = new SqlConnection(MyConnectionString);
SqlCommand command = new SqlCommand(equery , connection);
 
// Adding the value to the parameter "UserID"
command.Parameters.AddWithValue("@UserId", txtUserID.Text); 
 
// Add all your parameters this way ... 
```