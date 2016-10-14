#include<stdio.h>
#include<string.h>
#define  MAX 200

void main(){
	char num1[MAX+10];
	char num2[MAX+10];
	int shu1[MAX+10];
	int shu2[MAX+10];

	int result[2*MAX+10];

	int i;
	int j;

	int bstart=0;

	int len1;
	int len2;

	scanf("%s",num1);
	scanf("%s",num2);

	memset(shu1,0,sizeof(shu1));
	memset(shu2,0,sizeof(shu2));
	memset(result,0,sizeof(result));
	
	j=0; 
	len1=strlen(num1);
	for(i=len1-1;i>=0;i--)
		shu1[j++]=num1[i]-'0';

    j=0;
	len2=strlen(num2);
	for(i=len2-1;i>=0;i--)
		shu2[j++]=num2[i]-'0';

	for(i=0;i<len1;i++){
		for(j=0;j<len2;j++)
			result[i+j]+=shu1[i]*shu2[j];
	}

	for(i=0;i<2*MAX;i++){
		if(result[i]>=10){
			result[i+1]+=result[i]/10;
			result[i]%=10;
		}
	}



	
	for(i=2*MAX;i>=0;i--){
		if(bstart)
			printf("%d",result[i]);
		else if(result[i]){
			printf("%d",result[i]);
			bstart=1;
		}
		//if 与 else if的区别 这语句只会执行一次 挺有意思的
	}
	if(!bstart)
		printf("0");
}
