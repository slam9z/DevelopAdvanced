#include<stdio.h>

int common(int x,int y){
	if(x==y) return x;
	if(x>y)  return common(x/2,y);
    return common(x,y/2);
}

void main(){
	int n,m;
	scanf("%d%d",&m,&n);
	printf("%d\n",common(m,n));
}