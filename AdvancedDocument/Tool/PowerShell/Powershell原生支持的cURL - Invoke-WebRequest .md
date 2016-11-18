[Powershell原生支持的cURL - Invoke-WebRequest ](http://blog.csdn.net/quicknet/article/details/20286075)

cURL (clients for URL) 是一款常用的命令行工具，它被用于基于URL传输数据，它支持HTTP， HTTPS，FTP等协议。
其实，在Windows平台上，从Powershell 3.0开始也增加了一个类似的命令 Invoke-WebRequest, 执行 Get-Help 
Invoke-WebRequest 会看到下面的帮助信息。注意看一下其中的ALIASES部分，curl赫然在列。也就是说，你可以直接使用curl作为命令名字，呵呵！


       
Invoke-WebRequest的语法与cURL有所不同，但如果会用cURL，转换到使用Invoke-WebRequest非常简单，下面举几个
使用cURL和Invoke-WebRequest操作ElasticSearch的例子 (cURL表示cURL.exe命令，Invoke-WebRequest则是
Powershell中的的实现)：

cURL -XGET 'localhost:9200/library/book/_search'
Invoke-WebRequest http://localhost:9200/library/book/_search -Method GET (GET操也作可以省略)

cURL -XPOST 'localhost:9200/library'
Invoke-WebRequest http://localhost:9200/library -Method POST

cURL -XPUT 'localhost:9200/library/book/_mapping' -d @mapping.json
Invoke-WebRequest http://localhost:9200/library/book/_mapping -Method PUT -InFile mapping.json


cURL -XPOST 'localhost:9200/blog/article' -d '{"title":"ElasticSearch"}'
Invoke-WebRequesthttp://localhost:9200/blog/article -Method POST -Body '{"title":"ElasticSearch"}'



cURL -XGET 'localhost:9200/library/book/_search?q=title:crime&pretty=true'
Invoke-WebRequesthttp://localhost:9200/library/book/_search?q=title:ElasticSearch"&"pretty=true 
-OutFile response.txt

cURL -XPOST 'localhost:9200/library/book/_search?pretty=true' -d '{"query":{"term":{"title":"wp8.1"}}}'
Invoke-WebRequesthttp://localhost:9200/library/book/_search?pretty=true -Method POST -Body'{"query":{"term":{"title":"wp8.1"}}}'

cURL -XPOST 'localhost:9200/library/book/_search?pretty=true' -d'{"query":{"terms":{"tags":["surface", "wp8.1"],"minimum_match":2}}}'
Invoke-WebRequesthttp://localhost:9200/library/book/_search?pretty=true -Method POST -Body '{"query":{"terms":{"tags":["surface", "wp8.1"],"minimum_match":2}}}'

curl -XPUT localhost:9200/_cluster/settings -d '{"transient":{"cluster.routing.allocation.enable": none}}'
Invoke-RestMethodhttp://localhost:9200/_cluster/settings -Method PUT -Body '{"transient":{"cluster.routing.allocation.enable": "none"}}'


Invoke-RestMethod http://localhost:9200/_cluster/settings -Method PUT -Body '{"transient":{"cluster.routing.allocation.cluster_concurrent_rebalance": "6"}}'



除了Invoke-WebRequest，Powershell 3.0起还提供了Invoke-RestMethod 命令，它专门用于向RESTful
web服务发送HTTP和HTTPS数据。两个命令很相似，但也有不同，具体的不同可以参见《Widnows PowerShell》一书。