[解决my97datepicker在ie6下报$lang未定义的问题 ](http://blog.csdn.net/harrymeinan/article/details/8295870)


由于之前并没有在多个浏览器下测试过，所以一直没有发现过问题，但是近日，在一个装有ie6的机器上，居然会报$lang未定义的错误，于是多处寻求原因，未果，无奈只好上官网仔细阅读文档，才发现，是这样的一个原因：

my97datepicker的config.js中有一段这样的配置：

```js
var langList =
[
{name:'en', charset:'UTF-8'},
{name:'zh-cn', charset:'GBK'},
{name:'zh-tw', charset:'GBK'}
]; 
```

我用的语言是zh-cn,对应了lang目录下的zh-cn.js,之前我已经将zh-cn.js的编码转变为utf-8, 却没有将config.js里面的charset设置为utf-8,所以会报错，改为如下即可：
Js代码 复制代码 收藏代码

```js
var langList =
    [
    {name:'en', charset:'UTF-8'},
    {name:'zh-cn', charset:'UTF-8'},
    {name:'zh-tw', charset:'GBK'}
    ]; 
 ```   

再回到ie6下，一切正常，呵呵，看来，阅读官方的文档是最权威有效的办法啊~~~