[I Love C# Extension Methods](https://www.codeproject.com/articles/271683/i-love-csharp-extension-methods)

## Introduction

I found a nice way to avoid nested "if"s or nested property checks against null:


##　The Implementation of GetProperty

The implementation is small - and if you really like generic programming - nice, if not ... I did this for you - you can just use it.

```cs
public static TP GetProperty<tc>(this TC that, Func<tc> func)
	where TC : class {
	if (object.ReferenceEquals(that, null)) {
		return default(TP);
	}
	return func(that);
}
public static TP GetProperty<tc>(this TC that, Func<tc> func, TP defaultValue)
	where TC : class {
	if (object.ReferenceEquals(that, null)) {
		return defaultValue;
	}
	return func(that);
}
public static TP GetProperty<tc>(this TC that, Func<tc> func, Func<tp /> funcDefault)
	where TC : class {
	if (object.ReferenceEquals(that, null)) {
		return funcDefault();
	}
	return func(that);
}
```