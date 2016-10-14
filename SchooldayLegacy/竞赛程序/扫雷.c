#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#define M 18
void print1(int *a,int n){
    int *b;
    for(b=a;b<=a+n-1;b++){
        if( (b-a)%M==0 )printf("\n");
        printf("%2d",*b);
    }
    printf("\n\n");
}
void print2(int a[][M]){
    int i,j;
    for(i=1;i<17;i++){
        for(j=1;j<17;j++){
            if(a[i][j]==-1)
                printf(" *");
            else if(a[i][j]==0)
                printf(" .");
            else
                printf("%2d",a[i][j]);
        }
       printf("\n");
    }
}
void init(int a[][M]){//二维数组做形参，一行的元素个数必须是已知的。如果深究的话，二维数组的内存动态分配也挺麻烦的。
    int i,j;
    int n; int mun; int m=0;
    int b[M][M];//这是一个临时数组，用来存储计算的周围雷数目的。
//定义的时候与a是一样的都是18而不是16，这里是个技巧是，为了统计计算雷公式。
    for(i=0;i<M;i++){
        for(j=0;j<M;j++)
            a[i][j]=b[i][j]=0;
    }
    mun=40;
    srand((int)time(0));
    while(m!=40){//由于随机可能产生，相同的（i,j）这样保证能有40个雷。
        mun=40-m; m=0;
        for(n=0;n<mun;n++){
            i=1+rand()%16; j=1+rand()%16; a[i][j]=-1;
        }
        for(i=0;i<M;i++)
            for(j=0;j<M;j++)
                if(a[i][j]==-1)m++;
    }
    for(i=1;i<M-1;i++){
        for(j=1;j<M-1;j++)
            b[i][j]=abs(a[i-1][j-1]+a[i-1][j]+a[i-1][j+1]+a[i][j-1]+a[i][j+1]+a[i+1][j-1]+a[i+1][j]+a[i+1][j+1]);
    }
    for(i=0;i<M;i++)
        for(j=0;j<M;j++){
            if(!a[i][j])
                a[i][j]+=b[i][j];
        }
    
}

void main(){
    int a[M][M];
    init(a);
	print2(a);
}