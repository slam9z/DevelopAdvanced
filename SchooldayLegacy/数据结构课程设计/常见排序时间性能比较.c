
#include "stdio.h"   
#include "stdlib.h"   
#include "string.h"   
#include "malloc.h"
#include "time.h"   
#define Max 100
#define Maxsize 1000

int n;
long com[6];
long exc[6];


/*直接插入排序
无哨兵
*/
void InsertSort(int a[]){
	int i,j,temp;
	for (i =1; i<n; i++) {
        temp = a[i]; 
		exc[0]++;
        for (j = i-1;j>-1&&a[j] >temp ; j--) {
			a[j+1] = a[j]; 
			a[j] = temp;
			com[0]++;
			exc[0]=exc[0]+2;
		}
	}
	
	for(i=0;i<n;i++){
		printf("%d ",a[i]);
		if(i%18==0&&i!=0)
			printf( "\n");
	}
		printf( "\n");
}

/*直接选择排序*/
void SelectSort(int a[]){
     int i,j,min,temp; 
     for(i=0;i<n;i++) {
          min=i; 
          for(j=i+1;j<n;j++) {
               if(a[min]>a[j]) {
                temp=a[i]; 
                a[i]=a[j]; 
                a[j]=temp;
			
				 exc[1]++;
               }
			   com[1]++;
          }
    }
	 for(i=0;i<n;i++){
		 printf("%d ",a[i]);
		 if(i%18==0&&i!=0)
			printf( "\n");	
	 }
	 printf( "\n");

}




/* 冒泡排序 */

void BubbleSort(int a[]){
	int i,j,k;
    for(j=0;j<n;j++){
          for(i=0;i<n-j-1;i++){
               if(a[i]>a[i+1]){
                    k=a[i];
                    a[i]=a[i+1];
                    a[i+1]=k;
					exc[2]++;
               }
			   com[2]++;
          }
     }

	for(i=0;i<n;i++){
		printf("%d ",a[i]);
		if(i%18==0&&i!=0)
			printf( "\n");

	 }
	printf( "\n");
}


/* 希尔排序 */
void ShellSort(int a[]){
     int gap,i,j,temp;
     for(gap=n/2;gap>0;gap /= 2) {
          for(i=gap;i<n;i++) {
               for(j=i-gap;(j >= 0) && (a[j] > a[j+gap]);j -= gap ){
                temp=a[j];
                a[j]=a[j+gap];
                a[j+gap]=temp;
				com[3]++;
				exc[3]++;
               }
          }
     }
	 for(i=0;i<n;i++){
		 printf("%d ",a[i]);
		 if(i%18==0&&i!=0)
			 printf( "\n");
	 }
	printf( "\n");
}

 /* 堆排序 */

void HeapAdjust(int data[],int s,int m){ 
     int j,rc; 
     rc=data[s];    
     for(j=2*s;j<=m;j*=2) {     
		 if(j<m && data[j]<data[j+1]) {
			  com[4]++;
			  j++;
		 }
          if(rc>data[j]) 
			  break; 
          data[s]=data[j];  
          s=j; 
		  exc[4]++;
		 }
    data[s]=rc;     
}

void HeapSort(int a[]){
     int i,temp; 
     for(i=n/2;i>0;i--)  
     
      HeapAdjust(a,i,n);
     
     for(i=n;i>1;i--){
      temp=a[1];    
      a[1]=a[i]; 
      a[i]=temp;   
      HeapAdjust(a,1,i-1);
     }
	 
	 for(i=1;i<=n;i++){
		  printf("%d ",a[i]);
		  if(i%18==0&&i!=0)
			printf( "\n");
	 }
	 printf( "\n");
}

/*快速排序*/



int Partition(int data[],int low,int high){
	int mid;
	data[0]=data[low];
	mid=data[low];
	while(low < high){
		while((low < high) && (data[high] >= mid)){
	        com[5]++;
			--high;
		}
		data[low]=data[high]; 
		exc[5]++;
		while((low < high) && (data[low] < mid)){
			com[5]++;
			++low;
		}
		data[high]=data[low];
	}
	data[low]=data[0];   
	return low;    
}


void Quick(int data[],int low,int high){
	int mid;
	if(low<high){
		mid=Partition(data,low,high);
		Quick(data,low,mid-1); 
		Quick(data,mid+1,high);
	}
}

void QuickSort(int b[]){
	int i;
	Quick(b,1,n); 
	for(i=1;i<=n;i++){
		printf("%d ",b[i]);
		if(i%18==0&&i!=0)
			printf( "\n");
	}
	printf( "\n");
}

/*归并排序*/



void Merge(int sr[],int tr[],int i,int m,int l){
	int s;
	int r;
	int j;
	int k;
	
	for(j=m+1,k=i;i<=m&&j<=l;++k){
		if(sr[i]<sr[j]){
			tr[k]=sr[i];
			i++;
		}
		else {
			tr[k]=sr[j];
			j++;
		
		}
		com[6]++;
		exc[6]++;
	}
	if(i<=m){
		for(s=k,r=i;s<=l&&r<=m;r++){
	        tr[s]=sr[r];
		    s++;
		    exc[6]++;
		}
	   
	}
	if(j<=l){
		for(s=k,r=j;s<=l&&j<=l;r++){
	        tr[s]=sr[r];
		    s++;
		    exc[6]++;
	   }
	}
}



void MSort(int sr[],int tr1[] ,int s,int t){
	
	int m=0;
	int tr2[Max]={0};

	if(s==t)
		tr1[s]=sr[s];
	else{
		m=(s+t)/2;
	    MSort(sr,tr2,s,m);
		MSort(sr,tr2,m+1,t);
		Merge(tr2,tr1,s,m,t);
	}
}

void MergeSort(int a[]){
	int b[Max]={0};
	int i;
	MSort(a,b,1,n);
		 for(i=1;i<=n;i++){
		     printf("%d ",b[i]);
			 if(i%18==0&&i!=0)
			     printf( "\n");
		 }
		 printf( "\n");
}


void Sort(int a[]){
	int b[Max];
	int i;
	for( i=0; i<n;i++)
		b[i]=a[i];
	printf("直接插入排序结果为：\n");
	InsertSort(b);
	
	for( i=0; i<n;i++)
		b[i]=a[i];
	printf("直接选择排序结果为：\n");
	SelectSort(b);

	for( i=0; i<n;i++)
		b[i]=a[i];
			 
	printf("冒泡排序结果为：\n");
	BubbleSort(b);
	
	for( i=0; i<n;i++)
		b[i]=a[i];
	printf("希尔排序结果为:\n");
	ShellSort(b);  
	
	for( i=0; i<n;i++)
	    b[i+1]=a[i];
	printf("堆排序结果为\n");
	HeapSort(b);
		     
	for( i=0; i<n;i++)
		b[i+1]=a[i];
	printf("快速排序结果为:\n");
	QuickSort(b);
	
	for( i=0; i<n;i++)
		b[i+1]=a[i];
	printf("归并排序结果为:\n");
	MergeSort(b);
	
	printf("\n数据数目:%d\n",n);
	printf(  "直接插入排序: 比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[0],exc[0],com[0]+exc[0]);
	printf(  "直接选择排序: 比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[1],3*exc[1],com[1]+3*exc[1]);
	printf(  "冒泡排序:     比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[2],3*exc[2],com[2]+3*exc[2]);
	printf(  "希尔排序:     比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[3],3*exc[3],com[3]+3*exc[3]);
	printf(  "堆排序:       比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[4],exc[4],com[4]+exc[4]);
	printf(  "快速排序:     比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[5],2*exc[5],com[5]+2*exc[5]);
	printf(  "归并排序:     比较次数为:%5ld 移动次数为:%6ld 基本操作次数为：%ld\n",com[6],exc[6],com[6]+exc[6]);
	for(i=0;i<7;i++){
		com[i]=0;
		exc[i]=0;
			
	}
			  
}




void main(){ 
	int a[Max];
   
	int m;
	int i,j,k;
	    
    printf("请输入数据数目(<100)数目为0退出程序\n");
	scanf("%d",&n);

	while(n!=0){
		printf("请选择测试数据类型:1:正序 2:逆序 3:随机 其它则返回上一层(4:清屏)\n");
		scanf("%d",&m);
	
		switch(m){
		case 1:
			printf("\n排序前正序数据为：\n");
			srand( (unsigned)time( NULL ) );     
			 for( i= 0; i <n;i++ ){
				 a[i]=rand()%Maxsize;
				
			 }

			 for(j=0;j<n;j++){
				 for(i=0;i<n-j-1;i++)  
				 {
					if(a[i]>a[i+1]){
                     k=a[i];
                    a[i]=a[i+1];
                    a[i+1]=k;
					 }
				 }
			 }
			 
			 for( i=0; i <n;i++ ){
			     printf( "%d ",a[i]);
				 if(i%19==0&&i!=0)
			         printf( "\n");
			 }
			 printf( "\n");
			 Sort(a);

			 break;
         case  2:
			 printf("\n排序前逆序数据为：\n");
			 srand( (unsigned)time( NULL ) );     
			  for( i=0; i<n;i++ ){
				 a[i]=rand()%Maxsize;
				
			 }
			 for(j=0;j<n;j++){
				 for(i=0;i<n-j-1;i++)  
				 {
					 if(a[i]<a[i+1]){
                     k=a[i];
                     a[i]=a[i+1];
                     a[i+1]=k;
					 }
				 }
			 }
			 for( i=0; i <n;i++ ){
			      printf( "%d ",a[i]);
				  if(i%19==0&&i!=0)
			          printf( "\n");
			 }
			 printf( "\n");
			 Sort(a);
		     break;
         case  3:
			 printf("\n排序前的随机数据为：\n");
			 srand( (unsigned)time( NULL ) );     
			 for( i= 0; i <n;i++ ){
				 a[i]=rand()%Maxsize;
				 printf( "%d ",a[i]);
				 if(i%19==0&&i!=0)
			         printf( "\n");
				 
			 }
			 printf( "\n");
             Sort(a);
			 break;
		  case 4:
			  system("cls");
			  printf("数据数目:%d\n",n);
			
        default:
			 break;
		}

		if(m<1||m>4){
			printf("请输入数据数目,数目为0退出程序\n");
			scanf("%d",&n);
		}
	}
}



