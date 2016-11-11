using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    [TestClass()]
    public class FibonacciHeapTests
    {
        private FibonacciHeap<int> _heap1;
        private FibonacciHeap<int> _heap2;

        private void InitHeap()
        {
            _heap1 = new FibonacciHeap<int>((a, b) => a < b);

            _heap2 = new FibonacciHeap<int>((a, b) => a < b);


            _heap1.Insert(3);
            _heap1.Insert(1);
            _heap1.Insert(2);

            _heap2.Insert(5);
            _heap2.Insert(4);
            _heap2.Insert(6);

        }

        [TestMethod()]
        public void InsertTest()
        {
            InitHeap();
        }

        [TestMethod()]
        public void UnionTest()
        {
            InitHeap();
            _heap1.Union(_heap2);
        }
    }
}