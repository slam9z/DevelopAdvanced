[Utility pattern](https://en.wikipedia.org/wiki/Utility_pattern)

The Utility pattern is a software pattern that is used for utility classes that do not require instantiation and only have static methods. The stateless class is designated as static so that no instance can be created. Good candidates for utility classes are convenience methods that can be grouped together functionally.

Furthermore, methods in Utility classes are usually deterministic. As the Utility class is stateless, all parameters in each method must pass all necessary information to the method.

Example in C#

> Util 比 Utility好看。

```cs
public static class LogUtil
{
    public static void LogError(String message)
    {
        MyLogger logger = new MyLogger();
        logger.LogError(message);
    }
    
    public static void LogWarning(String message)
    {
        MyLogger logger = new MyLogger();
        logger.LogWarning(message);
    }
    
    public static void LogInfo(String message)
    {
        MyLogger logger = new MyLogger();
        logger.LogInfo(message);
    }
}

// A simple example showing how the utility methods are used
in "My Program"
{
   static void Main(String[] args)
   {
       if (args.Length > 0)
       {
           // Call our utility helper methods.  Note that these are static methods
           // that are called directly off the class.
           LogUtil.LogError("User ran app with arguments!");
       }
       else
       {
           LogUtil.LogInfo("Running program.");
           Run();
       }
   }
}
```