[JavaScript Array 对象](http://www.w3school.com.cn/jsref/jsref_obj_array.asp)



# JavaScript Array 对象



## Array 对象

Array 对象用于在单个的变量中存储多个值。

### 创建 Array 对象的语法：

<pre>new Array();
new Array(_size_);
new Array(_element0_, _element1_, ..., _elementn_);
</pre>

### 参数

参数 _size_ 是期望的数组元素个数。返回的数组，length 字段将被设为 _size_ 的值。

参数 _element_ ..., _elementn_ 是参数列表。当使用这些参数来调用构造函数 Array() 时，新创建的数组的元素就会被初始化为这些值。它的 length 字段也会被设置为参数的个数。

### 返回值

返回新创建并被初始化了的数组。

如果调用构造函数 Array() 时没有使用参数，那么返回的数组为空，length 字段为 0。

当调用构造函数时只传递给它一个数字参数，该构造函数将返回具有指定个数、元素为 undefined 的数组。

当其他参数调用 Array() 时，该构造函数将用参数指定的值初始化数组。

当把构造函数作为函数调用，不使用 new 运算符时，它的行为与使用 new 运算符调用它时的行为完全一样。

 

 

## Array 对象属性

<table class="dataintable">
  <tbody><tr>
    <th style="width:25%">属性</th>
    <th>描述</th>
  </tr>
  <tr>
    <td>[constructor](/jsref/jsref_constructor_array.asp)</td>
    <td>返回对创建此对象的数组函数的引用。</td>
  </tr>
  <tr>
    <td>[length](/jsref/jsref_length_array.asp)</td>
    <td>设置或返回数组中元素的数目。</td>
  </tr>
  <tr>
    <td>[prototype](/jsref/jsref_prototype_array.asp)</td>
    <td>使您有能力向对象添加属性和方法。</td>
  </tr>
  </tbody></table>
 

 

## Array 对象方法

<table class="dataintable">
  <tbody><tr>
    <th style="width:25%">方法</th>
    <th>描述</th>
  </tr>
  <tr>
    <td>[concat()](/jsref/jsref_concat_array.asp)</td>
    <td>连接两个或更多的数组，并返回结果。</td>
  </tr>
  <tr>
    <td>[join()](/jsref/jsref_join.asp)</td>
    <td>把数组的所有元素放入一个字符串。元素通过指定的分隔符进行分隔。</td>
  </tr>
  <tr>
    <td>[pop()](/jsref/jsref_pop.asp)</td>
    <td>删除并返回数组的最后一个元素</td>
  </tr>
  <tr>
    <td>[push()](/jsref/jsref_push.asp)</td>
    <td>向数组的末尾添加一个或更多元素，并返回新的长度。</td>
  </tr>
  <tr>
    <td>[reverse()](/jsref/jsref_reverse.asp)</td>
    <td>颠倒数组中元素的顺序。</td>
  </tr>
  <tr>
    <td>[shift()](/jsref/jsref_shift.asp)</td>
    <td>删除并返回数组的第一个元素</td>
  </tr>
  <tr>
    <td>[slice()](/jsref/jsref_slice_array.asp)</td>
    <td>从某个已有的数组返回选定的元素</td>
  </tr>
  <tr>
    <td>[sort()](/jsref/jsref_sort.asp)</td>
    <td>对数组的元素进行排序</td>
  </tr>
  <tr>
    <td>[splice()](/jsref/jsref_splice.asp)</td>
    <td>删除元素，并向数组添加新元素。</td>
  </tr>
  <tr>
    <td>[toSource()](/jsref/jsref_tosource_array.asp)</td>
    <td>返回该对象的源代码。</td>
  </tr>
  <tr>
    <td>[toString()](/jsref/jsref_toString_array.asp)</td>
    <td>把数组转换为字符串，并返回结果。</td>
  </tr>
  <tr>
    <td>[toLocaleString()](/jsref/jsref_toLocaleString_array.asp)</td>
    <td>把数组转换为本地数组，并返回结果。</td>
  </tr>
  <tr>
    <td>[unshift()](/jsref/jsref_unshift.asp)</td>
    <td>向数组的开头添加一个或更多元素，并返回新的长度。</td>
  </tr>
  <tr>
    <td>[valueOf()](/jsref/jsref_valueof_array.asp)</td>
    <td>返回数组对象的原始值</td>
  </tr>
</tbody></table>
 

 