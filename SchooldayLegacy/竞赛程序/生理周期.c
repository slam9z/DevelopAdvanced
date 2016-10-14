/*
#include<stdio.h>

void main(){
	int p,e,i,d,no=1;
	int j;
	scanf("%d%d%d%d",&p,&e,&i,&d);
	while(p!=-1&&e!=-1&&i!=-1&&d!=-1){
		for(j=d+1;j<21252;j++)
			if( (j-p)%23==0&&(j-e)%28==0&&(j-i)%33==0 )
				break;
		printf("case %d %d days\n",no,j-d);
		no++;
		scanf("%d%d%d%d",&p,&e,&i,&d);
	}
}
*/

#include<stdio.h>

void main(){
	int p,e,i,d,no=1;
	int j;
	scanf("%d%d%d%d",&p,&e,&i,&d);
	while(p!=-1&&e!=-1&&i!=-1&&d!=-1){
		for(j=d+1;j<21252;j++)
			if( (j-p)%23==0)
				break;
		for(;j<21252;j=j+23)
			if( (j-e)%28==0)
				break;
		for(;j<21252;j=j+23*28)
			if( (j-i)%33==0)
				break;


		printf("case %d %d years  %d  days\n",no,(j-d)/365,(j-d)%365);
		no++;
		scanf("%d%d%d%d",&p,&e,&i,&d);
	}
}