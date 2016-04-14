[全面理解Javascript中Function对象的属性和方法 ](http://www.cnblogs.com/liontone/p/3970420.html)

##属性

* arguments

* length 

    获取函数定义的参数个数，
    ```
    functionName.length
``` 
* caller 

    获取调用当前函数的函数。caller属性只有当函数正在执行时才被定义。
    ```
    functionName.caller
    ```

* callee 
    返回正被执行的 Function 对象，即指定的 Function 对象的正文。
    ```
    [functionName.]arguments.callee
    ```

* constructor

* prototype


##方法