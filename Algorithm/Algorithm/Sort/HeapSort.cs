﻿using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    public class HeapSort : SortBase
    {


        public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> larger)
        {
            var result = new List<T>(source);
            var maxHeap = new BinanyHeap<T>();

            maxHeap.BuildMaxHeap(result, larger);

            return  maxHeap.Sort();
        }

    }
}
