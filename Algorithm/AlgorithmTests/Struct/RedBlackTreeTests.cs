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
	public class RedBlackTreeTests
	{
		[TestMethod()]
		public void RedBlackTreeTest()
		{
			Assert.Fail();
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
			tree.Inorder(tree.Root, (node) =>
			 {
				 var redBlackNode = node as RedBlackTreeNode<int>;
				 if (redBlackNode.Color == NodeColor.Red)
				 {
					 Assert.AreEqual(GetRedBlackTreeNode(redBlackNode.Right).Color, NodeColor.Black);
					 Assert.AreEqual(GetRedBlackTreeNode(redBlackNode.Left).Color, NodeColor.Black);
				 }
			 }
			);
		}



		private RedBlackTreeNode<int> GetRedBlackTreeNode(BinaryTreeNode<int> node)
		{
			return node as RedBlackTreeNode<int>;
		}
	}
}