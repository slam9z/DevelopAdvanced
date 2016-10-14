#include<stdio.h>
#include<string.h>

void reverse_str(char* buf, int n)
{
	if(n<2) return;
	char tmp = buf[0];
	buf[0] = buf[n-1];
	buf[n-1] = tmp;
	reverse_str(buf+1, n-2);
}


void main(){
	char buf[10]="abcsssdsd";

	reverse_str(buf,strlen(buf));
	puts(buf);

}