using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public static class ConnectedComponenetsExtentions
    {
        /// <summary>
        /// 获取连通图的算法是我有信心感觉我自己想的结构是对的，之后继续再验证。
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static IEnumerable<DisjointSet<AdjacencyVertex<T>>> GetConnectedComponenets<T>
            (
            this AdjacencyListGraph<T> graph
            ) where T : IEquatable<T>
        {
            var vertexs = graph.GetVertexs();
            var edges = graph.GetEdges();

            var sets = new Dictionary<AdjacencyVertex<T>, DisjointSet<AdjacencyVertex<T>>>();

            var result = new List<DisjointSet<AdjacencyVertex<T>>>();



            foreach (var vertex in vertexs)
            {
                sets[vertex] = (new DisjointSet<AdjacencyVertex<T>>(vertex));
            }

            foreach (var edge in edges)
            {
                var startDisjointSet = sets[edge.Start];
                var endDisjointSet = sets[edge.End];

                DisjointSet<AdjacencyVertex<T>> newSet;

                if (startDisjointSet.Find(startDisjointSet.GetNode(edge.Start))
                    != endDisjointSet.Find(endDisjointSet.GetNode(edge.End)))
                {
                    newSet = startDisjointSet.Union(endDisjointSet);

                    sets[edge.Start] = newSet;
                    sets[edge.End] = newSet;


                    if (!result.Contains(newSet))
                    {
                        result.Add(newSet);
                    }

                }
            }
            return result;
        }
    }
}
