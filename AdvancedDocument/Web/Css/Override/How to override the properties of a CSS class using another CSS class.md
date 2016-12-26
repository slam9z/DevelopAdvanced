[How to override the properties of a CSS class using another CSS class](http://stackoverflow.com/questions/20954715/how-to-override-the-properties-of-a-css-class-using-another-css-class)


There are different ways in which properties can be overridden. Assuming you have

```css
.left { background: blue }
```

e.g. any of the following would override it:

```css
a.background-none { background: none; }
body .background-none { background: none; }
.background-none { background: none !important; }
```

The first two “win” by selector specificity; the third one wins by !important, a blunt instrument.

You could also organize your style sheets so that e.g. the rule

```css
.background-none { background: none; }
```

wins simply by order, i.e. by being after an otherwise equally “powerful” rule. But this imposes restrictions and requires you to be careful in any reorganization of style sheets.

These are all examples of the CSS Cascade, a crucial but widely misunderstood concept. It defines the exact rules for resolving conflicts between style sheet rules.

P.S. I used left and background-none as they were used in the question. They are examples of class names that should not be used, since they reflect specific rendering and not structural or semantic roles.
