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

		#region insert

		public override void Insert(BinaryTreeNode<T> newNode)
		{
			Insert(newNode as RedBlackTreeNode<T>);
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
					var uncle = grandfather.Right.ToRedBlackTreeNode();

					#region //case 1 uncle is red then red up
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
						#region // case 2  uncle is black, node is right 
						if (fixupNode == parent.Right)
						{
							fixupNode = fixupNode.GetParentNode();
							LeftRotate(fixupNode);
						}
						#endregion

						#region // case 2  uncle is black, node is left。变成一条线再旋转 

						fixupNode.GetParentNode().Color = NodeColor.Black;
						var tempGrandfather = fixupNode.GetParentNode().GetParentNode();
						tempGrandfather.Color = NodeColor.Red;

						RightRotate(tempGrandfather);

						#endregion
					}

				}
				else
				{
					var uncle = grandfather.Left.ToRedBlackTreeNode();

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

			var root = Root.ToRedBlackTreeNode();
			//保证性质2
			root.Color = NodeColor.Black;
		}

		#endregion

		#region delete

		public override void Delete(BinaryTreeNode<T> node)
		{
			Delete(node as RedBlackTreeNode<T>);
		}

		public void Delete(RedBlackTreeNode<T> node)
		{
			RedBlackTreeNode<T> deleteNode;
			RedBlackTreeNode<T> deleteNodeChild;


			if (IsEmpty(node.Left) || IsEmpty(node.Right))
			{
				deleteNode = node;
			}
			else
			{
				deleteNode = Successor(node).ToRedBlackTreeNode();
			}


			if (!IsEmpty(deleteNode.Left))
			{
				deleteNodeChild = deleteNode.Left.ToRedBlackTreeNode();
			}
			else
			{
				deleteNodeChild = deleteNode.Right.ToRedBlackTreeNode();
			}



			deleteNodeChild.Parent = deleteNode.Parent;


			if (IsEmpty(deleteNodeChild.Parent))
			{
				Root = deleteNodeChild;
			}
			else if (deleteNodeChild.Parent.Left == deleteNode)
			{
				deleteNodeChild.Parent.Left = deleteNodeChild;
			}
			else
			{
				deleteNodeChild.Parent.Right = deleteNodeChild;
			}


			if (deleteNode != node)
			{
				node.Data = deleteNode.Data;
			}

			if (deleteNode.Color == NodeColor.Black)
			{
				DeleteFixup(deleteNodeChild);
			}

		}

		private void DeleteFixup(RedBlackTreeNode<T> node)
		{
			while (node != Root && node.Color == NodeColor.Black)
			{
				#region left
				if (node == node.Parent.Left)
				{
					#region case1   brother is red

					var brother = node.Parent.Right.ToRedBlackTreeNode();
					if (brother.Color == NodeColor.Red)
					{
						brother.Color = NodeColor.Black;
						node.GetParentNode().Color = NodeColor.Red;
						LeftRotate(node.GetParentNode());
						brother = node.GetParentNode().Right.ToRedBlackTreeNode();
					}

					#endregion

					#region brother  child  all  black

					if (brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black
						&& brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black)
					{
						brother.Color = NodeColor.Red;
						node = node.GetParentNode();
					}

					#endregion

					#region brother  left child  is red  right child is black

					else if (brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black
					&& brother.Right.ToRedBlackTreeNode().Color == NodeColor.Red)
					{
						brother.Color = NodeColor.Red;
						RightRotate(brother);
						brother = node.Parent.Right.ToRedBlackTreeNode();
					}

					#endregion

					else
					{
						brother.Color = node.GetParentNode().Color;
						node.GetParentNode().Color = NodeColor.Black;
						brother.Right.ToRedBlackTreeNode().Color = NodeColor.Black;
						LeftRotate(node.GetParentNode());
						node = Root.ToRedBlackTreeNode();
					}
				}
				#endregion

				#region right
				else
				{
					#region case1   brother is red

					var brother = node.Parent.Left.ToRedBlackTreeNode();
					if (brother.Color == NodeColor.Red)
					{
						brother.Color = NodeColor.Black;
						node.GetParentNode().Color = NodeColor.Red;
						RightRotate(node.GetParentNode());
						brother = node.GetParentNode().Left.ToRedBlackTreeNode();
					}

					#endregion

					#region brother  child  all  black

					if (brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black
						&& brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black)
					{
						brother.Color = NodeColor.Red;
						node = node.GetParentNode();
					}

					#endregion

					#region brother  left child  is red  right child is black

					else if (brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black
					&& brother.Left.ToRedBlackTreeNode().Color == NodeColor.Red)
					{
						brother.Color = NodeColor.Red;
						RightRotate(brother);
						brother = node.Parent.Left.ToRedBlackTreeNode();
					}

					#endregion

					else
					{
						brother.Color = node.GetParentNode().Color;
						node.GetParentNode().Color = NodeColor.Black;
						brother.Left.ToRedBlackTreeNode().Color = NodeColor.Black;
						RightRotate(node.GetParentNode());
						node = Root.ToRedBlackTreeNode();
					}

				}

				#endregion
			}
			node.Color = NodeColor.Black;
		}

		#endregion

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

			sourceNode.Parent = targetNode;

			#endregion

		}


		#endregion


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



	}


}
