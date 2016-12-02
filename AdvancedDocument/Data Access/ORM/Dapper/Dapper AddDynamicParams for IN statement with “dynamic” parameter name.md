[Dapper AddDynamicParams for IN statement with “dynamic” parameter name](http://stackoverflow.com/questions/12723922/dapper-adddynamicparams-for-in-statement-with-dynamic-parameter-name)


## question

I have simple SQL string like this:
"SELECT * FROM Office WHERE OfficeId IN @Ids"
The thing is that the @Ids name is entered in an editor so it could be whatever, and my problem is that if I want to pass in, say an array of integers, it only works with Dapper if I use:

```cs
var values = new DynamicParameters();
values.AddDynamicParams(new { Ids = new[] { 100, 101 } });
```
But this requires me to KNOW that the parameter name is "Ids" and that's not the case in my scenario.
I can set a "dynamic parameter" in Dapper with a "dynamic" name like this:

```cs
var values = new DynamicParameters();
values.Add("Ids", new[] { 100, 101 });
```
But then Dapper doesn't construct the "IN (....)" SQL with separate parameters for each value.
Is there a way to construct the dynamic object passed in to "AddDynamicParams" but setting the member name and value without knowing the name beforehand?

## answer

I have just submitted a fix to the repository that allows any of the following to work correctly:
by object (this worked previously):

```cs
values.AddDynamicParams(new { ids = list });
```

or, by single name:

```cs
values.Add("ids", list);
```

or, as a dictionary:

```cs
var args = new Dictionary<string, object>();
args.Add("ids", list);
values.AddDynamicParams(args);
```
I have not yet deployed to NuGet. Let me know if this is a problem.