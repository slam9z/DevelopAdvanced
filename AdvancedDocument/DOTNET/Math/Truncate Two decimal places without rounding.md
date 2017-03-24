[Truncate Two decimal places without rounding](http://stackoverflow.com/questions/3143657/truncate-two-decimal-places-without-rounding)




One issue with the other examples is they multiply the input value before dividing it. There is an edge case here that you can overflow decimal by multiplying first, an edge case, but something I have come across. It's safer to deal with the fractional part separately as follows:

```cs
public static decimal TruncateDecimal(this decimal value, int decimalPlaces)
{
    decimal integralValue = Math.Truncate(value);

    decimal fraction = value - integralValue;

    decimal factor = (decimal)Math.Pow(10, decimalPlaces);

    decimal truncatedFraction = Math.Truncate(fraction * factor) / factor;

    decimal result = integralValue + truncatedFraction;

    return result;
}
```
