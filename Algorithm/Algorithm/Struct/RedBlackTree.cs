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
				Color = NodeColor.Black,
				IsEmpty = true,
			};


		public RedBlackTree()
		{
			Root = Empty;
		}

		public RedBlackTree(BinaryTreeNode<T> root) : base(root)
		{

		}

		public override void Create(IEnumerable<T> datas)
		{
			foreach (var data in datas)
			{
				var node = new RedBlackTreeNode<T>(data);
				Insert(node);
			}
		}



		public void Insert(RedBlackTreeNode<T> newNode)
		{
			var positionNode = Root;
			var parentNode = positionNode;

			while (!IsEmpty(positionNode))
			{
				parentNode = positionNode;

				if (positionNode.Data.CompareTo(newNode.Data) > 0
					)
				{
					positionNode = positionNode.Left;
				}

				else
				{
					positionNode = positionNode.Right;
				}
			}


			if (IsEmpty(parentNode))
			{
				newNode.Parent = Empty;
				Root = newNode;
			}
			else
			{
				newNode.Parent = parentNode;
				if (parentNode.Data.CompareTo(newNode.Data) > 0)
				{
					parentNode.Left = newNode;
				}
				else
				{
					parentNode.Right = newNode;
				}
			}


			newNode.Left = Empty;
			newNode.Right = Empty;
			newNode.Color = NodeColor.Red;
			InsertFixup(newNode);

		}

		private void InsertFixup(RedBlackTreeNode<T> fixupNode)
		{

			while (fixupNode.GetParentNode().Color == NodeColor.Red)
			{
				var parent = fixupNode.GetParentNode();
				var grandfather = parent.GetParentNode();

				if (parent == grandfather.Left)
				{
					var uncle = DowntoRedBlackTreeNode(grandfather.Right);

					#region case 1 red up
					if (uncle.Color == NodeColor.Red)
					{
						parent.Color = NodeColor.Black;
						uncle.Color = NodeColor.Black;
						grandfather.Color = NodeColor.Red;
						fixupNode = grandfather;
					}

					#endregion

					else
					{
						if (fixupNode == parent.Right)
						{
							fixupNode = fixupNode.GetParentNode();
							LeftRotate(fixupNode);
						}

						fixupNode.GetParentNode().Color = NodeColor.Black;
						var tempGrandfather = fixupNode.GetParentNode().GetParentNode();
						tempGrandfather.Color = NodeColor.Red;

						RightRotate(tempGrandfather);
					}
				}
				else
				{
					var uncle = DowntoRedBlackTreeNode(grandfather.Left);

					#region case 1 red up
					if (uncle.Color == NodeColor.Red)
					{
						parent.Color = NodeColor.Black;
						uncle.Color = NodeColor.Black;
						grandfather.Color = NodeColor.Red;
						fixupNode = grandfather;
					}

					#endregion

					else
					{
						if (fixupNode == parent.Left)
						{
							fixupNode = fixupNode.GetParentNode();
							RightRotate(fixupNode);
						}

						fixupNode.GetParentNode().Color = NodeColor.Black;
						var tempGrandfather = fixupNode.GetParentNode().GetParentNode();
						tempGrandfather.Color = NodeColor.Red;

						LeftRotate(tempGrandfather);
					}
				}
			}

			var root = DowntoRedBlackTreeNode(Root);
			//保证性质2
			root.Color = NodeColor.Black;
		}

		/// <summary>
		/// 各种遍历还是很有用的，很多操作需要换操作类型。
		/// </summary>
		public void ReplaceNullToEmpty()
		{

			Postorder(Root,
				(node) =>
				{
					if (node.Parent == null)
					{
						node.Parent = Empty;
					}
					if (node.Left == null)
					{
						node.Left = Empty;
					}
					if (node.Right == null)
					{
						node.Right = Empty;
					}
				});
		}


		#region Rotate  //看着图写步骤，并不是太复杂的逻辑


		public void LeftRotate(BinaryTreeNode<T> sourceNode)
		{
			var targetNode = sourceNode.Right;

			#region //使targetNode的Left节点变成source的right节点

			sourceNode.Right = targetNode.Left;
			targetNode.Left.Parent = sourceNode;

			#endregion


			#region //使targetNode的变成source的父亲节点

			targetNode.Parent = sourceNode.Parent;

			//漏掉的逻辑,设置parent引用
			if (IsEmpty(sourceNode.Parent))
			{
				Root = targetNode;
			}
			else if (sourceNode == targetNode.Parent.Left)
			{
				sourceNode.Parent.Left = targetNode;
			}
			else
			{
				sourceNode.Parent.Right = targetNode;

			}

			targetNode.Left = sourceNode;

			sourceNode.Parent = targetNode;


			#endregion

		}


		public void RightRotate(BinaryTreeNode<T> sourceNode)
		{
			var targetNode = sourceNode.Left;

			#region 使target的right节点成为source的left节点

			sourceNode.Left = targetNode.Right;
			sourceNode.Right.Parent = targetNode;

			#endregion

			#region 使target的成为source的parent node

			targetNode.Parent = sourceNode.Parent;

			//设置parent的引用
			if (IsEmpty(sourceNode.Parent))
			{
				Root = targetNode;
			}
			else if (targetNode.Parent.Right == sourceNode)
			{
				sourceNode.Parent.Right = targetNode;
			}
			else
			{
				sourceNode.Parent.Left = targetNode;
			}

			targetNode.Right = sourceNode;

			sourceNode.Parent =targetNode;

			#endregion

		}


		#endregion


		public RedBlackTreeNode<T> DowntoRedBlackTreeNode(BinaryTreeNode<T> node)
		{
			return node as RedBlackTreeNode<T>;
		}

	}


}
