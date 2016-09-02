[How to show git log history for a sub directory of a git repo?](http://stackoverflow.com/questions/16343659/how-to-show-git-log-history-for-a-sub-directory-of-a-git-repo)

1. 只显示指定的folder下的change,支持多个文件夹

git log --  BlueOfficeCommon   BlueOfficeWindows10

2. 通过命令使用Git GUI查看

gitk -- BlueOfficeCommon
gitk -- BlueOfficeWindows10