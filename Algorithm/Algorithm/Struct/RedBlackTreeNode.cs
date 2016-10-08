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

	
    }


}
