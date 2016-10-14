#include <malloc.h>  
#include <stdio.h>  
#include <stdlib.h>  
#include <string.h>  
#define NULL 0 
#define MaxSize 30 

typedef struct athletestruct /*运动员*/ 
{ 
 char name[20];  
 int score; /*分数*/ 
 int range; /**/ 
 int item; /*项目*/ 
}ATH; 
typedef struct schoolstruct /*学校*/ 
{ 
 int count; /*编号*/ 
 int serial; /**/  
 int menscore; /*男选手分数*/ 
 int womenscore; /*女选手分数*/ 
 int totalscore; /*总分*/ 
 ATH athlete[MaxSize]; /**/ 
 struct schoolstruct *next;  
}SCH; 

int nsc,msp,wsp;  
int ntsp;  
int i,j;  
int overgame;  
int serial,range;  
int n;  
SCH *head,*pfirst,*psecond;  
int *phead=NULL,*pafirst=NULL,*pasecond=NULL;  

void input () 
{ 
	void create() ;
 char answer;  
 head = (SCH *)malloc(sizeof(SCH)); /**/ 
 head->next = NULL; 
 pfirst = head;  
 answer = 'y'; 
 while ( answer == 'y' ) 
 { 
Is_Game_DoMain: 
 printf("\nGET Top 5 when odd\nGET Top 3 when even"); 
 printf("\n输入运动项目序号 (x<=%d):",ntsp); 
 scanf("%d",pafirst); 
 overgame = *pafirst; 
 if ( pafirst != phead ) 
 { 
 for ( pasecond = phead ; pasecond < pafirst ; pasecond ++ ) 
 { 
 if ( overgame == *pasecond ) 
 { 
 printf("\n这个项目已经存在请选择其他的数字\n"); 
 goto Is_Game_DoMain; 
 } 
 } 
 } 
 pafirst = pafirst + 1; 
 if ( overgame > ntsp ) 
 { 
 printf("\n项目不存在"); 
 printf("\n请重新输入"); 
 goto Is_Game_DoMain; 
 } 
 switch ( overgame%2 ) 
 { 
 case 0: n = 3;break; 
 case 1: n = 5;break; 
 } 
 for ( i = 1 ; i <= n ; i++ ) 
 { 
Is_Serial_DoMain: 
 printf("\n输入序号 of the NO.%d (0<x<=%d): ",i,nsc); 

 scanf("%d",&serial); 
 if ( serial > nsc )  
 { 
 printf("\n超过学校数目,请重新输入"); 
 goto Is_Serial_DoMain; 
 } 
 if ( head->next == NULL )  
 { 
 create(); 
 } 
 psecond = head->next ;  
 while ( psecond != NULL )  
 { 
 if ( psecond->serial == serial ) 
 { 
 pfirst = psecond; 
 pfirst->count = pfirst->count + 1; 
 goto Store_Data; 
 } 
 else 
 { 
 psecond = psecond->next; 
 } 
 } 


 create(); 
Store_Data: 

 pfirst->athlete[pfirst->count].item = overgame; 
 pfirst->athlete[pfirst->count].range = i; 
 pfirst->serial = serial; ("Input name:) : "); 

 scanf("%s",pfirst->athlete[pfirst->count].name); 
 } 
 printf("\n继续输入运动项目(y&n)？"); 
 answer = getchar(); 
 printf("\n"); 
 } 
} 

void calculate() /**/ 
{ 
 pfirst = head->next; 
 while ( pfirst->next != NULL ) 
 { 
 for (i=1;i<=pfirst->count;i++) 
 { 
 if ( pfirst->athlete[i].item % 2 == 0 )  
 { 
 switch (pfirst->athlete[i].range) 
 { 
 case 1:pfirst->athlete[i].score = 5;break; 
 case 2:pfirst->athlete[i].score = 3;break; 
 case 3:pfirst->athlete[i].score = 2;break; 
 } 
 } 
 else  
 { 
 switch (pfirst->athlete[i].range) 
 { 
 case 1:pfirst->athlete[i].score = 7;break; 
 case 2:pfirst->athlete[i].score = 5;break; 
 case 3:pfirst->athlete[i].score = 3;break; 
 case 4:pfirst->athlete[i].score = 2;break; 
 case 5:pfirst->athlete[i].score = 1;break; 
 } 
 } 
 if ( pfirst->athlete[i].item <=msp )  
 { 
 pfirst->menscore = pfirst->menscore + pfirst->athlete[i].score; 
 } 
 else  
 { 
 pfirst->womenscore = pfirst->womenscore + pfirst->athlete[i].score; 
 } 
 } 
 pfirst->totalscore = pfirst->menscore + pfirst->womenscore; 
 pfirst = pfirst->next; 
 } 
} 

void output() 
{ 
 pfirst = head->next; 
 psecond = head->next; 
 while ( pfirst->next != NULL )  
 { 
// clrscr(); 
 printf("\n第%d号学校的结果成绩:",pfirst->serial); 
 printf("\n\n项目的数目\t学校的名字\t分数"); 
 for (i=1;i<=ntsp;i++)  
 { 
 for (j=1;j<=pfirst->count;j++)  
 { 
 if ( pfirst->athlete[j].item == i ) 
 { 
  
  
 printf("\n %d\t\t\t\t\t\t%s\n %d",i,pfirst->athlete[j].name,pfirst->athlete[j].score);break; 
  
 } 
 } 
 } 
 printf("\n\n\n\t\t\t\t\t\t按任意建 进入下一页"); 
 getchar(); 
 pfirst = pfirst->next; 
 } 
 //clrscr(); 
 printf("\n运动会结果:\n\n学校编号\t男运动员成绩\t女运动员成绩\t总分"); 
 pfirst = head->next; 
 while ( pfirst->next != NULL ) 
 { 
 printf("\n %d\t\t %d\t\t %d\t\t %d",pfirst->serial,pfirst->menscore,pfirst->womenscore,pfirst->totalscore); 
 pfirst = pfirst->next; 
 } 
 printf("\n\n\n\t\t\t\t\t\t\t按任意建结束"); 
 getchar(); 
} 

void create() 
{ 
  
 pfirst = (struct schoolstruct *)malloc(sizeof(struct schoolstruct)); 
 pfirst->next = head->next ; 
 head->next = pfirst ; 
  
 pfirst->count = 1; 
 pfirst->menscore = 0; 
 pfirst->womenscore = 0; 
 pfirst->totalscore = 0; 
} 
void Save() 
{FILE *fp; 
 if((fp = fopen("school.dat","wb"))==NULL) 
 {printf("can't open school.dat\n"); 
 fclose(fp); 
 return; 
 } 
 fwrite(pfirst,sizeof(SCH),10,fp); 
 fclose(fp); 
 printf("文件已经成功保存\n"); 
} 

void main() 
{ 
 system("cls"); 
 printf("\n\t\t\t 运动会分数统计\n"); 
 printf("输入学校数目 (x>= 5):"); 
 scanf("%d",&nsc);  
 printf("输入男选手的项目(x<=20):"); 
 scanf("%d",&msp);  
 printf("输入女选手项目(<=20):"); 
 scanf("%d",&wsp);  
 ntsp = msp + wsp;  

 phead = (int *)calloc(ntsp,sizeof(int)); 
 pafirst = phead; 
 pasecond = phead; 
 input(); 
 calculate();  
 output(); 
 Save(); 
}


