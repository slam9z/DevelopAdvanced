[Convert string to Boolean in javascript](http://stackoverflow.com/questions/4344542/convert-string-to-boolean-in-javascript)


##Question

How to convert a string to Boolean ? 
I tried using the constructor Boolean("false"), but it's always true.

  
##Answer

I would use a simple string comparison here, as far as I know there is no built in function for what you want to do (unless you want to resort to eval... which you don't).
var myBool = myString == "true";

###Comment

Reading this in 2014, and still amazed by the simplicity of this solution. – sargas Apr 3 '14 at 15:56 

perhaps var myBool = myString.toLowerCase() == "true" will be better – Stupidfrog Jan 8 '15 at 3:09 
  
  
THIS DOES NOT WORK! Just initialize myString = true; and you will see that myBool returns false!!! – Eugen Mihailescu Apr 30 '15 at 9:51 