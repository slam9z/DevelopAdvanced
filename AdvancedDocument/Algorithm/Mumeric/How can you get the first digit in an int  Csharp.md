[How can you get the first digit in an int (C#)?](http://stackoverflow.com/questions/701322/how-can-you-get-the-first-digit-in-an-int-c/701621#701621)

##Question

GetFirstDigit

In C#, what's the best way to get the 1st digit in an int? The method I came up with is to turn the int into a string, 
find the 1st char of the string, then turn it back to an int.

```C#
int start = Convert.ToInt32(curr.ToString().Substring(0, 1));
```

While this does the job, it feels like there is probably a good, simple, math-based solution to such a problem. 
String manipulation feels clunky.


##Best answer

*Benchmarks*


Firstly, you must decide on what you mean by "best" solution, of course that takes into account the efficiency of the algorithm,
 its readability/maintainability, and the likelihood of bugs creeping up in the future. Careful unit tests can generally avoid those problems, however.
I ran each of these examples 10 million times, and the results value is the number of ElapsedTicks that have passed.
Without further ado, from slowest to quickest, the algorithms are:

###Converting to a string, take first character

```c#
int firstDigit = (int)(Value.ToString()[0]) - 48;
```

Results:
12,552,893 ticks

###Using a logarithm

```c#
int firstDigit = (int)(Value / Math.Pow(10, (int)Math.Floor(Math.Log10(Value))));
```

Results:
9,165,089 ticks

###Looping

```C#
while (number >= 10)
    number /= 10;
```

Results:


6,001,570 ticks


###Conditionals

int firstdigit;

```C#
if (Value < 10)
     firstdigit = Value;
else if (Value < 100)
     firstdigit = Value / 10;
else if (Value < 1000)
     firstdigit = Value / 100;
else if (Value < 10000)
     firstdigit = Value / 1000;
else if (Value < 100000)
     firstdigit = Value / 10000;
else if (Value < 1000000)
     firstdigit = Value / 100000;
else if (Value < 10000000)
     firstdigit = Value / 1000000;
else if (Value < 100000000)
     firstdigit = Value / 10000000;
else if (Value < 1000000000)
     firstdigit = Value / 100000000;
else
     firstdigit = Value / 1000000000;
```

Results:
1,421,659 ticks

###Unrolled & optimized loop

```C#
if (i >= 100000000) i /= 100000000;
if (i >= 10000) i /= 10000;
if (i >= 100) i /= 100;
if (i >= 10) i /= 10;
```

Results:
1,399,788 ticks
Note:
each test calls Random.Next() to get the next int

##Relative question

求比当前数大，但是除最高位数外其它为0的数中的最小数。

##Answer

###Looping

```C#

int base=1;
while (number >= 10){
    number /= 10;
    base*=10;
}

var result = (long)(Math.Ceiling(number / base) * base);

```


###Using a logarithm

```
var base = Math.Pow(
                 10
                , (int)Math.Floor(
                    Math.Log10(number)
                    )
                );

var result = (long)(Math.Ceiling(number / base) * base);

```