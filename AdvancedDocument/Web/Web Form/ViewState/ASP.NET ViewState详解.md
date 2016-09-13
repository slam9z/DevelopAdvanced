[ASP.NET ViewState详解 ](http://www.cnblogs.com/CrabMan/p/5201974.html)

[TRULY Understanding ViewState](http://weblogs.asp.net/infinitiesloop/Truly-Understanding-Viewstate) 


*ViewState 还是不要用，但是还是要理解它*

##ViewState可以用来做什么？　　

这里列举的每一项都是ViewState需要完成的主要工作，我们将根据这些工作来学习ViewState是如何实现这些功能。

1. 以键值对的方式来存控件的值，和Hashtable的结构类似；
1. 跟踪那些ViewState中出现改变的值，以便对这些脏数据(dirty)进行进一步的处理；
1. 通过序列化将ViewState中的值保存在页面的隐藏域(Hidden Field)中(这是默认的持久化方式)，并通过反序列化得到对应的ViewState对象以便进行相应的操作；
1. 在页面回传的过程中自动的存储ViewState中的跟踪的值。

下面列举的是ViewState不能用来做什么的列表，这个其实比了解ViewState是用来做什么的还重要。　　　　

1. 自动保存一个类中变量的状态，无论是private, protected还是public的变量；
1. 可以在页面回传的过程中记住所有状态值；
1. 只要有了ViewState那么每次页面请求时重新构造的数据的操作是不必要的了；
1. ViewState is not responsible for the population of values that are posted such as by TextBox 
controls (although it does play an important role) ViewState并不存储那些通过Post键值对回传的数据值(如TextBox的TextBox.Text)；
虽然ViewState作为一个整体出现在.NET Framework框架中有它的唯一目的，那就是在页面回传的过程中保存状态值，
使原本没有“记忆”的Http协议变得有“记忆”起来。但是上面列举的ViewState的四个主要功能之间却没有太多的关联。所以从逻辑上我们可以将其划分开来，各个击破。


##1. ViewState就是用来存储数据的

如果你曾经使用过HashTable的话，那么你应该明白我的意思了。这里并没有什么高深的理论。V
iewState通过String类型的数据作为索引(注意在ViewState中不允许通过整形下表的方式对其中的项进行访问，
如：ViewState.Item(0) 的形式是不允许的。)ViewState对应项中的值可以存储任何类型的值，实施上任何类型的值存储到ViewState中都会被装箱为Object类型。
以下是几个对ViewState进行赋值的几个例子。

```cs
ViewState["Key1"] = 123.45M; // store a decimal value
ViewState["Key2"] = "abc"; // store a string
ViewState["Key3"] = DateTime.Now; // store a DateTime
```

实际上ViewState仅仅就是一个定义在System.Web.UI.Control类中的一个保护类型(Protected)的属性名称。
由于所有服务器端的控件，用户自定义控件还有页面(Page)类都是继承自System.Web.UI.Control类，所以这些控件都具有这些属性。
ViewState的真正类型实际应该是System.Web.UI.StateBag类。严格的说，虽然StateBag类虽然定义在System.Web的命名空间下，
实际上StateBag类和ASP.NET并没有严格上的依存关系，它也完全可以System.Collections命名空间下。
事实上许多服务器端控件大多数属性值都是利用ViewState来进行数据存储。你可能认为

TextBox.Text属性是按如下形式存储的：

```cs
public string Text 
{ 
    get { return _text; } 
    set { _text = value; } 
}
```

但是你必须注意，上面的形式(通过类的私有字段)并不是大多数ASP.NET 服务器控件存储其属性值得方式。
这些控件的属性值大多是通过ViewState来进行存储的。通过Reflector查看TextBox.Text属性的源代码你可以看到类似如下的代码：

```
public string Text 
{
    get { return (string)ViewState["Text"]; }
    set { ViewState["Text"] = value; }
}
```

 为了表示这个观点的重要性，我这里再重申一遍“大多数ASP.NET 服务器控件存储其属性值得方式是通过ViewState的方式存储的
，而不是我们通常想象的那样通过类的私有字段来存储。”即便是用于设定服务器控件样式的Style类中的大多数属性值也是通过ViewState来进行存储的。
所以在设计自定义的组件时，对于那些需要存储的组件属性值也最好遵循这个方式。这里我还需要着重讲一个问题，
在以ViewState为存储方式的情况下，如果实现属性的默认值(default value)，我们可能会认为属性值是这样实现的：

```cs
public class MyClass
{
    private string _text = "DefauleValue";
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
}
```

这样如果在对Text的属性没有设置的时候，直接取Text属性，那么我们可以得到默认值"Default Value!"。
那么如果我们使用ViewState来存储的时候如何实现默认值呢？如下所示：

```cs
public string Text
{
    get { return ViewState["Text"] == null ? "Default Value!" : (string)ViewState["Text"]; }
    set { ViewState["Text"] = value; }
}
```

就像操作HashTable一样，如果StateBag(ViewState)中没有包含某个键值的项，那么它会返回一个null(在VB.NET中是返回Nothing)。
所以我们可以通过判断对应键值的项是否是null来判断某个ViewState项是否被赋值。然后我们通过三目运算符来根据实际情况来返回默认值或者设置的值。
并且使用三目运算符实际上这里还出于一个考虑，那么就是在服务器控件中，如果将某个属性值设置为空(null)，那么往往代表的意思是使用此属性的默认值。
所以第一种实现方法还存在一个问题，那就是如果把某个属性值设置为null,当我们再取这个属性的时候我们将得到null，而不是我们期望的"Default Value!"了，
所以对于第一种实现方法还需要对null这个特殊值进行判断才可以完全满足需求。ViewState还可以被用作其他的作用，
比如在页面回传过程中保留某些值，比如我们在页面后台代码中常常使用ViewState("Key") = "SomeValue"的方式来存储值，
实际上就是使用了Page类的ViewState属性来进行值得存储。同样的我们也可以在控件级别进行ViewState的字定义存储。
 

###2. ViewState可以跟踪值的变化

如果你设置一个控件的属性值，那么你会把ViewState中这个属性值对应的数据弄脏(dirty)的。
当然数据这个和数据库中的脏数据不同，这里的脏可以理解为“发生变化”的意思。
你知道为什么StateBag会存在，而不会被HashTable取代吗？。虽然他们都是通过名值对的方式来存储值，
但是StateBag还具有对其中数据更改的跟踪过程(Tracking ability)。是否进行跟踪的开关可以被设置成开或者关，
当调用StateBag.TrackViewState()方法后跟踪开关将被开启。只有在跟踪的开关设置为“开”的情况下StateBag中的数据更改才会被跟踪，
只要数据出现修改，那么对应StateBag项的数据将会被标记为“脏的”(dirty)。StateBag还提供了检查一个数据项是否是脏数据的方法 -- IsItemDirty(string key)。
你也可以在不更改项数据数值的情况下将对应项设置为脏数据，这里需要使用SetItemDirty(string key)方法。为了说明这些，我们看一下以下的例子。
这里我们假设当前的StateBag跟踪的开关是处于关闭状态的。

```cs
stateBag.IsItemDirty("key"); // returns false
stateBag["key"] = "abc";
stateBag.IsItemDirty("key"); // still returns false 

stateBag["key"] = "def";
stateBag.IsItemDirty("key"); // STILL returns false 

stateBag.TrackViewState();
stateBag.IsItemDirty("key"); // yup still returns false 

stateBag["key"] = "ghi";
stateBag.IsItemDirty("key"); // TRUE! 

stateBag.SetItemDirty("key", false);
stateBag.IsItemDirty("key"); // FALSE!

```


注意一点，在TrackViewState()方法调用后，只要是出现了赋值操作那么就会使其被标记为脏数据，
StateBag并不会判断赋值前后对应项的值是否出现了变化。


可能你会认为根据赋值前后ViewState是否存在变化然后再标记是否是脏数据这样更加符合常理。
但是必须注意的是ViewState的项是可以存储任何类型的值的(实际上任何赋值给ViewState的变量都会被装箱为Object类型的变量)，
所以比较赋值前后的值是否一致实际上并没有变面上看的那么容易。而且不是每种类型都实现了IComparable的接口，
所以通过调用CompareTo方法来进行比较也是不可行的。另外还有一个原因，我们知道ViewState还需要将其内部的数据进行序列和反序列化，
当这些操作发生后，你得到的对象已经不是原来那个对象了，所以比较对象之间的引用也是无法完成的。基于以上这些原因
，ViewState采取了一种简单的做法，也就意味着ViewState的数据变化跟踪也是一个简要的跟踪。为了解释清楚这个问题，
我们首先需要先了解一下ASP.NET是怎样建立静态控件的。所谓静态控件(declarative control)就是那些从页面或者用户自定义控件的源码中可以看到声明代码的控件。
如

而且如果ViewState已经开始了跟踪数据，那么此次属性的赋值就会导致“脏数据”的产生，但是如果ViewState还没有开始跟踪数据，那么脏数据的标记值就一直为False。
现在问题就是在当前ASP.NET解析静态控件的时候是否开始跟踪和是否产生了“脏数据”呢？答案是，没有。
原因是此时的ViewState赋值之前ASP.NET并没有去调用TrackViewState()方法，所以ViewState是不会对数据的更改进行跟踪的。
事实上ASP.NET是在页面生命周期的OnInit阶段才调用TrackViewState()方法的。这样做的目的就是让ASP.NET可以很方便的区分控件的哪些属性值在初次声明后仍未改变，
那些属性值已经被改变了(可能是程序的方式也可能是人工输入的方式)。如果到目前为止你还没有意识到这个观点很重要的话，那么请继续往下读吧。


##3. 序列化和反序列化(SERIALIZATION AND DESERIALIZATION )

我们前面提到的ViewState的两个重要功能(1. ViewState可以像HashTable那样通过名值对来存储值；2. ViewState可以对那些修改的数据进行跟踪。 )
现在我们将来讨论另外一个话题，那就是ASP.NET是怎样通过StateBag类的特性来实现那些看似诡异的功能的。

如果你在ASP.NET中使用过ViewState，事实上我相信只要是ASP.NET的开发者都会使用过ViewState了。而且可能你也知道了序列化(serialization)的问题。
如果是默认的方式，那么VIewState中的值会被序列化成一个基于Base64编码的字符串，然后存储在页面中一个叫做_ViewState的隐藏变量中。

这里在继续之前，我需要稍稍叉开一下话题先说一些页面的控件树。我发现有不少有多年工作ASP.NET开放经验的程序员还不知道控件树的存在。
由于他们仅仅是对.aspx页面进行操作，所以他们仅仅只关心那些页面上声明的控件。但是我们必须认识到页面的控件实际是以一颗控件树存在的
，并且控件中还可以包含子控件。这颗控件树的根节点就是页面本身(Page)，然后树的第二层通常是包含3个控件，
它们分别是用于保存表单(<form>)标签前所有信息的文本控件(Literal)，然后是表单控件(Form)，然后是表单(</form>)标签后面的所有信息的文本控件(Literal)
。接着是树的第三层包含的控件就是在表单标签内声明的那些控件，如果这些控件中还包含子控件，那么这颗控件树的深度将会不断的加深，
一直到所有页面的控件都被包含在这颗控件树中。每个控件都会有自己的ViewState对象，
并且由于这些控件共同的基类(System.Web.UI.Control)中包含一个受保护(protected)的方法SaveViewState，方法的返回值是一个Object变量

。在Control.SaveViewState方法中如果发现ViewState不为空，那么就直接调用其私有变量_viewState(StateBag类型)的SaveViewState方法。
通过这个方法就将ViewState中被标记为脏数据(dirty)的项的键和值都存储在一个ArrayList中，然后再将这个ArrayList进行返回。
通过递归的方法遍历整个控件树的各个节点，并递归的调用各个控件的SaveViewState方法，这样当整个控件树被遍历完成以后，
那么和控件树一一对应的会形成一个由ViewState的值组成的数据树。

在这个阶段，ViewState中存储的数据还没有被转化为我们在_ViewState隐藏变量中存储的Base64编码的字符串。
这里仅仅是形成了一颗需要被持久化存储的数据树。这里再强调一下，存储的数据是ViewState中那些被标记为Dirty的项
。StateBag类具有跟踪功能就是为了在存储的时候判断哪些数据需要被存储，哪些数据不需要被存储(实际上这是StateBag具有跟踪数据功能的唯一原因)。
很聪明是吧，但是如果使用不当的话，在ViewState中依然可能保存一些不必要的数据。我会在后面的例子中来说明这些可能犯的错误。



##4. 自动恢复数据(AUTOMATICALLY RESTORES DATA)

到此为止我们已经说到了ViewState最后一个功能，那就是自动恢复数据。有些文章将这个过程和上面提到的反序列化过程混淆在一起，这样的理解是不正确的，
实际上自动恢复数据的过程并不是反序列化过程的一部分。ASP.NET首先反序列化_ViewState中的值，将其还原为对象，然后再将这些还原的值重新赋值给其对应的控件。

作为所有控件包括Page类基类的System.Web.UI.Control类型中包含一个LoadViewState(object savedState)方法。
其中需要被载入的数据就是通过参数savedState进行传递的。LoadViewState和前面所说的SaveViewState是相对应的方法。
而且和SaveViewState方法类似的是，Control.LoadViewState也是简单的调用了StateBag中的LoadViewState方法。通过查看LoadViewState的源代码可以发现，
这个函数实际就是将savedState中存储的名值对重新Add到StateBag列表中(StateBag.Add(key, value))。

同时我们从LoadViewState也可以发现在.NET Framework 1.1中传入的object变量是一个pair类型的变量。pair类型包含两个属性First, Second都是object类型的变量，
在ViewState中其中一个属性存储的是包含ViewState.Item.Key的ArrayList而另外一个属性包含的是ViewState.Item.Value的ArrayList，
相对应的Key和Value在ArrayList中的下标相同。
然后StateBag类就通过遍历两个ArrayList将值添加到状态项中(注意在.NET Framework 2.0中这个方法的实现有些小小的改动，
放弃使用Pair类型而仅仅使用一个ArrayList, ArrayList中每个名值对占两个Item, 前一个为key后一个为value, 循环的时候以步进2进行循环)。

这里需要注意的是从LoadViewState()重新载入到ViewState的数据仅仅包含前一次请求被标记为Dirty的那些数据(注意不是当次请求(current request)，
而是前一次请求(previous request)就是当前请求的前一次请求。)在载入_ViewState中包含的数据之前，对应控件的ViewState中可能已经包含了一些值了，
比如那些静态控件中预先声明好的值(如：<asp:Label Text="abc"/>中的Text属性在LoadViewState()之前就已经是"abc"了)。

如果LoadViewState()中需要载入的数据中已经存在值了，那么对应的值将被新值所覆盖。
为了让大家有一个完整的认识，这里将页面回传以后发生的事情再简单的描述一下。首先页面回传以后，
整个Page将重新生成并且那些页面上声明的静态控件也都已经被解析添加到以Page为根节点的控件树中，那些静态控件对应的静态声明的属性值也都被初始化。
然后是OnInit阶段，在这个阶段ASP.NET会调用TrackViewState方法，从此以后所有对控件属性的赋值操作都将导致被跟踪。
接着就是LoadViewState()方法被调用，这里那些从_ViewState中反序列化出来的值将被重新赋给对应的控件，由于在此之前TrackViewState()已经被调用了，
_ViewState中包含的数据对应的属性值都会被标记为Dirty。这样当调用SaveViewState的时候，这些属性值还是会被持久的保留到_ViewState中，
这样在页面的一次次回传和页面一次次的重新建立的过程中，这些控件的值就被保留下来了。


##5.错误使用ViewState的情况(CASES OF MISUSE)

1. 为服务器端控件赋默认值(Forcing a Default);
1. 持久化静态数据(Persisting static data);
1. 持久化廉价数据(Persisting cheap data);
1. 以编码的方式初始化子控件(Initializing child controls programmatically);
1. 以编码的方式创建控件(Initializing dynamically created controls programmatically)。

###1. 为服务器端控件(webcontrol)设置默认值(Forcing a Default)


这个错误是开发服务器端控件(WebControl)中最常见的错误，不过这个错误修改起来非常的简单，
而且修改后的代码会更加的简洁明了(事情往往就是这样，约正确的方式，越优的方式往往也是最简明的方式。be simple is good)。
造成这种错误的原因往往是开发人员没有了解ViewState的跟踪机制或者根本就不知道有跟踪机制这种说法。
我们来看一个例子，我们现在需要一个控件，这个控件有一个Text属性，如果没有对Text进行赋值，那么就从一个Session变量中得到其默认值。
我们的程序员Joe写下了如下代码


```cs
public class JoesControl : WebControl
{
　　public string Text       
　　{        
          get { return this.ViewState["Text"] as string; }        
          set { this.ViewState["Text"] = value; 
    } 
　　protected override void OnLoad(EventArgs args) 
　　{
          if(this.Text == null)
          {
               this.Text = Session["SomeSessionKey"] as string;
     　　}
          base.OnLoad(args); 
      }
}
```

(注：这里我将if (!this.IsPostBack) 的条件设置为if (this.Text == null)就是指当Text属性没有赋值时，那么就赋初值。)
以上代码存在一个问题，第一个问题是Joe花了大力气为控件设置一个Text，他希望使用者可以对这个控件赋值。Jane是其中一个使用者，
她写下了如下的代码：<abc:JoesControl id="joe1" runat="server" />
 当Jane查看其页面HTML源代码的时候，她发现她的页面ViewState的体积也变大了。Joe的第二次实现方式，问题解决：

```cs
 public class JoesControl : WebControl
{
    public string Text
    {
        get
        {
            return this.ViewState["Text"] == null ? Session["SomeSessionKey"] as string : this.ViewState["Text"] as string;
        }
        set
        {
            this.ViewState["Text"] = value;
        }
    }
}

```

1. 为什么第一种实现方式会使页面的ViewState大小变大？
这里先要说明的是，如果在使用JoesControl的时候赋了初值，如下：
<abc:JoesControl id="joe1" runat="server" Text="ViewState rocks!" />
这样和后面的实现方式在现实上也是没有区别的。因为这里并没有执行this.Text = Session["SomeSessionKey"]这个语句，
自然this.Text并不认为出现了变化，那么ViewState["Text"]并不会被标记为Dirty，所以也不会被序列化到_ViewState中。
现在我们讨论一下如果没有设置Text属性初值的情况，那么这个时候就会在JoesControl的OnLoad方法中执行this.Text = Session["SomeSessionKey"]这个语句，
但是这个时候各个控件已经执行完成了OnInit阶段，所以TrackViewState()已经调用，这个时候this.Text已经被标记为Dirty了，
所以会被持久化到_ViewState隐藏变量中，这样就增加了ViewState的大小。那么如果使用了第二种方法，判断是否设置了初值，
如果没有那么就通过Session["SomeSessionValue"]中的默认值替代，这个阶段是在生成JoesControl(New JoesControl)的时候进行赋值的，
这个时候由于还未到达OnInit阶段，所以TrackViewState()方法还没有被调用，所以ViewState["Text"]并不会被标记为Dirty，
当然也就不会记录到_ViewState中进行持久化。所以第二种实现方式是优于第一种实现方式的。


###2. 持久化静态数据(Persisting static data)

我们这里所说的静态数据是那些不会被改变的数据(never change)或者在页面的生命周期中、一个用户会话中不会被改变的数据。
还是我们可爱的程序员Joe，最近他又接到了一个改造网站的任务，在他们公司的eCommerce网站上显示那些已经登录的用户，
比如“嗨，XXXX，欢迎回来！”Joe的前提条件是这个网站已经有了一个业务层的API,可以通过CurrentUser.Name的方法方便的得到当前已经验证的用户姓名。
剩下的把这个人名显示到页面上的工作就看Joe的了。以下是Joe的代码：

(ShoppingCart.aspx)

```aspx
<!--用于显示登录用户姓名的Label控件-->
<asp:Label id="lblUserName" runat="server" />
```

(ShoppingCart.aspx.cs)
//用于在Label中动态显示登录用户姓名的代码；
 View Code
好了，F5，运行，一切正常，Joe又开始得意洋洋了。但是我们知道其实这里Joe还是犯了个错误。用户的名称不仅仅会显示在Label中，
同样还会被序列化到_ViewState中，并根据页面/服务器之间的来来回回而不停的被序列化、反序列化...。这个开销是值得的吗？J
oe耸耸肩说，这有什么关系，就那么几个字节而已。但是可以节约一点为什么不节约呢，而且解决的方法还是如此的简单
。第一种方法，不用修改源代码，直接禁用Label控件的ViewState，如：

```aspx
<asp:Label id="lblUserName" runat="server" EnableViewState="false" />
```

好了，问题解决了。但是是否有更加好的解决方法呢？有！Label控件可能是ASP.NET中最最被高估的控件了。
这个可能是由于那些WinForm的VB编程者，在WinForm中如果要显示一些文本信息，你可能需要一个Label。
而ASP.NET中的这个Label可能被认为和WinForm中的Label是等价的了。但是真的就是这样的吗
？通过HTML源码我们可以看到Label控件实际被解析成了HTML中的<span>标签。你必须问问你自己是否真的需要这个<span>标签呢？
如果不需要涉及到特定的格式，仅仅是显示信息那么我觉得答案是否定的。请看：

```aspx
<%= CurrentUser.Name %>
```

恩，这样你就可以避免生成一个<span>标签了，并且可以很好的解决问题。但是从编程习惯上来说，这种将前台和后台代码混合的形式是不提倡的，
这样会使代码的可读性下降，并且使开发的职责无法明确区分。所以这里还可以使用一种ASP.NET中存在但是确被Label控件的光环笼罩的控件 -- Literal。
这个控件仅仅将其Text中的内容输出到客户端，并且不会生成<span>标签。是不是觉得对这个控件有些印象，对了，前面在说道将页面解析成一个控件树的时候，
第二层一般由三个控件组成，一个是Literal，用于存储到<form>标签以前的所有html代码。就是这个控件。以下就是使用Literal控件来替代Label控件的方法。当然这里也需要将EnableViewState设置为false。问题解决了的同时，我们节省了网络传输的资源。不错!

```aspx
<asp:Literal id="litUserName" runat="server" EnableViewState="false"/>
```

###3. 持久化廉价的数据(Persisting cheap data) 


这个问题实际上包含了第一个问题。静态数据往往是很容易就可以得到的(取得的开销成本比较小)，
但是并不是所有容易取得的数据都是静态数据。可能这些数据会不停的被更改，但是总体来说得到这些数据的成本很低。
一个典型的例子是美国各个州的列表。除非你要回到1787年12月7日(here)，那么当前美国的所有州列表在短期内是不会有改变的。
当然我们现在的程序员都很痛恨硬编码。“让我把美国各个州的列表都静态的写在页面上？傻子才这样做呢。
”我们更加倾向于将州名都保留在一个数据库(或者其他易于修改的配置文件中。)，这样如果州名或者州的列表出现了任何变化，就不用修改源代码了。
恩，我完全同意这一点，我们的著名程序员Joe也是这样认为的，而且这张表在他们公司已经存在了，表名叫做USSTATES，这回Joe的任务就是和操作这张表有关系的。
下面是用于显示美国各个州列表的下拉菜单(DropDownList)：

```aspx
<asp:DropdownList id="lstStates" runat="server"    DataTextField="StateName" DataValueField="StateCode" />
```

这里显示的是绑定从数据库中取得的美国州列表的数据代码：

```cs
protected override void OnLoad(EventArgs e)
{
    if (!this.IsPostBack)
    {
        this.lstStates.DataSource = QueryDatabase();
        this.lstStates.DataBind();
    }
    base.OnLoad(e);
}
```


由于美国50个州是在OnLoad阶段中被绑定到下拉菜单(DropDownList)中的，所以这些信息在绑定到下拉菜单的同时，
还被序列化并被记录到了ViewState中了。天哪，那可能一个庞大的数据，特别是对于那些低速接入网络的用户。

这个问题和上面提到的静态数据有些类似，一种比较通用的解决方法就是将控件的EnableViewState属性设置为False。
但是这种解决方法并不是万能药，比如我们现在的例子，如果Joe仅仅是将用于显示美国各州的DropDownList的EnableViewState控件设置为false，
并且将OnLoad函数中的!Page.IsPostBack的限制条件去掉(这样就保证每次载入页面后DropDownList都会被重新绑定，
而不会再页面回传以后导致DropDownList中的数据丢失。)，那么在使用的时候，Joe就会发现他有麻烦了。什么麻烦呢？
“当页面回传以后，Joe发现他先前选择的州并不是下拉菜单(DropDownList)中的默认值。”
(注意这里的DropDownList是静态控件才会出现上面说的这种情况，如果是在OnLoad中动态生成的DropDownList控件然后再绑定数据那么不会出现此问题)
怎么会这个样子！！这是对ViewState的另外一个误解。下拉菜单之所以没有保留页面回传前的选择值并不是因为我们禁用了下拉菜单的ViewState。
在页面回传的时候还有一些用于获取页面信息的控件值并不是通过ViewState来进行保存的，他们是通过名值对的方式通过Http请求(HttpRequest)的方式进行回传的，
这些值被称为回传值(PostData)(可以通过将回传方式修改为GET来从URL中查看存在哪些回传值)。
所以即便是我们禁用了DropDownList的ViewState，DropDownList依然可以将那个选择的值回传服务器。这
里之所以下拉菜单(DropDownList)会在页面回传后“忘记”上次选择的值是因为在OnLoad阶段之前的ProcessPostData已经对DropDownList设置了默认值，
但是这个时候DropDownList还没有ListItem，自然无法设置到最后一次回传选择的值。然后是OnLoad事件中对DropDownList进行数据绑定，
但是由于没有执行ProcessPostData方法所以不会再次设置默认值。前面的括号中有说明，如果这个DropDownList控件也是在OnLoad中动态生成的，
那么由于进度追赶，在OnLoad阶段后还会重新执行一次ProcessPostData，在这里又会把下拉菜单中的值设置为默认值，
所以说以上描述的问题仅仅只有在DropDownList为静态控件的时候才会存在。幸运的是我们解决这个问题的方法也很简单，
我们将绑定数据的代码移动到OnInit阶段，这个阶段将先于ProcessPostData执行，所以下拉菜单将被设置为最后一次回传的默认值。

```aspx
<asp:DropdownList id="lstStates" runat="server"    DataTextField="StateName" DataValueField="StateCode" EnableViewState="false" />
```

```cs
protected override void OnInit(EventArgs e)
{
    this.lstStates.DataSource = QueryDatabase();
    this.lstStates.DataBind();
    base.OnInit(e);
}
```

上面这种方法适用于几乎所有的廉价数据(cheap data，容易获得的，获得的代价很低的数据)。
你可能会反驳我说如果每次都重新去取数据，如：每次都连接数据库去取得对应的数据可能会比将数据存储在ViewState中代价更高，
但是我不这样认为。当前的数据库管理系统(DBMS, 如SQL Server)已经相当的成熟，它们往往具有良好的缓存机制，
如果配置得当的话执行的效率也非常高。(译者：我也有这样的经验，我曾经比较两种处理数据的方式，
其中一种是先取得一个大范围的数据，然后在代码中通过循环的方式将其中不符合条件的数据过滤掉；
另外一种方式是直接通过SQL语句在数据库中进行数据筛选，然后将符合条件的数据进行返回。根据页面显示的速度来判断，后者的执行效率远远高于前者。)
其实想想到底是将一堆无用的数据通过56kbps的速度和千里之外的客户传来传去还是将少许的数据在可能只相距几百英尺的应用服务器和数据库服务器之间传递(它们之间的连接速度一般都高于10M)的代价高，
这个结果应该已经很明显了。当然如果你一定想精益求精的话，那么你可以选择把一些常用不易变的数据缓存起来这样可以进一步的提高性能。


##4. 通过编码的方式初始化子控件(Initializing child controls programmatically)

让我们再一起面对这样一个事实，我们不可能什么都事先把所需的控件声明好，有时候页面的显示，
控件的现实和外观都和一定的业务逻辑有关系(这不正是我们程序员存在的原因吗？)。
但是麻烦的是ASP.NET并没有提供一种简单的方式让我们“正确”的创建动态控件。当然我们可以通过重写OnLoad方法并在这里声明动态的控件，
事实上我们也常常这样做，但是这样做的结果有时候会让我们在ViewState持久化了一些本不应该持久化的数据。
我们同样可以重写OnInit方法，但是同样的问题依然存在。还记得我们前面提到过ASP.NET在OnInit阶段是怎样调用TrackViewState()方法的吗？
它是从控件树的底部递归调用每个子控件的TrackViewState()方法，最后一个调用的就是控件树的根节点(Page)，
所以当你在Page.OnInit阶段的时候对动态控件进行操作的话，那么页面的子控件的TrackViewState已经被调用了，
所以这个时候你赋值的数据也会被标记为脏数据(dirty data)并最终被ViewState进行持久化保存。让我们再看看Joe的例子，
Joe在页面中定义了用于显示当前日期和时间的标签控件(Label)，声明代码如下所示：

```
<asp:Label id="lblDate" runat="server" />

protected override void OnInit(EventArgs e)
{
    this.lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"); 
    base.OnInit(e);
}
```

虽然Joe已经在最早的事件中将Label的Text属性设置为了当前的日期时间信息。但是还是晚了，原因我们前面已经分析过了，
这个时候Label.TrackViewState()已经被调用，所以Label.Text的赋值操作将导致Label.Text被标记为脏数据(dirty data)，
从而被记录到ViewState中。但是这个是不必要的，应为这个日期很容易得到，可以归结于我们前面总结的“持久化廉价的数据”这个问题。
要解决这个问题我们可以简单的将Label控件的EnableViewState属性设置为false。但是我们这里将用另外一种方法来解决，
因为这种解决方法揭示了一个重要的概念。首先我们看看Joe的做法，直接将Label中的Text属性设置为当前时间信息，如下所示

```aspx
<asp:Label id="Label1" runat="server" Text="<%= DateTime.Now.ToString() %>" />
```

你可能也有过这样尝试，但是ASP.NET会给你当头一棒，它会明确的告诉你<%= %>语法不能对服务器端控件的属性进行赋值操作。
当然Joe也可以使用<%# %>的方法，但是这个方法和我们前面提到的禁用Label的ViewState同时在每次请求页面的时候绑定数据的方法实际上是一样的。
问题是我们希望通过编码的方式为Label的Text属性的初值进行赋值操作(我们不希望这些赋值操作导致ViewState大小的增加)，
同样在以后的操作中我们希望这个Label控件依然可以像一个普通的Label控件被使用。简单的说就是这样，我们需要一个Label，
它的默认值是当前的日期和时间，但是如果我们人工的对其Label.Text进行了赋值操作，
那么我们还是希望这个值在页面的回传之间可以保留(即通过ViewState进行持久化)。举个简单的例子，
Joe的页面上有一个按钮，当用户点击这个按钮那么显示当前日期和时间的Label将显示一个“空时间”(即：“--/--/---- --:--:--”)，此按钮的响应代码为：


View Code
如果需要实现这样一个需求那么我们前面的做法(简单的将Label的EnableViewState属性设置为false)将不能解决这个问题，
因为如果用户通过按钮取消了时间的显示，由于Label的ViewState被禁用，那么就意味着Label的值在回传之间不会被保存，
所以在下次页面回传以后Label依然会显示当前的日期和时间。那么Joe需要怎么做呢？
实际上上面的例子描述的就是一个逻辑，Label控件必须按照逻辑来决定应该显示什么内容。上面的逻辑我们简化的说就是，
对于Label的初值我们不希望它保留在ViewState中而以后如果出现了改变那么我们希望都保留在ViewState中，
以便在页面回传的过程中进行状态的保留。从这个表述我们可以看出，如果我们能在控件的TrackViewState()被调用前为其赋初值，那么什么问题都解决了。
但是前面我提到过，ASP.NET并没有提供一种简单的方法来实现这个过程(在TrackViewState()被调用前进行操作)。
在ASP.NET 2.0的版本中已经为我们提供了一些先于OnInit阶段的阶段(如：OnPreInit阶段)，这里针对ASP.NET 1.1版本，
我们确实没有一个先于OnInit阶段进行控件的初值设置(其实这个表述是不正确的，
在ASP.NET 1.1中你可以通过重写DeterminePostBackMode方法来实现对控件进行赋初值，由于这个方法先于OnInit方法，
所以此时赋的初值是不会被记录到ViewState中)。一下作者提供了另外两种实现方法：

1. 在控件的OnInit事件对其进行赋值操作(Declaratively hook into the Init event) 如:

　　<asp:Label id="Label2" runat="server" OnInit="lblDate_Init" />
　　同样在后台编写Label.OnInit事件对应的响应函数并对Label.Text赋初值也是可以的。

2. 创建用户自定义组件(Create a custom control):

这里在构造函数中就对Label.Text的属性进行初值赋值，一定是在TrackViewState()方法之前，所以这样也可以达到我们前面提到的目的。

###5. 以编码的方式创建动态控件(Initializing dynamically created controls programmatically)

这个实际上和上面的就是一个问题，由于到目前为止我们对ViewState已经有了一定深度的了解，所以我们解决起问题来就更加的得心应手。
让我们来看看我们的老朋友Joe编写的一个用户自定义组件，这个组件重写了Control类的一个CreateChildControls方法，动态生成了一个Label控件。代码如下：

```cs
public class JoesCustomControl : Control 
{    
    protected override void CreateChildControls() 
    {        
        Label l = new Label();         
        this.Controls.Add(l);        
        l.Text = "Joe's label!";    
    }
}
```

好了，我们现在考虑的是那些被动态创建的控件(例子中是Label控件)什么时候开始跟踪它的ViewState呢？
我们知道我们可以在页面生命周期的任何阶段动态生成控件并添加到页面的控件树中，
但是ASP.NET中是在OnInit阶段调用TrackViewState()以开始跟踪控件ViewState的变化。
那么我们这里动态创建的控件是否会由于错过了OnInit事件从而导致不能对动态生成的控件的状态进行跟踪和持久化呢？
答案是否定的，这个奥秘就是Controls.Add()方法，这个方法并不像我们原来使用ArrayList.Add方法仅仅是将一个Object添加到一个列表中，
Controls.Add()方法在将子控件添加到当前控件下后还需要调用一个叫做AddedControl()的方法，
就是这个方法对于那些新加入的控件状态进行检查，如果发现当前控件的状态落后于页面的生命周期，
那么将会调用对应的方法使当前控件的状态和页面声明周期保持一致，这个过程叫做“追赶(catch up)”。
比如我们举一个稍稍极端的例子，我们在页面生命周期的OnPreRender阶段动态生成了一个控件并将其添加到当前页面的控件树中，
那么系统发现新添加的控件并不是出于OnPreRender状态便会调用方法使这个控件经历LoadViewState,LoadPostBackData,OnLoad等方法
(页面声明周期中的一些私有方法将被忽略)，直到这个控件也到了OnPreRender状态。
其实通过查看Temporary ASP.NET Files中编译过的ASP.NET aspx页面的类代码你就可以发现在创建页面控件树的时候
，调用的是一个叫做__BuildControlTree()的方法，里面对于添加子控件使用的是AddParsedSubObject()方法，
而这个方法实际就是调用了Controls.Add()方法，同样的过程。

我们再回到Joe编写的用户自定义组件，由于CreateChildControls无法确定在何时被调用，
如果页面已经执行到了OnInit阶段，那么只要调用了Controls.Add()方法那么这个控件马上就会被调用TrackViewState()方法，
并立即开始对ViewState进行跟踪。而Joe的代码是在this.Contorls.Add(l)之后再对Label进行初值赋值(l.Text = “Joe’s Label!”)，
这样”Joe’s Label!”将被添加到ViewState进行保存。那么知道了一切原因都源于Controls.Add()方法后，解决方法也就出来了，
我们只要颠倒一些最后两个语句的顺序就可以解决问题，代码如下所示：

```cs
public class JoesCustomControl : Control 
{    
    protected override void CreateChildControls() 
    {        
        Label l = new Label();        
        l.Text = "Joe's label!";         
        this.Controls.Add(l);    
    }
}
```


很玄妙是吧？理解了这个我们再回头看看我们前面提到的通过下拉菜单(DropDownList)列举美国所有州的名称的例子。在前面提供的解决方法中，
我们是先禁用DropDownList的ViewState，然后在OnInit阶段对DropDownList进行数据绑定。那么我们这里又提供了一个新的解决方法。
首先在页面中去掉静态声明的DropDownList，然后在页面生命周期OnLoad阶段前的任何位置动态生成DropDownList，并且对其进行值的绑定，
然后通过Controls.Add()方法将其添加到页面控件树中，同样可以达到一样的效果。
