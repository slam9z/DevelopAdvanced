[git中二进制文件冲突了怎么办 ](https://segmentfault.com/q/1010000000095771)

##Question

比如psd，图片什么的


##Answer

###1
对于二进制文件的冲突，你肯定不想通过编辑二进制文件来解决冲突，那是不可能完成的事情。
你要做的就是：要么选择对方的修改，要么选择自己的修改。
你可以用git checkout的--theirs或--ours选项。

```
git pull
git checkout --theirs YOUR_BINARY_FILE
// git checkout --ours YOUR_BINARY_FILE
git add YOUR_BINARY_FILE
git commit -m 'merged with the remote repos.'
git push
```

###2
冲突文件剪切到桌面，pull，从桌面剪切回去，commit，push

 
###3
加上-f强制覆盖。
