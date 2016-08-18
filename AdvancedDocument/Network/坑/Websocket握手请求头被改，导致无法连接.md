
##问题

正常的创建websocket Request Headers（这个是使用客户的账号我登录看到的请求。）


Key Value 

Connection Upgrade 
Upgrade websocket 
Host bocorp.yipinapp.net 
User-Agent websocket-sharp/1.0 
Sec-WebSocket-Key io4ZvlZBrspSUCt7rvJB+A== 
Sec-WebSocket-Version 13 


客户那边创建的 Request Headers


Key Value 

Connection close 
Host bocorp.yipinapp.net 
User-Agent websocket-sharp/1.0 
Sec-WebSocket-Key gZT405xMWD3p6CY09zcejA== 
Sec-WebSocket-Version 13 

刚才看到客户那边有一个正常的请求



##解决方法

    使用https,需要服务端配置，客户端将http换成https。