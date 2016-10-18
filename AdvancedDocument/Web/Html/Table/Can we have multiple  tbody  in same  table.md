[Can we have multiple <tbody> in same <table>?](http://stackoverflow.com/questions/3076708/can-we-have-multiple-tbody-in-same-table)


##Question

Can we have multiple <tbody> tags in same <table>? If yes then in what scenarios should we use multiple <tbody> tags?


##Answer

Yes you can use them, for example I use them to more easily style groups of data, like this:

```html
<table>
    <thead>
        <tr><th>Customer</th><th>Order</th><th>Month</th></tr>
    </thead>
    <tbody>
        <tr><td>Customer 1</td><td>#1</td><td>January</td></tr>
        <tr><td>Customer 1</td><td>#2</td><td>April</td></tr>
        <tr><td>Customer 1</td><td>#3</td><td>March</td></tr>
    </tbody>
    <tbody>
        <tr><td>Customer 2</td><td>#1</td><td>January</td></tr>
        <tr><td>Customer 2</td><td>#2</td><td>April</td></tr>
        <tr><td>Customer 2</td><td>#3</td><td>March</td></tr>
    </tbody>
    <tbody>
        <tr><td>Customer 3</td><td>#1</td><td>January</td></tr>
        <tr><td>Customer 3</td><td>#2</td><td>April</td></tr>
        <tr><td>Customer 3</td><td>#3</td><td>March</td></tr>
    </tbody>
</table>
```


Then you can style them easily, like this:

```css
tbody:nth-child(odd) { background: #f5f5f5; }
tbody:nth-child(even) { background: #e5e5e5; }
```
You can view an example here, it'll only work in newer browsers...but that's what I'm supporting in my current application, 
you can use the grouping for JavaScript etc. The main thing is it's a convenient way to visually group the rows to make 
the data much more readable. There are other uses of course, but as far as applicable examples, this one is the most
 common one for me.

