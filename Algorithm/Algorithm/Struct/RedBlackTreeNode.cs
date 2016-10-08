using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public enum NodeColor
	{
		Black = 0,
		Red = 1,
	}

	public static class BinaryTreeNodeExtentions
	{

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

	public class RedBlackTreeNode<T> : BinaryTreeNode<T>, IEmptyNodeInterface
	{
		public NodeColor Color { get; set; }

		public bool IsEmpty
		{
			get; set;
		}

		public RedBlackTreeNode()
		{

		}

		public RedBlackTreeNode(T data)
		{
			Data = data;
		}

		public RedBlackTreeNode<T> GetParentNode()
		{
			return Parent as RedBlackTreeNode<T>;
		}

        public RedBlackTreeNode<T> GetGrandparentNode()
        {
            return GetParentNode().GetParentNode();
        }
    }


}
