[Actual table Vs. Div table](http://stackoverflow.com/questions/2617895/actual-table-vs-div-table)

##Question


This

```html
<table>
    <tr>
        <td>Hello</td>
        <td>World</td>
    </tr>
</table>
```

Can be done with this:

```html
<div>
    <div style="display: table-row;">
        <div style="display: table-cell;">Hello</div>
        <div style="display: table-cell;">World</div>
    </div>
</div>
```

Now, is there any difference between these two in terms of performance and/or render speed or they're just the same?


###2 

Tables make layout possible, divs make it impossible. Tables make for readable, easy to understand code.
Divs require loads of hackery and tricks not so easy to understand and even then the results are so so
and full of unwanted behaviour. – Anderson Jul 9 '13 at 11:32 

###1 
  
If you're filling this table with DATA? Yea, use tables. Are you using it to LAYOUT your WEBSITE? 
*DON'T USE TABLES*! Div's are for website content/layout, tables are an antiquated element that has a sole 
purpose of laying out numbers/data really nicely. – Mike Feb 3 '14 at 18:10 


##Answers 

###Best

It is semantically incorrect to simulate data tables with divs and in general irrelevant to performance as rendering is 
instant. The bottle neck comes from JavaScript or extremely long pages with a lot of nested elements which usually in the
 old days is 100 nested tables for creating layouts.

Use tables for what they are meant to and div's for what they are meant to. The display table-row and cell properties are
 to utilize div layouts more then creating tables for data representations. Look at them as a layout columns and rows same
 as those you can find in a newspaper or a magazine.

Performance wise you have couple of more bytes with the div example, lol :)

###Other

In first instance, I wouldn't worry about performance, but more about semantics. If it's tabular data, use a <table>. 
If it are just block elements representing a layout element, use <div>.

If you really, really worry about performance, then the answer would still depend on the client used.
 MSIE for example is known to be slow in table rendering. You should at least test yourself in different browsers.
If this worriness is caused by large data, then I'd consider to introduce paging/filtering of the data you're about to show.

