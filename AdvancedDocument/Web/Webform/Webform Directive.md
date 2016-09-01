##Control 

##Register

TagPrefix是必须的，TagName也是必须的，感觉使用ascx加载资源有点怪。

##Import

添加命名空间
You are looking for the @Import directive.
At the top of the control or page (can be mixed and matched with other directives):

```xml
<%@ Import Namespace="MyNamespace" %>
```

Or (different placing of the @ character):

```xml
<% @Import Namespace="MyNamespace" %>
```