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
    public class DisjointSetTests
    {

        private AdjacencyListGraph<int> CreateNotConnectedComponenetsGraph1()
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


                graph.CreateEdge(vertexs[5],vertexs[6]),
                graph.CreateEdge(vertexs[6],vertexs[7]),
                graph.CreateEdge(vertexs[7],vertexs[5]),

            };

            graph.CreatGraph(vertexs, edges);

            return graph;

        }



        [TestMethod()]
        public void DisjointSetTest()
        {
            var values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var sets = new List<DisjointSet<int>>();

            foreach (var item in values)
            {
                var set = new DisjointSet<int>(item);
                sets.Add(set);
            }

            var unionSet = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                unionSet = unionSet.Union(sets[i]);
            }

            var unionValues = unionSet.GetNodes();

            //如何自己实现区并集
            //如何证明两个集合值都是相等的呢？

            //这样感觉的实现是对的！
            Assert.AreEqual(values.Count, unionValues.Count());

            foreach (var item in unionValues)
            {
                Console.WriteLine(item);
                Assert.IsTrue(values.Contains(item.Value));
            }

        }


        [TestMethod()]
        public void ConnectedComponenetsTest()
        {
            var graph = CreateNotConnectedComponenetsGraph1();
            var result = graph.GetConnectedComponenets();
            Assert.AreEqual(2, result.Count());
        }



        private void PrintDisjointSet<T>(DisjointSet<T> set) where T : IEquatable<T>
        {
            var nodes = set.GetNodes();
            foreach (var item in nodes)
            {
                Console.WriteLine(item);
            }
        }
    }
}