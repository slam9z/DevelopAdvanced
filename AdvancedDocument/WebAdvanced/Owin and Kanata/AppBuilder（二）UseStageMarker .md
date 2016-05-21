
*总结middleware注入与重建是两个逆向的过程，按顺序注入，反向遍历重建，微软工程师巧妙地保证了pipeline注入顺序，
且保证了在两个StageMarker的middleware被包装在一个Func<AppFunc,AppFunc>中，经过约定每个middleware返回next，
在未遇到ExitPointInvoked之前，都不会发生PipelineStage的切换。这将需要PipelineStage切换机制的支持和对middleware输入参数、输出参数的约定。
现在还有些模糊的地方，在下一章将通过对IntegratedPipeline以及AppBuilder.Build的阅读来说清楚这些流程。*

[AppBuilder（二）UseStageMarker ](http://www.cnblogs.com/hmxb/p/5300342.html)

