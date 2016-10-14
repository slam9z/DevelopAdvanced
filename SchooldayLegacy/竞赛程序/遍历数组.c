#include<stdio.h>
void main(){
	int a[7];
	int i, j, c;
	for(j=0;j<6;j++)
			a[j]=0;
	for(i=0;i<64;i++){
		for(j=0;j<6;j++)
			printf("%d",a[j]);
		printf("  ");
		a[0]++; c=0;
		while(a[c]>1){
			a[c]=0; c++; a[c]++;
		}
	}
	printf("\n");
}

	//这技巧牛逼 相当于每一次加一。