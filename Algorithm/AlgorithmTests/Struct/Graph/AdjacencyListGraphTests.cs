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
            var vertexs = new List<int>
            {
                1,2,3,4,5
            };

            var edges = new List<Tuple<int, int>>
            {
                new Tuple<int,int>(0,1),
                new Tuple<int,int>(0,4),

                new Tuple<int,int>(1,2),
                new Tuple<int,int>(1,3),
                new Tuple<int,int>(1,4),

                new Tuple<int,int>(2,3),
                new Tuple<int,int>(3,4),
            };

            var graph = new AdjacencyListGraph<int>(hasDirection);

            graph.CreatGraph(vertexs, edges);

            return graph;
        }

        private AdjacencyListGraph<int> CreateDirectionNoCircuitGraph1(bool hasDirection = false)
        {
            var vertexs = new List<int>
            {
                1,2,3,4,5
            };

            var edges = new List<Tuple<int, int>>
            {
                new Tuple<int,int>(0,1),
                new Tuple<int,int>(0,4),

                new Tuple<int,int>(1,2),
                new Tuple<int,int>(1,3),

            };

            var graph = new AdjacencyListGraph<int>(true);

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
            var source = graph.GetVertexByIndex(0);
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
            var source = graph.GetVertexByIndex(0);
            Console.WriteLine(source);
            graph.BreadthFirstSearch(source,
                (vertex) =>
                {

                }
            );

            Console.WriteLine();
            Console.WriteLine("PrintPath");
            Console.WriteLine();
            graph.PrintPath(source, graph.GetVertexByIndex(4));
        }

        [TestMethod()]
        public void GetVertexEdgeTest()
        {
            Console.WriteLine("GetVertexEdgeTest");
            var graph = CreateDirectionNoCircuitGraph1();
            var source = graph.GetVertexByIndex(0);
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
    }
}