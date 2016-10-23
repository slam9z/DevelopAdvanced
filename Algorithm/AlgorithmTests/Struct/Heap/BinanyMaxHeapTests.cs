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
    public class BinanyMaxHeapTests
    {
        private IList<int> _testData1 = new List<int>() { 12, 3, 8, 19, 5, 1, 2, 9, 3 };

        private BinanyHeap<int> CreateBinanyMaxHeap1()
        {
            var heap = new BinanyHeap<int>(_testData1, (a, b) => a > b);

            return heap;
        }


        private BinanyHeap<int> CreateBinanyMinHeap1()
        {
            var heap = new BinanyHeap<int>(_testData1, (a, b) => a < b);

            return heap;
        }


        [TestMethod()]
        public void BinanyMaxHeapTest()
        {
            var heap = CreateBinanyMaxHeap1();
            heap.Insert(30);
            Assert.AreEqual(30, heap.Peek());

            Assert.AreEqual(30, heap.Extract());
            Assert.AreEqual(19, heap.Extract());
            Assert.AreEqual(12, heap.Extract());
        }

        [TestMethod()]
        public void BinanyMinHeapTest()
        {
            var heap = CreateBinanyMinHeap1();
            heap.Insert(30);
            Assert.AreEqual(1, heap.Peek());

            Assert.AreEqual(1, heap.Extract());
            Assert.AreEqual(2, heap.Extract());
            Assert.AreEqual(3, heap.Extract());
        }
    }
}