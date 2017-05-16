[javascript的Foreach语法](http://www.cnblogs.com/Fskjb/archive/2011/03/26/1996165.html)


先，虽然叫Foreach语法但关键字还是用for哦，这个语法只是对平时用开的for语法的一个简化而已。

 

这个语法平时最多还是用来遍历数组，这时候每一个循环得到的是数组的索引(一个整形数字),然后通过数组名[整形索引]获得数组中的对象。

 

但是这个语法还可以用来遍历对象，拿到的是对象的属性名称(一个字符串).然后通过对象名[属性名称]就可以拿到对象。

 

所以理解该语法的关键是理解每次循环得到的到底是什么。

其实，这一功能的实现得益于javascript的数组索引可以是字符串，如果不能（想想java）就没有这出戏唱了。

```html
<html>
<heap>
<script type="text/javascript">
    var mycolors = new Array('blue','red','yellow');
    function f1(){        
        var content="";
        for(var key in mycolors){
            content += key+": "+mycolors[key]+"<br/>";
        }    
        document.getElementById("content").innerHTML = content;
    }
    
    function User(){}
    
    function f2(){                
        var u1=new User();
        u1.uname="张三";
        u1.age="18";
        
        var content="";
        for(var key in u1){
            content += key+": "+u1[key]+"<br/>";
        }    
        document.getElementById("content").innerHTML = content;
    }
</script>
</heap>
<body>
<input type="button" id="c1" name="c1" onclick="f1();" value="click one"/>    
<input type="button" id="c2" name="c2" onclick="f2();" value="click two"/>    
<div id="content"></div>
</body>
</html>
```

点击click one后输出：

```
0: blue
1: red
2: yellow
```
 

看到最后聪明的你现在应该知道如何遍历一个JSON对象了吧，呵呵！！ 