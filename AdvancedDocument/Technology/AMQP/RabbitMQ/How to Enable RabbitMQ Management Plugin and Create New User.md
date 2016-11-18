[How to Enable RabbitMQ Management Plugin and Create New User](http://www.tuicool.com/articles/n2qUfy)

##Install

```
rabbitmq-plugins enable rabbitmq_management
```

##Website

默认地址

http://localhost:15672/#/

默认账号和密码

```
guest  guest
```


>之前一直出问题是设置了一个变量导致文件enabled_plugins有在一个cmd里两个地址。
>`set RABBITMQ_BASE=c:\Data\RabbitMQ`
>*No Do No Die*