#include<stdio.h>
#include<memory.h>
#define MAX 100

int D[MAX+10][MAX+10];
int MaxSum[MAX+10][MAX+10];
int N;

int maxsum(int k,int r){

	if(k==N)
		return D[k][r];
	if(MaxSum[k+1][r+1]==-1)
		MaxSum[k+1][r+1]=maxsum(k+1,r+1);
	if(MaxSum[k+1][r]==-1)
		MaxSum[k+1][r]=maxsum(k+1,r);
	if(MaxSum[k+1][r+1]>MaxSum[k+1][r])

		return D[k][r]+MaxSum[k+1][r+1];
	else
		return D[k][r]+MaxSum[k+1][r];

}

void main(){
	int i,j;
	scanf("%d",&N);
	for(i=1;i<=N;i++)
		for(j=1;j<=i;j++)
			scanf("%d",&D[i][j]);
	memset( MaxSum,-1,sizeof(MaxSum) );
	printf( "%d\n",maxsum(1,1) );
}