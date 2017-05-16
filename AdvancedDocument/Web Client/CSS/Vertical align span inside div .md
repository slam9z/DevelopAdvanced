[Vertical align span inside div ](http://stackoverflow.com/questions/21577730/vertical-align-span-inside-div)

##answer

down vote
You should try this:

```css

.container {
    width: 100%;
    height: 50px;
    border: 1px solid black;
    display: table;
    text-align: center;
}

.container span {
    display: table-cell;
    vertical-align: middle;
}

```