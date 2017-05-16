#[为WebForms说几句话，以及一些ASP.NET开发上的经验（1）](http://www.cnblogs.com/JeffreyZhao/archive/2007/12/22/Experience-for-Asp-dot-net-and-WebForms.html)

##一、ViewState

HTTP是无连接无状态的协议，因此ASP.NET中提出了ViewState的概念，这样数据被重新Post回页面时，页面（控件）的状态就能恢复，因此才有了很多丰富的功能
，例如一些*复杂的控件事件*。但是ViewState带来的问题就是，如果使用不当，那么页面体积就会增加许多，网络中传输的数据太多自然会影响性能。


但是ViewState真是必须的吗？我可以很负责任地说，在如今大部分Web应用的页面中，出现的几乎都是大量的链接，
点击链接就会跳转到一个和当前页面完全无关的新页面，这样的话，页面上的ViewState又有什么用？因此我如果新建一个Web项目，
做的第一件事情就是去Web.config中将enableViewState从全局关闭——同时关闭的还有enableSessionState，
这也是影响性能的因素之一（stateless也便于做Web服务器层面的负载均衡）。


嗯？刚才不是说只有保持ViewState才能使用控件的事件吗？没有ViewState怎么从控件中重新获取状态呢？请注意我之前所说的是“复杂事件”。
什么是复杂事件？TextBox的TextChange事件就是“复杂事件”，GridView的Command事件也是复杂事件，但是Button的Click事件就是“简单事件”；
与此相对的，GridView里的每一行的数据每一个子控件的状态是“复杂状态”，而TextBox的Text属性则是“简单状态”。
“复杂状态”和“复杂事件”需要ViewState，因为与之有关的这些“控件”是ASP.NET“无中生有”的，但是“简单事件”和“简单状态”基于页面中“必然”会提交的数据，
它们自然能够还能够使用。在我的ASP.NET开发过程中，使用的几乎都是“简单事件”和“简单状态”，而印象中放弃“复杂事件”和“复杂状态”并没有给我带来任何的困扰。


当某人送给我们10件礼物，而其中只有4件是我需要的，那么为什么不能简单地放弃其余6件，偏偏要去感谢只送给我们3件礼物的人而去指责前者呢？
要知道他并没有恶意，那多余的6件也没有给我们造成任何困扰。

但人就是那么奇怪。


##二、性能


WebForms的一个重要特点就是一个强大（很多情况下也是“复杂”的代名词）的组件模型。这个组件模型包含一个叫做“生命周期”的玩意儿，
也就是这个玩意儿被不少人指责为性能杀手。这个复杂的生命周期的确在很多时候只是“无谓”地一遍遍执行，似乎的确造成了“浪费”，但是这真的到了“杀手”级别了吗？


#[为WebForms说几句话，以及一些ASP.NET开发上的经验（2）](http://www.cnblogs.com/JeffreyZhao/archive/2007/12/22/Experience-for-Asp-dot-net-and-WebForms-2.html)

##三、生成丑陋的HTML，难以进行样式控制

我想为WebForms喊冤。不过首先我会打倒以GridView为首的复杂控件（包扩DataList、FormView等等）并狠狠踩上几脚。
有人说，当抛弃了GridView之后，用WebForms还有什么意义？其实类似的话也不断在我说要抛弃ViewState和（复杂）的PostBack时出现。
如果您觉得抛弃了这些东西WebForms就失去意义的话，那么我想说，ViewState、PostBack、GridView远不是WebForms的全部。
我认为，Control模型（或者说组件化模型）才是WebForms的关键。而这个模型的“基础”是绝对优秀的。下面我会进行一些展示，虽然这些展示我觉得是基础中的基础。


Repeater是ASP.NET 2.0中我最喜欢用的控件，它的功能很简单，把ItemTemplate和AlternatingItemTemplate的内容返回生成在页面上，
并且将HeaderTemplate和FooterTemplate的内容显示在头尾。除此之外——没了。但是这已经足够了，对于绑定控件来说，还需要什么呢？
这里面每一行代码都由我们自己编写，想定义样式也易如反掌，我们对于HTML的控制没有任何损失。


但是在ASP.NET 3.5中又多了一个ListView控件，使用这个控件进行展示可以分组循环，可以指定容器，真可谓无比强大。
重要的是ListView和Repeater一样，所有的HTML都由自己控制，一个多余的字符都没有。

有了Repeater和ListView，真可谓打遍天下无敌手，任何页面的展示方式都不在话下。


#[为WebForms说几句话，以及一些ASP.NET开发上的经验（3）](http://www.cnblogs.com/JeffreyZhao/archive/2007/12/23/experience-for-asp-dot-net-and-webforms-3.html)


##四、生成复杂的ID难以使用JavaScript操作


我在上一篇文章的最后提到了，虽然使用WebForms我们能够对于页面上的HTML属性和样式等进行自由的定制和控制，但是有一点是毋庸置疑的，
我们没有办法（正常的办法吧，Hack不算）让服务器端控件在客户端生成一个简单的ID。
例如，一个TextBox控件，在服务器端的ID是txtUserName，但是最终在客户端生成的ID可能是LoginForm_txtUserName，
因为它被放在一个ID为LoginForm的NamingContainer中。


有了组件模型，就出现了大量控件。控件最主要的目的之一就是复用，而复用的一个特点就是应该高度内聚，而不依赖于外部环境。
因此，为了使组件内部的服务器控件最终生成的客户端ID能够在页面上唯一，WebForms引入了NamingContainer这个概念。
在NamingContainer中的服务器端控件最终在客户端生成的ID，会使用NamingContainer的“客户端ID”作为前缀。
如此“递归”的做法保证了服务器控件在客户端的ID唯一。


但是由于NamingContainer的缘故，我们在使用WebForms的服务器端的控件时就可能无法通过textBox在客户端获得文本框（生成的<input />元素）。
为了解决这个问题，服务器端的控件模型提供了一个ClientID属性，通过这个属性，我们就可以在服务器端得到控件最终在客户端的ID。
例如，如果上面的代码放在一个用户控件里的话，就一定必须写成如下形式：

```aspx
<%@ Control Language="C#" AutoEventWireup="true" %> 

<asp:TextBox runat="server" ID="textBox" />

<script language="javascript" type="text/javascript">
    document.getElementById("<%= this.textBox.ClientID %>").value = "Hello World!";
</script>
```

此时，当控件被放到页面上之后，它在客户端生成的代码则会是：

```aspx
<input name="DemoControl1$textBox" type="text" id="DemoControl1_textBox" />

<script language="javascript" type="text/javascript">
    document.getElementById("DemoControl1_textBox").value = "Hello World"!;
</script>
```


这种在设计器很难预测的客户端ID，就是使用WebForms时所谓的“客户端ID污染”。


接下来我们不妨来看一个略为复杂点的例子：

```aspx
<%@ Control Language="C#" AutoEventWireup="true" %> 

<asp:TextBox runat="server" ID="textBox" />

<script language="javascript" type="text/javascript">
    var counter = 0;

    function increase() 
    { 
        document.getElementById("<%= this.textBox.ClientID %>").value = (counter++); 
        window.setTimeout(increase, 500); 
    } 

    increase(); 
</script>
```

上面这段JavaScript代码的作用是每500为一个计数器加1，并且显示在文本框上。随着项目的发展，页面上复杂的JavaScript代码会越来越多，
于是我们就会想办法将其转移到js文件中并且在页面上引用它们。使用js文件的好处很多，便于进行代码管理是一方面，但是最重要的好处之一还是对于性能的提高。
如果JavaScript代码完全写在页面上，这样每次加载页面都需要下载这些JavaScript代码，而js文件可以缓存，这样客户端只需要在第一次加载时下载这个文件就可以了。
减少了客户端与服务器之间数据通信的大小，也就加快页面加载的速度，提高了性能。

不过问题就此出来了：为了能够正确引用到页面上的某个服务器控件生成的DOM元素，我们就必须在页面中使用<%= %>标记来输出控件的ClientID，
但是<%= %>无法写在js文件中，这可怎么办？于是很多人着急了起来，我也不时会收到此类问题，似乎很难找到合适的解决办法。
于是“客户端ID污染”似乎也就成了一个使用WebForms时非常严重的问题。

有些朋友会说：“这个没有问题啊，仔细观察ClientID的组成方式能够很容易找到规律的。”
服务器控件的ClientID是由自身ID和它所在的NamingContainer“树”来共同决定的，
因此在理论上我们也完全可以在设计器得到“已经放置在页面中”的某个服务器控件的客户端ID，
并将其写进JavaScript代码中。话虽如此，的确没错，但是这个解决方案实在不好，因为它违背了控件的重要特性：“复用”。
作为一个控件来说，它可能会被放在任意的NamingContainer树下，也就是说，它的客户端ID在不同的环境中并不固定。另外，如果控件上层NamingContainer树中有任何一个的服务器端ID被修改的话，js文件中使用的ID就需要进行改变，这样实在不利于的维护，随着项目增大，此类问题会愈发明显。
那么我们究竟该怎么做呢？

在设法解决这个问题之前，我们先来思考一下这个问题。如果我们没有使用WebForms进行开发，就在普通的页面上编写代码，
那么我们对于上面的功能会如何将其提取到js文件中呢？嗯，就直接在代码中通过textBox这个ID来获得DOM元素吧。那么好，请您先回答我以下几个疑问：

* 为什么要写textBox而不是其他ID呢？ 
* 如果其他页面上有个同样需要实现的功能，而那个文本框的id是txtCounter，那么该怎么作呢？ 
* 如果一张页面上有两个文本框需要显示这样的计数器，那么又该怎么做呢？


上面的几个疑问其实只反应了一件事情，那就是这个计数器的复用性实在太差。什么叫做好的复用性呢？那么我们来看一下一个典型的示例，MaskedEditExtender。
我们来看看它是怎么做的：

```aspx
<ajaxToolkit:MaskedEditExtender
    TargetControlID="TextBox1" 
    Mask="9,999,999.99"
    MessageValidatorTip="true" 
    OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError"
    MaskType="Number" 
    InputDirection="RightToLeft" 
    AcceptNegative="Left" 
    DisplayMoney="Left"
    ErrorTooltipEnabled="True" />
```

MaskedEditExtender的第一个属性TargetControlID，就可以决定了究竟是为哪个文本框添加效果，然后效果的样式可以由MaskType和Mask决定，
获得焦点的样式和输入错误的样式可以由OnFocusCssClass和OnInvalidCssClass属性决定，连字符输入的顺序都可以定制。

这就是复用：爱怎么用，就怎么用。爱给谁用，就给谁用。想什么时候用，就什么时候用。

要复用，一般总需要组件化或模块化，内部实现通用的功能，而具体的信息应该由外部传入。
例如我们上面的计数器就应该进行改造（用到了MS AJAX Lib里的Function.createDelegate方法）：

```js
function Counter(textBoxId, interval)
{
    this._counter = 0;
    this._textBox = document.getElementById(textBoxId);
    this._interval = interval;
}
Counter.prototype =
{
    run : function()
    {
        this._textBox.value = (this._counter ++);
        window.setTimeout(
            Function.createDelegate(this, this.run), this._interval);            
    }
};
```

现在这个技术器的复用性已经有质的飞跃了，因为我们可以随意指定一个客户端的文本框进行显示，并且可以自由地设置计数器增长的间隔时间。
于是我们在WebForms页面中就可以写如下的代码了：

``aspx
<asp:TextBox runat="server" ID="textBox1" />
<asp:TextBox runat="server" ID="textBox2" />

<script language="javascript" type="text/javascript">
    new Counter("<%= this.textBox1.ClientID %>", 500).run();
    new Counter("<%= this.textBox2.ClientID %>", 1000).run();
</script>
```

现在WebForms客户端ID污染已经不构成问题了吧！
其实解决客户端ID污染的做法用一句话就能说清：“将不变的部分提取至js文件，将变化的部分（例如服务器控件的客户端ID）留在页面中”。
但是我在这里将它上升到组件化的高度，因为它能让我们开发出更优秀的客户端程序。组件化的客户端编程方式较之传统的零散function的做法，更有利于代码的管理，
并且增强了复用性和可维护性。有人说，客户端ID污染问题使脚本代码很难做到“内聚”——可能他的意思是将脚本代码提取到js文件中吧——但是我认为，
这种污染“迫使”我们使用组件化的方式进行客户端开发，而这种组件化或者模块化的做法恰恰提高了代码的内聚性。

不过，似乎组件化的编程方式会写更多的代码，不是吗？从理论上来说，可能的确是。不过需要注意的是，我上面提出的例子非常简单，
简单到了其中的一半代码是用于“组件化”编程的“骨架”上。而对于一个略为复杂的功能来说，例如一个通用的表单验证组件，或者客户端级联组件，
增加的这点“骨架”还算得了什么呢？
这也算是一种因祸得福吧。


