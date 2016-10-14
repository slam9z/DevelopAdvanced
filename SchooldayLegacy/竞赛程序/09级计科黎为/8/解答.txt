#include<stdio.h>

void main(){
	int a[9][9];
	int x=-1;
	int y=0;
	int xl=0;
	int yl=0;
	int tc=0;
	int n=6;
	int i,j;
	int cr=0;




	for(i=n;i>0;i--){
		tc++;
		if(tc%3==1){
			for(j=0;j<i;j++){
				cr++;
				x++;y=yl;
				a[y][x]=cr;
			}
			yl++;
		}

		if(tc%3==2){
			for(j=0;j<i;j++){
				cr++;
				x--;y++;
				a[y][x]=cr;
			}
		}
		
		if(tc%3==0){
			for(j=0;j<i;j++){
				cr++;
				y--;x=xl;
				a[y][x]=cr;
			}
			xl++;
		}
	}

	for(i=0;i<n;i++){
		for(j=0;j<n-i;j++)
			printf("%d ",a[i][j]);
		printf("\n");
	}

}