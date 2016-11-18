using Algorithm.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    //在不知道怎么设计的时候，先做出基本功能再优化，做的过程中会有思路的。

    public static class MininumSpanningTreeExtentions
    {
        /// <summary>
        /// MST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="weightFunc"></param>
        /// <returns></returns>
        public static IEnumerable<AdjacencyEdge<T>> GetMininumSpanningTreeKruskal<T>(
           this AdjacencyListGraph<T> graph,
           Func<AdjacencyEdge<T>, int> weightFunc
            ) where T : IEquatable<T>
        {
            var vertexs = graph.GetVertexs();
            var edges = graph.GetEdges().ToList();

            var sets = new Dictionary<AdjacencyVertex<T>, DisjointSet<AdjacencyVertex<T>>>();

            var result = new List<AdjacencyEdge<T>>();

            foreach (var vertex in vertexs)
            {
                sets[vertex] = (new DisjointSet<AdjacencyVertex<T>>(vertex));
            }

            foreach (var edge in edges)
            {
                edge.Weight = weightFunc(edge);
            }

            //吃自己写的狗粮,这狗粮不好吃啊
            var sort = new QuickSort();
            var sortEdges = sort.Sort(edges, (f, s) => f.Weight.CompareTo(s.Weight) > 0);

            foreach (var sortEdge in sortEdges)
            {
                var startDisjointSet = sets[sortEdge.Start];
                var endDisjointSet = sets[sortEdge.End];

                DisjointSet<AdjacencyVertex<T>> newSet;

                if (startDisjointSet.Find(startDisjointSet.GetNode(sortEdge.Start))
                    != endDisjointSet.Find(endDisjointSet.GetNode(sortEdge.End)))
                {
                    newSet = startDisjointSet.Union(endDisjointSet);

                    result.Add(sortEdge);
                    sets[sortEdge.Start] = newSet;
                    sets[sortEdge.End] = newSet;
                }
            }

            return result;
        }


        /// <summary>
        /// 怎么输出最小生成树？
        /// 很神奇的是这个贪心算法是对的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="root"></param>
        /// <param name="weightFunc"></param>
        public static IList<AdjacencyEdge<T>> GetMininumSpanningTreePrim<T>(
            this AdjacencyListGraph<T> graph,
            AdjacencyVertex<T> root,
            Func<AdjacencyEdge<T>, int> weightFunc
            ) where T : IEquatable<T>
        {
            var vertexs = graph.GetVertexs().ToList();

            //TempStorage顶点到到某一顶点相连的最小权值。
            foreach (var vertex in vertexs)
            {
                vertex.TempStorage = int.MaxValue;
                vertex.Predecessor = null;
            }
            root.TempStorage = 0;


            var result = new List<AdjacencyEdge<T>>();

            var queue = new ExtentionBinanyHeap<AdjacencyVertex<T>>(vertexs, (f, s) => f.TempStorage < s.TempStorage);

            while (!queue.IsEmpty)
            {
                var min = queue.Extract();
                if (min != root)
                {
                    result.Add(graph.GetEdge(min.Predecessor, min));
                }

                var edges = graph.GetVertexEdge(min);
                foreach (var edge in edges)
                {
                    var weight = weightFunc(edge);
                    //TODO: 还有一个包含判断
                    if (edge.End.BelongedHeap == queue && weight < edge.End.TempStorage)
                    {
                        edge.End.Predecessor = min;
                        edge.End.TempStorage = weight;
                        queue.UpdateKey(edge.End, edge.End);
                    }
                }
            }

            return result;

        }
    }
}