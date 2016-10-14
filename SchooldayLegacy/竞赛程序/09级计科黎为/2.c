#include<stdio.h>
	double bad;
double score(double x[], int n)
{
	int i,j;
	double dif = -1;

	for(i=0; i<n; i++)
	{
		double sum = 0;
		for(j=0; j<n; j++)
		{
			if(j!=i) sum += x[j];
		}
		double t = x[i] - sum / (n-1);
		if(t<0) t = -t;
		if(t>dif)
		{
			dif = t;
			bad = x[i];
			printf("%d, %f\n", i, x[i]);
		}
	}

	return bad;
}

void main(){
	double x[10]={45,77,54,66,55};
	int n=5;
	 
    printf("%f\n",score(x,n));


}