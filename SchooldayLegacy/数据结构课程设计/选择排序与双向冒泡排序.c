#include "stdlib.h"
#include "stdio.h"
#include "malloc.h"
#define null 0
#include "stdio.h"   
#define Max 100


struct List
{
	int data;
	struct List *next;
}List;

int  m=1;
int n;


struct List * InitList(int a[])
{
	struct List *head,*p,*q;
	int i;
	head=(struct List *)malloc(sizeof(struct List));
	head->next=null; 
	q=head;
	for(i=0;i<n;i++){
	   p=(struct List *)malloc(sizeof(struct List));
	   p->next=null;
	   p->data=a[i];
	   q->next=p;
	   q=p;
	   }
	return head;
}

void print(struct List *head)
{//输出当前链表
	struct List *p;
	if(head)
	{
	   p=head->next; 
	   while(p) 
	   {
		printf("%4d ",p->data);
		p=p->next;
	   }
	   printf("\n");
	}
}

void SelectSort (struct List *head)
{//用选择法进行排序
	struct List *p,*q;
	int t;
	for(p=head->next;p->next;p=p->next){
		for(q=p->next;q;q=q->next)
		   if(p->data>q->data)
			{
				t=p->data;
				p->data=q->data;
				q->data=t;
			}

		  printf("第%d趟排序后的序列:\n",m);
			  print(head);

		  m++;
        }
	
}

void BubbleSort (int a[]){

	int flag=-1;
	int i,j;
	int k;

	
	for(j=1;j<n;j++){
	
	    flag=flag*-1;
		if(flag==1){
			for(i=j/2;i<=n-2-j/2;i++){
					if(a[i]>a[i+1]){
                    k=a[i];
                    a[i]=a[i+1];
                    a[i+1]=k;
					}
				}
				printf( "\n第%d趟排序后的序列为:\n",j);
		        for(i=0;i<n;i++){
					printf( "%4d ",a[i]);
					if(i%18==0&&i!=0)
						printf( "\n");
				}
			}
			else{
				for(i=n-1-j/2;i>=j/2;i--)  {
					if(a[i]<a[i-1]){
						k=a[i-1];
						a[i-1]=a[i];
						a[i]=k;
					}
				}
				printf( "\n第%d趟排序后的序列为:\n",j);
				for(i=0;i<n;i++){
					printf( "%4d ",a[i]);
					if(i%18==0&&i!=0)
						printf( "\n");
				}
			}
			
		}
	}



void main(){
	struct List *head;
	int a[Max];

	int i;
	printf("请输入数据数目(<99)数目为0退出程序\n");
	scanf("%d",&n);
	printf("请输入%d个数据\n",n);
	while(n!=0){
		for(i=0;i<n;i++){
			scanf("%d",&a[i]);
		}

	
		head=InitList(a);
		printf("选择排序的数列每趟调整结果（链表存储）\n");
		SelectSort(head);

		printf("\n双向冒泡排序的数列每趟调整结果（数组存储)");
	
		BubbleSort(a);
		printf("\n请输入数据数目,数目为0退出程序\n");
		scanf("%d",&n);
		printf("请输入%d个数据\n",n);
	}
}





