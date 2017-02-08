[Path to Test Data Files for Unit Testing](http://stackoverflow.com/questions/1776331/path-to-test-data-files-for-unit-testing)


ou're saying the test file's location will differ depending on the test runner, so I assume it's included in the project and copied together with the dll's.

```cs
string path = AppDomain.CurrentDomain.BaseDirectory;
```

This will get you the folder where you're executing the test from. 