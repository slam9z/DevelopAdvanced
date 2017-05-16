[ThreadAbortException handling in asp.net while executing Response.End() method](http://stackoverflow.com/questions/21042931/threadabortexception-handling-in-asp-net-while-executing-response-end-method)


## answer

According to PRB: ThreadAbortException Occurs If You Use Response.End, Response.Redirect, or Server.Transfer:

    If you use the Response.End, Response.Redirect, or Server.Transfer method, a ThreadAbortException exception occurs. You can use a try-catch statement to catch this exception.

    The Response.End method ends the page execution and shifts the execution to the Application_EndRequest event in the application's event pipeline. The line of code that follows Response.End is not executed.

    This problem occurs in the Response.Redirect and Server.Transfer methods because both methods call Response.End internally.

        To work around this problem

    , use one of the following methods:

    For Response.End, call the HttpContext.Current.ApplicationInstance.CompleteRequest method instead of Response.End to bypass the code execution to the Application_EndRequest event.

    For Response.Redirect, use an overload, Response.Redirect(String url, bool endResponse) that passes false for the endResponse parameter to suppress the internal call to Response.End. For example: Response.Redirect ("nextpage.aspx", false); If you use this workaround, the code that follows Response.Redirect is executed.

    For Server.Transfer, use the Server.Execute method instead.

    This behavior is by design.

