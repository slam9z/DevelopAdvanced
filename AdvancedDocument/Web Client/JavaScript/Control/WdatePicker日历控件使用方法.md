﻿[WdatePicker日历控件使用方法 ](http://www.cnblogs.com/yuhanzhong/archive/2011/08/10/2133276.html)


##1. 跨无限级框架显示 
 
无论你把日期控件放在哪里,你都不需要担心会被外层的iframe所遮挡进而影响客户体验,因为My97日期控件是可以跨无限级框架显示的
 
示例2-7 跨无限级框架演示
可无限跨越框架iframe,无论怎么嵌套框架都不必担心了,即使有滚动条也不怕
 
##2. 民国年日历和其他特殊日历 
 
当年份格式设置为yyy格式时,利用年份差量属性yearOffset(默认值1911民国元年),可实现民国年日历和其他特殊日历
示例2-8 民国年演示

```html
<input type="text" id="d28" onClick="WdatePicker({dateFmt:'yyy/MM/dd'})"/>
```

注意:年份格式设置成yyy时,真正的日期将会减去一个差量yearOffset(默认值为:1911),如果是民国年使用默认值即可无需另外配置,如果是其他的差量,可以通过参数的形式配置
 
##3. 为编程带来方便 
如果el的值是this,可省略,即所有的el:this都可以不写 
日期框设置为disabled时,禁止更改日期(不弹出选择框) 
如果没有定义onpicked事件,自动触发文本框的onchange事件 
如果没有定义oncleared事件,清空时,自动触发onchange事件
 
##4. 其他属性 
设置readOnly属性,可指定日期框是否只读 
设置highLineWeekDay属性,可指定是否高亮周末 
设置isShowOthers属性,可指定是否显示其他月的日期 
加上class="Wdate"就会在选择框右边出现日期图标
 
 
##5.多语言和自定义皮肤

###1. 多语言支持 

通过lang属性,可以为每个日期控件单独配置语言,当然也可以通过WdatePicker.js配置全局的语言
语言列表和语言安装说明详见语言配置
示例3-1 多语言示例
繁体中文: 

```html
<input id="d311" class="Wdate" type="text" onFocus="WdatePicker({lang:'zh-tw'})"/>
```

英文: 

```html
<input id="d312" class="Wdate" type="text" onFocus="WdatePicker({lang:'en'})"/>
```

简体中文: 

```html
<input id="d313" class="Wdate" type="text" onFocus="WdatePicker({lang:'zh-cn'})"/>
```
注意:默认情况lang='auto',即根据浏览器的语言自动选择语言.
 
###2. 自定义和动态切换皮肤 
通过skin属性,可以为每个日期控件单独配置皮肤,当然也可以通过WdatePicker.js配置全局的皮肤
皮肤列表和皮肤安装说明详见皮肤配置
 
示例3-2 皮肤演示
默认皮肤default: skin:'default'

```html
<input id="d321" class="Wdate" type="text" onfocus="WdatePicker()"/>
```

注意:在WdatePicker里配置了skin='default',所以此处可省略,同理,如果你把WdatePicker里的skin配置成'whyGreen'那么在不指定皮肤的情况下都使用'whyGreen'皮肤了
 

       whyGreen皮肤: skin:'whyGreen' 

```html
<input id="d322" class="Wdate" type="text" onfocus="WdatePicker({skin:'whyGreen'})"/>
```
 
 
##6. 日期范围限制

###1. 静态限制

注意:日期格式必须与 realDateFmt 和 realTimeFmt 一致 
你可以给通过配置minDate(最小日期),maxDate(最大日期)为静态日期值,来限定日期的范围
示例4-1-1 限制日期的范围是 2006-09-10到2008-12-20
<input id="d411" class="Wdate" type="text" onfocus="WdatePicker({skin:'whyGreen',minDate:'2006-09-10',maxDate:'2008-12-20'})"/>
 
示例4-1-2 限制日期的范围是 2008-3-8 11:30:00 到 2008-3-10 20:59:30
<input type="text" class="Wdate" id="d412" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'2008-03-08 11:30:00',maxDate:'2008-03-10 20:59:30'})" value="2008-03-09 11:00:00"/>
 
示例4-1-3 限制日期的范围是 2008年2月 到 2008年10月
<input type="text" class="Wdate" id="d413" onfocus="WdatePicker({dateFmt:'yyyy年M月',minDate:'2008-2',maxDate:'2008-10'})"/>
 
示例4-1-4 限制日期的范围是 8:00:00 到 11:30:00
<input type="text" class="Wdate" id="d414" onfocus="WdatePicker({dateFmt:'H:mm:ss',minDate:'8:00:00',maxDate:'11:30:00'})"/>
 
###2. 动态限制
注意:日期格式必须与 realDateFmt 和 realTimeFmt 一致 
你可以通过系统给出的动态变量,如%y(当前年),%M(当前月)等来限度日期范围,你
还可以通过#{}进行表达式运算,如:#{%d+1}:表示明天

动态变量表
格式
说明
%y
当前年
%M
当前月
%d
当前日
%ld
本月最后一天
%H
当前时
%m
当前分
%s
当前秒
 #{}
运算表达式,如:#{%d+1}:表示明天
 #F{}
{}之间是函数可写自定义JS代码
 
示例4-2-1 只能选择今天以前的日期(包括今天)
<input id="d421" class="Wdate" type="text" onfocus="WdatePicker({skin:'whyGreen',maxDate:'%y-%M-%d'})"/>
 
示例4-2-2 使用了运算表达式只能选择今天以后的日期(不包括今天)
<input id="d422" class="Wdate" type="text" onfocus="WdatePicker({minDate:'%y-%M-#{%d+1}'})"/>
 
示例4-2-3 只能选择本月的日期1号至本月最后一天
<input id="d423" class="Wdate" type="text" onfocus="WdatePicker({minDate:'%y-%M-01',maxDate:'%y-%M-%ld'})"/>
 
示例4-2-4 只能选择今天7:00:00至明天21:00:00的日期
<input id="d424" class="Wdate" type="text" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss',minDate:'%y-%M-%d 7:00:00',maxDate:'%y-%M-#{%d+1} 21:00:00'})"/>
 
      示例4-2-5 使用了运算表达式只能选择 20小时前至 30小时后的日
期
<input id="d425" class="Wdate" type="text" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',minDate:'%y-%M-%d #{%H-20}:%m:%s',maxDate:'%y-%M-%d #{%H+30}:%m:%s'})"/>
 
###3 . 脚本自定义限制
       注意:日期格式必须与 realDateFmt 和 realTimeFmt 一致 
 
系统提供了$dp.$D和$dp.$DV这两个API来辅助你进行日期运算,此外你还可以通过在 #F{} 中填入你自定义的脚本,做任何你想做的日期限制
 
示例4-3-1 前面的日期不能大于后面的日期且两个日期都不能大于 2020-10-01
合同有效期从  到 
<input id="d4311" class="Wdate" type="text" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'d4312\')||\'2020-10-01\'}'})"/> 
<input id="d4312" class="Wdate" type="text" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'d4311\')}',maxDate:'2020-10-01'})"/>

注意:
两个日期的日期格式必须相同

$dp.$ 相当于 document.getElementById 函数.
那么为什么里面的 ' 使用 \' 呢? 那是因为 " 和 ' 都被外围的函数使用了,故使用转义符 \ ,否则会提示JS语法错误.
所以您在其他地方使用时注意把 \' 改成 " 或者 ' 来使用.

 #F{$dp.$D(\'d4312\')||\'2020-10-01\'} 表示当 d4312 为空时, 采用 2020-10-01 的值作为最大值
 
示例4-3-2 前面的日期+3天 不能大于 后面的日期
       日期从  到 
       <input type="text" class="Wdate" id="d4321" 
       onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'d4322\',{d:-3});}'})"/>
       <input type="text" class="Wdate" id="d4322" 
       onFocus="WdatePicker({minDate:'#F{$dp.$D(\'d4321\',{d:3});}'})"/>

       使用 $dp.$D 函数 可以将日期框中的值,加上定义的日期差量:
       两个参数: id={字符类型}需要处理的文本框的id值 , obj={对象类型}日期差量
   
       日期差量用法:
       属性y,M,d,H,m,s分别代表年月日时分秒