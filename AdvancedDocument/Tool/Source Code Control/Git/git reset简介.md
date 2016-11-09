[git reset简介 ](http://blog.csdn.net/hudashi/article/details/7664464)



git reset [--hard|soft|mixed|merge|keep] [<commit>或HEAD]：将当前的分支重设（reset）到指定的<commit>或者HEAD
（默认，如果不显示指定commit，默认是HEAD，即最新的一次提交），并且根据[mode]有可能更新index和working directory。
mode的取值可以是hard、soft、mixed、merged、keep。下面来详细说明每种模式的意义和效果。


##A) --hard：重设（reset） index和working directory，自从<commit>以来在working directory中的任何改变都被丢弃，
并把HEAD指向<commit>。 

>*慎用*


B). --soft：index和working directory中的内容不作任何改变，仅仅把HEAD指向<commit>。这个模式的效果是，执行完毕后，
自从<commit>以来的所有改变都会显示在git status的"Changes to be committed"中。 

git reset --soft HEAD~1


C). --mixed：仅reset index，但是不reset working directory。这个模式是默认模式，即当不显示告知git reset模式时，
会使用mixed模式。这个模式的效果是，working directory中文件的修改都会被保留，不会丢弃，但是也不会被标记成"Changes to 
be committed"，但是会打出什么还未被更新的报告。报告如下： 
