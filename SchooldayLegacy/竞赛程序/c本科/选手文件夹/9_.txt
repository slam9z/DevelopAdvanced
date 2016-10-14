#include<stdio.h>
#define MAX 10
int N=0;
int moy=1000;
int num;

int mn[MAX];
int ms[MAX][1];
int mr[MAX][MAX][1];

void dg(int m){	
	int i;
	for(i=0;i<1000/mn[m];i++){
		if(m<0)
			return ;
		if(moy==0){
			for(i=0;i<num;i++)
				mr[N][m][0]=ms[m][0];
		    moy=1000;
		    N++;
		    return;
		}

		moy-=i*mn[m];
		ms[m][0]=i;
		dg(--m);
	}
}

void main(){
	int m;
	int i;
	
	scanf("%d",&m);
	num=m;
	for(i=0;i<m;i++)
		scanf("%d",&mn[i]);
	for(i=0;i<m;i++)
		ms[i][0]=-1;
	dg(--m);
	printf("%d",N);

}
