

##Websocket
  
  Websocket是一种长连接，双通道的网络协议。
  
  Websocket是html5的一部分，通过javascript可以直接使用Websocket。

####.NET实现

  * WinRt与UWP实现
    * MessageWebSocket 
      * Closed 
      * MessageReceived 
      * ConnectAsync 
      * Control(MessageWebSocketControl) 支持设置代理（ProxyCredential）和验证（ServerCredential）

  `MessageWebSocket 类提供 WebSocket 协议的基于消息的抽象。使用 MessageWebSocket 时，在单个操作中读取或写入整个 WebSocket         消息。与之对比，使用 StreamWebSocket 允许每个读取操作读取消息的各个部分，而不是要求在单个操作读取整个消息。
对于 UTF-8 消息，必须使用 MessageWebSocket。StreamWebSocket 仅支持二进制消息。`


##HTTP


##TCP/IP


###Proxy
