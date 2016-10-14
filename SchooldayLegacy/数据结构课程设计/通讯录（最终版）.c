#include "stdio.h"
#include "malloc.h"
#include "string.h"
#define  maxsize 12
#define  size 100

/*定义节点*/

typedef struct node{
	char phonenum[maxsize];
	char name[maxsize];
    struct node*  next;
} phonelist;

struct node2{
	 char name[maxsize];
    char phonenum[maxsize];
 } phonearray[size];

phonelist* head;

phonelist* creatphonelist(){
	phonelist *p,*s;
	printf("***********创建新通讯录***********\n");
	printf("请依次输入姓名和号码以#结束输入\n");
	while(1){
		s=malloc(sizeof(phonelist));
		scanf("%s",s->name);
		
		if(s->name[0]=='#')
			break;
		scanf("%s",s->phonenum);
		p=head;
		s->next=p->next;
		p->next=s;
	}
	return(head);
}

void output(phonelist *head){
	phonelist *p,*q;
	char nametemp[maxsize];
    char phonenumtemp[maxsize];

	for(p=head->next;p->next;p=p->next){
		for(q=p->next;q;q=q->next)
		   if((strcmp(p->name,q->name))>0)
			{
				strcpy(nametemp,p->name);
		    	strcpy(p->name,q->name);
				strcpy(q->name,nametemp);
				strcpy(phonenumtemp,p->phonenum);
		    	strcpy(p->phonenum,q->phonenum);
				strcpy(q->phonenum,phonenumtemp);
			}
	}
	p=head->next;

	while(p!=NULL){
		printf("%10s%15s\n",p->name,p->phonenum);
		p=p->next;
	}
	
}

void insert(phonelist *head){
	phonelist *p,*s;
	s=malloc(sizeof(phonelist));
	printf("请依次输入姓名和号码\n");
	scanf("%s%s",s->name,s->phonenum);
	p=head;
	while(p->next!=NULL){
	    p=p->next;
	}
	s->next=p->next;
	p->next=s;
}

void delete(phonelist *head,char name[]){
	phonelist *p,*s;
	
	p=head;
	while(p->next!=NULL)
		if(strcmp(name,p->next->name)!=0)
	    p=p->next;
		else
		break;
		
		if(p->next!=NULL){
			s=p->next;
			p->next=s->next;
			printf("%s已从通讯录中删除\n",name);
		}
		else
			printf("%s不存在通讯录中\n",name);
}


phonelist *search(phonelist *head,char name[]){
	phonelist *p;
	p=head->next;
	while(p!=NULL&&strcmp(name,p->name)!=0)
		p=p->next;
	return p;
}


void save(phonelist *head){
	int i=0;
	FILE *out;
	phonelist *p;
	if((out=fopen("通讯录","wb"))==NULL){
			printf("打不开文件\n");
			return;
	}

	p=head->next;
	while(p!=NULL){
		strcpy(phonearray[i].name,p->name);
		strcpy(phonearray[i].phonenum,p->phonenum);
		if(fwrite(&phonearray[i],sizeof(struct node2),1,out)!=1)
		    printf("写入信息失败\n");
		i++;
		p=p->next;
	}
	fclose(out);
}

int open(){
	int i=0;

	phonelist *p,*s;

	FILE*open;
	long  siz = 0;

	if((open=fopen("通讯录","rb"))==NULL){
		printf("*********通讯录文件不存在*********\n");
		return 0;
	}
	 
	if (open) 
      fseek(open, 0, SEEK_END);
      siz = ftell(open);
	
	 fseek(open, 0, 0);

	 if(siz%(long)sizeof(struct node2)!=0){
		 printf("*********通讯录文件被修改*********\n");
		 return 0;
	 }

	while(i<siz/(long)sizeof(struct node2)){
		fread(&phonearray[i],sizeof(struct node2),1,open);
		s=malloc(sizeof(phonelist));
		strcpy(s->name,phonearray[i].name);
		strcpy(s->phonenum,phonearray[i].phonenum);
		p=head;
		s->next=p->next;
		p->next=s;
	    i++;
	}
	fclose(open);
	printf("***********恢复旧通讯录***********\n");
	return 1;
}

	
void main(){
	phonelist *s;

	char name[maxsize];
	int i=0;
	int choice=10;
	head=malloc(sizeof(phonelist));
    head->next=NULL;

	if(!open())
		head=creatphonelist();
	printf("1*********输出所有联系人**********\n");
	printf("2***********添加联系人************\n");
	printf("3************删除联系人***********\n");
	printf("4*********查询联系人信息**********\n");
	printf("5*********修改联系人信息**********\n");
	printf("6*************清屏****************\n");
	printf("0**********结束输入***************\n");
			
	while(choice!=0){
	 
	
	
		scanf("%d",&choice);
		switch(choice){
		case 1:
			output(head);
			break;
		case 2:
			insert(head);
		
			break;
		case 3:	
			printf("请输入要删除的联系人的姓名\n");
			scanf("%s",&name);
			delete(head,name);
		
			break;
		case 4:	
			printf("请输入要查询的联系人的姓名\n");
			scanf("%s",&name);
			s=search(head,name);
			if(s!=NULL)
				printf("%10s%15s\n",s->name,s->phonenum);
			else
				printf("%s不存在通讯录中\n",name);
			break;
       case 5:	
			printf("请输入要修改的联系人的姓名\n");
			scanf("%s",&name);
			s=search(head,name);
			if(s!=NULL){
				printf("请输入新的号码\n");
		    	scanf("%s",&s->phonenum);
			}
			else
				printf("%s不存在通讯录中\n",name);
			break;
	   case 6:	
			system("cls");
		  	break;
	   case 7:	
			printf("1*********输出所有联系人**********\n");
			printf("2***********添加联系人************\n");
			printf("3************删除联系人***********\n");
			printf("4*********查询联系人信息**********\n");
			printf("5*********修改联系人信息**********\n");
			printf("6*************清屏****************\n");
			printf("0**********结束输入***************\n");
		
		    break;
		case 0:
			break;
		default:
			 break;
		}
	 printf("1:显示 2:添加 3:删除 4 :查询\n5:修改 6:清屏 7:显示菜单 0:结束程序\n ");
	}
	save(head);
}