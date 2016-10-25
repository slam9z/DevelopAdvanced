using Algorithm.Struct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct.Graph.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Graph.Extention.Tests
{

}

namespace Algorithm.Struct.Tests
{




    [TestClass()]
    public class ShortestPathExtentionsTests
    {
        [TestMethod()]
        public void BellmanFordTest()
        {
            var graph = GraphData.CreateShortestPathGraph1();
            var source = graph.GetVertexByKey(3);
            var edges = graph.GetEdges();
            var vertexs = graph.GetVertexs();

            foreach (var item in edges)
            {
                Console.WriteLine($"{item}");
            }

            var hasEdge = graph.BellmanFord(source, GetEdgeWeight);
            Assert.AreEqual(false, hasEdge);

            foreach (var item in vertexs)
            {
                Console.WriteLine($"{item}\n{item.WeightBound}");
            }
        }


        [TestMethod()]
        public void DijkstraTest()
        {
            var graph = GraphData.CreateShortestPathGraph1();
            var source = graph.GetVertexByKey(3);
            var edges = graph.GetEdges();
            var vertexs = graph.GetVertexs();

            foreach (var item in edges)
            {
                Console.WriteLine($"{item}");
            }

            graph.Dijkstra(source, GetEdgeWeight);


            foreach (var item in vertexs)
            {
                Console.WriteLine($"{item}\n{item.WeightBound}");
            }
        }


        [TestMethod()]
        public void DAGShortestPathTest()
        {
            var graph = GraphData.CreateDAGShortestPathGraph1();
            var source = graph.GetVertexByKey(3);
            var edges = graph.GetEdges();
            var vertexs = graph.GetVertexs();

            foreach (var item in edges)
            {
                Console.WriteLine($"{item}");
            }

            graph.DAGShortestPath(source, GetEdgeWeight);

            foreach (var item in vertexs)
            {
                Console.WriteLine($"{item}\n{item.WeightBound}");
            }
        }


        private int GetEdgeWeight(AdjacencyListGraph<int> graph, AdjacencyVertex<int> first, AdjacencyVertex<int> second)
        {
            var edge = graph.GetEdge(first, second);
            return edge == null ? int.MaxValue : edge.Weight;
        }

        private Random random = new Random();

        private int GetEdgeWeight(AdjacencyListGraph<int> graph, AdjacencyEdge<int> edge)
        {
            return random.Next(1, 10);
        }


    }
}