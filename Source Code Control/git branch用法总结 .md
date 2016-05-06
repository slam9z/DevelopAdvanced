[git branch用法总结 ](http://blog.csdn.net/xiruanliuwei/article/details/6919319)

##git branch

* 不带参数，列出本地的branch。 -r列出远程的，-a列出所有

* git branch  newbranchname
    创建从当前的branch创建新的branch

* git branch  sourcebranchname  newbranchname 

* git branch -m | -M oldbranch newbranch 重命名分支，如果newbranch名字分支已经存在，则需要使用-M强制重命名，否则，使用-m进行重命名。

* git branch -d | -D branchname 删除branchname分支

* git branch -d -r branchname 删除远程branchname分支

##git checkout branchname

却换当前的branch