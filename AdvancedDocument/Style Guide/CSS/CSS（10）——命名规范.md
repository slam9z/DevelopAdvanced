[CSS（10）——命名规范 ](http://blog.csdn.net/dananhai381/article/details/6785957)


任何代码编程都有各自特点的常用命名规范，div+css页面设计也不例外。遵守常用的css命名规范有利于代码的升级和扩展，
也有利于让别人读懂你的css代码，让你的页面显得清晰有条理。
 
##css文件名命名规范
 
* 模块：module.css 
* 基本共用：base.css 
* 布局、版面：layout.css 
* 主题：themes.css 
* 专栏：columns.css 
* 文字：font.css 
* 表单：forms.css 
* 补丁：mend.css 
* 打印：print.css 

##页面结构元素div命名规范
 
* 头：header　　 
* 内容：content/container　　 
* 尾：footer　　 
* 导航：nav　　 
* 侧栏：sidebar 
* 栏目：column　　 
* 页面外围控制整体布局宽度：wrapper　　 
* 左右中：left right center　　 
* 登录条：loginbar　　 
* 标志：logo　　 
* 广告：banner　　 
* 页面主体：main　　 
* 热点：hot　　 
* 新闻：news 
* 下载：download　　 
* 子导航：subnav　　 
* 菜单：menu　　 
* 子菜单：submenu　　 
* 搜索：search　　 
* 友情链接：friendlink　　 
* 页脚：footer　　 
* 版权：copyright　　 
* 滚动：scroll　　 
* 内容：content 
* 标签页：tab 
* 文章列表：list 
* 提示信息：msg 
* 小技巧：tips 
* 栏目标题：title 
* 加入：joinus 
* 指南：guild 
* 服务：service 
* 注册：regsiter 
* 状态态：status 
* 投票：vote 
* 合作伙伴：partner 


##颜色命名-使用颜色的名称或者16进制代码
 
* .red {color: red;} 
* .f60 {color: #f60;} 
* .ff8600 {color: #ff8600;} 

##字体大小命名-直接使用“font+字体大小”作为名称
 
* .font12px {font-size: 12px;} 
* .font9pt {font-size: 9pt;} 

##对齐样式命名-使用对齐目标的英文名称
 
* .left {float:left;} 
* .bottom {float:bottom;} 


##标题栏样式命名-使用“类别+功能”的方式命名
 
* .barnews { } 
* .barproduct { } 

##注释书写规范
 
1. 行间注释-直接写于属性值后面，如：
    
    ```css
    .search{
    border:1px solid #fff;/*定义搜索输入框边框*/
    background:url(../images/icon.gif) no-report #333;/*定义搜索框的背景*/
    }
    ``
     
2. 整段注释-分别在开始及结束地方加入注释，如：
    
    ```css
    /*=====搜索条=====*/
    .search {
    border:1px solid #fff;
    background:url(../images/icon.gif) no-repeat #333;
    }
    /*=====搜索条结束=====*/
    ```

以上是我们整理的关于div+css页面设计中常用的命名规范，大家在日常的页面设计中要逐步养成规范命名的好习惯。