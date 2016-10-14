#include<stdio.h>
#include<string.h>
char mj[26];

char * init(char *ms){
	
	int flags[26];
	int i;
    int len;
	len=strlen(ms);
	memset(flags,0,sizeof(flags));

	for(i=0;i<len;i++)
		mj[i]=ms[i];

	for(i=0;i<26;i++){
		flags[ms[i]-'a']=1;
	}
	
	for(i=0;i<26;i++)
		if(flags[i]==0){
			mj[len++]=i+'a';
		}
	mj[25]='\0';


	return mj;
}
int search(char a){
	int i;
	for(i=0;i<26;i++)
		if(a==mj[i])
			return i;
	return -1;
}

void main(){
	char ms[26];
	char mw[100];
	char mc[100];
	int i;
	int j;
	int len;
	scanf("%s",ms);
	scanf("%s",mw);
	init(ms);
	len=strlen(mw);

	mc[len]='\0';
	if(len%2){
		len=len-1;
		mc[len]=mw[len];
	}

	
	for(i=0,j=1;j<len;i=i+2,j=j+2){
		if(mw[i]==mw[j]||(search(mw[i])==-1||search(mw[j])==-1)){
			mc[i]=mw[i];
			mc[j]=mw[j];
			continue;
		}
		else if(search(mw[i])/5==search(mw[j])/5){
			mc[j]=mw[i];
			mc[i]=mw[j];
			continue;
		}
		
		else {
			mc[i]=mj[search(mw[i])/5*5+search(mw[j])%5];
			mc[j]=mj[search(mw[j])/5*5+search(mw[i])%5];
		}
		
		
	}
	puts(mc);
}
	

