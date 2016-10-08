using Algorithm.Struct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    public static class BinarySearchTreeExtensions
    {
        public static void CheckNodeParent(this BinarySearchTree<int> tree)
        {
            tree.Inorder(tree.Root, (node) =>
            {
                if (!node.Left.IsEmpty())
                {
                    if (node.Left.Parent != node)
                    {
                    }

                    Assert.AreEqual(node.Left.Parent, node);
                }
                if (!node.Right.IsEmpty())
                {
                    if (node.Right.Parent != node)
                    {
                    }

                    Assert.AreEqual(node.Right.Parent, node);
                }
            });
        }

    }
}
