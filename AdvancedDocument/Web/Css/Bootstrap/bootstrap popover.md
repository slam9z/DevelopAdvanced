[bootstrap popover](http://stackoverflow.com/questions/11993903/bootstrap-popover)

##answer

Why popover and tooltip don't work in bootsrap?
The answer is popover or tooltip initializing code $("#myButton").popover(); should be place 
under <script src="js/jquery-1.10.1.min.js"></script>; and <script src="js/bootstrap.min.js"></script>.

Like the following procedure. I think will be ok.


```html
<button type="button" id="myPopover" class="btn btn-default" data-toggle="popover" data-placement="right"
 data-content="Vivamus sagittis lacus vel augue laoreet rutrum faucibus." data-original-title="" title="">
      Popover on Right
</button>

<script src="js/jquery-1.10.1.min.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/main.js"></script>

<script type="text/javascript">
      $('#myButton').tooltip();
      $('#myPopover').popover();
</script> 
```