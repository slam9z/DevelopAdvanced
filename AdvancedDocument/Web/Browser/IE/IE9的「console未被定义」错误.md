[IE9的「console未被定义」错误](http://www.iefans.net/ie9-console-weibei-dingyi-cuowu/)

```
搜索
ie 9   undefined
```

一直以来，有个奇怪胡现象缠着我挥之不去，console.log常因不明原因在IE9出现SCRIPT5009: 'console' is undefined (console未被定义) 错误！ 我当然知道IE从IE8+才支持console物件，但如上图所示，网页明明是IE9标准模式，为什么IE9却说console物件不存在? 但进行侦错，console.log()却又正常！ IE9的「console未被定义」错误 想了好久，今天才解开谜团: IE8/IE9要先按F12开启IE Dev Tools才能存取console物件啦！笨蛋！ 参考来源:

```
http://msdn.microsoft.com/zh-cn/library/ie/gg589530%28v=vs.85%29.aspx
```

    使用控制台对象，以将消息从代码发送到控制台。 测试代码时使用控制台而不使用 "window.alert()"，这样不会太明显，因而不会通过模式对话框停止执行。此对象提供大量表单，以便在需要时能够区分信息消息和错误消息。使用控制台对象时，请确保打开 F12 工具。为了避免执行不必要的代码，请使用以下功能测试：

所以，如果使用环境包含IE8/9，请养成良好习惯，用if (window.console) { ... }包住console.log()动作，切忌把IE8/9想成Chrome/Firefox，以为永远有window.console可用！ PS: 终于，IE10改邪归正向Chrome/Firefox看齐，console不再像段誉的六脉神剑时有时无。但只要IE8/9还在一天，console检查还是不能少。 via：黑暗执行绪 