#include<stdio.h>
#include<string.h>
#define  MAX 200

void main(){
	char num1[MAX+10];
	char num2[MAX+10];
	int shu1[MAX+10];
	int shu2[MAX+10];
	
	int i;
	int j;

	int bstart=0;

	scanf("%s",num1);
	scanf("%s",num2);
	memset(shu1,0,sizeof(shu1));
	memset(shu2,0,sizeof(shu2));

    j=0;
	for(i=strlen(num1)-1;i>=0;i--)
		shu1[j++]=num1[i]-'0';

    j=0;
	for(i=strlen(num2)-1;i>=0;i--)
		shu2[j++]=num2[i]-'0';

	for(i=0;i<MAX+1;i++){
		shu1[i]+=shu2[i];
		if(shu1[i]>=10){
			shu1[i]-=10;
			shu1[i+1]++;
		}
	}

	
	for(i=MAX+1;i>=0;i--){
		if(bstart)
			printf("%d",shu1[i]);
		else if(shu1[i]){
			printf("%d",shu1[i]);
			bstart=1;
		}
		//if 与 else if的区别 这语句只会执行一次 挺有意思的
	}
}
