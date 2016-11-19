using Algorithm.Extensions;
using Algorithm.Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.GreedyAlgorithms
{
    //huffman 编码也不是非常简单的问题
    public class Huffman
    {

        public BinaryTree<Frequencytem<T>> Compute<T>(IList<Frequencytem<T>> items)
        {
            //头一次碰到普通BinaryTree应用。

            var queue = new BinanyHeap<BinaryTreeNode<Frequencytem<T>>>
                (
                items.Select(i => new BinaryTreeNode<Frequencytem<T>>() { Data = i }).ToList()
                , (a, b) => a.Data.Frequency < b.Data.Frequency
                );

            for (int i = 1; i < items.Count; i++)
            {
                var childRoot = new BinaryTreeNode<Frequencytem<T>>();

                childRoot.Left = queue.Extract();
                childRoot.Right = queue.Extract();
                childRoot.Data = new Frequencytem<T>()
                {
                    Frequency = childRoot.Left.Data.Frequency
                     + childRoot.Right.Data.Frequency
                };

                queue.Insert(childRoot);

            }

            //也是头一次用
            var oneBit = new BitArray(1, true);
            var zeroBit = new BitArray(1, false);

            var tree = new BinaryTree<Frequencytem<T>>(queue.Extract());
            tree.Preorder(tree.Root, (node) =>
                {
                    var code = node.Data.Code;


                    if (node == tree.Root)
                    {
                        code = new BitArray(0);
                    }

                    if (node.Left != null)
                    {
                        node.Left.Data.Code = zeroBit.Append(code);
                    }
                    if (node.Right != null)
                    {
                        node.Right.Data.Code = oneBit.Append(code);
                    }

                }
                );

            return tree;
        }

    }
}
