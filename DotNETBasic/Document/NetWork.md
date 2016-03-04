

##Websocket

  The WebSocket protocol was standardized by IETF(Internet Engineering Task Force ) as  [RFC 6455](http://tools.ietf.org/html/rfc6455).
  
  RFC(Request For Commits)
  
  A WebSocket connection is established by a HTTP handshake (prefixed by "ws://" or "wss://") between the client and the server. the client sends a request:
```
GET /mychat HTTP/1.1
Host: server.example.com
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Key: x3JJHMbDL1EzLkh9GBhXDw==
Sec-WebSocket-Protocol: chat
Sec-WebSocket-Version: 13
Origin: http://example.com
```
  Then the server sends a response to accept the connection:

```
Hide   Copy Code
HTTP/1.1 101 Switching Protocols
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Accept: HSmrc0sMlYUkAGmm5OPpG2HaGWk=
Sec-WebSocket-Protocol: chat
```
  
  Websocket是一种长连接，双通道的网络协议。
  
  Websocket是html5的一部分，通过javascript可以直接使用Websocket。
  
  Websocket支持代理，需要在Connect之前设置。
  
  [wiki](https://en.wikipedia.org/wiki/WebSocket)

####.NET实现
  
  * WinRt与UWP实现
    * MessageWebSocket 支持UTF-8消息
      * Closed 
      * MessageReceived 
      * ConnectAsync 
      * Control(MessageWebSocketControl) 支持设置代理（ProxyCredential）和验证（ServerCredential）
      * OutputStream  通过[DataWriter](https://msdn.microsoft.com/zh-cn/library/windows/apps/windows.storage.streams.datawriter.aspx)发送信息给服务端

     `MessageWebSocket 类提供 WebSocket 协议的基于消息的抽象。使用 MessageWebSocket 时，在单个操作中读取或写入整个 WebSocket         消息。与之对比，使用 StreamWebSocket 允许每个读取操作读取消息的各个部分，而不是要求在单个操作读取整个消息。
对于 UTF-8 消息，必须使用 MessageWebSocket。StreamWebSocket 仅支持二进制消息。`

      [MessageWebSocket类](https://msdn.microsoft.com/zh-cn/library/windows/apps/windows.networking.sockets.messagewebsocket.aspx)
    
    * StreamWebSocket支持二进制
      * InputStream 读取服务端数据
      
      `StreamWebSocket 类提供基于消息的 WebSocket 协议的基于流的抽象。这对于其中大型文件（例如图片或影片）需要传输的方案很有用。使用 StreamWebSocket 允许消息的部分由每个读取操作读取，而不是要求所有消息在单个操作读取（与 MessageWebSocket）。
StreamWebSocket 仅支持二进制消息。对于 UTF-8 消息，必须使用 MessageWebSocket。`

      [StreamWebSocket类](https://msdn.microsoft.com/zh-cn/library/windows/apps/windows.networking.sockets.streamwebsocket.aspx)
   
  * .NET4.5的支持Server与Client（感觉十分的复杂）
    
    * CodeProject系列文章：
    
      [Overview on WebSocket protocol and .NET support](http://www.codeproject.com/Articles/617611/Using-WebSocket-in-NET-4-5-Part-1)
    
      [Using WebSocket in Traditional ASP.NET and MVC 4](http://www.codeproject.com/Articles/618032/Using-WebSocket-in-NET-4-5-Part-2)
    
      [Using WCF support for WebSocket](http://www.codeproject.com/Articles/619343/Using-WebSocket-in-NET-4-5-Part-3)
    
      [Using Microsoft.WebSockets.dll](http://www.codeproject.com/Articles/620731/Using-WebSocket-in-NET-4-5-Part-4) 
  

##HTTP


##TCP/IP


###Proxy
