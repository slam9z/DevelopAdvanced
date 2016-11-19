Test Name:	ComputeTest
Test Outcome:	Passed
Result StandardOutput:	

```
l: 1

1,1: 0.45  2,2: 0.4  3,3: 0.25  4,4: 0.3  5,5: 0.5  
 l: 2

1,2: 0.9  2,3: 0.7  3,4: 0.65  3,4: 0.6  4,5: 1.05  4,5: 0.9  
 l: 3

1,3: 1.3  1,3: 1.25  2,4: 1.2  3,5: 1.55  3,5: 1.35  3,5: 1.3  
 l: 4

1,4: 1.95  1,4: 1.75  2,5: 2.2  2,5: 2.1  2,5: 2  
 l: 5

1,5: 3.05  1,5: 2.75  
w: 

0  	0  	0  	0  	0  	0  	
0.05  	0.3  	0.45  	0.55  	0.7  	1  	
0  	0.1  	0.25  	0.35  	0.5  	0.8  	
0  	0  	0.05  	0.15  	0.3  	0.6  	
0  	0  	0  	0.05  	0.2  	0.5  	
0  	0  	0  	0  	0.05  	0.35  	

e: 

0  	0  	0  	0  	0  	0  	
0.05  	0.45  	0.9  	1.25  	1.75  	2.75  	
0  	0.1  	0.4  	0.7  	1.2  	2  	
0  	0  	0.05  	0.25  	0.6  	1.3  	
0  	0  	0  	0.05  	0.3  	0.9  	
0  	0  	0  	0  	0.05  	0.5  	
root

0  0  0  0  0  0  
0  1  1  2  2  2  
0  0  2  2  2  4  
0  0  0  3  4  5  
0  0  0  0  4  5  
0  0  0  0  0  5  
init 1 5 -1
rootChildIndex 2
root node k2

init 1 1 2
rootChildIndex 1
left node k1

init 1 0 1
left leaf d0

init 2 1 1
right leaf d1

init 3 5 2
rootChildIndex 5
right node k5

init 3 4 5
rootChildIndex 4
left node k4

init 3 3 4
rootChildIndex 3
left node k3

init 3 2 3
left leaf d2

init 4 3 3
right leaf d3

init 5 4 4
right leaf d4

init 6 5 5
right leaf d5

d0 k1 d1 k2 d2 k3 d3 k4 d4 k5 d5

```