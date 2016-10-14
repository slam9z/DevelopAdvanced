#include "stdio.h"
#include "malloc.h"
#define MAX_VERTEX_NUM 20
#define Maxsize 20
#define VertextType char
#define FALSE 0
#define TRUE 1


/*定义图的邻接链表存储表示*/
typedef struct ArcNode{
	int  adjvex;
	struct ArcNode *nextarc;
}ArcNode; ArcNode *p;

typedef struct VNode{
	 
	VertextType data[6];
    ArcNode *firstarc;
}VNode,AdjList[MAX_VERTEX_NUM];

typedef struct{
    AdjList vertices;
	int indegree [MAX_VERTEX_NUM];
    int vexnum,arcnum;
}ALGraph;ALGraph *G;

/*定义顺序栈*/
typedef int datatype;
typedef struct{
	datatype date[Maxsize];
	int top;
}seqstack;seqstack*s;

/*构造空栈*/
void InitStack(seqstack *s){
	s->top=-1;
}

/*判栈空*/
int StackEmpty(seqstack *s){
	if(s->top>=0)
	    return FALSE;
	else 
		return TRUE;
}


seqstack* Push(seqstack *S,datatype x){
	if(s->top==Maxsize){
		printf("overflow");
		return NULL;
	}
	else{
		s->top++;
		s->date[s->top]=x;
	}
	return(s);
}

datatype Pop(seqstack *s){
	if(StackEmpty(s)){
		printf("underflow");
		return -1;
	}
	else{
		s->top--;
		return(s->date[s->top+1]);
	}
}







/*创建邻接表*/
void CteatadjList(ALGraph *G){
	int i,j,k;
    ArcNode*s;
    printf("please input the ALGraph's vexnum and arcnum\n");
    scanf("%d,%d",&G->vexnum,&G->arcnum); 

	printf("please input the ALGraph's %d vexnum's date \n",G->vexnum);
	for( k=0;k<G->vexnum;k++){
		scanf("%s",&G->vertices[k].data);
        G->vertices[k].firstarc=NULL;
		}
	printf("please input  %d arc'Tail and Head's num \n",G->arcnum);
	for( k=0;k<G->arcnum;k++){
		scanf("%d,%d",&i,&j);
		s=malloc(sizeof(ArcNode));
		s->adjvex=j;
        s->nextarc=G->vertices[i].firstarc;
        G->vertices[i].firstarc=s;
	}

}

/*求图定点的入度*/
void FindInDegree(ALGraph *G,int indegree[]){
	int i;
	for(i=0;i<G->vexnum;i++)
		indegree[i]=0;

	for(i=0;i<G->vexnum;i++){
		p=G->vertices[i].firstarc;
			while(p){
				
				indegree[p->adjvex]++;
				p=p->nextarc;
			}
	}
}

void TopologicalSort(ALGraph *G){
	int i,count,k;
	s=(seqstack*)malloc(sizeof(seqstack));
	FindInDegree(G,G->indegree);
	InitStack(s);
	for(i=0;i<G->vexnum;i++)
		if(G->indegree[i]==0)
			Push(s,i);

    count=0;
	while(!StackEmpty(s)){
		i=Pop(s);
		printf("%d,%s  ",i,G->vertices[i].data);
		count++;

		for(p=G->vertices[i].firstarc;p;p=p->nextarc){
				k=p->adjvex;
			if((--G->indegree[k])==0)
				Push(s,k);
		}

       /*
		p=G->vertices[i].firstarc;
			while(p){
				k=p->adjvex;
			if((--G->indegree[k])==0)
				Push(s,k);
				p=p->nextarc;
			}
		*/
	}
    
	if(count<G->vexnum)
		return FALSE;
	else
		return TRUE;
  
}
//这样只有一种结果，怎么办啊。






void main(){
	int k;
	G=(ALGraph*)malloc(sizeof(ALGraph));
	CteatadjList(G);
	FindInDegree(G,G->indegree);	
	for(k=0;k<G->vexnum;k++){
    printf("%d vertice's date:%s \n",k,G->vertices[k].data);
	printf("%d vertice's indegree:%d \n",k,G->indegree[k]);
	}
	TopologicalSort(G);
	/*
    p=G->vertices[k].firstarc;
	    while(p!=NULL){
			 printf("%d vertice's local:%d \n",k,p->adjvex);
			 p=p->nextarc;
		}
	}
    */
}





