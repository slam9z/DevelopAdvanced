[Create table with jQuery - append](http://stackoverflow.com/questions/8749236/create-table-with-jquery-append)

##Question



##Answer

下面这个方法真的不错,自己拼字符串完全是没有办法的事情。

$('<table></table>')可以用$('<table/>')替代。

```js
$('#here_table').append( '<tr><td>' + 'result' +  i + '</td></tr>' );
Appends to the div#here_table not the new table. 
There are several approaches:
/* Note that the whole content variable is just a string */
var content = "<table>"
for(i=0; i<3; i++){
    content += '<tr><td>' + 'result ' +  i + '</td></tr>';
}
content += "</table>"

$('#here_table').append(content);
```
But, with the above approach it is less manageable to add styles and do stuff dynamically with <table>.
But how about this one, it does what you expect nearly great:

```js
var table = $('<table></table>').addClass('foo');
for(i=0; i<3; i++){
    var row = $('<tr></tr>').addClass('bar').text('result ' + i);
    table.append(row);
}

$('#here_table').append(table);
Hope this would help.
```