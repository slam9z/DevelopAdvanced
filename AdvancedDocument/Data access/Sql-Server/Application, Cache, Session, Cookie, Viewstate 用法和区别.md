[Application, Cache, Session, Cookie, Viewstate 用法和区别](http://blog.csdn.net/avon520/article/details/4922887)

Application 任意大小， 整个应用程序的生命周期， 所有用户，服务器端 。
Cache 任意大小，程序指定生命周期，所有用户，服务器端。（比较灵活）
Session 小量数据， 某个用户活动时间 + 延迟时间（默认20分钟）， 单个用户， 服务器端。
Cookie 小量数据， 程序指定生命周期， 单个用户， 客户端 。
ViewState 小量数据，一个web页面的生命期，单个用户，客户端。 

