在Tutorial文件夹下运行cmd。

##添加VsDevCmd环境
```
%comspec% /k ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat""
```

##RabbitMQ服务环境

>在C:\Program Files\RabbitMQ Server\rabbitmq_server-3.6.5\sbin目录运行cmd。

If you want to check on the queue, try using `rabbitmqctl list_queues`.


##Hello World

```
csc "1. Hello World/Send.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/Send.exe"
csc "1. Hello World/Receive.c"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/Receive.exe"

start /d "bin" Send.exe

start /d "bin" Receive.exe
```


##Work queues

```
csc "2. Work queues/NewTask.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/NewTask.exe"
csc "2. Work queues/Worker.cs"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/Worker.exe"


start /d "bin" Worker.exe
start /d "bin" Worker.exe


start /d "bin" NewTask.exe First message.
start /d "bin" NewTask.exe Second message..
start /d "bin" NewTask.exe Third message...
start /d "bin" NewTask.exe Fourth message....
start /d "bin" NewTask.exe Fifth message.....

```

##Publish Subscribe

```
csc "3. Publish Subscribe/EmitLog.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/EmitLog.exe"
csc "3. Publish Subscribe/ReceiveLogs.cs"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/ReceiveLogs.exe"

start /d "bin" ReceiveLogs.exe
start /d "bin" ReceiveLogs.exe > logs_from_rabbit.log

start /d "bin" EmitLog.exe

start /d "bin" EmitLog.exe   405


```


##Routing

```
csc "4. Routing/EmitLogDirect.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/EmitLogDirect.exe"
csc "4. Routing/ReceiveLogsDirect.cs"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/ReceiveLogsDirect.exe"

start /d "bin" ReceiveLogsDirect.exe  info
start /d "bin" ReceiveLogsDirect.exe  error /t >> logs_from_rabbit.log

start /d "bin" EmitLogDirect.exe  info  "login"

start /d "bin" EmitLogDirect.exe error warning   "Run. Run. Or it will explode"


```

##Topics

```
csc "5. Topics/EmitLogTopic.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/EmitLogTopic.exe"
csc "5. Topics/ReceiveLogsTopic.cs"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/ReceiveLogsTopic.exe"

start /d "bin" ReceiveLogsTopic.exe  "#"
start /d "bin" ReceiveLogsTopic.exe  "kern.*"
start /d "bin" ReceiveLogsTopic.exe  "kern.*" "*.critical"


start /d "bin" EmitLogTopic.exe  "kern.critical"  "A critical kernel error"

start /d "bin" EmitLogTopic.exe  "rabbit.critical"  "A critical rabbit error"

```


##RPC

```
csc "6. RPC/RPCClient.cs"   /r:"bin/RabbitMQ.Client.dll"    /out:"bin/RPCClient.exe"
csc "6. RPC/RPCServer.cs"  /r:"bin/RabbitMQ.Client.dll"   /out:"bin/RPCServer.exe"

start /d "bin" RPCServer.exe  
start /d "bin" RPCClient.exe  30

```