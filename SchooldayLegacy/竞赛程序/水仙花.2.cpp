#include <iostream>
#include <time.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

using namespace std;

char *multi(char *p, char *q);//大乘法
void chushi();//将nums和value两个数组初始化
char *convert(int a);//将整形转换成字符型
void add(char* A0,char* A1,char* A2,char* A3,char* A4,char* A5,char* A6,char* A7,char* A8,char* A9,char* c);
//将A0 - A9全部加起来，结果放置到c中
bool judge(int i0, int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, char *p);
//判断i0 - i9枚举出现的次数跟p中出现的次数是否一致，如果一致则说明该数为水仙花数

char nums[10][22];//存放0-9的21次方
char value[10][22][80];//存放0-9出现次数的和，例如0如果在水仙花数中出现的次数为1次，则为0 ， 或者2在水仙花数出现次数为2，则是
//2^21*2（2的21次方乘以2）以此类推..就可以得到
/**
 * 0^21*0 0^21*1 0^21*2 0^21*3.......0^21*9
 * 1^21*0 1^21*1 0^21*2..............1^21*9
 * 2^21*0......按照这个规律下去，以便以后好使用
 */

void main()
{
	//程序执行的时间大概需要25s左右

	int start = clock();//计时开始

	int count = 0;//已经知道了21位水仙花数有两个，所以当count==2的时候结束程序... 当然你也可以不这样，全部循环完大概要40s左右吧

	chushi();

	char ans[30];//存放水仙花数的

	//用枚举的方法枚举出0-9在水仙花数中出现的次数...	
	//咋一看如此多代码，其实很好理解，将0 - 9 出现的次数分析出来，所以就写了10个循环...

for(int i9=0;i9<=9;i9++)//枚举从9开始，因为9出现的次数最多只能是9次，如果出现10次则9^21是22位数
 {
  for(int i0=0;i0<=20;i0++)//第一个不能为0，所以0出现的最多次数位20次，其余的皆可出现21次
  {
   if(i0+i9==21)
   {
    add(value[0][i0],value[1][0],value[2][0],value[3][0],value[4][0],value[5][0],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
   //      在这里就用到了先前存放的0 - 9出现次数的21次方，0出现了i0次,1出现了0次.....9出现了i9次。
	if(ans[0]!='0' && judge(i0,0,0,0,0,0,0,0,0,i9,ans))  //ans[0]!='0'保证ans为21位
    {
     cout<<ans<<endl;
     count++;
	 /*
     if(count==2)
     {
		 int end = clock();
		 cout<<"用时："<<end - start<<"毫秒"<<endl;
		 return;
     }
	 */
    }
    break; //因为i0+i9的次数已经是21次了，如果再多一次的话那肯定就超过了21次...所以直接跳出循环..
   }



   for(int i1=0;i1<=21;i1++)
   {
    if(i0+i1+i9==21)
    {
     add(value[0][i0],value[1][i1],value[2][0],value[3][0],value[4][0],value[5][0],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
     if(ans[0]!='0' && judge(i0,i1,0,0,0,0,0,0,0,i9,ans))
     {
      cout<<ans<<endl;
      count++;
      if(count==2)
      {
		  int end = clock();
		  cout<<"用时："<<end - start<<"毫秒"<<endl;
		  return ;
      }
     }
     break;
    }
    for(int i2=0;i2<=21;i2++)
    {
     if(i0+i1+i2+i9==21)
     {
      add(value[0][i0],value[1][i1],value[2][i2],value[3][0],value[4][0],value[5][0],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
      if(ans[0]!='0' && judge(i0,i1,i2,0,0,0,0,0,0,i9,ans))
      {
       cout<<ans<<endl;
       count++;
       if(count==2)
       {
		   int end = clock();
		   cout<<"用时："<<end - start<<"毫秒"<<endl;
	       return ;
       }
      }
      break;
     }
     for(int i3=0;i3<=21;i3++)
     {
      if(i0+i1+i2+i3+i9==21)
      {
       add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][0],value[5][0],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
       if(ans[0]!='0' && judge(i0,i1,i2,i3,0,0,0,0,0,i9,ans))
       {
        cout<<ans<<endl;
        count++;
        if(count==2)
        {
			int end = clock();
			cout<<"用时："<<end - start<<"毫秒"<<endl;
			return ;
        }
       }
       break;
      }
      for(int i4=0;i4<=21;i4++)
      {
       if(i0+i1+i2+i3+i4+i9==21)
       {
        add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][i4],value[5][0],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
        if(ans[0]!='0' && judge(i0,i1,i2,i3,i4,0,0,0,0,i9,ans))
        {
         cout<<ans<<endl;
         count++;
         if(count==2)
         {
			int end = clock();
			cout<<"用时："<<end - start<<"毫秒"<<endl;
			return ;
         }
        }
        break;
       }
       for(int i5=0;i5<=21;i5++)
       {
        if(i0+i1+i2+i3+i4+i5+i9==21)
        {
         add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][i4],value[5][i5],value[6][0],value[7][0],value[8][0],value[9][i9],ans);
         if(ans[0]!='0' && judge(i0,i1,i2,i3,i4,i5,0,0,0,i9,ans))
         {
          cout<<ans<<endl;
          count++;
          if(count==2)
          {
			int end = clock();
			cout<<"用时："<<end - start<<"毫秒"<<endl;
			return ;
          }
         }
         break;
        }
        for(int i6=0;i6<=21;i6++)
        {
         if(i0+i1+i2+i3+i4+i5+i6+i9==21)
         {
          add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][i4],value[5][i5],value[6][i6],value[7][0],value[8][0],value[9][i9],ans);
          if(ans[0]!='0' && judge(i0,i1,i2,i3,i4,i5,i6,0,0,i9,ans))
          {
           cout<<ans<<endl;
           count++;
           if(count==2)
           {
			int end = clock();
			cout<<"用时："<<end - start<<"毫秒"<<endl;
            return ;
           }
          }
          break;
         }
         for(int i7=0;i7<=21;i7++)
         {
          if(i0+i1+i2+i3+i4+i5+i6+i7+i9==21)
          {
           add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][i4],value[5][i5],value[6][i6],value[7][i7],value[8][0],value[9][i9],ans);
           if(ans[0]!='0' && judge(i0,i1,i2,i3,i4,i5,i6,i7,0,i9,ans))
           {
            cout<<ans<<endl;
            count++;
            if(count==2)
            {
				int end = clock();
				cout<<"用时："<<end - start<<"毫秒"<<endl;
				return ;
            }
           }
           break;
          }
          for(int i8=0;i8<=21;i8++)
          {
           if(i0+i1+i2+i3+i4+i5+i6+i7+i8+i9==21)
           {
            add(value[0][i0],value[1][i1],value[2][i2],value[3][i3],value[4][i4],value[5][i5],value[6][i6],value[7][i7],value[8][i8],value[9][i9],ans);
            if(ans[0]!='0' && judge(i0,i1,i2,i3,i4,i5,i6,i7,i8,i9,ans))
            {
             cout<<ans<<endl;
             count++;
             if(count==2)
             {
				int end = clock();
				cout<<"用时："<<end - start<<"毫秒"<<endl;
				return ;
             }
            }
            break;
           }
          } //i8
         } //i7
        } //i6
       } //i5
      } //i4
     } //i3
    } //i2
   } //i1
  } //i0
 } //i9



	
}  

char *convert(int a)//将整形转化成字符串
{
	char *c = new char[3];

	if(a >  10)//因为只用的到21一下的转换，所以只考虑两位数的转换
	{
		c[0] = a / 10 + '0';
		c[1] = a % 10 + '0';
		c[2] = '\0';
	}
	else
	{
		c[0] = a + '0';
		c[1] = '\0';
	}

	return c;
}

void chushi()
{
	int i, j;
	 
	strcpy(nums[0], "0");
	strcpy(nums[1], "1");

	for(i = 2 ; i <= 9; i++)
	{
		char c[2];
		c[0] = i + '0';
		c[1] = '\0';

		//cout<<c<<endl;

		strcpy(nums[i], c);//将c赋值给nums[i]

		for(j = 1; j < 21; j++)//因为nums[i]有初值了，所以只用循环20次
		{
			strcpy(nums[i], multi(nums[i], c));//nums[i]则得到0-9的21次方
		}

		
	}	




	for(i = 0; i < 10; i++)
	{
		for(j = 0; j < 22; j++)
		{
			strcpy(value[i][j], multi(nums[i], convert(j)));//value[i][j]得到0 - 9出现次数 * 0 - 9的21方
		}
	}
	


}


char *multi(char *p, char *q)//大乘法，仔细想想，用笔写写就可以看懂了
{
	int maxLen, minLen, i, j;

	char *min, *max;
	
	int *a = new int[100];

	if(strlen(p) > strlen(q))
	{
		maxLen = strlen(p);
		minLen = strlen(q);
		
		min = q;
		max = p;
	}
	else
	{
		maxLen = strlen(q);
		minLen = strlen(p);

		min = p;
		max = q;
	}

	memset(a, 0, 100);//将字符串置0

	int length = maxLen;

	char *c = new char[50];

	for(i = minLen - 1 ; i >= 0 ; i--)
	{
		for(j = 0 ; j < maxLen ; j++)
		{
			a[j] += (max[j] - '0') * (min[i] - '0');//将结果存放在a[j]中
		}

		if(i > 0)
		{
			for(int k = length - 1 ; k >= 0 ; k--)//如果i>0则说明乘数前面还有位数，则求的结果要向有移动一位
			{
				a[k + 1] = a[k]; 
			}
			a[0] = 0;
			length ++;
		}
	}



	for(i = length - 1 ; i > 0 ; i--)
	{
		a[i - 1] += a[i] / 10;//满十进一
		a[i] %= 10;
	}


	if(a[0] > 9)//当a[0] > 9的时候则说明位数不够
	{
		int temp = a[0];

		a[0] %= 10;

		temp /= 10;

		while(temp)
		{
			for(i = length - 1 ; i >= 0 ; i--)//补齐位数
				a[i + 1] = a[i];
	
			a[0] = temp % 10;
			length ++;
			temp /= 10;
		}

	}

	for(i = 0; i < 21 - length; i++)//如果结果不足21位，往前补0
		c[i] = '0';

	int k = 0;

	for(i = 21 - length; i < 21; i++)
		c[i] = a[k++] + '0';


	c[i] = '\0';


	return c;
}

void add(char* A0,char* A1,char* A2,char* A3,char* A4,char* A5,char* A6,char* A7,char* A8,char* A9,char* c)
{
	int result[21] = {0};

	int i;

	for(i = 0; i < 21; i++)
	{
		result[i] = A0[i] - '0' + A1[i] - '0' + A2[i] - '0' + A3[i] - '0' + 
			A4[i] - '0' + A5[i] - '0' + A6[i] - '0' + A7[i] - '0' + A8[i] - '0' + A9[i] - '0';
		//将每一位都加起来
	}

	for(i = 20; i > 0; i--)
	{
		result[i - 1] += result[i] / 10;
		result[i] %= 10;//满十进一
	}

	if(result[0] > 10 || result[0] == 0) //位数超过21位或者位数少于21位，提前结束
	{
		return;
	}

	for(i = 0; i < 21; i++)
		c[i] = result[i] + '0';

	c[i] = '\0';

}

bool judge(int i0, int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, char *p)
{
	int times[10];

	int i, result = 0;
	for(i = 0; i < 10; i++)
		times[i] = 0;

	for(i = 0; i < strlen(p); i++)
		times[p[i] - '0']++;
	//每一位进行比较

	if(i0 == times[0] && i1 == times[1] && i2 == times[2] && i3 == times[3] && i4 == times[4] &&
		times[5] == i5 && i6 == times[6] && times[7] == i7 && i8 == times[8] && i9 == times[9])
		return true;

	return false;
}

