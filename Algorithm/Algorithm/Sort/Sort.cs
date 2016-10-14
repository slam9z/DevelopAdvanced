using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System
{
	/// <summary>
	/// Time:2016/10/14
	/// 两年前写的自己都忘记写过了。排序算法经常写，但是感觉还是容易忘记。
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public abstract class Sortor<T>
    where T : IComparable<T>
    {
        public readonly IComparer<T> DefaultCom = new DefaultComparator();
        public readonly IComparer<T> ReverseCom = new ReverseComparator();
        public abstract void Sort(T[] array, int from, int end, IComparer<T> com);
        public void Sort(T[] array, IComparer<T> com)
        {
            Sort(array, 0, array.Length - 1, com);
        }

        public void Sort(T[] array)
        {
            Sort(array, DefaultCom);
        }

        private class DefaultComparator : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return x.CompareTo(y);
            }
        }

        private class ReverseComparator : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return y.CompareTo(x);
            }
        }

        protected void Move(T[] array, int startIndex, int endIndex, int step)
        {
            for (int i = endIndex; i >= startIndex; i -= step)
            {
                array[i + step] = array[i];
            }
        }

        protected void Wrap(T[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }



    //插入排序 思想对了才能写出正确的代码
    public class InsertSortor<T> : Sortor<T>
            where T : IComparable<T>
    {
        public override void Sort(T[] array, int from, int end, IComparer<T> com)
        {
            int i, j;

            for (i = from; i <= end; i++)
            {
                var insertedElem = array[i];
                for (j = i - 1; j > -1 && com.Compare(array[j], insertedElem) > 0; j--)
                {
                    array[j + 1] = array[j];
                    array[j] = insertedElem;
                }
            }
        }
    }

    //希尔排序
    public class ShellSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            var step = (end - from) / 2;
            step = (step) % 2 == 0 ? step + 1 : step;

            while (true)
            {
                for (var i = step; i <= end; i++)
                {
                    var insertedElem = array[i];
                    for (var j = i - step; j >= from && cmp.Compare(array[j], insertedElem) > 0; j -= step)
                    {
                        array[j + step] = array[j];
                        array[j] = insertedElem;
                    }
                }

                if (step <= 1)
                {
                    break;
                }

                step = step / 2;
                step = (step) % 2 == 0 ? step + 1 : step;
            }
        }
    }

    // 冒泡排序
    public class BubbleSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            for (var i = 0; i <= end - from; i++)
            {
                //for(var j = i;j<end-1;j++) { //思想啊！思维，谁也不可能记代码啊
                for (var j = 0; j < end - i; j++)
                {
                    if (cmp.Compare(array[j], array[j + 1]) > 0)
                    {
                        Wrap(array, j, j + 1);
                    }
                }
            }
        }
    }

    // 归并算法，使用递归挺不错的，很自然的就处理了分组问题。循环应该也可以的
    //就是知道算法，明白怎么写，写对也不容易呢
    public class MergeSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        private void Merge(T[] source, int start, int end, int divide, T[] mergeDst, IComparer<T> cmp)
        {
            var mergePiont = start;
            var firstPoint = start;
            //var scdPoint = divide; 边界条件最需要注意。思考算法可以减少bug！
            var scdPoint = divide + 1;

            for (mergePiont = start; firstPoint <= divide && scdPoint <= end; ++mergePiont)
            {
                if (cmp.Compare(source[firstPoint], source[scdPoint]) > 0)
                {
                    mergeDst[mergePiont] = source[scdPoint];
                    scdPoint++;
                }
                else
                {
                    mergeDst[mergePiont] = source[firstPoint];
                    firstPoint++;
                }
            }

            if (firstPoint <= divide)
            {
                for (var i = firstPoint; i <= divide; i++)
                {
                    mergeDst[mergePiont] = source[i];
                    mergePiont++;
                }
            }

            if (scdPoint <= end)
            {
                for (var i = scdPoint; i <= end; i++)
                {
                    mergeDst[mergePiont] = source[i];
                    mergePiont++;
                }
            }

            for (var i = start; i <= end; i++)
            {
                source[i] = mergeDst[i];

                //  Console.Write(string.Format("[{0}:  {1}]   ", i, source[i]));
            }

            // Console.WriteLine(string.Format(" Merge from:{0}, end:{1} ", start, end));
        }

        private void MergeSort(T[] array, int from, int end, T[] mergeDst, IComparer<T> cmp)
        {
            //Console.WriteLine(string.Format("from:{0}, end:{1} ", from, end));

            if (end == from)
            {
                mergeDst[from] = array[from];
                return;
            }

            var divide = (end + from) / 2; //(end-from)/2+from;
            MergeSort(array, from, divide, mergeDst, cmp);
            MergeSort(array, divide + 1, end, mergeDst, cmp);
            Merge(array, from, end, divide, mergeDst, cmp);
        }

        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            var mergeDst = new T[array.Length];

            MergeSort(array, from, end, mergeDst, cmp);

            array = mergeDst;
        }
    }

    //直接选择排序
    public class SelectSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            for (var i = from; i <= end; i++)
            {
                var currentPoint = i;

                for (var j = i + 1; j <= end; j++)
                {
                    if (cmp.Compare(array[currentPoint], array[j]) > 0)
                    {
                        currentPoint = j;
                    }
                }

                Wrap(array, i, currentPoint);
            }
        }
    }

    //快速排序
    public class QuickSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        int Partition(T[] array, int from, int end, IComparer<T> cmp)
        {
            var pivot = array[from];
            var lowPoint = from;
            var highPoint = end;
            while (lowPoint < highPoint)
            {
                while (lowPoint < highPoint && cmp.Compare(array[highPoint], pivot) > 0)
                {
                    highPoint--;
                }

                if (lowPoint < highPoint)
                {
                    array[lowPoint] = array[highPoint];
                    lowPoint++;
                }

                while (lowPoint < highPoint && cmp.Compare(pivot, array[lowPoint]) > 0)
                {
                    lowPoint++;
                }

                if (lowPoint < highPoint)
                {
                    array[highPoint] = array[lowPoint];
                    highPoint--;
                }
            }

            array[lowPoint] = pivot;

            // Console.WriteLine(string.Format("lowPoint:{0} highPoint：{1}", lowPoint, highPoint));
            return lowPoint;
        }

        void QuickSort(T[] array, int from, int end, IComparer<T> cmp)
        {
            if (from < end)
            {
                var pivot = Partition(array, from, end, cmp);
                //QuickSort(array，from,pivot,cmp);
                QuickSort(array, from, pivot - 1, cmp);
                QuickSort(array, pivot + 1, end, cmp);
            }
        }

        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            QuickSort(array, from, end, cmp);
        }
    }

    public class HeapSortor<T> : Sortor<T>
    where T : IComparable<T>
    {
        int LeftChild(int node)
        {
            return (node + 1) * 2 - 1;
        }

        int RightChild(int node)
        {
            return (node + 1) * 2;
        }


        void Adjust(T[] array, int adjustNode, int headEnd, IComparer<T> cmp)
        {
            var left = LeftChild(adjustNode);
            var right = RightChild(adjustNode);
            var largest = adjustNode;

            //最后节点位置计算！ 考虑只有两个数字
            while (left <= headEnd || right <= headEnd)
            {
                if (left <= headEnd && cmp.Compare(array[left], array[largest]) > 0)
                {
                    largest = left;
                }

                if (right <= headEnd && cmp.Compare(array[right], array[largest]) > 0)
                {
                    largest = right;
                }

                if (largest == adjustNode)
                {
                    break;
                }

                Wrap(array, adjustNode, largest);

                adjustNode = largest;
                left = LeftChild(largest);
                right = RightChild(largest);
            }


        }



        public override void Sort(T[] array, int from, int end, IComparer<T> cmp)
        {
            //build
            for (var i = (from + end) / 2 + 1; i >= from; i--)
            {

                Adjust(array, i, end, cmp);
            }

            for (var i = end; i > from; i--)
            {
                Wrap(array, i, from);
                Adjust(array, from, i - 1, cmp);
            }
        }
    }

    class Program
    {
        static void ShowSortResult()
        {
            InsertSortor<int> insertSort = new InsertSortor<int>();
            SortorHelper.TestSort<int>(insertSort, null);

            ShellSortor<int> shellSort = new ShellSortor<int>();
            SortorHelper.TestSort<int>(shellSort, null);

            BubbleSortor<int> bubbleSort = new BubbleSortor<int>();
            SortorHelper.TestSort<int>(bubbleSort, null);

            int[] intgArr = { 5, 9, 1, 4, 1, 2, 6, 3, 8, 0, 7 };
            var mergeSort = new MergeSortor<int>();
            SortorHelper.TestSort<int>(mergeSort, null);

            SortorHelper.TestSort<int>(new SelectSortor<int>(), null);

            SortorHelper.TestSort<int>(new QuickSortor<int>(), null);

            SortorHelper.TestSort<int>(new HeapSortor<int>(), null);


        }

        static void PerformanceTest()
        {

            ShellSortor<int> shellSort = new ShellSortor<int>();

            Console.WriteLine("ShellSortor ");
            SortorHelper.TestSort<int>(shellSort, null, true);

            int[] intgArr = { 5, 9, 1, 4, 1, 2, 6, 3, 8, 0, 7 };
            var mergeSort = new MergeSortor<int>();
            Console.WriteLine("MergeSortor ");
            SortorHelper.TestSort<int>(mergeSort, null, true);

            Console.WriteLine("QuickSortor ");
            SortorHelper.TestSort<int>(new QuickSortor<int>(), null, true);

            Console.WriteLine("HeapSortor ");
            SortorHelper.TestSort<int>(new HeapSortor<int>(), null, true);


        }
        static void Main(string[] args)
        {
            ShowSortResult();
            PerformanceTest();
            Console.ReadKey();
        }
    }


    public class SortorHelper
    {
        //测试方法  
        static bool _isPerformanceTest;
        static int length;
        public static void TestSort<T>(Sortor<T> sorter, T[] array, bool isPerformanceTest = false)
        where T : IComparable<T>
        {
            _isPerformanceTest = isPerformanceTest;

            if (array == null)
            {
                array = RandomArray<T>();
            }

            //为了第二次排序，需拷贝一份  
            var tmpArr = new T[array.Length];
            Array.Copy(array, tmpArr, array.Length);

            if (!_isPerformanceTest)
            {
                Console.WriteLine("源 - " + ArrayToString(tmpArr));
            }

            Console.WriteLine("源 - " + tmpArr.Length);

            Stopwatch sw = new Stopwatch();
            //开始计数
            sw.Start();
            //sorter.Sort(array, sorter.ReverseCom);


            //Console.WriteLine("降 - " + ArrayToString(array));
            sorter.Sort(tmpArr, sorter.DefaultCom);

            Console.WriteLine("时间  " + sw.ElapsedMilliseconds);

            if (!_isPerformanceTest)
            {
                Console.WriteLine("升 - " + ArrayToString(tmpArr));
            }
        }

        public static string ArrayToString<T>(T[] array)
        {
            string result = null;
            foreach (var item in array)
            {
                result += item.ToString() + ",";
            }

            return result;
        }

        //生成随机数组  
        private static T[] RandomArray<T>()
        where T : IComparable<T>
        {
            //   var a = new Int32[r.Next(3, 30)];intgArr = { 5, 9, 1, 4, 1, 2, 6, 3, 8, 0, 7, 34, 23, 0, 3 };
            // //
            // return intgArr as T[];
            Random r = new Random(Environment.TickCount);

            int[] a;

            if (_isPerformanceTest)
            {
                if (length == 0)
                {
                    length = r.Next(3000, 3000000);

                }

                a = new Int32[length];

            }
            else
            {
                a = new Int32[r.Next(3, 30)];
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (_isPerformanceTest)
                {
                    a[i] = r.Next(3000000);
                }
                else
                {
                    a[i] = r.Next(300);
                }


            }

            return a as T[];
        }
    }
}