[你会初始化C#数组吗？](http://www.cnblogs.com/Okalun/archive/2012/11/19/2776839.html)

>当如有点二逼的文章，错别字有点多我现在都忘记了多维数组和交错数组的存在了。工作和学习中从来没有用过，
矩阵是唯一想到的实用方式。
>还有相同类型的元祖数组可以堪称一个二维数组。

>之前写的太少了，至少要把遍历什么都写上还有扩展什么的，怎么实现存储的也应该弄明白



外话：学习.NET已经有一年了，从C#->ASP.NET->WPF。主要以看电子书为主，比较少写代码。现在回头学习以前接触过的，随着知识与经验的的积累。
总是有各种惊喜，震惊！C#数组就是其中之一，我把它作为自己博客园的处女作。
 
C#数组与其它C系列语言有着很多的不同，以前接触的时候理解出现很大的偏差。尤其是对多维数组的认识。多维数组与C语言相比是一个新概念。而最开始的
时候我把它当成交错数组的特殊类型。

## 二维数组与简单的交错数组

首先从二维数组与简单的交错数组的初始化与访问开始


```cs
int[,] nums={

                {1,2,3},

                {1,2,0}

         };

         for (int i = nums.GetLowerBound(0); i <= nums.GetUpperBound(0); i++)

         {

             for (int j = nums.GetLowerBound(1); j <= nums.GetUpperBound(1); j++)

             {

                 Console.WriteLine(nums[i,j]);

                 Console.WriteLine(nums.GetValue(i,j));

             }

                 

         }

         foreach (var num in nums)

         {

             Console.WriteLine(num);
         }
            //对任意维度的数组，都可以这样快速访问，只是foreach不能修改变量。
```

而交错数组也能实现差不多的内容

```cs
 int[][] nums2 ={
                 new int[]{1,2,3},
                 new int[]{1,2,0}
           };
           for (int i = nums2.GetLowerBound(0); i <= nums2.GetUpperBound(0); i++)
           {
               for (int j = nums2[i].GetLowerBound(0); j <= nums2[i].GetUpperBound(0); j++)
               {
                   Console.WriteLine(nums2[i][j]);
                 
               }
           }
            foreach (var ia in nums2)
           {
               foreach (var i in ia)
               {
                   Console.WriteLine(i);
               }   
           }
```

多维数组存储的数据可以用交错数组替代。*交错数组是一个有高维度的特殊数组，而交错数组是数组的数组*。而且数组有
一个很重要的性质，数组里面储蓄的必须是相同的类型！这对理解各种复杂数组是很重要的。

```cs
bool[][][] cells31 = new bool[2][][]
                         {
                             new bool[2][]
                                 {
                                     new bool[] {false},
                                     new bool[] {true}
                                 },
                             new bool[3][]
                                 {
                                     new bool[] {false},
                                     new bool[] {true}，
                                     new bool[] {true}
                                 }
                         };

```

## 复杂的交错数组 

我们必须这样初始化 有一大堆new 因为交错数组是数组的数组，所以我们以前一直嵌套。但是需要很多的数组类型，也可以创建
无数的数组类型。

```cs
 Console.WriteLine("交错数组类型"); 
 Console.WriteLine(cells31[0].GetType()); 
 Console.WriteLine(cells31[0][0].GetType()); 
 Console.WriteLine(cells31[0][0][0].GetType()); 

//交错数组类型
//System.Boolean[][]
//System.Boolean[]
//System.Boolean 
//这是交错数组里面的类型。 
// bool[2][] 与boo[3][] 是相同的类型，所以我们创建存储结构不一致的数组
```

## 交错数组与多维数组混合

接下来是最复杂的类型。将交错数组与多维数组混合起来。如果能初始化下面的数组那么应该就理解的比较透彻了吧！

```cs
bool  [][,,][][,,][]Foo;
```
我选择一个简单点作为示例  

```cs
bool  [][,][]Foo;
```

```cs
bool[][,][] Foo = new bool[1][,][]
                                  {
                                      new bool[2,2][]
                                          {
                                              {
                                                  new bool[2] {false, true},
                                                  new bool[2] {false, true}
                                              },
                                          
                                              {
                                                  new bool[2] {false, true},
                                                  new bool[2] {false, true}
                                              }
                                          }
                                  };
            Console.WriteLine("混合数组类型");
            Console.WriteLine(Foo.GetType());
            Console.WriteLine(Foo[0].GetType());
            Console.WriteLine(Foo[0][0,0].GetType());
            Console.WriteLine(Foo[0][0, 0][0].GetType());

//结果 混合数组类型
//system.boolean[][,][]
//system.boolean[][,]
//system.boolean[]
//system.boolean
 


   //定义交错数组：一维数组存放（二维int数组存放（一维int数组存放（四维int数组）））

//标准的C#定义描述  array of( multi-array of( array of (nulti-array)))

 int[][,][][, , ,] arr = new int[10][,][][,,,];
            
            //初始化 二维int数组存放（一维int数组存放（四维int数组））
            
            arr[4] = new int[1, 2][][,,,];
            
            //初始化 一维int数组存放（四维int数组）

            arr[4][0, 1] = new int[3][, , ,];
            //初始化 四维int数组

            arr[4][0, 1][2] = new int[1, 2, 3, 4];
            
            Console.WriteLine(arr.GetType());

            Console.WriteLine(arr[4].GetType());

            Console.WriteLine(arr[4][0, 1].GetType());

            Console.WriteLine(arr[4][0, 1][2].GetType());

//System.Int32[,,,][][,][]
//System.Int32[,,,][][,]
//System.Int32[,,,][]
//System.Int32[,,,]
//C#编译器生成的名字与我们声明的是倒着的。理解起来应该也没差异吧

```
 
现在应该比较清晰了吧。我也不知道到底是不是每个程序员都理解这些，不过我是花了不少时间才明白的。
最后再考虑一下对数组方法的影响。尤其是 Clear();

```cs
Console.WriteLine(Foo[0][0,0][0]);
//输出为Flase
Array.Clear(Foo,0,1);
Console.WriteLine(Foo[0][0, 0][0]);
//这里会引发空引用异常。因为 bool[][,]的类型的值已经变为null。
```