using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	//插入和删除太复杂了
	public class RedBlackTree<T> : BinarySearchTree<T> where T : IComparable
	{
		public static RedBlackTreeNode<T> Empty { get; set; }
			= new RedBlackTreeNode<T>()
			{
				Color = NodeColor.Black
			};
	}


}
