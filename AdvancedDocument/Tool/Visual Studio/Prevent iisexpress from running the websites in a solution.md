被这个问题困扰好久，电脑不行直接会崩溃！



##Best Answer

不需要改变项目文件很适合多人维护的解决方案


There's easy way to do this: in web project's properties F4 (NOT right click-> properties) 
set "always start when debugging" to false

##Other Answer

In the Property Pages-> Start Options: Set Start Action = "Don't open a page..."; Set Server -> 
Use custom server and leave Base URL blank.

