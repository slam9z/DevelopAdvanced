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

        private Dictionary<int, FibonacciNode<int>> _nodes;

        private void InitHeap()
        {
            _nodes = new Dictionary<int, FibonacciNode<int>>();

            _heap1 = new FibonacciHeap<int>((a, b) => a < b);

            _heap2 = new FibonacciHeap<int>((a, b) => a < b);


            _nodes[3] = _heap1.Insert(3);
            _nodes[1] = _heap1.Insert(1);
            _nodes[2] = _heap1.Insert(2);

            _nodes[5] = _heap2.Insert(5);

            _nodes[4] = _heap2.Insert(4);

            _nodes[6] = _heap2.Insert(6);


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



            heap.Traverse(heap.Peak,
                (node) =>
                {
                    Console.Write($"{node} ");
                }
                );

            FibonacciNode<int> prePeak = null;

            while (!heap.IsEmpty)
            {
                var peak = heap.Extract();

                Console.WriteLine();
                Console.WriteLine($"Extract {peak}");

                heap.Traverse(heap.Peak,
                    (node) =>
                    {
                        Console.Write($"{node} ");
                    }
                    );

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

        private void CheckHeap(FibonacciHeap<int> heap)
        {

            FibonacciNode<int> prePeak = null;

            while (!heap.IsEmpty)
            {
                var peak = heap.Extract();

                Console.WriteLine();
                Console.WriteLine($"Extract {peak}");

                heap.Traverse(heap.Peak,
                    (node) =>
                    {
                        Console.Write($"{node} ");
                    }
                    );

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
            var heap = new FibonacciHeap<int>((a, b) => a < b);
            var node1 = new FibonacciNode<int>(12);
            var node2 = new FibonacciNode<int>(11);

            heap.Exchange(ref node1, ref node2);

            Assert.AreEqual(node1.Key, 11);
        }

        [TestMethod()]
        public void TraverseTest()
        {
            var heap = UnionHeap();

            heap.Traverse(heap.Peak,
                  (node) =>
                  {
                      Console.Write($"{node} ");
                  }
                  );
        }

        [TestMethod()]
        public void UpdateKeyTest()
        {
            var heap = UnionHeap();

            heap.Extract();

            heap.UpdateKey(_nodes[4], -1);

            heap.UpdateKey(_nodes[6], -2);

            heap.UpdateKey(_nodes[2], -8);


            CheckHeap(heap);

            heap.Traverse(heap.Peak,
                  (node) =>
                  {
                      Console.Write($"{node} ");
                  }
                  );
        }
    }
}