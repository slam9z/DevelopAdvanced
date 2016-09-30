[Create table without header in markdown](http://stackoverflow.com/questions/17536216/create-table-without-header-in-markdown)


##Question
Is it possible to create a table without a header in markdown?

The HTML would look like this:

```html
<table>
<tr>
    <td>Key 1</td>
    <td>Value 1</td>
</tr>
<tr>
    <td>Key 2</td>
    <td>Value 2</td>
</tr>
</table>
```

markdown html-table multimarkdown 

##Answer

Most markdown parsers don't support tables without headers. That means the separation line for headers is mandatory.
Parsers that do not support tables without headers

* multimarkdown
* Maruku: A popuplar implementation in ruby
* byword: "All tables must begin with one or more rows of headers"
* PHP Markdown Extra "second line contains a mandatory separator line between the headers and the content" 
* RDiscount Uses PHP Markdown Extra syntax.
* [Github Flavoured Markdown](https://help.github.com/articles/organizing-information-with-tables/)

Parsers that do supports tables without headers.

* Kramdown: A parser in Ruby
* Text::MultiMarkdown: Perl CPAN module.
* MultiMarkdown: Windows application.
* ParseDown Extra: A parser in PHP.

Pandoc: A document converter for the command line written in Haskell (supports header-less tables via its simple_tables and mutiline_tables extensions)
CSS solution
If you're able to change the CSS of the HTML output you can however leverage the :empty pseudo class to hide an empty header and make it look like there is no header at all.