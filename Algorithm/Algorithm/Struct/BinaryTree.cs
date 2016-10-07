using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class BinaryTree<T>
	{
		public BinaryTreeNode<T> Root { get; protected set; }

		public BinaryTree()
		{

		}

		public BinaryTree(BinaryTreeNode<T> root)
		{
			Root = root;
		}

		public virtual void Create(IEnumerable<T> datas)
		{
		}

		public virtual void Insert(BinaryTreeNode<T> newNode)
		{
		}

		public virtual void Delete(BinaryTreeNode<T> node)
		{

		}

		protected bool IsEmpty(BinaryTreeNode<T> node)
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

		/// <summary>
		/// 后序
		/// </summary>
		/// <param name="parent"></param>
		//TODO:应该使用动词·
		public void Postorder(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> action)
		{
			if (!IsEmpty(node))
			{
				Postorder(node.Left, action);
				Postorder(node.Right, action);
				action(node);
			}
		}

		/// <summary>
		/// 先序  Predecessor
		/// </summary>
		/// <param name="node"></param>
		/// <param name="action"></param>
		public void Preorder(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> action)
		{
			if (!IsEmpty(node))
			{
				action(node);
				Preorder(node.Left, action);
				Preorder(node.Right, action);
			}
		}

		/// <summary>
		/// 中序
		/// </summary>
		/// <param name="node"></param>
		/// <param name="action"></param>
		public void Inorder(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> action)
		{
			if (!IsEmpty(node))
			{
				Inorder(node.Left, action);
				action(node);
				Inorder(node.Right, action);
			}
		}

		public void SetNodeParent()
		{
			SetNodeParentCore(Root);
		}

		private void SetNodeParentCore(BinaryTreeNode<T> node)
		{
			if (node != null)
			{
				if (node.Left != null && node.Left.Parent == null)
				{
					node.Left.Parent = node;
					SetNodeParentCore(node.Left);
				}

				if (node.Right != null && node.Right.Parent == null)
				{
					node.Right.Parent = node;
					SetNodeParentCore(node.Right);
				}

			}
		}
	}
}
