#include<stdio.h>
#include<string.h>
#include<malloc.h>

int g(int a, int b)
{
	char sa[]="00000000";
	char sb[]="00000000";
	int n = 0;
	int i,j;
	

	sprintf(sa,"%8d",a);
	sprintf(sb,"%8d",b);
	for(i=0; i<8; i++)
	{
		for(j=1; j<=8-i; j++)
		{
		    char t = sa[i+j];
			sa[i+j] = 0;
			if(strstr(sb, sa+i))
			{
				if(j>n) n=j;
			}
			sa[i+j] = t;
		}
	}

	return n;
}



void main ( ){	
	int a=12345678;
	int b=34567212;
	printf("%d",g(a,b));
	
 }
