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

		public override void Create(IEnumerable<T> datas)
		{
			foreach (var data in datas)
			{
				var node = new BinaryTreeNode<T>(data);
				Insert(node);
			}
		}

		public override void Insert(BinaryTreeNode<T> newNode)
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

		public override void Delete(BinaryTreeNode<T> node)
		{
			//没有子女   直接去掉父节点的引用
			//两个节点   删除后继点，然后与当前值交换
			//只有一个子节点   建立父节点与子节点的链接

			//3种情况直接写会包含很多冗余代码，删除是一个比较复杂的操作

			BinaryTreeNode<T> deleteNode;
			BinaryTreeNode<T> deleteNodeChild;


			#region //确定要删除的点
			if (node.Left == null || node.Right == null)
			{
				deleteNode = node;
			}
			else
			{
				deleteNode = Successor(node);
			}
			#endregion

			#region //Child
			if (deleteNode.Left != null)
			{
				deleteNodeChild = deleteNode.Left;
			}
			else
			{
				deleteNodeChild = deleteNode.Right;
			}

			#endregion

			#region //通过修改指针删除 deleteNode

			if (deleteNodeChild != null)
			{
				deleteNodeChild.Parent = deleteNode.Parent;
			}

			if (deleteNode.Parent == null)
			{
				Root = deleteNodeChild;
			}
			else if (deleteNode == deleteNode.Parent.Left)
			{
				deleteNode.Parent.Left = deleteNodeChild;
			}
			else
			{
				deleteNode.Parent.Right = deleteNodeChild;
			}

			#endregion


			#region //如果node的后继就是要删除的点，就把deleteNode复制过去
			if (deleteNode != node)
			{
				node.Data = deleteNode.Data;
				//不要改变删除对象的数据，不这样更新很复杂
			}

			#endregion
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
