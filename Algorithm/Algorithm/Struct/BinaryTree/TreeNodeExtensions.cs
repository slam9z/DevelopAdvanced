using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public static class BinaryTreeNodeExtentions
    {

        public static bool IsEmpty<T>(this BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return true;
            }
            var maybeEmptyNode = node as IEmptyNodeInterface;

            if (maybeEmptyNode != null && maybeEmptyNode.IsEmpty)
            {
                return true;
            }
            return false;
        }
        public static RedBlackTreeNode<T> ToRedBlackTreeNode<T>(this BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            var rebBlackNode = node as RedBlackTreeNode<T>;
            if (rebBlackNode != null)
            {
                return rebBlackNode;
            }

            throw new InvalidCastException(" node can't cast to RedBlackTreeNode");
        }


    }

    public static class RedBlackTreeNodeExtentions
    {
        public static RedBlackTreeNode<T> GetParentNode<T>(this RedBlackTreeNode<T> node)
        {
            return node.Parent as RedBlackTreeNode<T>;
        }

        public static RedBlackTreeNode<T> GetGrandparentNode<T>(this RedBlackTreeNode<T> node)
        {
            return node.GetParentNode().GetParentNode();
        }
    }
}
