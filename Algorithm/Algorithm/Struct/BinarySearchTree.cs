using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
	{
		public BinarySearchTree() : base()
		{

		}
		public BinarySearchTree(BinaryTreeNode<T> root) : base(root)
		{

		}

		public void Create(IEnumerable<T> datas)
		{
			foreach (var data in datas)
			{
				var node = new BinaryTreeNode<T>(data);
				Insert(node);
			}
		}

		public void Insert(BinaryTreeNode<T> newNode)
		{
			//怎样命名？
			BinaryTreeNode<T> y = null;
			var x = Root;
			while (x != null)
			{
				y = x;
				if (newNode.Data.CompareTo(x.Data) > 0)
				{
					x = x.Right;
				}
				else
				{
					x = x.Left;
				}
			}

			newNode.Parent = y;

			if (y == null)
			{
				Root = newNode;
			}
			else
			{
				if (newNode.Data.CompareTo(y.Data) > 0)
				{
					y.Right = newNode;
				}
				else
				{
					y.Left = newNode;
				}
			}
		}

		public void Delete(BinaryTreeNode<T> node)
		{
			//没有子女   直接去掉父节点的引用
			//两个节点   删除后继点，然后与当前值交换
			//只有一个子节点   建立父节点与子节点的链接

			//3种情况直接写会包含很多冗余代码，删除是一个比较复杂的操作

			BinaryTreeNode<T> y;
			BinaryTreeNode<T> x;

			//确定要删除的点
			if (node.Left == null || node.Right == null)
			{
				y = node;
			}
			else
			{
				y = Successor(node);
			}


			if (y.Left != null)
			{
				x = y.Left;
			}
			else
			{
				x = y.Right;
			}

			if(x!=null)
			{
				x.Parent = y.Parent;
			}

			if (y.Parent == null)
			{
				Root = x;
			}
			else if (y == y.Parent.Left)
			{
				y.Parent.Left = x;
			}
			else
			{
				y.Parent.Right = x;
			}

			if (y != node)
			{
				//node.Data = y.Data;
				//不要改变删除对象的数据，不这样更新很复杂
			}

		}

		public BinaryTreeNode<T> Search(BinaryTreeNode<T> node, T data)
		{
			if (node == null || node.Data.CompareTo(data) == 0)
			{
				return node;
			}

			if (node.Data.CompareTo(data) > 0)
			{
				return Search(node.Left, data);
			}
			else
			{
				return Search(node.Right, data);
			}
		}


		public BinaryTreeNode<T> Minimum(BinaryTreeNode<T> node)
		{
			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}


		public BinaryTreeNode<T> Maximum(BinaryTreeNode<T> node)
		{
			while (node.Right != null)
			{
				node = node.Right;
			}
			return node;
		}


		public BinaryTreeNode<T> Successor(BinaryTreeNode<T> node)
		{
			if (node.Right != null)
			{
				return Minimum(node.Right);
			}
			var parent = node.Parent;
			while (parent != null && parent.Right == node)
			{
				node = parent;
				parent = parent.Parent;
			}

			return parent;

		}

		public BinaryTreeNode<T> Predecessor(BinaryTreeNode<T> node)
		{
			if (node.Left != null)
			{
				return Maximum(node.Left);
			}
			var parent = node.Parent;
			while (parent != null && parent.Left == node)
			{
				node = parent;
				parent = parent.Parent;
			}

			return parent;

		}


	}
}
