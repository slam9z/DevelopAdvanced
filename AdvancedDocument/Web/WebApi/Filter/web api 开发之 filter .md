[web api 开发之 filter ](http://www.cnblogs.com/wymlvjing/p/3488275.html)

##1、使用filter之前应该知道的（不知道也无所谓，哈哈！）

  谈到filter 不得不先了解下aop（Aspect Oriented Programming）面向切面的编程。（度娘上关于aop一大堆我就不在这废话了）


##2、实际使用中遇到的问题

###1）问题一  filter触发不了

   写了一个filter的例子，继承actionfilterattribute，死活触发不了!呵呵，搞了半天后来才搞明白，filter 继承了mvc4的。

   原来webapi 在system.web.http命名空间下，mvc在System.web.mvc下，两个空间都有filter，不知道怎么搞得，继承mvc的了，呵呵！
