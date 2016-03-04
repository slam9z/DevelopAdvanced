

##Websocket
  
  Websocket是一种长连接，双通道的网络协议。
  
  Websocket是html5的一部分，通过javascript可以直接使用Websocket。
  
  Websocket支持代理，需要在Connect之前设置。

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
    


##HTTP


##TCP/IP


###Proxy
