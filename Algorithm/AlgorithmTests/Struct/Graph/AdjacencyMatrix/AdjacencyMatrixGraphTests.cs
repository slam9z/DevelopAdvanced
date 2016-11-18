using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    [TestClass()]
    public class AdjacencyMatrixGraphTests
    {
        [TestMethod()]
        public void SlowAllPairsShortestPathTest()
        {
            var graph = GraphData.CreateShortestPathMartrixGraph1();
            graph.PrintMartrix(graph.Matrix);
            var martrix = graph.SlowAllPairsShortestPath();
        }

        [TestMethod()]
        public void FastAllPairsShortestPathTest()
        {
            var graph = GraphData.CreateShortestPathMartrixGraph1();
            graph.PrintMartrix(graph.Matrix);
            var martrix = graph.FastAllPairsShortestPath();
        }

        [TestMethod()]
        public void FloydWarshallTest()
        {
            var graph = GraphData.CreateShortestPathMartrixGraph1();
            graph.PrintMartrix(graph.Matrix);
            var martrix = graph.FloydWarshall();
            graph.PrintMartrix(martrix);
        }
    }
}