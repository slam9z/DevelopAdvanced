[IE 7 expected identifier, string or number](http://stackoverflow.com/questions/8840420/ie-7-expected-identifier-string-or-number)


##Answer

IE is confused by the extra comma:
Change:

```
height: '365px', }, 500 );
```

To:

```
height: '365px' }, 500 );
```

##MyAnswer

通俗的讲就是在ie7里，json最后一个值不能包含, 。也就是说最后一个值, 是一个规范。