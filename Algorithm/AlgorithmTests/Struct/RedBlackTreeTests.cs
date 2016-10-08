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
    public class RedBlackTreeTests : BinarySearchTreeTests
    {

        private BinarySearchTree<int> CreateTree1()
        {
            //var root = new BinaryTreeNode<int>(2)
            //{
            //	Right = new BinaryTreeNode<int>(3)
            //	{
            //		Right = new BinaryTreeNode<int>(7)
            //		{
            //			Left = new BinaryTreeNode<int>(5)
            //			{
            //				Left = new BinaryTreeNode<int>(4),
            //			},
            //			Right = new BinaryTreeNode<int>(8),
            //		}
            //	},
            //};

            var tree = new RedBlackTree<int>();
            tree.Create(TreeData1);
            //	tree.SetNodeParent();
            //	tree.ReplaceNullToEmpty();
            return tree;
        }





        [TestMethod]
        public void RotaleTest()
        {
            for (int i = 0; i < 10; i++)
            {
                Rotale(GetRandomData().Take(10).ToList());
            }
            //Rotale(TreeData3);
        }

        private void Rotale(IList<int> datas)
        {
            var tree = new RedBlackTree<int>();

            tree.Create(datas);

            foreach (var data in datas)
            {
                var node = tree.Search(tree.Root, data);
                if (!node.Right.IsEmpty())
                {
                    tree.LeftRotate(node);
                    tree.CheckNodeParent();
                }
            }

            foreach (var data in datas)
            {
                var node = tree.Search(tree.Root, data);
                if (!node.Left.IsEmpty())
                {
                    tree.RightRotate(node);
                }
                tree.CheckNodeParent();
            }
        }


        //有啥好的好图工具？
        [TestMethod()]
        public void RebBlackInsertTest()
        {
            //4报错

            for (int i = 0; i < 10; i++)
            {
                RebBlackInsert(GetRandomData().Take(100).ToList());
            }

            RebBlackInsert(TreeData3);
        }

        private void RebBlackInsert(IList<int> datas)
        {
            var tree = new RedBlackTree<int>();
            foreach (var data in datas)
            {
                var node = new RedBlackTreeNode<int>(data);

                Console.WriteLine("insert node {0}", data);

                tree.Insert(node);

                tree.CheckRedBlackTree();
            }
        }


        [TestMethod()]
        public void RebBlackDeleteTest()
        {
			//4报错
			// RebBlackDelete(TreeData1);

			for (int i = 0; i < 100; i++)
			{
				RebBlackDelete(GetRandomData().Take(100).ToList());
			}

			//RebBlackDelete(TreeData2);

			//RebBlackDelete(TreeData3);

			RebBlackDelete(TreeData4);
        }


        private void RebBlackDelete(IList<int> datas)
        {
            var tree = new RedBlackTree<int>();
            foreach (var data in datas)
            {
                var node = new RedBlackTreeNode<int>(data);
                tree.Insert(node);
            }
            Console.WriteLine("new tree data");
            foreach (var d in datas)
            {
                Console.Write("{0}  ", d);
            }
            Console.WriteLine();

            Console.WriteLine("new tree ");
            PrintTree(tree);

            tree.CheckRedBlackTree();

            var count = datas.Count();
            for (int i = 0; i < count; i++)
            {
                var data = datas[i];

                var node = tree.Search(tree.Root, data);

                Console.WriteLine("delete node: {0}", node.Data);
                Console.WriteLine();


                tree.Delete(node);
                PrintTree(tree);
                tree.CheckRedBlackTree();
            }
        }





    }
}