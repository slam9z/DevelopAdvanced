#include<stdio.h>
#include<math.h>

int queePlaces[92][8];
int count=0;
int board[8][8];
void putQueen(int ithQueen);
void main(){
	int i,j;
	int n;
	for(i=0;i<8;i++){
		for(j=0;j<8;j++)
			board[i][j]=-1;
		for(j=0;j<92;j++)
			queePlaces[j][i]=0;
	}
	putQueen(0);



	scanf("%d",&n);
	
	for(i=0;i<n;i++){
		int ith;
		scanf("%d",&ith);
		for(j=0;j<8;j++)
			printf("%d",queePlaces[ith][j]);
		printf("\n");
	}
   

}

void putQueen(int ithQueen){
	int i,k,r;
	if(ithQueen==8){
		count++;
		return;
	}
	for(i=0;i<8;i++){
		if(board[i][ithQueen]==-1){
			board[i][ithQueen]=ithQueen;
			for(k=count;k<92;k++)
				queePlaces[k][ithQueen]=i+1;

			for(k=0;k<8;k++)
				for(r=0;r<8;r++){
					if( board[k][r]==-1&&( k==i||r==ithQueen||abs(k-i)==abs(r-ithQueen) ) )
						board[k][r]=ithQueen;
				}
			putQueen(ithQueen+1);

			for(k=0;k<8;k++)
				for(r=0;r<8;r++){
					if(board[k][r]==ithQueen)
						board[k][r]=-1;
				}
		}
	}
}