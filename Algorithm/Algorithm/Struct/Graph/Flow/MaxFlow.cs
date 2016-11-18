using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class MaxFlow<T> where T : IEquatable<T>
    {
        /// <summary>
        /// 看不懂算法导论上的实现
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public int EdmondsKarp(
            AdjacencyListGraph<T> graph
            , AdjacencyVertex<T> source
            , AdjacencyVertex<T> target)
        {

            var maxflow = 0;

            var edges = graph.GetEdges().Select(d => d as FlowEdge<T>);

            foreach (var edge in edges)
            {
                edge.Flow = 0;
            }


            while (true)
            {
                var path = GetResidualPath(graph, source, target);
                if (path == null || path.Count == 0)
                {
                    break;
                }
                var tempFlow = int.MaxValue;

                //感觉第一次写这样的for循环
                for (var edge = path[target]; edge != null; edge = path[edge.Start])
                {
                    tempFlow = Math.Min(tempFlow, edge.ResidualCapacity);
                }

                Console.WriteLine();
                Console.WriteLine($"pathflow:{tempFlow}");

                for (var edge = path[target]; edge != null; edge = path[edge.Start])
                {
                    edge.Flow = edge.Flow + tempFlow;
                    edge.Revolution.Flow = edge.Revolution.Flow - tempFlow;
                    Console.WriteLine($"edge:{edge}");
                }

                maxflow = maxflow + tempFlow;
            }

            return maxflow;
        }

        public IDictionary<AdjacencyVertex<T>, FlowEdge<T>> GetResidualPath(
            AdjacencyListGraph<T> graph
            , AdjacencyVertex<T> source
            , AdjacencyVertex<T> target)
        {

            var path = new Dictionary<AdjacencyVertex<T>, FlowEdge<T>>();

            var vertexs = graph.GetVertexs();
            foreach (var vertex in vertexs)
            {
                vertex.Predecessor = null;
            }

            path[source] = null;

            var queue = new Queue<AdjacencyVertex<T>>();
            queue.Enqueue(source);

            while (!queue.IsEmpty)
            {
                var current = queue.Dequeue();
                var currentEdges = graph.GetVertexEdge(current);
                foreach (FlowEdge<T> currentEdge in currentEdges)
                {
                    if (currentEdge.End != source
                        && currentEdge.End.Predecessor == null
                        && currentEdge.Capacity > currentEdge.Flow
                        )
                    {
                        path[currentEdge.End] = currentEdge;
                        currentEdge.End.Predecessor = current;
                        queue.Enqueue(currentEdge.End);
                    }
                }
            }

            if (target.Predecessor == null)
            {
                return null;
            }

            return path;
        }

        #region PushRelabel

        public int GenericPushRelabel(
             AdjacencyListGraph<T> graph
            , AdjacencyVertex<T> source
            , AdjacencyVertex<T> target
            )
        {
            var maxFolw = 0;

            InitializePreflow(graph, source);

            
            while (true)
            {
                    

            }

            return maxFolw;
        }

        private void InitializePreflow(AdjacencyListGraph<T> graph
            , AdjacencyVertex<T> source)
        {
            var vertexs = graph.GetVertexs();

            foreach (var vertex in vertexs)
            {
                vertex.Height = 0;
                vertex.Preflow = 0;
            }

            var edges = graph.GetEdges();

            foreach (FlowEdge<T> edge in edges)
            {
                edge.Flow = 0;
                edge.Revolution.Flow = 0;
            }

            source.Height = vertexs.Count();

            var sourceEdges = graph.GetVertexEdge(source);

            foreach (FlowEdge<T> sourceEdge in sourceEdges)
            {
                sourceEdge.Flow = sourceEdge.Capacity;
                sourceEdge.Revolution.Flow = -sourceEdge.Capacity;
                sourceEdge.End.Preflow = sourceEdge.Capacity;
                source.Preflow = source.Preflow - sourceEdge.Capacity;
            }

        }

        private void Push(AdjacencyEdge<T> edge)
        {

        }

        private void Relabel(AdjacencyListGraph<T> graph, AdjacencyVertex<T> vertex)
        {

        }

        #endregion
    }
}
