using Algorithm.Struct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    public static class RedBlackTreeExtensions
    {
        public static void CheckRedBlackTree(this RedBlackTree<int> tree)
        {
            CheckRoot(tree);
            CheckRedNode(tree);
            tree.CheckNodeParent();
        }


        public static void CheckRoot(this RedBlackTree<int> tree)
        {
            Assert.AreEqual(tree.Root.ToRedBlackTreeNode().Color, NodeColor.Black);
        }


        public static void CheckRedNode(this RedBlackTree<int> tree)
        {
           // Console.WriteLine("CheckRedNode Start");
            tree.Inorder(tree.Root, (node) =>
            {
                var redBlackNode = node as RedBlackTreeNode<int>;
                if (redBlackNode.Color == NodeColor.Red)
                {
                    Assert.AreEqual(redBlackNode.Right.ToRedBlackTreeNode().Color, NodeColor.Black);
                    Assert.AreEqual(redBlackNode.Left.ToRedBlackTreeNode().Color, NodeColor.Black);
                }
            //    PrintNode(redBlackNode);
            }
            );

           // Console.WriteLine("CheckRedNode End");

        }

    }
}
