[ASP.NET Web Project Folder Structure](https://msdn.microsoft.com/en-us/library/ex526337.aspx)



To sum it up :

* **IIS will NEVER serve any file** located in those folders (the same way it will not ever serve the Web.config file)


* files in the `App_Code` folder will automatically be recompiled when a change occurs in the code.

