<http://js.do/>

```html

<script src="http://libs.baidu.com/jquery/1.9.1/jquery.min.js"></script>
<div class='panel'>123</div>
<script>


$(function()
{
	//$('.panel').val('234');  error
	$('.panel').text('234');
	$('.panel').html('234');
	$('.panel')[0].innerHTML='234';
});

</script>
```



