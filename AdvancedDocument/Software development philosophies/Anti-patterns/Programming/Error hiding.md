[Error hiding](https://en.wikipedia.org/wiki/Error_hiding)

 Catching an error message before it can be shown to the user and either showing nothing or showing a meaningless message. This anti-pattern is also named Diaper Pattern. Also can refer to erasing the Stack trace during exception handling, which can hamper debugging.


Error hiding is an anti-pattern in computer programming. The programmer hides error messages by overriding them with exception handling. As a result of this the root error message is hidden from the user (hence 'error hiding') and so they will not be told what the actual error is. Error hiding is a bane of support engineers' jobs as it often delays the resolution of the problem by hiding information needed to identify what is going wrong.

A common argument for error hiding is the desire to hide complexity from the user. Frequently best practice is to raise an exception to the user to hide a complex error message but to save the full error message to an error log which a support engineer can access to resolve the problem.

