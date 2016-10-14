#include<stdio.h>
#include<string.h>
#include<ctype.h>
#define N 100
char a[N];



void main(){
	int i;
	int j;
	int zm=0;
	int sz=0;
	int kg=0;
	char c;
	
	gets(a);
	for(i=0;(c=a[i])!='\0';i++){
		if('a'<=c&&c<='z'){
			zm=1;
			if(i==0||kg||sz){
				a[i]=a[i]&0xDF;
			}
			if(sz){
				for(j=N;j>i;j--)
				   a[j]=a[j-1];
				a[i]='_';
				i++;
			}
			kg=0; sz=0;
		//	printf("z  %4d  %s\n",i,a);
		}
	
		
		else if('1'<=c&&c<='9'){
			sz=1;
			if(zm){
				for(j=N;j>i;j--)
					a[j]=a[j-1];
			a[i]='_';i++;
				
			}
			zm=0;
			kg=0;
	    //	printf("s  %4d  %s\n",i,a);
		}
		
		else if(c==' '){
			kg++;
			if(kg>1){
				for(j=i;j<=N;j++)
					a[j]=a[j+1];
				i--;
				kg--;
			}
		   zm=0;
		   sz=0;
		 //  printf("k  %4d  %s\n",i,a);
		}
	}

    puts(a);
}








