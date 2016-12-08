[UserDefinedField](http://martinfowler.com/bliki/UserDefinedField.html#footnote-non-uniform)




# [UserDefinedField](UserDefinedField.html)



[![](/mf.jpg "Photo of Martin Fowler")](/)

[Martin Fowler](/)

23 July 2013



[database](/tags/database.html)&nbsp;· [application architecture](/tags/application architecture.html)&nbsp;· [API design](/tags/API design.html)

tags:



A common feature in software systems is to allow users to define
  their own fields in data structures. Consider an address book -
  there's a host of things that you might want to add. With new social
  networks popping up every day, users might want to add a new field
  for a Bunglr id to their contacts.

## Memory 

For in-memory purposes, often the best way to do this is to allow
  classes to include a hashmap field for user-defined fields (a
  pattern Kent Beck calls _[Variable State](https://www.amazon.com/gp/product/013476904X?ie=UTF8&amp;tag=martinfowlerc-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=013476904X)![](https://www.assoc-amazon.com/e/ir?t=martinfowlerc-20&amp;l=as2&amp;o=1&amp;a=0321601912)_). 

```ruby
class Contact
  attr_accessor :firstname, :lastname

  def initialize
    @data = {}
  end

  def [] arg
    return @data[arg]
  end
  def []= key, value
    @data[key] = value
  end
end

aCustomer = Contact.new
aCustomer.firstname = "Martin"
aCustomer[:bunglrId] = 'fowl'
```

With a setup like this you can add affordances to your UI to
  allow users to attach new fields to objects. If you want common user
  defined fields, you can use a class variable to keep a list of
  common keys for the hashmap. There is some awkwardness in that
  regular fields are accessed differently to user-defined fields, but
  depending on your language even this can be overcome. If your
  language supports [Dynamic Reception](/dslCatalog/dynamicReception.html) then you
  use this to access the hashmap with regular field access.

class Contact...

```ruby 
 def method_missing(meth, *args)
    if @data.has_key? meth
      return @data[meth]
    else
      super
    end
  end
```

## Storage

Often the trickiest part of this is figuring out how to persist
  this. If you're using a schemaless database, then it's usually
  straightforward - you just add the user-defined keys to your
  application defined ones. The trickiness comes from a database with
  a [storage schema](http://martinfowler.com/articles/schemaless/), particularly a relational database. 

Usually the best option is to use a [**Serialized LOB**](/eaaCatalog/serializedLOB.html),
  essentially creating a large text column into which you store the
  user-defined fields as a JSON or XML document. Many databases these
  days offer pretty nice support for this approach, including support
  for indexing and querying based on the data structure within the
  LOB. However such support, if available, is usually more awkward
  than using fields. [[1]](#footnote-index)

Another route is using some kind of **attribute table**. A table
  might look something like this.

```sql
CREATE TABLE ContactAttributes (
  contactId   INTEGER, 
  attribute   TEXT, 
  value       TEXT, 
  PRIMARY KEY (contactId, attribute))
```

Again, querying and indexing are awkward. Queries can involve a
  good bit of extra joins that can get rather messy.

**Pre-defined custom fields** offer another system. Here you set the
  schema up with fields like `custom_field_1` (and perhaps
  `custom_field_1_name`. You are limited to only the number
  of custom fields per instance that you have pre-defined. As usual
  indexing and querying are awkward.

When using a attribute table or pre-defined custom fields you may
  choose to have different columns for different SQL data types. So
  pre-defined fields might be `integer_1, integer_2,
  text_1…`, or a attribute table might have multiple value
  fields (`text_value, integer_value`).

A **dynamic schema** is an approach that's often overlooked.
  To do this you set things up so that when someone adds a field, you
  use an `alter table` statement to add that field to the
  table. Our [Mingle team](http://www.thoughtworks-studios.com/mingle-agile-project-management) does this and have been
  happy with how it's worked out. [[2]](#footnote-mingle) The new fields can be indexed and
  queried just the same as application-defined fields. This does mean
  all instances get all fields, so is less handy if you get a lot of
  variance between instances. 

Your persistance scheme choices will be affected by what you use
  for relational mapping. User-defined fields aren't the most
  well-trod parts of the relational mapping problem, so there's a lot
  of variation in support from different relational mapping libraries.

User-defined fields are a similar problem to non-uniform types [[3]](#footnote-non-uniform).
  Both problems lead to the need for a more flexible schema, or indeed
  a truly schemaless approach (although remember that [schemaless doesn't
  mean you don't have a schema](http://martinfowler.com/articles/schemaless/)). If you have non-uniform types
  that aren't changing at the users' behest, then one of the
  inheritance oriented patterns may make sense. ([Single Table
  Inheritance](/eaaCatalog/singleTableInheritance.html), [Class Table
  Inheritance](/eaaCatalog/classTableInheritance.html), or [Concrete Table
  Inheritance](/eaaCatalog/concreteTableInheritance.html).)



## Notes



<span class="num">1: </span>
      Bret Taylor [describes
      a scheme](http://backchannel.org/blog/friendfeed-schemaless-mysql) for indexing fields in a such a scheme by building
      separate index tables for each indexable field.





<span class="num">2: </span>
      Mingle's approach is actually a bit more involved than just
      adding fields to an existing table. Mingle's central record type
      is a card (which represents stories, tasks etc). The fields on a
      card vary by project and you can have many projects in the same
      database. So rather than use a single card table, mingle creates
      a new table for each project's card. It then adds fields
      dynamically to this table as users desire.





<span class="num">3: </span>
     Non-uniform types are types where instances use a small and very
     different selection of fields. Sometimes these are called sparse
     tables, because if you look at the whole table each row only uses
     a small number of a large list of columns. The difference between
     non-uniform types and user-defined fields is that non-uniform
     types have all the potential fields known to developers, while
     user-defined fields allow fields to be created that developers
     will never know about.




**Translations: **[Slovenian](http://www.pkwteile.ch/science/userdefinedfield/)


<span class="label">Share:</span>[![](/t_mini-a.png)](https://twitter.com/intent/tweet?url=http://martinfowler.com/bliki/UserDefinedField.html&amp;text=Bliki: UserDefinedField ➙  "Share on Twitter")[![](/fb-icon-20.png)](https://facebook.com/sharer.php?u=http://martinfowler.com/bliki/UserDefinedField.html "Share on Facebook")[![](/gplus-16.png)](https://plus.google.com/share?url=http://martinfowler.com/bliki/UserDefinedField.html "Share on Google Plus")

if you found this article useful, please share it. I appreciate the feedback and encouragement







## Find similar articles at these tags

[database](/tags/database.html) [application architecture](/tags/application architecture.html) [API design](/tags/API design.html)




