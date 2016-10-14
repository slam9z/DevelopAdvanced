#include<stdio.h>
#include<math.h>

int ans[92][8];
int hang[8];
int count=0;

void queen(int ith){
	int j,k;
	if(ith==8){
		for(j=0;j<8;j++){
			ans[count][j]=hang[j]+1;
		}
		count++;
		return;
	}
	for(j=0;j<8;j++){
		for(k=0;k<ith;k++){
			if( hang[k]==j||abs(k-ith)==abs(hang[k]-j) )break;
		}
		if(k==ith){
			hang[ith]=j;
			queen(ith+1);
		}
	}

}

void main(){
	int i,j,n,b;
	queen(0);
	scanf("%d",&n);
	for(i=0;i<n;i++){
		scanf("%d",&b);
		for(j=0;j<8;j++)
			printf("%d",ans[b-1][j]);
		printf("\n");
	}
}
