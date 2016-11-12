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

        private FibonacciHeap<int> UnionHeap()
        {
            InitHeap();
            _heap1.Union(_heap2);
            return _heap1;
        }

        [TestMethod()]
        public void InsertTest()
        {
            InitHeap();
        }

        [TestMethod()]
        public void UnionTest()
        {
            UnionHeap();
        }

        [TestMethod()]
        public void ExtractTest()
        {
            var heap = UnionHeap();

            FibonacciNode<int> prePeak = null;

            while (!heap.IsEmpty)
            {
                var peak = heap.Extract();

                Console.WriteLine(peak);

                if (prePeak == null)
                {
                    prePeak = peak;
                }
                else
                {
                    Assert.IsTrue(prePeak.Key < peak.Key);
                    prePeak = peak;
                }

            }
        }

        [TestMethod()]
        public void ExchangeTest()
        {
            var heap =new FibonacciHeap<int>((a,b)=>a<b);
            var node1 = new FibonacciNode<int>(12);
            var node2 = new FibonacciNode<int>(11);

            heap.Exchange(ref node1,ref node2);

            Assert.AreEqual(node1.Key, 11);
        }
    }
}