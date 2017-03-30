[textarea 空格回车问题]()


和pre控件一样，会导致一堆回车空格 


```html
 <textarea
    class='RuleItemTextData'
    style='width: 500px; height: 50px; margin-left:-4px'>

    <%=item.RuleItemText%>
</textarea>  
``` 

正确写法

```html
 <textarea
    class='RuleItemTextData'
    style='width: 500px; height: 50px; margin-left:-4px'><%=item.RuleItemText%></textarea>  
``` 