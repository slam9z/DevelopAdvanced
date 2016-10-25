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
    public class AdjacencyListGraphTests
    {
     
        [TestMethod()]
        public void CreatGraphTest()
        {
        }


        [TestMethod()]
        public void BreadthFirstSearchTest()
        {
            var graph = GraphData.CreateGraph1();
            var source = graph.GetVertexByKey(1);
            Console.WriteLine(source);
            graph.BreadthFirstSearch(source,
                (vertex) =>
                {
                    Console.Write("{0} ,", vertex);
                }
            );
        }

        [TestMethod()]
        public void DepthFirstSearchTest()
        {
            var graph = GraphData.CreateGraph1(true);

            Console.WriteLine();
            Console.WriteLine("DepthFirstSearchTest");
            Console.WriteLine();

            graph.DepthFirstSearch(
                (vertex) =>
                {
                    Console.WriteLine("{0} ,", vertex);
                }
            );

        }

        [TestMethod()]
        public void TopologicalSortTest()
        {
            var graph = GraphData.CreateDirectionNoCircuitGraph1();

            Console.WriteLine("CreateDirectionNoCircuitGraph1");
            Console.WriteLine();
            Console.WriteLine("TopologicalSortTest");
            Console.WriteLine();

            var result = graph.TopologicalSort();

            Console.WriteLine();
            Console.WriteLine("TopologicalSortTestReslut");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }



        }

        [TestMethod()]
        public void PrintPathTest()
        {
            var graph = GraphData.CreateGraph1(true);
            var source = graph.GetVertexByKey(1);
            Console.WriteLine(source);
            graph.BreadthFirstSearch(source,
                (vertex) =>
                {

                }
            );

            Console.WriteLine();
            Console.WriteLine("PrintPath");
            Console.WriteLine();
            graph.GetPath(source, graph.GetVertexByKey(1));
        }

        [TestMethod()]
        public void GetVertexEdgeTest()
        {
            Console.WriteLine("GetVertexEdgeTest");
            var graph = GraphData.CreateDirectionNoCircuitGraph1();
            var source = graph.GetVertexByKey(1);
            var edges = graph.GetVertexEdge(source);
            foreach (var item in edges)
            {
                Console.WriteLine(item);
            }

        }

        [TestMethod()]
        public void CreateTransposeGraphTest()
        {
            var graph = GraphData.CreateGraph1(true);
            var transposeGraph = graph.CreateTransposeGraph();

        }

        [TestMethod()]
        public void GetStronglyConnectedComponenetsTest()
        {
            var graph = GraphData.CreateStronglyConnectedComponenetsGraph1();
            var stonaglyGraphs = graph.GetStronglyConnectedComponenets();
            Assert.AreEqual(2, stonaglyGraphs.Count);

            //好像得不到所有的边，前连通算法无法构成原来的图。
            foreach (var stonaglyGraph in stonaglyGraphs)
            {
                Console.WriteLine("stonaglyGraph {0}", stonaglyGraphs.IndexOf(stonaglyGraph));
                foreach (var item in stonaglyGraph.GetVertexs())
                {
                    Console.WriteLine(item);
                }
            }
        }


        [TestMethod()]
        public void CheckStronglyConnectedGraphTest()
        {
            var graph = GraphData.CreateStronglyConnectedGraph1();
            var stonaglyGraphs = graph.GetStronglyConnectedComponenets();
            Assert.AreEqual(1, stonaglyGraphs.Count);
            Assert.AreEqual(graph.GetVertexs().Count(), stonaglyGraphs[0].GetVertexs().Count());

        }
    }
}