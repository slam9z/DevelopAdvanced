using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{

    public class AdjacencyListGraph<T> : GraphBase<T> where T : IComparable
    {
        private IDictionary<int, AdjacencyVertex<T>>
            _adjacencyList = new Dictionary<int, AdjacencyVertex<T>>();

        public readonly bool HasDirection;

        public AdjacencyListGraph()
        {

        }

        public AdjacencyListGraph(bool hasDirection)
        {
            HasDirection = hasDirection;
        }



        #region   //BFS

        public void BreadthFirstSearch(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> action)
        {
            var grayQueue = new Queue<AdjacencyVertex<T>>();

            foreach (var vertex in _adjacencyList.Values)
            {
                if (vertex == source)
                {
                    continue;
                }
                vertex.Distance = int.MaxValue;
                vertex.Parent = null;
                vertex.Color = Color.White;
            }

            source.Color = Color.Gray;
            source.Parent = null;
            source.Distance = 0;

            grayQueue.Enqueue(source);
            while (!grayQueue.IsEmpty)
            {
                var startVertex = grayQueue.Dequeue();
                foreach (var edge in GetVertexEdge(startVertex))
                {
                    var endeEnd = edge.End;
                    var endVertex = _adjacencyList[endeEnd];
                    if (endVertex.Color == Color.White)
                    {
                        endVertex.Color = Color.Gray;
                        endVertex.Distance = startVertex.Distance + 1;
                        endVertex.Parent = startVertex;
                        grayQueue.Enqueue(endVertex);
                    }
                }
                startVertex.Color = Color.Black;
                action(startVertex);
            }
        }

        #endregion


        #region  //DFS

        private int _time;
        public void DepthFirstSearch(Action<AdjacencyVertex<T>> action)
        {
            foreach (var item in _adjacencyList.Values)
            {
                item.Color = Color.White;
                item.Parent = null;
            }
            _time = 0;

            foreach (var item in _adjacencyList.Values)
            {
                if (item.Color == Color.White)
                {
                    DepthFirstSearchVisit(item, action);
                }
            }
        }

  

        private void DepthFirstSearchVisit(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> action)
        {
            source.Color = Color.Gray;
            _time = _time + 1;
            source.FisrtVisitTime = _time;

            var edges = GetVertexEdge(source);

            foreach (var item in edges)
            {
                var vertex = _adjacencyList[item.End];
                if (vertex.Color == Color.White)
                {
                    vertex.Parent = source;
                    DepthFirstSearchVisit(vertex, action);
                }
            }

            source.Color = Color.Black;

            _time = _time + 1;
            source.FinalVisitTime = _time;

            action(source);
        }

        #endregion

        public IEnumerable<AdjacencyVertex<T>> TopologicalSort()
        {
            var result = new List<AdjacencyVertex<T>>();
            DepthFirstSearch((vertex) =>
            {
                result.Insert(0, vertex);
            });

            return result;
        }

        public void PrintPath(AdjacencyVertex<T> source, AdjacencyVertex<T> vertex)
        {
            if (source == vertex)
            {
                Console.WriteLine(vertex);
                return;
            }
            if (vertex.Parent == null)
            {
                Console.WriteLine("no path form source:{0} vertex:{1}", source, vertex);
            }
            else
            {
                PrintPath(source, vertex.Parent);
                Console.WriteLine(vertex);
            }
        }

        public void StronglyConnectedComponenets()
        {
            DepthFirstSearch((vertex) => { });

            var transpose = CreateTransposeGraph();

            transpose.DepthFirstSearch((vertex) => { });


        }

        #region base

        private int _currentIdentifier = -1;

        private int GetCurrentIdentifier()
        {
            return _currentIdentifier = _currentIdentifier + 1;
        }


        #region graph

        public void CreatGraph(
            IEnumerable<AdjacencyVertex<T>> vertexs
            , IEnumerable<AdjacencyEdge> edges)
        {
            _adjacencyList.Clear();
            foreach (var item in vertexs)
            {
                AddVertex(item);
            }
            foreach (var item in edges)
            {
                AddEdge(item);
            }
        }

        public AdjacencyListGraph<T> CreateTransposeGraph()
        {
            if (!HasDirection)
            {
                throw new InvalidOperationException("must hasdirection graph");
            }
            var graph = new AdjacencyListGraph<T>(true);

            var vertexs = new List<AdjacencyVertex<T>>();
            var edges = new List<AdjacencyEdge>();

            foreach (var item in _adjacencyList.Values)
            {
                var newVertex = new AdjacencyVertex<T> { Key = item.Key, Identifier = item.Identifier };
                vertexs.Add(newVertex);

                edges.AddRange(GetVertexEdge(item).Select(o => new AdjacencyEdge(o.End, o.Start)));
            }
            graph.CreatGraph(vertexs, edges);
            return graph;
        }

        #endregion

        #region vertex

        public IList<AdjacencyVertex<T>> CreateVertexs(IEnumerable<T> keys)
        {
            var vertexs = new List<AdjacencyVertex<T>>();
            foreach (var key in keys)
            {
                vertexs.Add(CreateVertex(key));
            }
            return vertexs;
        }

        public AdjacencyVertex<T> CreateVertex(T key)
        {
            var vertex = new AdjacencyVertex<T>() { Key = key };
            vertex.Identifier = GetCurrentIdentifier();
            return vertex;
        }


        public IEnumerable<AdjacencyVertex<T>> GetVertexs()
        {
            return _adjacencyList.Values;
        }

        public AdjacencyVertex<T> GetVertex(int id)
        {
            return _adjacencyList[id];
        }

        public AdjacencyVertex<T> GetVertexByKey(int key)
        {
            return _adjacencyList.Values.Where(o => o.Key.CompareTo(key) == 0).FirstOrDefault();
        }

        public AdjacencyVertex<T> AddVertex(T key)
        {
            var vertex = CreateVertex(key);
            _adjacencyList[vertex.Identifier] = vertex;
            return vertex;
        }

        public void AddVertex(AdjacencyVertex<T> vertex)
        {
            _adjacencyList[vertex.Identifier] = vertex;
        } 
        #endregion

        #region edge

        public IEnumerable<AdjacencyEdge> GetVertexEdge(AdjacencyVertex<T> vertex)
        {
            var edges = new List<AdjacencyEdge>();
            var edge = vertex.FirstEdge;
            while (edge != null)
            {
                edges.Add(edge);
                edge = edge.Next;
            }
            return edges;
        }

        public AdjacencyEdge CreateEdge(AdjacencyVertex<T> start, AdjacencyVertex<T> end)
        {
            return new AdjacencyEdge(start.Identifier, end.Identifier);
        }

        public void AddEdge(AdjacencyEdge item)
        {
            AddEdgeCore(item.Start, item.End);
        }


        public void AddEdge(int first, int second)
        {
            AddEdgeCore(first, second);
            if (!HasDirection)
            {
                AddEdgeCore(second, first);
            }
        }

        public void AddEdgeCore(int start, int end)
        {
            if (!_adjacencyList.Keys.Contains(start) || !_adjacencyList.Keys.Contains(end))
            {
                throw new ArgumentOutOfRangeException("can't find vertex");
            }
            var startVertex = _adjacencyList[start];
            var edge = new AdjacencyEdge(start, end);

            if (startVertex.FirstEdge == null)
            {
                startVertex.FirstEdge = edge;
            }
            else
            {
                var pointerEdge = startVertex.FirstEdge;
                while (pointerEdge.Next != null)
                {
                    pointerEdge = pointerEdge.Next;
                }
                //竟然这里错了,写了这么多才发现，之前的测试不完备呢。 不是因为拓扑排序都不知道
                //pointerEdge=edge;
                pointerEdge.Next = edge;
            }
        }

        #endregion

        #endregion
    }
}