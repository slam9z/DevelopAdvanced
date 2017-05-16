[How to browse between pages without screen flickering?](http://stackoverflow.com/questions/13122769/how-to-browse-between-pages-without-screen-flickering/13122814#13122814)


## answer
	
just suggestion, set the body background color to the main background color of your site. Probably your main background is not white. 


I've added an answer - check it - flickering is because your background color is transparent and the default page color is white – Reflective Oct 29 '12 at 14:03 


```html
<style>
    html {
        background: red;
        margin: 0;
        padding: 0;
    }
</style>
```

> 调一个舒服的颜色会好点
