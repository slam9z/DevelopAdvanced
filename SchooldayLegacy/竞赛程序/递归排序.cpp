#include<stdio.h>
#include<stdlib.h>
#define N 30
int a[N];

void sort(int n){
	int m;
	if(n==0)
		return;
	sort(n-1);
	if(a[n]<a[n-1]){
		m=a[n];
		a[n]=a[n-1];
		a[n-1]=m;
     	sort(n-1); 
	}
	

}
void print(int a[]){
	int i;
	for(i=0;i<N;i++)
		printf("%4d",a[i]);
	printf("\n");
}

void main(){
	int i;
	for(i=0;i<N;i++)
		a[i]=rand()%100;
	print(a);
	sort(N-1);
	print(a);
}
