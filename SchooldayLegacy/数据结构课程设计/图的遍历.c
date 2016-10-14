#include<stdio.h>
#include<malloc.h>
#include<stdlib.h>
#include<stddef.h>

#define MAX 50
//邻接矩阵节点类型定义
typedef char vextype;
typedef struct {
   vextype vexs[MAX];
   int arcs[MAX][MAX];
   int vexnum;
   int arcnum;
}MGraph;
 MGraph *G;
//队列的类型描述
typedef struct{
    int data[MAX];
    int front,rear;
  }queue;
   queue *q;
//邻接矩阵建图
int LocateVex(MGraph *G,int v){
    int i=0;
    while(G->vexs[i]!=v){
        i++;
    }  
    return i;
}
void CreateUDN(MGraph *G){
    int i,j,k;

	printf("\n请输入图的顶点数目:");
    scanf("%d",&G->vexnum);
    printf("\n请输入图边的数目:");
    scanf("%d",&G->arcnum);
	printf("\n请输入%d个顶点的信息:\n",G->vexnum);
	getchar();
	for(i=0;i<G->vexnum;i++){
		printf("\n请输入第%d个顶点的信息:",i+1);
		G->vexs[i]=getchar();
		getchar();		 
	}
	printf("\n打印确认输入的顶点是否准确:\n");
	for(i=0;i<G->vexnum;i++){
		 printf("%2c   ",G->vexs[i]);
	}
	printf("\n\n");
	for(i=0;i<G->vexnum;i++){
		for(j=0;j<G->vexnum;j++)
			G->arcs[i][j]=0;
	}
	for(k=0;k<G->arcnum;k++){
		printf("请输入第%d条边所在行,列:",k+1);
		scanf("%d%d",&i,&j);
		printf("\n");
		G->arcs[i][j]=G->arcs[j][i]=1;	
	}
}	

//队列置空
void setnull(queue *q){
    q->front=q->rear=-1;
}
//判队列空
int empty(queue *q){
    if(q->front==q->rear){
		return 1;
    }
    else
		return 0;
}
//入队操作
void enqueue(queue *q,int x){
    if(q->rear<G->vexnum-2){
        q->rear++;
        q->data[q->rear]=x;
      }
}
//出队操作
int dequeue(queue *q){
	
    if(!empty(q)){
		q->front++;
		return 	q->data[q->front];	    
    }
	else 
	   return 0;
}

//BFS广度优先遍历图

int visited[MAX]; 
void BFS(MGraph *G,int k){
    int i=0 ,j; 
   
    setnull(q);
	visited[k]=1;
    enqueue(q,k);
    while(!empty(q)){
		i=dequeue(q);
		for(j=0;j<G->vexnum-1;j++){
			if(G->arcs[i][j]==1&&visited[j]!=0){
				printf("(%c,%c)",G->vexs[i],G->vexs[j]);
				visited[j]=1;
				enqueue(q,j);
			}
		}
	}
}

 
//DFS深度优先遍历图
void DFS(MGraph *G,int i){
	int j=0;
    visited[i]=1;
    for(j=0;j<G->vexnum;j++){
		if(G->arcs[i][j]==1&&visited[j]==0){
			printf("(%c,%c)",G->vexs[i],G->vexs[j]);
			DFS(G,j);
		}
    }

}
//**********************************  main   函数*****************
void main(){
	G=(MGraph *)malloc(sizeof(MGraph));
	q=(queue *)malloc(sizeof(queue));
	printf("******************************************************\n");
	printf("************请按提示输入要遍历的无向连通图************\n");
	printf("******************************************************\n");
	CreateUDN(G);
	printf("\n无向连通图深度优先遍历生成树的每一条边为:\n");
	printf("******************************************************\n");
	DFS(G,0);
	printf("\n");
	printf("******************************************************\n");	
	printf("\n无向连通图广度优先遍历生成树的每一条边为:\n");
	printf("******************************************************\n");
	BFS(G,0);
	printf("\n");
	printf("******************************************************\n");

}
    