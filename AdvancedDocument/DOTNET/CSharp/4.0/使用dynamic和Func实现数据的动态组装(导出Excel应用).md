

```C#
var obj = new { Name = "Joey", Age = 25 };//;
var anonArray = new[] { new { name = "apple", diam = 4 }, new { name = "grape", diam = 1 } };

Dictionary<string, Func<dynamic, object>> columnNameValues = new Dictionary<string, Func<dynamic, object>>();
columnNameValues.Add("序号", o => o - 1);
columnNameValues.Add("项目名称", item => item.ProjectName);
foreach (var item in anonArray)
{
    foreach (var nameValue in columnNameValues)
    {
        if (nameValue.Key == "序号")
        {
            nameValue.Value(1);
        }
        else
        {
            nameValue.Value(item);
        }
    }
}
```