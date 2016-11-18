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
    public class MaxFlowTests
    {
        //最后的一条路径有负流，这就是我最不懂的地方了。
        //回流式合法的啊
        private Tuple<
            AdjacencyListGraph<string>
            , AdjacencyVertex<string>
            , AdjacencyVertex<string>>
            CreateMaxFlowData()
        {
            var graph = new AdjacencyListGraph<string>();
            var vertexData = new List<string>()
            {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
            };

            var vertexs = new Dictionary<string, AdjacencyVertex<string>>();

            foreach (var data in vertexData)
            {
                vertexs.Add(data, graph.CreateVertex(data));
            }

            var edges = new List<FlowEdge<string>>();
            edges.Add(new FlowEdge<string>(vertexs["A"], vertexs["B"], 3));
            edges.Add(new FlowEdge<string>(vertexs["A"], vertexs["D"], 3));

            edges.Add(new FlowEdge<string>(vertexs["B"], vertexs["C"], 4));

            edges.Add(new FlowEdge<string>(vertexs["C"], vertexs["A"], 3));
            edges.Add(new FlowEdge<string>(vertexs["C"], vertexs["D"], 1));
            edges.Add(new FlowEdge<string>(vertexs["C"], vertexs["E"], 2));

            edges.Add(new FlowEdge<string>(vertexs["D"], vertexs["E"], 2));
            edges.Add(new FlowEdge<string>(vertexs["D"], vertexs["F"], 6));


            edges.Add(new FlowEdge<string>(vertexs["E"], vertexs["B"], 1));
            edges.Add(new FlowEdge<string>(vertexs["E"], vertexs["G"], 1));

            edges.Add(new FlowEdge<string>(vertexs["F"], vertexs["G"], 9));

            for (int i = edges.Count - 1; i >= 0; i--)
            {
                edges.Add(edges[i].GetRevolution());
            }

            graph.CreatGraph(vertexs.Values, edges);

            return new Tuple<AdjacencyListGraph<string>
               , AdjacencyVertex<string>
               , AdjacencyVertex<string>>(graph, vertexs["A"], vertexs["G"]);
        }





        [TestMethod()]
        public void EdmondsKarpTest()
        {
            var data = CreateMaxFlowData();
            var maxFlowTool = new MaxFlow<string>();
            var maxFlow = maxFlowTool.EdmondsKarp(data.Item1, data.Item2, data.Item3);
            Assert.AreEqual(5, maxFlow);
        }
    }
}