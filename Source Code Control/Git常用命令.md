#Git常用命令

##1.git reset 
git reset --hard <sha1-commit-id>

操作完成提示语句:
HEAD is now at bf28ab9 remove file

会移除commit id之后提交的commit, VS中commit details 的Actions可以简单复制id


move  documents  location


##2.git commit 
修改已经提交的commit
git commit --amend -m "New commit message"

http://stackoverflow.com/questions/179123/edit-an-incorrect-commit-message-in-git

##3. fetch与pull的区别,fetch是获取到远程的变化,但是pull会获取远程的变化并且merge