[How do you debug a single ASPX file?](stackoverflow.com/questions/15538520/how-do-you-debug-a-single-aspx-file)


Assuming you've already got ASPX page to render on local IIS server (i.e. by dropping to folder for default site).

In Visual Studio

* Open page (from localhost:....) in browser of your choice
* Debugger -> Attach To process
* See if w3wp is in the list, if not check "show from all users" checkbox, may need to run VS as admin (or user that have debug privileges)
* Attach to w3wp (make sure to pick correct .Net version if necessary)
* Open ASPX/ASPX.cs from and set breakpoints
* Refresh page in browser to hit breakpoints

Note: creating one of Web projects would be easier as you will be able to use local server provided by VS and easier overall experience.
