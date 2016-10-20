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
        #region data

        private AdjacencyListGraph<int> CreateGraph1(bool hasDirection = false)
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5
            };

            var graph = new AdjacencyListGraph<int>(hasDirection);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>
            {
                graph.CreateEdge(vertexs[0],vertexs[1]),
                graph.CreateEdge(vertexs[0],vertexs[4]),

                graph.CreateEdge(vertexs[1],vertexs[2]),
                graph.CreateEdge(vertexs[1],vertexs[3]),
                graph.CreateEdge(vertexs[1],vertexs[4]),

                graph.CreateEdge(vertexs[2],vertexs[3]),
                graph.CreateEdge(vertexs[3],vertexs[4]),

            };

            graph.CreatGraph(vertexs, edges);

            return graph;
        }

        private AdjacencyListGraph<int> CreateDirectionNoCircuitGraph1(bool hasDirection = false)
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5
            };


            var graph = new AdjacencyListGraph<int>(hasDirection);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>
            {
                graph.CreateEdge(vertexs[0],vertexs[1]),
                graph.CreateEdge(vertexs[0],vertexs[4]),

                graph.CreateEdge(vertexs[1],vertexs[2]),
                graph.CreateEdge(vertexs[1],vertexs[3]),

            };

            graph.CreatGraph(vertexs, edges);

            return graph;
        }


        private AdjacencyListGraph<int> CreateStronglyConnectedComponenetsGraph1()
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5,6,7,8
            };

            var graph = new AdjacencyListGraph<int>(true);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>
            {
                graph.CreateEdge(vertexs[0],vertexs[1]),
                graph.CreateEdge(vertexs[1],vertexs[2]),
                graph.CreateEdge(vertexs[2],vertexs[3]),
                graph.CreateEdge(vertexs[2],vertexs[0]),
                graph.CreateEdge(vertexs[3],vertexs[4]),
                graph.CreateEdge(vertexs[4],vertexs[0]),

                graph.CreateEdge(vertexs[2],vertexs[5]),

                graph.CreateEdge(vertexs[5],vertexs[6]),
                graph.CreateEdge(vertexs[6],vertexs[7]),
                graph.CreateEdge(vertexs[7],vertexs[5]),

            };

            graph.CreatGraph(vertexs, edges);

            return graph;

        }


        private AdjacencyListGraph<int> CreateStronglyConnectedGraph1()
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5
            };

            var graph = new AdjacencyListGraph<int>(true);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>
            {
                graph.CreateEdge(vertexs[0],vertexs[1]),
                graph.CreateEdge(vertexs[1],vertexs[2]),
                graph.CreateEdge(vertexs[2],vertexs[3]),
                graph.CreateEdge(vertexs[2],vertexs[0]),
                graph.CreateEdge(vertexs[3],vertexs[4]),
                graph.CreateEdge(vertexs[4],vertexs[0]),

            };

            graph.CreatGraph(vertexs, edges);

            return graph;

        }

        #endregion

        [TestMethod()]
        public void CreatGraphTest()
        {
        }


        [TestMethod()]
        public void BreadthFirstSearchTest()
        {
            var graph = CreateGraph1();
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
            var graph = CreateGraph1(true);

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
            var graph = CreateDirectionNoCircuitGraph1();

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
            var graph = CreateGraph1(true);
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
            graph.PrintPath(source, graph.GetVertexByKey(1));
        }

        [TestMethod()]
        public void GetVertexEdgeTest()
        {
            Console.WriteLine("GetVertexEdgeTest");
            var graph = CreateDirectionNoCircuitGraph1();
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
            var graph = CreateGraph1(true);
            var transposeGraph = graph.CreateTransposeGraph();

        }

        [TestMethod()]
        public void GetStronglyConnectedComponenetsTest()
        {
            var graph = CreateStronglyConnectedComponenetsGraph1();
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
            var graph = CreateStronglyConnectedGraph1();
            var stonaglyGraphs = graph.GetStronglyConnectedComponenets();
            Assert.AreEqual(1, stonaglyGraphs.Count);
            Assert.AreEqual(graph.GetVertexs().Count(), stonaglyGraphs[0].GetVertexs().Count());

        }
    }
}