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
    public class DisjointSet<T> where T :IEquatable<T>
    {
        public DisjointSetNode<T> Delegate { get; set; }

        public IDictionary<T,DisjointSetNode<T>> _nodes = new Dictionary<T,DisjointSetNode<T>>();


        public DisjointSet()
        {

        }

        public DisjointSet(T value)
        {
            var node = new DisjointSetNode<T>();
            node.Value = value;
            node.Parent = node;
            node.Rank = 0;

            _nodes.Add(node.Value,node);

            Delegate = node;
        }

        public DisjointSet<T> Union(DisjointSet<T> set)
        {
            var newdelegate = Link(Find(Delegate), Find(set.Delegate));

            //消灭旧的DisjointSet，而不是创建全新的Set
            //虽然它们的引用还是不一样，但是Delegate和node是完全一样的。

            Delegate = newdelegate;
            set.Delegate=newdelegate;

            foreach (var item in set._nodes)
            {
                _nodes.Add(item);
            }
            set._nodes = _nodes;

            return this;

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


        public IEnumerable<T> GetVaules()
        {
            return _nodes.Values.Select(o => o.Value);
        }

        public IEnumerable<DisjointSetNode<T>> GetNodes()
        {
            return _nodes.Values;
        }

        public DisjointSetNode<T> GetNode(T value)
        {
            return _nodes[value];
        }

       
    }
}
