[javascript RegExp ](http://www.cnblogs.com/pengjielee/p/4441054.html)

ECMAScript通过RegExp类型来支持正则表达式。
创建正则表达式
一、使用字面量，创建正则表达式语法：
var expression=/pattern/flags；
每个正则表达式都可带有一个或多个标志（falgs），用以标明正则表达式的行为。
正则表达式的匹配模式支持下列3个标志：
(1) g：表示全局模式； 
(2)  i：表示不区分大小写； 
(3) m：表示多行模式；
ex:
var pattern1 = /at/g; //匹配字符串中所有"at"的实例
var pattern2 = /[bc]at/i; //匹配第一个"bat"或"cat"，不区分大小写
var pattern3 = /.at/gi; //匹配所有以"at"结尾的3个字符的组合，不区分大小写
正则表达式模式中使用的所有元字符必须转义。
正则表达式中的元字符包括：(  ) [  ] {  }  \  |  ^  $  ?  *  +  .
ex:
var pattern1 = /[bc]at/i; //匹配第一个"bat"或"cat"，不区分大小写
var pattern2 = /\[bc\]at/i; //匹配第一个"[bc]at"，不区分大小写
var pattern3 = /.at/gi; //匹配所有以"at"结尾的3个字符的组合，不区分大小写
var pattern4 = /\.at/gi; //匹配所有".at"，不区分大小写
二、使用RegExp构造函数，创建正则表达式语法：
var pattern=new RegExp(param1,param2)；
参数param1：要匹配的字符串模式； 
参数param2：可选的标志字符串；
ex:
var pattern1 = /[bc]at/i; //匹配第一个"bat"或"cat"，不区分大小写
var pattern2 = new RegExp("[bc]at", "i"); //与pattern1相同，只不过是使用构造函数创建的
 
注意：传递给RegExp构造函数的两个参数都是字符串； 
使用正则表达式字面量和使用RegExp构造函数创建的正则表达式不一样。
在ECMAScript 3中，正则表达式字面量始终会共享同一个RegExp实例，而使用构造函数创建的每一个新RegExp实例都是一个新实例。
ECMAScript 5明确规定，使用正则表达式字面量必须像直接调用RegExp构造函数一样，每次都创建新的RegExp实例。IE9+、Firefox 4+和Chrome都据此做出了修改。
RegExp实例属性
RegExp的每个实例都具有下列属性，通过这些属性可以取得有关模式的各种信息。
(1) global：布尔值，表示是否设置了 g 标志； 
(2) ignoreCase：布尔值，表示是否设置了 i 标志； 
(3) lastIndex：整数，表示开始搜索下一个匹配项的字符位置，从0算起；
(4) multiline：布尔值，表示是否设置了 m 标志； 
(5) source：正则表达式的字符串表示； 
通过这些属性可以获知一个正则表达式的各方面信息，但却没有多大用处，因为这些信息全都包含在模式声明中。
var pattern1 = /\[bc\]at/i;
 
alert(pattern1.global); //false
alert(pattern1.ignoreCase); //true
alert(pattern1.multiline); //false
alert(pattern1.lastIndex); //0
alert(pattern1.source); //"\[bt\]at"
 
var pattern2 = new RegExp("\\[bc\\]at", "i");
alert(pattern2.global); //false
alert(pattern2.ignoreCase); //true
alert(pattern2.multiline); //false
alert(pattern2.lastIndex); //0
alert(pattern2.source); //"\[bt\]at"
 
注意：尽管第一个模式使用的是字面量，第二个模式使用了RegExp构造函数，但它们的source属性是相同的。可见，source属性保存的是规范形式的字符串，即字面量形式所用的字符串。
RegExp实例方法
第一个方法exec()：
RegExp对象的主要方法是exec()，该方法是专门为捕获组而设计的。
exec(strInput)；
输入：
接受一个参数，即要应用模式的字符串；
输出：
返回包含第一个匹配项信息的数组；
没有匹配项的情况下返回null；
返回数组：
返回的数组虽然是Array的实例，但包含两个额外的属性：index和input。其中，index表示匹配项在字符串中的位置，而input表示应用正则表达式的字符串。
在数组中，第一项是与整个模式匹配的字符串，其他项是与模式中的捕获组匹配的字符串（如果模式中没有捕获组，则该数组只包含一项）。
var text = "mom and dad and baby";
var pattern = /mom (and dad (and baby)?)?/gi;
var matches = pattern.exec(text);
alert(matches.index); //0
alert(matches.input); //"mom and dad and baby"
alert(matches[0]); //"mom and dad and baby"
alert(matches[1]); //" and dad and baby"
alert(matches[2]); //" and baby"
对于exec()方法而言，即使在模式中设置了全局标志（g），它每次也只会返回一个匹配项。
在不设置全局标志的情况下，在同一个字符串上多次调用exec()将始终返回第一个匹配项的信息。
而在设置全局标志的情况下，每次调用exec()则都会在字符串中继续查找新匹配项。
var text = "cat, bat, sat, fat";
var pattern1 = /.at/;
 
var matches = pattern1.exec(text);
alert(matches.index); //0
alert(matches[0]); //cat
alert(pattern1.lastIndex); //0
 
matches = pattern1.exec(text);
alert(matches.index); //0
alert(matches[0]); //cat
alert(pattern1.lastIndex); //0
 
 
 
var pattern2 = /.at/g;
 
var matches = pattern2.exec(text);
alert(matches.index); //0
alert(matches[0]); //cat
alert(pattern2.lastIndex); //3
 
matches = pattern2.exec(text);
alert(matches.index); //5
alert(matches[0]); //bat
alert(pattern2.lastIndex); //8
在全局匹配模式下，lastIndex的值在每次调用exec()后都会增加，而在非全局模式下则始终保持不变。
注意：IE 的JavaScript 实现在lastIndex 属性上存在偏差，即使在非全局模式下，lastIndex属性每次也会变化。
第二个方法test()：
输入：接受一个字符串参数；
输出：在模式与该参数匹配的情况下返回true；否则，返回false；
在只想知道目标字符串与某个模式是否匹配，但不需要知道其文本内容的情况下，使用这个方法非常方便。因此，test()方法经常被用在if语句中。
var text = "000-00-0000";
var pattern = /\d{3}-\d{2}-\d{4}/;
 
if (pattern.test(text)) {
alert("matched");
}
其他方法：
RegExp实例继承的toLocaleString()和toString()方法都会返回正则表达式的字面量，与创建正则表达式的方式无关。
正则表达式的valueOf()方法返回正则表达式本身。
RegExp构造函数属性
RegExp构造函数包含一些属性（这些属性在其他语言中被看成是静态属性）。这些属性适用于作用域中的所有正则表达式，并且基于所执行的最近一次正则表达式操作而变化。
关于这些属性的另一个独特之处，就是可以通过两种方式访问它们。换句话说，这些属性分别有一个长属性名和一个短属性名（Opera是例外，它不支持短属性名）。
使用这些属性可以从exec()或test()执行的操作中提取出更具体的信息。
RegExp构造函数的各个属性返回了下列值：
(1) input属性返回了原始字符串；
(2) leftContext属性返回了单词short之前的字符串，而rightContext属性则返回了short之后的字符串；
(3) lastMatch属性返回最近一次与整个正则表达式匹配的字符串，即short；
(4) lastParen属性返回最近一次匹配的捕获组，即例子中的s。
还有多达9个用于存储捕获组的构造函数属性。访问这些属性的语法是RegExp.
1、RegExp.
1、RegExp.
2…RegExp.$9，分别用于存储第一、第二……第九个匹配的捕获组。在调用exec()或test()方法时，这些属性会被自动填充。
模式的局限性
ECMAScript正则表达式不支持的特性：
 匹配字符串开始和结尾的\A和\Z锚①
 向后查找（lookbehind）②
 并集和交集类
 原子组（atomic grouping）
 Unicode支持（单个字符除外，如\uFFFF）
 命名的捕获组③
 s（single，单行）和x（free-spacing，无间隔）匹配模式
 条件匹配
 正则表达式注释
① 但支持以插入符号（^）和美元符号（$）来匹配字符串的开始和结尾。
② 但完全支持向前查找（lookahead）。
③ 但支持编号的捕获组。
即使存在这些限制，ECMAScript正则表达式仍然是非常强大的，能够帮我们完成绝大多数模式匹
配任务。