
##Problem

因为History过多，导致Repo体积很大。

##Solution

在Clone的时候指定Depth。

```
Git clone --Depth
```

##New Problem

又想获取完整的版本

[Convert shallow clone to full clone](http://stackoverflow.com/questions/6802145/convert-shallow-clone-to-full-clone)


he below command (git version 1.8.3) will convert the shallow clone to regular one

```
git fetch --unshallow
```
