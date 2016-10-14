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
}ArcNode; ArcNode *p;ArcNode *p1;

typedef struct VNode{
	VertextType data[4];
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

int show[Maxsize];
int g=-1;
int k=50;
int t=0;
int visited[Maxsize];

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

/*求图顶点的入度*/
void FindInDegree(ALGraph *G){
	int i;
	for(i=0;i<G->vexnum;i++)
		G->indegree[i]=0;

	for(i=0;i<G->vexnum;i++){
		p=G->vertices[i].firstarc;
			while(p){
				
				G->indegree[p->adjvex]++;
				p=p->nextarc;
			}
	}
}


void TopologicalSort(ALGraph *G){
	int i,m=0;
	ArcNode *s;
	int flag=0;
	for(i=0;i<G->vexnum;i++){
		if(visited[i]==0){
		flag=1;
		break;
		}
	}
	if(flag==0){  
			printf("[%d]: ",(t+1));
		for(i=0;i<G->vexnum;i++)
			printf("%s ",G->vertices[show[i]].data);
			printf("\n");
			t++;
	}
	else{	
		for(i=0;i<G->vexnum;i++){
			if(G->indegree[i]==0&&visited[i]==0){ 
				g++;
				show[g]=i;
				visited[i]=1;
				s=G->vertices[i].firstarc;
				while(s!=NULL){
						G->indegree[s->adjvex]--;
						s=s->nextarc;
				}
				TopologicalSort(G); 
				visited[i]=0;
				g--;
				p=G->vertices[i].firstarc;
				while(p){
					G->indegree[p->adjvex]++;
					p=p->nextarc;
				}
			}
		}
	}
}
void main(){
	int j,i,count,k;

	int indegree[Maxsize];
	G=(ALGraph*)malloc(sizeof(ALGraph));
	CteatadjList(G);
	FindInDegree(G);

	for( j=0;j<G->vexnum;j++)
		indegree[j]=G->indegree[j];

	s=(seqstack*)malloc(sizeof(seqstack));
	FindInDegree(G);
	InitStack(s);
	for(i=0;i<G->vexnum;i++)
		if(indegree[i]==0)
			Push(s,i);

    count=0;
	while(!StackEmpty(s)){
		i=Pop(s);
	
		count++;

		for(p=G->vertices[i].firstarc;p;p=p->nextarc){
				k=p->adjvex;
			if((--indegree[k])==0)
				Push(s,k);
		}
	}

	if(count<G->vexnum){
		printf("图中有环，无法进行拓扑排序\n");
		return ;
	}
	for( j=0;j<G->vexnum;j++)
	visited[j]=0;
	TopologicalSort(G);
	printf("共[%d]种可行方案",t);
}
	






