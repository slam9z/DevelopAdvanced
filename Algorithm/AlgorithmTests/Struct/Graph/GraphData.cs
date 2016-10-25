using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct.Tests
{
    public class GraphData
    {
        #region AdjacencyListData

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

        public static AdjacencyListGraph<int> CreateShortestPathGraph1()
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };

            //需要无向的图啊
            var graph = new AdjacencyListGraph<int>(false);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>
            {
                graph.CreateEdge(vertexs[0],vertexs[1],3),
                graph.CreateEdge(vertexs[1],vertexs[2],4),
                graph.CreateEdge(vertexs[2],vertexs[3],1),
                graph.CreateEdge(vertexs[2],vertexs[0],2),
                graph.CreateEdge(vertexs[3],vertexs[4],3),
                graph.CreateEdge(vertexs[4],vertexs[0],1),

                graph.CreateEdge(vertexs[2],vertexs[5],1),

                graph.CreateEdge(vertexs[5],vertexs[6],1),
                graph.CreateEdge(vertexs[6],vertexs[7],2),
                graph.CreateEdge(vertexs[7],vertexs[5],2),

                graph.CreateEdge(vertexs[8],vertexs[7],9),
            };

            graph.CreatGraph(vertexs, edges);

            return graph;

        }


        public static AdjacencyListGraph<int> CreateMininumSpanningTreeGraph1()
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };

            var graph = new AdjacencyListGraph<int>(true);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>();

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[0], vertexs[1], 3));
            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[1], vertexs[2], 4));
            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[2], vertexs[3], 1));


            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[3], vertexs[4], 3));

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[2], vertexs[5], 1));

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[5], vertexs[6], 1));
            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[6], vertexs[7], 2));

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[8], vertexs[7], 9));


            //这时的值就是一个生成树。值是24！

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[0], vertexs[4], 1));

            edges.AddRange(graph.CreateNonDirectionEdge(vertexs[6], vertexs[8], 8));

            //新的应该是

            graph.CreatGraph(vertexs, edges);

            return graph;

        }


        public static AdjacencyListGraph<int> CreateDAGShortestPathGraph1()
        {
            var vertexKeys = new List<int>
            {
                1,2,3,4,5,6,7,8,9
            };

            var graph = new AdjacencyListGraph<int>(true);

            var vertexs = graph.CreateVertexs(vertexKeys);

            var edges = new List<AdjacencyEdge<int>>();

            edges.Add(graph.CreateEdge(vertexs[0], vertexs[1], 3));
            edges.Add(graph.CreateEdge(vertexs[1], vertexs[2], 4));
            edges.Add(graph.CreateEdge(vertexs[2], vertexs[3], 1));


            edges.Add(graph.CreateEdge(vertexs[3], vertexs[4], 3));

            edges.Add(graph.CreateEdge(vertexs[2], vertexs[5], 1));

            edges.Add(graph.CreateEdge(vertexs[5], vertexs[6], 1));
            edges.Add(graph.CreateEdge(vertexs[6], vertexs[7], 2));

            edges.Add(graph.CreateEdge(vertexs[8], vertexs[7], 9));

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

        #region AdjacencyMartrixData


        public static AdjacencyMatrixGraph<int> CreateShortestPathMartrixGraph1()
        {
            var martrix = new AdjacencyMatrixNode<int>[,]
            {
                {
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(3),
                    new AdjacencyMatrixNode<int>(8),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(-4),
                },

                {
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(1),
                    new AdjacencyMatrixNode<int>(7)
                },

                {
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(4),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>()
                },

                {
                    new AdjacencyMatrixNode<int>(2),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(-5),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>()
                },
                
                {
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(),
                    new AdjacencyMatrixNode<int>(6),
                    new AdjacencyMatrixNode<int>()
                },
            };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (martrix[i, j].Weight == 0)
                    {
                        martrix[i, j].Weight = int.MaxValue;
                    }
                    if (i == j)
                    {
                        martrix[i, j].Weight = 0;
                    }

                    martrix[i, j].PathWeight = martrix[i, j].Weight;
                }
            }

            var graph = new AdjacencyMatrixGraph<int>(martrix);
            return graph;
        }

        #endregion
    }
}
