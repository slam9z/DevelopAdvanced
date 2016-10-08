using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public static class TreeNodeExtensions
    {
        public static bool IsEmpty<T>(this BinaryTreeNode<T> node)
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
    }
}
