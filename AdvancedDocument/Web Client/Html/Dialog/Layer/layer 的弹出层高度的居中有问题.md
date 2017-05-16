[layer 的弹出层高度的居中有问题](http://fly.layui.com/jie/107.html)

我在使用 layer 2.2 的版本时也发现该问题。

发现作者在获取浏览器高度的时候采用的是 $(window).height() 函数进行获取，小弟不才，请问是否考虑将 $(window).height() 更换为 document.body.clientHeight 获取高度更为合适？
自行测试发现在 IE、chrome 和 360 浏览器下使用 document.body.clientHeight 效果会更好，而且垂直居中也是相当准确。若有冒犯请见谅，有什么不恰当的位置请各路大神指点 




[layer.open弹出层有时不能居屏幕中间，是什么原因](http://fly.layui.com/jie/1240.html)

@empty_back 代码头中加入以下代码即可

```html
<!doctype html> 
```