#include<stdio.h>
#include<string.h>
#define N 8
#define B 99

long b2(char*a,int b){
	long ret=0;
	int len=strlen(a);
	int i;
	for(i=0;i<len;i++){
		if(a[i]-'0'>=b)
			return -1;
		ret*=b;
		ret+=a[i]-'0';
	}
	return ret;
}


void main(){
	char p[N],q[N],r[N];
	long p1,q1,r1;
	int b;
	int n;
	scanf("%d",&n);
	while(n--){
		scanf("%s%s%s",p,q,r);
		for(b=2;b<B;b++){
			p1=b2(p,b); q1=b2(q,b);  r1=b2(r,b); 
			if(p1==-1&&q1==-1&&r1==-1) continue;
			if(p1*q1==r1){printf("%d\n",b);break;}
		}
		if(b==B)printf("0\n");	
	}

}