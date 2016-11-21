using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class BinaryTreeNode<T>
	{
		public T Data { get; set; }

		public BinaryTreeNode<T> Left { get; set; }

		public BinaryTreeNode<T> Right { get; set; }

		public BinaryTreeNode<T> Parent { get; set; }


		public BinaryTreeNode()
		{

		}

		public BinaryTreeNode(T data)
		{
			Data = data;
		}

		public override string ToString()
		{
			return Data .ToString();
		}
	}
}
