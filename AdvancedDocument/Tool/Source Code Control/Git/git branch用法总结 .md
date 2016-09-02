[git branch用法总结 ](http://blog.csdn.net/xiruanliuwei/article/details/6919319)

##git branch

* 不带参数，列出本地的branch。 -r列出远程的，-a列出所有

* git branch  newbranchname
    创建从当前的branch创建新的branch

* git branch  sourcebranchname  newbranchname 

* git branch -m | -M oldbranch newbranch 重命名分支，如果newbranch名字分支已经存在，则需要使用-M强制重命名，否则，使用-m进行重命名。

* git branch -d  branchname 删除branchname分支


* git branch -d -r branchname 删除远程branchname分支


##git checkout branchname

却换当前的branch

##delete remote branch

git branch -d -r branchname 删除远程branchname分支

这个命令不可以用，提示  error: remote branch 'document' not found.

需要使用 git push origin --delete <branchname>


##rename remote branch

这个没什么办法，只能先修改本地的branch名，然后将新分支publish到remote上，再删除原来的remote branch。


##can not see  new remote branch

在cmd里面使用git pull命令。git branch -r可能也不会去拿remote的数据。




