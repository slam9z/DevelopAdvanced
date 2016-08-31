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

	public class RedBlackTreeNode<T> : BinaryTreeNode<T>
	{
		public NodeColor Color { get; set; }
	}
}
