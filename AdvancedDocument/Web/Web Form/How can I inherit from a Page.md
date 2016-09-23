[How can I inherit from a Page?](http://stackoverflow.com/questions/4931675/how-can-i-inherit-from-a-page)


##Question

I created a page to be the base Page in my web application to add inside it some common functionality. 
but when I try to inhirite from it then VS gives me the following error:

```
The type or namespace name 'CentralPage' could not be found (are you missing a using directive or an assembly reference?)
```

Note: I just want code-behind functionality in the base page.
What should I do ?


##Answer


You need your base page to inherit from page. From your comment, I've put the two in different namespaces,
 maybe that will help you:

```cs
namespace Project.BasePages
{
    public class BasePage : Page
    {
    }
}
```

then all other pages should inherit from BasePage instead of the default Page:
using Project.BasePages;

```cs
namespace Project.ContentPages
{
    public partial class Page1 : BasePage
    {
    }
}
```

or

```cs
namespace Project.ContentPages
{
    public partial class Page1 : Project.BasePages.BasePage
    {
    }
}
```

Note that BasePage shouldn't be a full ASPX page, just a normal *.cs class file.
share
improve this answer
edited Feb 8 '11 at 10:18 


answered Feb 8 '11 at 10:08 

Graham Clark 
10.3k73269 

   
  
Thanks, I know that but when BasePage exists in a another file, I couldn't use what you wrote. – Homam Feb 8 '11 at 10:14 
   
  
@John: it's perfectly normal for the two classes to be in separate files. If they're in the same project and in the same namespace, they should be able to "see" each other fine. If BasePage is in a different namespace to Page1, you'll need to have a using statement at the top of the file to include the correct namespace. – Graham Clark Feb 8 '11 at 10:16 
   
  
@Graham Clark: but seems there's no namespace!! and I couldn't access to the base page through base:: !!! – Homam Feb 8 '11 at 10:19 
   
  
@John: it's generally easier to partition your classes into namespaces; using C# in Visual Studio will always wrap a class inside a namespace. If no namespace is defined in the code file, I'm pretty sure it will just default to the namespace of the project it's in. So 2 classes in the same project without namespaces in their code files should be able to "see" each other. – Graham Clark Feb 8 '11 at 10:23 
   
  
@John: you don't want to be doing base::, to access a method of a base class from a child class, you just do base.methodName(). – Graham Clark Feb 8 '11 at 10:24 
   
  
@– Graham Clark: first, I solved my problem by moving the base page into the AppCode folder, there.. all pages can access to it. – Homam Feb 8 '11 at 10:35 
   
  
@– Graham Clark: "So 2 classes in the same project without namespaces in their code files should be able to "see" each other": seems no, if the two classes inside two pages files. try it please. – Homam Feb 8 '11 at 