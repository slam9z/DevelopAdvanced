## Content的url参数太长。

Content是一个url,但是不支持`post`请求。

[我传递参数时，参数过长，想用post的方式，不知道layer支持post吗](http://fly.layui.com/jie/2807.html)

没有办法，除非通过ajxa获得post页面内容，作为html放进content中。这个问题我也提过，挺无奈的，作者也许也觉得这不是个问题吧。
但layer弹出页面窗口很多时候只是表单内容，这个表单又不是个完整的页面，如果能通过get方法让人在浏览器访问到，体验很不好，有时候post方法还是挺重要的（虽说页面的显示不建议使用post方式，但是也得看具体情况）。 

