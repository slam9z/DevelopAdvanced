[object-relational mapping](http://stackoverflow.com/questions/3923611/object-relational-mapping)

## question

I need to store an instance of an complex datatype into a relational database. Is there a way to do this without moddeling the database structure first, like it is done in ado.net? The database (or table) structure should be created from the class structure. The class has some properties like ints, strings or bools but could also have more complex ones. I am grateful for every helping advice...

I want to do this in c#...

Update:

Thank you for your replys. I tried "code first" of EF 4 (Thank you very much Ramesh) and got my objects into the database. But now I am having a problem to get the data out of the db back to the instance of an object. My classes look like that:


```cs
class Foo {
    public int id { get; set; }
    public string Woot { get; set; } 
}

class Bar {
    public int id { get; set; }
    public string name { get; set; }
    public ICollection<Foo> FooList { get; set; }
}

```

So, as I said i can create instances of these classes and write them to the db. But when I try to create new instances from the db data, just the int and the string of the "Bar"type is recovered but the Collection of Foos is emtpy. My db context looks like this:

```cs
class DBstructure: DbContext
{
    public DbSet<Foo> Foos { get; set; }
    public DbSet<Bar> Bars { get; set; }
}
```

any Idea?
c# .net orm mapping



## answer1

Yes thats possible, you have to use Entity framework 4 for this. the approach is called "Code First".

you can read lot about this here in ScottGu's post Code-First Development with Entity Framework 4
shareimprove this answer

	

Subsonic is an open source project that will provide this functionality. If you want to do it yourself, there is a nice post here StackOverflow! that uses reflection to generate the SQL necessary to create the table structure you need.

edited to add: Doh! of course, EF 4 supports this as well <= palms face
shareimprove this answer
	
## answer2
	

The best is to use an ORM tool that can build the database for you from your domain model (classes).

Look at NHibernate, Entity Framework to mention a few.
shareimprove this answer