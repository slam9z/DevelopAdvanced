将Middleware装换为AppFunc

''' C#
目前能确定下来的时候每个PipelineStage的EntryPoint已经显式指定了，刚刚大概又想了一下，为了保证PipelineStage的规范性
，那么每个PipelineStage应该都是一个Func<AppFunc, AppFunc>形式的才对，而Middleware应该是被封装在这两个AppFunc之间的，这么说，应该是_conversions来完成了同一个PipelineStage中的Middleware的串联工作了，理应如此。下一节再验证这个问题。
'''

[AppBuilder（三）BuildInternal ](http://www.cnblogs.com/hmxb/p/5302866.html)