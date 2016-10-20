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


        #region TestData9

        private IList<int> TestData9_c = new List<int>()
        {
            68, 412, 336, 264, 867, 866, 109, 300, 724, 516, 118, 326, 368, 316,
            550, 661, 491, 216, 704, 331, 570, 333, 576, 49, 222, 842, 550, 780,
            73, 686, 80, 44, 155, 563, 827, 739, 542, 786, 202, 77, 250, 933, 885,
            693, 131, 609, 395, 688, 757, 538, 189, 453, 667, 96, 715, 735, 835, 288,
            43, 25, 317, 328, 228, 38, 436, 75, 172, 804, 488, 811, 120, 704, 14,
            628, 82, 637, 448, 882, 917, 613,
        };

        private IList<int> TestData9_d = new List<int>()
        {
            613, 917, 882, 448, 637, 82, 628, 300, 724, 120, 118, 488, 368, 172, 75, 436, 38, 228,
            328, 331, 25, 333, 288, 835, 735, 715, 96, 780, 453, 189, 538, 757, 688, 563,
            827, 131, 542, 786, 933, 250, 250, 202, 786, 542, 739, 827, 563, 155, 757, 80,
            189, 453, 780, 96, 715, 735, 49, 288, 333, 25, 331, 328, 228, 491, 436, 550,
            172, 368, 488, 118, 120, 724, 300, 109, 82, 867, 448, 882, 917, 68,
        };


        #endregion


        #region TestData10

        private IList<int> TestData10_c = new List<int>()
        {
            772, 686, 592, 302, 840, 268, 549, 697, 801, 699, 793, 588, 589, 278, 168, 53, 449,
            358, 683, 974, 896, 697, 246, 475, 448, 846, 726, 413, 408, 925, 509, 275, 73, 408,
            969, 784, 650, 204, 223, 740, 224, 489, 235, 369, 372, 770, 326, 936, 152, 427, 683,
            553, 312, 161, 9, 75, 441, 118, 854, 993, 541, 136, 290, 875, 190, 519, 515, 182,
            309, 384, 403, 246, 135, 943, 751, 407, 463, 877, 104, 677,
        };

        private IList<int> TestData10_d = new List<int>()
        {
            677, 104, 877, 463, 840, 751, 943, 135, 246, 403, 384, 588, 589, 278, 168, 190, 875,
            290, 136, 541, 993, 854, 246, 441, 448, 9, 161, 312, 553, 683, 427, 152, 936, 408,
            770, 784, 650, 235, 489, 224, 224, 223, 235, 650, 784, 770, 408, 73, 152, 427, 925,
            553, 413, 726, 9, 448, 475, 246, 697, 896, 974, 683, 358, 875, 53, 168, 278, 589,
            588, 793, 699, 801, 697, 943, 751, 840, 302, 592, 104, 677,
        };


        #endregion


        #region TestData11

        private IList<int> TestData11_c = new List<int>()
        {
             621, 100, 188, 559, 987, 273, 306, 415, 906, 104, 384, 678, 553, 442, 432,
            778, 30, 658, 780, 724, 697, 142, 637, 431, 200, 950, 924, 833, 337,
            349, 693, 722, 889, 359, 721, 373, 897, 163, 125, 862, 704, 584, 249, 554,
            556, 487, 931, 840, 837, 965, 961, 423, 385, 827, 468, 479, 463, 756, 360,
            38, 349, 472, 78, 557, 411, 661, 789, 194, 720, 59, 881, 867, 533, 917, 20,
            114, 893, 83, 875, 712,
        };

        private IList<int> TestData11_d = new List<int>()
        {
            621, 875, 83, 893, 114, 20, 917, 533, 867, 104, 384, 720, 194, 789, 432, 411, 30, 658,
            780, 349, 38, 142, 637, 463, 200, 468, 827, 385, 423, 961, 965, 837, 840,
            931, 487, 556, 554, 249, 584, 704, 704, 125, 249, 554, 373, 487, 359, 889,
            722, 965, 961, 337, 833, 827, 950, 200, 431, 637, 142, 38, 349, 780, 658, 30,
            411, 432, 442, 553, 720, 384, 104, 906, 415, 917, 273, 114, 559, 83, 875, 712,
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

        private void PassedDeteleTest()
        {
            DeleteList(TestData3, TestData3);
            DeleteList(TestData7, TestData7);
            DeleteList(TestData8, TestData8);

            DeleteList(TestData9_c, TestData9_d);

            DeleteList(TestData10_c, TestData10_d);
            DeleteList(TestData11_c, TestData11_d);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            PassedDeteleTest();

            Console.WriteLine();
            Console.WriteLine("Random Test");

            var count = TestHepler.GetRandom();
            for (int i = 0; i < 10; i++)
            {
                var datas = TestHepler.GetRandomList().ToList();
                DeleteList(datas, TestHepler.GetRandomList(datas));
            }
        }

        private void DeleteList(IList<int> datas, IList<int> deleteDatas)
        {
            Console.WriteLine();
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

                CheckTree(bTree);

                Console.WriteLine("detele order result:");
                bTree.Order(bTree.Root, (d) => { Console.Write("{0}, ", d); });
            }
        }


        private void PassedInsertDeleteTest()
        {

            InsertDelete(TestData4);
            InsertDelete(TestData5);
        }

        [TestMethod()]
        public void InsertDeleteTest()
        {
            PassedInsertDeleteTest();

            for (int i = 0; i < 10; i++)
            {
                var datas = TestHepler.GetRandomList().Take(200).ToList();
                var twoDatas = new List<int>(datas);
                twoDatas.AddRange(datas);

                InsertDelete(TestHepler.GetRandomList(twoDatas));
            }

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