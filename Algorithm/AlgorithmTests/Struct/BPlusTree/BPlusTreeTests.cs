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

        private IList<int> TestData3 = new List<int>()
        {
            355, 776, 836, 676, 732, 106, 838, 648
        };

        private IList<int> TestData4 = new List<int>()
        {
            294,
        };

        private IList<int> TestData5 = new List<int>()
        {
            271, 541, 889, 644, 188, 367, 704, 586, 238, 952, 394,
            956, 192, 519, 215, 34, 619, 881, 908, 654
        };


        private IList<int> TestData6 = new List<int>()
        {
            68, 707, 721, 563, 665, 304, 791, 910, 686, 740, 631, 462, 723,
            926, 185, 214, 722, 686, 298, 103, 914, 437, 786, 223, 691, 107,
            925, 199, 1, 321, 883, 331, 889, 486, 988, 681, 690, 164, 13, 75,
            210, 185, 323, 28, 801, 672, 735, 409, 351, 222, 677, 965, 383,
            647, 613, 159, 920, 557, 347, 359, 379, 592, 909, 10, 873, 566, 649,
            238, 79, 992, 473, 656, 953, 946, 704, 872, 795, 759, 422, 854, 680,
            928, 132, 744, 451, 188, 327, 333, 162, 324, 10, 723, 710, 558, 21,
            617, 275, 918, 430, 235, 24, 430, 459, 358, 749, 21, 558, 984, 776,
            10, 324, 162, 333, 327, 188, 644, 744, 132, 928, 371, 854, 422, 759,
            795, 603, 749, 370, 407, 656, 87, 328, 336, 238, 649, 566, 873, 10,
            490, 30, 379, 359, 347, 498, 863, 159, 613, 647, 355, 267, 99, 872,
            351, 996, 735, 381, 801, 134, 323, 486, 1, 75, 13, 164, 690, 681, 988,
            833, 812, 66, 314, 441, 1, 103, 427, 107, 128, 759, 786, 437, 788, 913,
            298, 686, 713, 694, 185, 926, 578, 462, 289, 740, 686, 910, 29, 549, 665,
            563, 867, 182, 171, 68, 707, 721, 39, 753, 304, 791, 5, 15, 481, 631, 806,
            723, 680, 34, 214, 722, 420, 563, 103, 914, 948, 502, 223, 691, 321, 925,
            199, 255, 321, 883, 331, 889, 486, 602, 41, 222, 57, 605, 927, 210, 185,
            154, 28, 707, 672, 750, 409, 144, 222, 677, 965, 383, 480, 957, 119, 920,
            557, 727, 559, 623, 592, 909, 694, 597, 996, 187, 329, 79, 992, 473, 821,
            953, 946, 704, 872, 595, 365, 316, 20, 680, 51, 848, 778, 451, 917, 711, 77,
            872, 483, 120, 723, 710, 454, 549, 617, 275, 918, 498, 235, 24, 498, 459,
            358, 749, 549, 454, 984, 776, 120, 483, 872, 77, 711, 917, 644, 778, 848,
            51, 371, 20, 316, 365, 595, 603, 749, 370, 407, 821, 87, 328, 336, 329, 187,
            996, 597, 694, 490, 30, 623, 559, 727, 498, 863, 119, 957, 480, 355, 267, 99,
            872, 144, 996, 750, 381, 707, 134, 154, 486, 1, 927, 605, 57, 222, 41, 602,
            833, 812, 66, 314, 441, 255, 103, 427, 321, 128, 759, 502, 948, 788, 913, 563,
            420, 713, 694, 34, 680, 578, 806, 289, 481, 15, 5, 29, 549, 753, 39, 867, 182,
            171,
        };


        private IList<int> TestData7 = new List<int>()
        {
              337, 787, 411, 908, 588, 769, 256, 239, 16, 326, 189,
              327, 517, 534, 296, 370, 304, 246, 251, 237
        };

        private IList<int> TestData8 = new List<int>()
        {
            888, 905, 680, 290, 203, 632, 653, 108, 334, 966, 98,
            57, 169, 168, 612, 317, 186, 937, 874, 523,
        };


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


        public BPlusTree<string> CreateAlphabetTree()
        {
            var bTree = new BPlusTree<string>();
            bTree.MinLimit = 3;
            foreach (var item in Alphabet)
            {
                bTree.Insert(item);
            };
            return bTree;
        }

        [TestMethod()]
        public void DeleteAlphabetTest()
        {
            var bTree = CreateAlphabetTree();

            Console.WriteLine("create order result:");
            bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
            Console.WriteLine();


            foreach (var item in Alphabet)
            {
                Console.WriteLine("detele item :{0}", item);

                bTree.Delete(item);

                Console.WriteLine("detele order result:");
                bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
                Console.WriteLine();

                bTree.Delete(item);
            };
        }


        [TestMethod()]
        public void DeleteTest()
        {
            //var count = TestHepler.GetRandom();
            //for (int i = 0; i < count; i++)
            //{
            //    var datas = TestHepler.GetRandomList().Take(20).ToList();
            //    DeleteList(datas, TestHepler.GetRandomList(datas));
            //}

            //  DeleteList(TestData3);

            //  DeleteList(TestData7, TestHepler.GetRandomList(TestData7));
            //  DeleteList(TestData7, TestData7);
            DeleteList(TestData8, TestData8);
        }

        private void DeleteList(IList<int> datas, IList<int> deleteDatas)
        {
            TestHepler.PrintList(datas, "create datas");

            TestHepler.PrintList(deleteDatas, "delete datas");


            var bTree = CreateTree(datas);

            Console.WriteLine("create order result:");
            bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });


            foreach (var item in deleteDatas)
            {
                Console.WriteLine();
                Console.WriteLine("detele item :{0}", item);

                bTree.Delete(item);

               // CheckTree(bTree);

                Console.WriteLine("detele order result:");
                bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
            }
        }

        [TestMethod()]
        public void InsertDeleteTest()
        {
            //for (int i = 0; i < 1000; i++)
            //{
            //    var datas = TestHepler.GetRandomList().Take(200).ToList();
            //    var twoDatas = new List<int>(datas);
            //    twoDatas.AddRange(datas);

            //    InsertDelete(TestHepler.GetRandomList(twoDatas));
            //}

            //InsertDelete(TestData4);

            //  InsertDelete(TestData5);

            InsertDelete(TestData6);
        }

        public void InsertDelete(IList<int> datas)
        {
            var bTree = new BPlusTree<int>();

            Console.WriteLine();
            TestHepler.PrintList(datas, "InsertDeleteTestStartData");

            for (int i = 0; i < datas.Count; i++)
            {
                var data = datas[i];
                Console.Write("{0} ", data);

                if (data % 3 == 0)
                {
                    bTree.Delete(data);

                    var preData = int.MinValue;

                    Console.WriteLine();
                    Console.WriteLine("InsertDeleteTest delete order start");
                    bTree.Order(bTree.Root, (d) =>
                    {
                        Console.Write("{0}, ", d);
                        Assert.IsTrue(preData <= d);
                        preData = d;
                    });
                    Console.WriteLine();
                    Console.WriteLine("InsertDeleteTest delete order end");
                }
                else
                {
                    bTree.Insert(data);

                    var preData = int.MinValue;

                    Console.WriteLine();
                    Console.WriteLine("InsertDeleteTest insert order start");
                    bTree.Order(bTree.Root, (d) =>
                    {
                        Console.Write("{0}, ", d);
                        Assert.IsTrue(preData <= d);
                        preData = d;
                    });
                    Console.WriteLine();
                    Console.WriteLine("InsertDeleteTest insert order  end");
                }
            }
            Console.WriteLine();
        }

        private void CheckTree(BPlusTree<int> bTree)
        {
            var preData = int.MinValue;

            Console.WriteLine();
            Console.WriteLine("CheckTree  start");
            bTree.Order(bTree.Root, (d) =>
            {
                Console.Write("{0}, ", d);
                Assert.IsTrue(preData <= d);
                preData = d;
            });
            Console.WriteLine();
            Console.WriteLine("CheckTree  end");

        }
    }
}