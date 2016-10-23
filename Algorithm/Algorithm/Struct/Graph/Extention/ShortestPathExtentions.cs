using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public static class ShortestPathExtentions
    {

        /// <summary>
        /// 有向无回路图
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="weightFuc"></param>
        /// <returns></returns>
        public static void DAGShortestPath<T>(
          this AdjacencyListGraph<T> graph,
          AdjacencyVertex<T> source,
          Func<AdjacencyListGraph<T>, AdjacencyVertex<T>, AdjacencyVertex<T>, int> weightFuc
       ) where T : IEquatable<T>
        {
            graph.TopologicalSort();

            graph.InitializeSingleSource(source);

            var vertexs = graph.GetVertexs();

            foreach (var orderVertex in vertexs)
            {
                var edges = graph.GetVertexEdge(orderVertex);

                foreach (var edge in edges)
                {
                    graph.Relax(edge.Start, edge.End, weightFuc);
                }
            }


        }

        /// <summary>
        /// 单源最短路径会把一个点到其它所有点的最短路径算出来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="weightFuc"></param>
        /// <returns></returns>
        public static bool BellmanFord<T>(
            this AdjacencyListGraph<T> graph,
            AdjacencyVertex<T> source,
            Func<AdjacencyListGraph<T>, AdjacencyVertex<T>, AdjacencyVertex<T>, int> weightFuc
        ) where T : IEquatable<T>
        {
            graph.InitializeSingleSource(source);

            for (int i = 1; i <= graph.VertexLenght; i++)
            {
                foreach (var edge in graph.GetEdges())
                {
                    graph.Relax(edge.Start, edge.End, weightFuc);
                }
            }

            foreach (var edge in graph.GetEdges())
            {
                if (edge.Start.WeightBound >
                     Add(edge.End.WeightBound, weightFuc(graph, edge.Start, edge.End)))
                {
                    return false;
                }
            }

            return true;
        }



        /// <summary>
        /// 单源最短路径会把一个点到其它所有点的最短路径算出来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="weightFuc"></param>
        /// <returns></returns>
        public static void Dijkstra<T>(
            this AdjacencyListGraph<T> graph,
            AdjacencyVertex<T> source,
            Func<AdjacencyListGraph<T>, AdjacencyVertex<T>, AdjacencyVertex<T>, int> weightFuc
        ) where T : IEquatable<T>
        {
            graph.InitializeSingleSource(source);

            var calcVertexs = new List<AdjacencyVertex<T>>();

            var vertexs = graph.GetVertexs().ToList();

            //不想再增加AdjacencyVertex节点的负担了

   

            var queue = new MapBinanyHeap<AdjacencyVertex<T>>
                (
                  vertexs,
                  (first, second) =>
                  {
                      return first.WeightBound < second.WeightBound;
                  }
                );

            //  while (!queue.IsEmpty) 竟然把while写成if有才
            while (!queue.IsEmpty)
            {
                var min = queue.Extract();
                calcVertexs.Add(min);

                var edges = graph.GetVertexEdge(min);
                foreach (var edge in edges)
                {
                    var oldWeight = edge.End.WeightBound;
                    Relax(graph, edge.Start, edge.End, weightFuc);
                    //没有索引位置一下让我傻逼了!网上找了一下也是有调整的。

                    if (oldWeight != edge.End.WeightBound)
                    {
                        queue.UpdateKey(edge.End,edge.End);
                    }

                }
            }
        }


        public static void InitializeSingleSource<T>(
        this AdjacencyListGraph<T> graph,
        AdjacencyVertex<T> source
        ) where T : IEquatable<T>
        {
            var vertexs = graph.GetVertexs();

            foreach (var vertex in vertexs)
            {
                vertex.WeightBound = int.MaxValue;
                vertex.Predecessor = null;
            }

            source.WeightBound = 0;
        }


        public static void Relax<T>(
            this AdjacencyListGraph<T> graph,
            AdjacencyVertex<T> first,
            AdjacencyVertex<T> second,
            Func<AdjacencyListGraph<T>, AdjacencyVertex<T>, AdjacencyVertex<T>, int> weightFuc
         ) where T : IEquatable<T>
        {
            var newBound = Add(first.WeightBound, weightFuc(graph, first, second));
            if (second.WeightBound > newBound)
            {
                second.WeightBound = newBound;
                second.Predecessor = first;
            }
        }

        private static int Add(int x, int y)
        {
            if (x == int.MaxValue || y == int.MaxValue)
            {
                return int.MaxValue;
            }
            return x + y;
        }

    }
}
