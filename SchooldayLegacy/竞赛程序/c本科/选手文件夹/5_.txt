#include<stdio.h>
#include<string.h>
#include<malloc.h>
void shift(char* s, int n)
{
	char* p;
	char* q;
	int len = strlen(s);
	if(len==0) return;
	if(n<=0 || n>=len) return;

	char* s2 = (char*)malloc(sizeof(s2));
	p = s;
	q = s2 + n % len;
	while(*p)
	{	
		*q++ = *p++;
		if(q-s2>=len)
		{
			*q =*p%len;
			q = s2;
		}
	}
	strcpy(s,s2);
	free(s2);
}


void main ( ){	
	char s[20]="abcdaaa";
	int n=1;
    shift(s,n);
    s[strlen(s)-1]='\0';
	puts(s);

	
}
