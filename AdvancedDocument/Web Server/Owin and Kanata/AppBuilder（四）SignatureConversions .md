�����Ѿ����Խ��ͺܶණ���ˡ�

1. ΪʲôҪ���������

    ��Ϊÿ��OwinMiddleware�Ĺ��캯���ĵ�һ����������Func<AppFunc,AppFunc>�Ĳ�������һ��next��ָ����һ��Ҫ���е��������ô���next��Ӧ��Ϊ�գ�����Ҫ��ʵ��Ч����������������ɺ���OwinMiddleware����Func��Ȼ��������Ϊǰһ���Ĳ��������ܱ�֤�����pipeline����Ч�ԡ�

2. OwinMiddleware����Func����δ������ģ�

    ����������ÿ��OwinMiddleware����Func�ĵ�һ����������һ��next��OwinMiddleware��Func�ķ������������Invoke��������ͬ����OwinMiddleware��Invoke��һ��������д�ķ���������ΪOwinContext����Func��Delegate����Invoke������ִͬ�����Func������ΪEnvrionment����Invoke�������Լ��Ĺ���֮��ִ��next.Invoke�����������������������ʹ������ˡ�

3. PipelineStage������л��ģ�

    �⽫����һ����Ҫ�漰�����ݣ�ÿ��PipelineStage����¼��NextStage��Pipeline���Ȳ��ֿ����������첽�������֮������NextStage������Ҫ��δ��Դ��System.Web.Application����ɵ��ȵģ��������¼��Ļ��ơ�

�ܽᣬÿ��PipelineStage�и�EntryPoint��ExitPoint�������Լ�����֮ǰ������OwinMiddleware����Funcͨ��next����������ִ�е�ʱ����HttpApplication������Ӧ���¼���
pipeline�������Ĺؼ�������ÿ�����������һ������кϷ���Ч���ã����Բ��÷�������ķ������ؽ���Func������һFuncΪnext.Invoke(environment)��OwinMiddleware������һOwinMiddlewareΪNext.Invoke(context)��
����conversion��Ҫ��OwinMiddleware����Func������next���Ǹ��Լ�һ�����͵ġ�OwinMiddlewareΪ����Funcһ�£���������Invoke��Ϊ��ڡ�



[AppBuilder���ģ�SignatureConversions ](http://www.cnblogs.com/hmxb/p/5303940.html)