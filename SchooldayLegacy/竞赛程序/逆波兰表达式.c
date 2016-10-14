#include<stdio.h>
#include<stdlib.h>
#define MAX 100
double polish()
{
   char a[MAX];
   scanf("%s",a);
   switch(a[0])
   {
       case '+':return polish()+polish();
       case '-':return polish()-polish();
       case '*':return polish()*polish();
       case '/':return polish()/polish();
       default:return atof(a);
   }
}
int main()
{
     double catouse;
     catouse=polish();
     printf("%f",catouse);
     return 0;
}