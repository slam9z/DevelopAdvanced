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
           Func<AdjacencyListGraph<T>, AdjacencyEdge<T>, int> weightFunc
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
                edge.Weight = weightFunc(graph, edge);
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


        public static IEnumerable<AdjacencyEdge<T>> GetMininumSpanningTreePrim<T>(this AdjacencyListGraph<T> graph,
           Func<AdjacencyListGraph<T>, AdjacencyEdge<T>, int> weightFunc
            ) where T : IEquatable<T>
        {
            return null;
        }
    }
}