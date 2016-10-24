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
    public class MininumSpanningTreeExtentionsTests
    {
        [TestMethod()]
        public void GetMininumSpanningTreeKruskalTest()
        {
            var graph = GraphData.CreateMininumSpanningTreeGraph1();
            var edges = graph.GetMininumSpanningTreeKruskal(GetEdgeWeight);
            var vertexs = graph.GetVertexs();

            //不好判断算法是否正确
            Assert.AreEqual(vertexs.Count() - 1, edges.Count());

            //强烈的需要一个求并集和差集的算法。

            var allEdgeVertexs = new List<AdjacencyVertex<int>>();
            foreach (var edge in edges)
            {
                if (!allEdgeVertexs.Contains(edge.Start))
                {
                    allEdgeVertexs.Add(edge.Start);
                }

                if (!allEdgeVertexs.Contains(edge.End))
                {
                    allEdgeVertexs.Add(edge.End);
                }
            }

            Assert.AreEqual(vertexs.Count(), allEdgeVertexs.Count);

            foreach (var item in allEdgeVertexs)
            {
                Assert.IsTrue(vertexs.Contains(item));
            }

            Console.WriteLine($"mininum tree { edges.Sum(o => o.Weight)}");

            //最后一个是权值最小的就不太好验证了。

        }



        private int GetEdgeWeight(AdjacencyEdge<int> edge)
        {
            return edge.Weight;
        }

        [TestMethod()]
        public void GetMininumSpanningTreePrimTest()
        {

            var graph = GraphData.CreateMininumSpanningTreeGraph1();
            var root = graph.GetVertexByKey(3);
            var edges = graph.GetMininumSpanningTreePrim(root, GetEdgeWeight);
            var vertexs = graph.GetVertexs();

            //不好判断算法是否正确
            Assert.AreEqual(vertexs.Count()-1, edges.Count());

            //强烈的需要一个求并集和差集的算法。

            Console.WriteLine($"mininum tree { edges.Sum(o => o.Weight)}");
        }
    }
}