#include<stdio.h>
#define MAX  20
#define REMUN  20000
int  Price[MAX];       //每一类商品的价格
int  Mun[REMUN][MAX];//记录最终结果的
int  Mmun[MAX];      //里面存储的是每一件商品可能的最大数目，减少不必要的循环
int  Cmun[MAX];      //保留当前各个商品的数目
int  N=0;                 //结果数
int  M;                     //商品总数
int  Money=1000;

void buy(int m){
    int i,k;
    if(Money<0) return ;
    if(Money==0){
        for(k=0;k<M;k++)
           Mun[N][k]=Cmun[k];
        N++; return ;
    }
    if(m==M)  return;
    for(i=0;i<Mmun[m];i++){
        Money-=i*Price[m]; Cmun[m]=i;
        buy(m+1);
     /*
      下面这几部分是用来分支状态的，然后进入别的分支状态，
      凡是遇到这种问题，回溯算法会是一个很好的解决方法吧
      2条回溯语句，将这些回复到原来的情况，只有 i 的值改变了！
      回溯部分最开始只有最后一条语句，然后我一个一个调试才出现了另外语句，真的很神奇！
     */
      Cmun[m]=0; Money+=i*Price[m];
    }
}

void main(){
    int i,j;
    scanf("%d",&M);
    for(i=0;i<M;i++){
        scanf("%d",&Price[i]);
    }
    for(i=0;i<M;i++){
        Mmun[i]=Money/Price[i]+1;
    }
    buy(0);
    printf("%d\n",N);
	
    for(i=0;i<N;i++){
        for(j=0;j<M;j++)
            printf("%-3d",Mun[i][j]);
        printf("\n");
    }

}