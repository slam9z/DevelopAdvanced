GitTfs可以将TFS的历史装换成本地分支，然后可以容易迁移到Git上。

[github.com git-tfs](https://github.com/git-tfs/git-tfs)


[TFS 迁移到 Git ](http://www.cnblogs.com/sorex/archive/2013/03/11/2954095.html)

##错误的尝试
git tfs  -u=weili -p=wei list-remote-branches http://10.1.1.32/8080/tfs/DefaultCollection 


git tfs clone -u=weili http://10.1.1.32/8080/tfs/DefaultCollection $/TaskForce

Initialized empty Git repository in e:/SoftWare/GitTfs-0.25.0/TaskForce/.git/
Please contact your administrator. There was an error contacting the server.
Technical information (for administrator):
  HTTP code 200: OK


##添加用户名和密码
  -u=weili -p=wei
  

##git bash Clone

###将当前git-tfs添加到当前用户的path属性。

###直接打开git bash

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/TaskForce

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/WpfInfrastructure

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/UWPInfrastructure

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOffice.UWP

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOffice

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/ClientInfrastructure
   
git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/OneDriveServiceCommon
	  
git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOfficeTest

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/WinRTInfrastructure


http://tfs2013app:8080/tfs/WindowsCollection/BlueOffice/_git/BlueOfficePC

http://tfs2013app:8080/tfs/WindowsCollection/BlueOffice/_git/BlueOfficeWP

http://tfs2013app:8080/tfs/WindowsCollection/BlueOffice/_git/BlueOfficeCommon

##直接使用git-tfs.exe


##提交到之前的Git上
先作为一个branch  push到上面  然后在merge就行了


先checkout之前的文件，再merge。

## TFS repository can not be root and must start with "$/".

https://github.com/git-tfs/git-tfs/issues/845

使用git bash出错，在前面加上MSYS_NO_PATHCONV=1

MSYS_NO_PATHCONV=1 git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOfficeTest


