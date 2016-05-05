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

  

##正确的Clone

直接打开git dash

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/TaskForce

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/WpfInfrastructure

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/UWPInfrastructure

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOffice.UWP

git tfs  clone  http://tfs2013app:8080/tfs/DefaultCollection   $/YPWare/Dev/Windows/BlueOffice
