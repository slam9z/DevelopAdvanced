using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    public  class GraphData
    {
        #region data

        public static AdjacencyListGraph<int> CreateGraph1(bool hasDirection = false)
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

        public static AdjacencyListGraph<int> CreateDirectionNoCircuitGraph1(bool hasDirection = false)
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


        public static AdjacencyListGraph<int> CreateStronglyConnectedComponenetsGraph1()
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


        public static AdjacencyListGraph<int> CreateStronglyConnectedGraph1()
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

    }
}
