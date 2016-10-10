using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTests;

namespace Algorithm.Struct.Tests
{
    [TestClass()]
    public class BPlusTreeTests
    {
        #region data1 and  result

        private IList<int> TestData1 = new List<int>
        {
            1,2,3,4,5,
            6,7,8,9,10,
            11,12,13,14,15,
        };

        //limit 4

        //			4,8  keys id 2
        //			1 3 4   childs

        //id 1		id 3      id 4
        //1,2,3      5,6,7       9到15 keys

        //但是有多余 但是有多余
        //的数据1到7 多余的数据5到11

        //结果是符合算法的。

        #endregion

        #region data2

        private IList<int> TestData2 = new List<int>()
        {

            357, 941, 305, 879, 809, 266, 961, 348, 154,
            941, 316, 430, 207, 883, 75, 591, 54, 76, 594, 391,
        };

        #endregion

        [TestMethod()]
        public void CreateTest()
        {
            CreateTree(TestData2);
        }


        [TestMethod()]
        public void InsertTest()
        {
            var bTree = new BPlusTree<int>();

            foreach (var item in TestData1)
            {
                bTree.Insert(item);
            }
        }

        [TestMethod()]
        public void SearchTest()
        {
            //AllInsert(TestData1);
            //AllInsert(TestData2);
            for (int i = 0; i < 100; i++)
            {
                AllInsert(TestHepler.GetRandomList().Take(100));
            }
        }

        private void AllInsert(IEnumerable<int> datas)
        {
            TestHepler.PrintList(datas, "datas: ");

            var bTree = CreateTree(datas);

            foreach (var item in datas)
            {
                var node = bTree.Search(bTree.Root, item);

                //节点的值呢 ?
                Console.WriteLine("Search key: {0}", item);
                Console.WriteLine(node);

                Assert.IsNotNull(node);
                Assert.IsTrue(node.Keys.Contains(item));
            }
        }


        private BPlusTree<int> CreateTree(IEnumerable<int> datas)
        {
            var bTree = new BPlusTree<int>();

            var insertedDatas = new List<int>();

            foreach (var item in datas)
            {
                bTree.Insert(item);
                insertedDatas.Add(item);

                Console.WriteLine("Insert Data:{0}", item);
                bTree.Order(bTree.Root,
                    (d) =>
                    {
                        Console.Write("{0}, ", d);
                        Assert.IsTrue(insertedDatas.Contains(d));
                    });
                Console.WriteLine();
            }

            return bTree;
        }


        [TestMethod()]
        public void OrderTest()
        {
            TestHepler.PrintList(TestData2, "datas: ");
            var bTree = CreateTree(TestData2);

            TestHepler.PrintListWithOrder(TestData2, "order datas: ");

            Console.WriteLine("order result:");
            bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
        }



        private IList<string> Alphabet = new List<string>()
        {
            "A","B","C","D",
            "E","F","G",
            "H","I","J","K",
            "L","M","N",
            "O","P","Q",
            "R","S","T",
            "U","V","W",
            "X","Y","Z"
        };

        [TestMethod()]
        public void CreateAlphabetTree()
        {
            var bTree = new BPlusTree<string>();
            bTree.MinLimit = 3;
            foreach (var item in Alphabet)
            {
                bTree.Insert(item);
            };

            Console.WriteLine("order result:");
            bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
        }

    }
}