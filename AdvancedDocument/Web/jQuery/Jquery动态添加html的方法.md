## clone方法

```html

<div class="place">
</div>
<input class="submit" type="button" value="提交" style="display:none"/>

<script>
    $(".place").append($(".submit").clone().show());    
<script>

```


## 动态创建


```html

<div class="place">
</div>


<script>
    var submit=$('<input/>')
                .addClass('submit')
                .attr('type','button')
                .val('提交')
                ;
    $(".place").append(submit);    
<script>

```


## 拼接字符串


```html

<div class="place">
</div>


<script>

    var submitStr="<input class='submit' type='button' value='提交'/>";

    var submit= $.parseHTML(submitStr);
    
    $(".place").append(submit);    
<script>

```

