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
            BinaryTreeNode<T> parentNode = Empty;
            //var parentNode = positionNode;

            while (!positionNode.IsEmpty())
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

            newNode.Parent = parentNode;

            if (parentNode.IsEmpty())
            {
                Root = newNode;
            }
            else
            {
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

        private void InsertFixup(RedBlackTreeNode<T> node)
        {

            while (node.GetParentNode().Color == NodeColor.Red)
            {
                #region Left

                if (node.GetParentNode() == node.GetGrandparentNode().Left)
                {
                    var uncle = node.GetGrandparentNode().Right.ToRedBlackTreeNode();

                    #region //case 1 uncle is red then red up
                    if (uncle.Color == NodeColor.Red)
                    {
                        node.GetParentNode().Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.GetGrandparentNode().Color = NodeColor.Red;

                        node = node.GetGrandparentNode();
                    }

                    #endregion

                    else
                    {
                        #region // case 2  uncle is black, node is right 
                        if (node == node.GetParentNode().Right)
                        {
                            node = node.GetParentNode();

                            LeftRotate(node);
                        }
                        #endregion

                        #region // case 2  uncle is black, node is left。变成一条线再旋转 

                        node.GetParentNode().Color = NodeColor.Black;
                        node.GetGrandparentNode().Color = NodeColor.Red;

                        RightRotate(node.GetGrandparentNode());

                        #endregion
                    }

                }
                #endregion

                #region right
                else
                {
                    var uncle = node.GetGrandparentNode().Left.ToRedBlackTreeNode();

                    #region case 1 red up
                    if (uncle.Color == NodeColor.Red)
                    {
                        node.GetParentNode().Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.GetGrandparentNode().Color = NodeColor.Red;

                        node = node.GetGrandparentNode();
                    }

                    #endregion

                    else
                    {
                        if (node == node.GetParentNode().Left)
                        {
                            node = node.GetParentNode();

                            RightRotate(node);
                        }

                        node.GetParentNode().Color = NodeColor.Black;
                        node.GetGrandparentNode().Color = NodeColor.Red;

                        LeftRotate(node.GetGrandparentNode());
                    }
                }
                #endregion
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
            if (node.IsEmpty())
            {
                throw new ArgumentException("node can't empty");
            }

            RedBlackTreeNode<T> deleteNode;
            RedBlackTreeNode<T> deleteNodeChild;


            if (node.Left.IsEmpty() || node.Right.IsEmpty())
            {
                deleteNode = node;
            }
            else
            {
                deleteNode = Successor(node).ToRedBlackTreeNode();
            }


            if (!deleteNode.Left.IsEmpty())
            {
                deleteNodeChild = deleteNode.Left.ToRedBlackTreeNode();
            }
            else
            {
                deleteNodeChild = deleteNode.Right.ToRedBlackTreeNode();
            }



            deleteNodeChild.Parent = deleteNode.Parent;


            if (deleteNodeChild.Parent.IsEmpty())
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

        //空节点的Parent属性不可靠，只能存储起来。
        private void DeleteFixup(RedBlackTreeNode<T> node)
        {
            while (node != Root && node.Color == NodeColor.Black)
            {
                var parent = node.Parent.ToRedBlackTreeNode();

                #region left
                if (node == parent.Left)
                {
                    #region case1   brother is red

                    var brother = parent.Right.ToRedBlackTreeNode();
                    if (brother.Color == NodeColor.Red)
                    {
                        brother.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        LeftRotate(parent);
                        brother = parent.Right.ToRedBlackTreeNode();

                    }

                    #endregion


                    #region case2 brother  child  all  black

                    if (
                        (
                             brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black)
                        &&
                        (
                             brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black))
                    {
                        brother.Color = NodeColor.Red;
                        node = parent;
                        parent = node.GetParentNode();
                    }

                    #endregion

                    else
                    {
                        #region  case3 brother  right child is black

                        if (
                            brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black)
                        {
                            brother.Left.ToRedBlackTreeNode().Color = NodeColor.Black;
                            brother.Color = NodeColor.Red;
                            RightRotate(brother);
                            brother = parent.Right.ToRedBlackTreeNode();
                        }

                        #endregion

                        #region case4 brother right child is red

                        brother.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        brother.Right.ToRedBlackTreeNode().Color = NodeColor.Black;
                        LeftRotate(parent);
                        node = Root.ToRedBlackTreeNode();


                        #endregion
                    }
                }
                #endregion

                #region right
                else
                {
                    #region case1   brother is red

                    var brother = parent.Left.ToRedBlackTreeNode();
                    if (brother.Color == NodeColor.Red)
                    {
                        brother.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        RightRotate(parent);
                        brother = parent.Left.ToRedBlackTreeNode();
                    }

                    #endregion



                    #region case2 brother  child  all  black

                    if (
                        (
                             brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black)
                        && (
                             brother.Right.ToRedBlackTreeNode().Color == NodeColor.Black))
                    {
                        brother.Color = NodeColor.Red;
                        node = parent;
                        parent = node.GetParentNode();
                    }

                    #endregion

                    else
                    {
                        #region case3  brother right child is black

                        if (brother.Left.ToRedBlackTreeNode().Color == NodeColor.Black)
                        {
                            brother.Right.ToRedBlackTreeNode().Color = NodeColor.Black;
                            brother.Color = NodeColor.Red;
                            RightRotate(brother);
                            brother = parent.Left.ToRedBlackTreeNode();
                        }

                        #endregion


                        #region  case4 brother right child is red

                        brother.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        brother.Left.ToRedBlackTreeNode().Color = NodeColor.Black;
                        RightRotate(parent);
                        node = Root.ToRedBlackTreeNode();

                        #endregion
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
            if (sourceNode.Parent.IsEmpty())
            {
                Root = targetNode;
            }
            else if (sourceNode == sourceNode.Parent.Left)
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
            //难怪导致一堆问题
            // sourceNode.Right.Parent = targetNode;
            targetNode.Right.Parent = sourceNode;


            #endregion

            #region 使target的成为source的parent node

            targetNode.Parent = sourceNode.Parent;

            //设置parent的引用
            if (sourceNode.Parent.IsEmpty())
            {
                Root = targetNode;
            }
            else if (sourceNode.Parent.Right == sourceNode)
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
