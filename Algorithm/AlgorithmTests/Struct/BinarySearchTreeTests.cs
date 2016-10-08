using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Algorithm.Struct.Tests
{
    [TestClass()]
    public class BinarySearchTreeTests
    {
        protected IEnumerable<int> TreeData1 = new List<int>
        {
            2,3,7,5,4,8
        };

        protected IList<int> TreeData2 = new List<int>()
        {
            571,  395,  567,  417,  88,  539,  877,  16,  842,  425,
        };

        protected IList<int> TreeData3 = new List<int>()
        {
          939 , 675 , 594 , 13  ,809  ,166  ,281  ,808  ,288  ,276,
        };

        private BinarySearchTree<int> CreateTree1()
        {
            var root = new BinaryTreeNode<int>(2)
            {
                Right = new BinaryTreeNode<int>(3)
                {
                    Right = new BinaryTreeNode<int>(7)
                    {
                        Left = new BinaryTreeNode<int>(5)
                        {
                            Left = new BinaryTreeNode<int>(4),
                        },
                        Right = new BinaryTreeNode<int>(8),
                    }
                },
            };

            var tree = new BinarySearchTree<int>(root);
            tree.SetNodeParent();
            return tree;
        }

        protected void PrintTree(BinarySearchTree<int> tree)
        {
            tree.Inorder
            (tree.Root
            ,
            (node) => Console.Write("{0} ", node.Data));
            Console.WriteLine("");
        }

        protected IEnumerable<int> GetRandomData()
        {
            var random = new Random();
            var length = random.Next(1, 1000);
            var data = new List<int>(length);
            for (int i = 0; i < length; i++)
            {
                data.Add(random.Next(1, 1000));
            }
            return data;
        }

        protected void PrintNode(BinaryTreeNode<int> node)
        {
            Console.Write("{0} ", node.Data);
        }

        //只有父节点出问题，不知道是旋转问题还是插入问题，很无语。
    
        [TestMethod()]
        public void CreateTest()
        {
            var tree = new BinarySearchTree<int>();
            tree.Create(TreeData1);
            PrintTree(tree);
        }

        [TestMethod()]
        public void SearchTest()
        {
            var tree1 = CreateTree1();
            var node = tree1.Search(tree1.Root, 2);
            Assert.IsNotNull(node);
        }

        [TestMethod()]
        public void MinimumTest()
        {
            var tree1 = CreateTree1();
            Assert.AreEqual(tree1.Minimum(tree1.Root).Data, 2);
        }

        [TestMethod()]
        public void MaximumTest()
        {
            var tree1 = CreateTree1();
            Assert.AreEqual(tree1.Maximum(tree1.Root).Data, 8);
        }


        [TestMethod()]
        public void SuccessorTest()
        {
            var tree1 = CreateTree1();
            var node2 = tree1.Search(tree1.Root, 2);
            var node3 = tree1.Successor(node2);
            Assert.AreEqual(node3.Data, 3);

            var node4 = tree1.Search(tree1.Root, 4);
            var node5 = tree1.Successor(node4);
            Assert.AreEqual(node5.Data, 5);
        }

        [TestMethod()]
        public void PredecessorTest()
        {
            var tree1 = CreateTree1();
            var node2 = tree1.Search(tree1.Root, 2);
            var node0 = tree1.Predecessor(node2);
            Assert.IsNull(node0);

            var node4 = tree1.Search(tree1.Root, 4);
            var node3 = tree1.Predecessor(node4);
            Assert.AreEqual(node3.Data, 3);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //var tree1_1 = CreateTree1();
            //PrintTree(tree1_1);

            //var node2 = tree1_1.Search(tree1_1.Root, 2);
            //var node2_d = tree1_1.Delete(node2);
            ////Assert.AreEqual(node2_d, node2);
            //Console.WriteLine("delete {0}", node2.Data);
            //PrintTree(tree1_1);


            var tree1_2 = CreateTree1();
            var node7 = tree1_2.Search(tree1_2.Root, 7);

            Console.WriteLine("delete {0}", node7.Data);
            tree1_2.Delete(node7);
            //Assert.AreEqual(node7_d, node7);

            PrintTree(tree1_2);

            //var tree1_3 = CreateTree1();
            //var node4 = tree1_3.Search(tree1_3.Root, 4);
            //var node4_d = tree1_3.Delete(node4);
            ////Assert.AreEqual(node4_d, node4);
            //Console.WriteLine("delete {0}", node4.Data);

            //PrintTree(tree1_3);
        }
    }
}