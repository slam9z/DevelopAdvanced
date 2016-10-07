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



		private void CheckRedBlackTree(RedBlackTree<int> tree)
		{
			CheckRoot(tree);
			CheckRedNode(tree);
		}



		public void CheckRoot(RedBlackTree<int> tree)
		{
			Assert.AreEqual(GetRedBlackTreeNode(tree.Root).Color, NodeColor.Black);
		}


		public void CheckRedNode(RedBlackTree<int> tree)
		{
			Console.WriteLine("CheckRedNode Start");
			tree.Inorder(tree.Root, (node) =>
			 {
				 var redBlackNode = node as RedBlackTreeNode<int>;
				 if (redBlackNode.Color == NodeColor.Red)
				 {
					 Assert.AreEqual(GetRedBlackTreeNode(redBlackNode.Right).Color, NodeColor.Black);
					 Assert.AreEqual(GetRedBlackTreeNode(redBlackNode.Left).Color, NodeColor.Black);
				 }
				 PrintNode(redBlackNode);
			 }
			);

			Console.WriteLine("CheckRedNode End");

		}

		private RedBlackTreeNode<int> GetRedBlackTreeNode(BinaryTreeNode<int> node)
		{
			return node as RedBlackTreeNode<int>;
		}

		[TestMethod]
		public void RotaleTest()
		{
			var tree = CreateTree1() as RedBlackTree<int>;

			var node3 = tree.Search(tree.Root, 3);
			var node7 = tree.Search(tree.Root, 7);

			tree.LeftRotate(node3);
			PrintTree(tree);

			tree.RightRotate(node3);
			PrintTree(tree);


			tree.LeftRotate(node7);
			PrintTree(tree);

			tree.RightRotate(node7);
			PrintTree(tree);

		}

		//有啥好的好图工具？
		[TestMethod()]
		public void RebBlackInsertTest()
		{
			//4报错

			var tree = new RedBlackTree<int>();
			foreach (var data in TreeData1)
			{
				var node = new RedBlackTreeNode<int>(data);
				tree.Insert(node);

				CheckRedBlackTree(tree);
			}
		}

		private IEnumerable<int> TreeData2 = new List<int>()
		{
			571,  395,  567,  417,  88,  539,  877,  16,  842,  425,
		};

		[TestMethod()]
		public void RebBlackDeleteTest()
		{
			//4报错
			//RebBlackDelete(TreeData1);

			//for (int i = 0; i < 10; i++)
			//{
			//	RebBlackDelete(GetRandomData().Take(10));
			//}

			RebBlackDelete(TreeData2);
		}


		private void RebBlackDelete(IEnumerable<int> datas)
		{
			var tree = new RedBlackTree<int>();
			foreach (var data in datas)
			{
				var node = new RedBlackTreeNode<int>(data);
				tree.Insert(node);
				//CheckRedBlackTree(tree);
			}
			Console.WriteLine("new tree data");
			foreach (var d in datas)
			{
				Console.Write("{0}  ", d);
			}

			foreach (var data in datas)
			{
				var node = tree.Search(tree.Root, data);

				Console.WriteLine("delete node");
				PrintNode(node);

				tree.Delete(node);
				PrintTree(tree);
				CheckRedBlackTree(tree);
			}
		}



	}
}