using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    /// <summary>
    /// 这是数据结构有点意思
    /// 算法导论的伪代码看的我莫名奇妙，什么结构都不理解
    /// 网上找的也是，按照自己的理解实现了一个,应该是对的。
    /// 
    /// DisjointSet的基础就是每一个T都不相等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DisjointSet<T> where T : IComparable
    {
        public DisjointSetNode<T> Delegate { get; set; }

        public List<DisjointSetNode<T>> _nodes = new List<DisjointSetNode<T>>();


        public DisjointSet()
        {

        }

        public DisjointSet(T value)
        {
            var node = new DisjointSetNode<T>();
            node.Value = value;
            node.Parent = node;
            node.Rank = 0;

            _nodes.Add(node);

            Delegate = node;
        }

        public DisjointSet<T> Union(DisjointSet<T> set)
        {
            var newdelegate = Link(Find(Delegate), Find(set.Delegate));
            if (newdelegate == Delegate)
            {
                _nodes.AddRange(set._nodes);
                return this;
            }
            else
            {
                set._nodes.AddRange(_nodes);
                return set;
            }
        }

        //虽然代码少，思想和不简单
        public DisjointSetNode<T> Find(DisjointSetNode<T> node)
        {
            if (node != node.Parent)
            {
                node.Parent = Find(node.Parent);
            }
            return node.Parent;
        }

        public IEnumerable<T> GetVaules()
        {
            return _nodes.Select(o => o.Value);
        }

        public DisjointSetNode<T> GetDisjointSetNode(T value)
        {
            return _nodes.Where(o => o.Value.CompareTo(value) == 0).SingleOrDefault();
        }

        private DisjointSetNode<T> Link(DisjointSetNode<T> first, DisjointSetNode<T> second)
        {
            if (first.Rank > second.Rank)
            {
                second.Parent = first;

                return first;
            }

            first.Parent = second;
            if (first.Rank == second.Rank)
            {
                second.Rank = second.Rank + 1;
            }

            return second;
        }


    }
}
